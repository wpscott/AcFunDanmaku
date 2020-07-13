using AcFunDanmu;
using AcFunDanmu.Enums;
using System;
using System.Buffers;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AcFunDMJ.Vanilla
{
    class Program
    {
        private static readonly byte[] NewLine = new byte[] { 0x0D, 0x0A }; // \r\n
        private const string WSKey = "258EAFA5-E914-47DA-95CA-C5AB0DC85B11";    // RFC 6455
        private static readonly Encoding Encoding = Encoding.UTF8;
        private static readonly Regex GetRegex = new Regex(@"^GET /(.*?) HTTP", RegexOptions.IgnoreCase | RegexOptions.Compiled);
        private static readonly Regex SecKeyRegex = new Regex("Sec-WebSocket-Key: (.*)", RegexOptions.IgnoreCase | RegexOptions.Compiled);

        private static NetworkStream ws;
        private static Client danmaku;

        private static readonly Config config;

        private static readonly byte[] Index;
        private static readonly byte[] Css;

        static Program()
        {
            var index = new FileInfo(@".\index.html");
            if (index.Exists)
            {
                Index = new byte[index.Length];
                using var reader = index.OpenRead();
                reader.Read(Index, 0, Index.Length);
            }
            var css = new FileInfo(@".\chatbox.css");
            if (css.Exists)
            {
                Css = new byte[css.Length];
                using var reader = css.OpenRead();
                reader.Read(Css, 0, Css.Length);
            }
            config = Config.LoadConfig().Result;
        }

        static async Task Main(string[] args)
        {
            var server = new TcpListener(IPAddress.Loopback, config.Port);
            server.Start();
            Console.WriteLine("Started");
            using var owner = MemoryPool<byte>.Shared.Rent();
            while (true)
            {
                var client = await server.AcceptTcpClientAsync();
                var stream = client.GetStream();
                _ = Task.Run(() => Serve(stream, owner.Memory));
            }
        }

        private static async void Serve(NetworkStream stream, Memory<byte> buffer)
        {
            try
            {
                var count = await stream.ReadAsync(buffer);
                var data = Encoding.GetString(buffer.Span);
                var match = GetRegex.Match(data);
                if (match.Success)
                {
                    var path = match.Groups[1].Value.Trim();
                    if (long.TryParse(path, out var _))
                    {
                        var key = SecKeyRegex.Match(data);
                        if (key.Success)
                        {
                            using var sha1 = SHA1.Create();
                            var acceptKey = Convert.ToBase64String(sha1.ComputeHash(Encoding.GetBytes(key.Groups[1].Value.Trim() + WSKey)));
                            stream.Write(Encoding.GetBytes($"HTTP/1.1 101 Switching Protocols\r\nConnection: Upgrade\r\nUpgrade: websocket\r\nSec-WebSocket-Accept: {acceptKey}\r\n\r\n"));
                            ws = stream;
                            await Start(path);
                            stream.Close();
                            stream.Dispose();
                            return;
                        }
                        else
                        {
                            StaticFile(stream, $"Content-Type: text/html; charset=utf-8", Index);
                        }
                    }
                    else if (path == "chatbox.css")
                    {
                        StaticFile(stream, $"Content-Type: text/css", Css);
                        stream.Close();
                        stream.Dispose();
                        return;
                    }
                    else
                    {
                        NotFound(stream);
                        stream.Close();
                        stream.Dispose();
                        return;
                    }
                }
                else
                {
                    NotFound(stream);
                    stream.Close();
                    stream.Dispose();
                    return;
                }
                Serve(stream, buffer);
            }
            catch (IOException)
            {
                _ = danmaku?.Stop("Disconnect");
                stream.Close();
                stream.Dispose();
                return;
            }
            catch (ObjectDisposedException)
            {
                _ = danmaku?.Stop("Disconnect");
                return;
            }
        }

        private static void StaticFile(NetworkStream stream, string header, byte[] content)
        {
            stream.Write(Encoding.GetBytes($"HTTP/1.1 200 OK\r\n{header}\r\nContent-Length: {content.Length}"));
            stream.Write(NewLine);
            stream.Write(NewLine);
            stream.Write(content);
            stream.Write(NewLine);
        }

        private static void NotFound(NetworkStream stream)
        {
            stream.Write(Encoding.GetBytes("HTTP/1.1 404 Not Found"));
            stream.Write(NewLine);
            stream.Write(NewLine);
        }

        private static async Task Start(string uid)
        {
            if (danmaku != null)
            {
                _ = danmaku.Stop("Disconnect");
                danmaku = null;
            }
            danmaku = new Client();
            danmaku.Handler += HandleSignal;
            await danmaku.Initialize(uid);
            await danmaku.Start();
        }

        enum MessageType
        {
            Comment,
            Follow,
            Like,
            Enter,
            Gift,
            Banana
        }

        private static void SendMessage(MessageType type, object obj)
        {
            var text = JsonSerializer.Serialize(new { Type = type, Obj = obj });
            var data = Encoding.GetBytes(text);
            int offset = 0;
            Span<byte> msg = stackalloc byte[data.Length + (data.Length < 126 ? 2 : 4)];
            msg[offset++] = 0b10000001;

            if (data.Length < 126)
            {
                msg[offset++] = (byte)(0b00000000 | data.Length);
            }
            else
            {
                msg[offset++] = 0b01111110;

                var len = BitConverter.GetBytes(data.Length);
                if (BitConverter.IsLittleEndian)
                {
                    msg[offset++] = len[1];
                    msg[offset++] = len[0];
                }
                else
                {
                    msg[offset++] = len[3];
                    msg[offset++] = len[4];
                }
            }
            for (var i = 0; i < data.Length; i++)
            {
                msg[offset + i] = data[i];
            }
            try
            {
                ws.Write(msg);
            }
            catch (IOException)
            {
                ws.Dispose();
            }
            catch (ObjectDisposedException)
            {
                _ = danmaku.Stop("Disconnected");
            }
        }

        private static void HandleSignal(string messagetType, byte[] payload)
        {
            switch (messagetType)
            {
                // Includes comment, gift, enter room, like, follower
                case PushMessage.ACTION_SIGNAL:
                    var actionSignal = ZtLiveScActionSignal.Parser.ParseFrom(payload);

                    foreach (var item in actionSignal.Item)
                    {
                        switch (item.SingalType)
                        {
                            case PushMessage.ActionSignal.COMMENT:
                                foreach (var pl in item.Payload)
                                {
                                    var comment = CommonActionSignalComment.Parser.ParseFrom(pl);
                                    SendMessage(MessageType.Comment, new Comment { Name = comment.UserInfo.Nickname, Content = comment.Content });
                                }
                                break;
                            case PushMessage.ActionSignal.LIKE:
                                if (config.ShowLike)
                                {
                                    foreach (var pl in item.Payload)
                                    {
                                        var like = CommonActionSignalLike.Parser.ParseFrom(pl);
                                        SendMessage(MessageType.Like, new Like { Name = like.UserInfo.Nickname });
                                    }
                                }
                                break;
                            case PushMessage.ActionSignal.ENTER_ROOM:
                                if (config.ShowEnter)
                                {
                                    foreach (var pl in item.Payload)
                                    {
                                        var enter = CommonActionSignalUserEnterRoom.Parser.ParseFrom(pl);
                                        SendMessage(MessageType.Enter, new Enter { Name = enter.UserInfo.Nickname });
                                    }
                                }
                                break;
                            case PushMessage.ActionSignal.FOLLOW:
                                if (config.ShowFollow)
                                {
                                    foreach (var pl in item.Payload)
                                    {
                                        var follower = CommonActionSignalUserFollowAuthor.Parser.ParseFrom(pl);
                                        SendMessage(MessageType.Follow, new Follow { Name = follower.UserInfo.Nickname });
                                    }
                                }
                                break;
                            case PushMessage.ActionSignal.KICKED_OUT:
                            case PushMessage.ActionSignal.VIOLATION_ALERT:
                                break;
                            case PushMessage.ActionSignal.THROW_BANANA:
                                //foreach (var pl in item.Payload)
                                //{
                                //    var banana = AcfunActionSignalThrowBanana.Parser.ParseFrom(pl);
                                //    UpdateDanmaku(new Banana { Name = banana.Visitor.Name, Count = banana.Count });
                                //}
                                break;
                            case PushMessage.ActionSignal.GIFT:
                                foreach (var pl in item.Payload)
                                {
                                    var gift = CommonActionSignalGift.Parser.ParseFrom(pl);
                                    if (!config.GiftList.Contains(gift.GiftId))
                                    {
                                        var info = Client.Gifts[gift.GiftId];
                                        SendMessage(MessageType.Gift, new Gift { Name = gift.User.Nickname, ComboId = gift.ComboId, Count = gift.Combo, Detail = new Gift.GiftInfo { Name = info.Name, Pic = info.Pic } });
                                    }
                                }
                                break;
                            default:
                                break;
                        }
                    }
                    break;
                //Includes current banana counts, watching count, like count and top 3 users sent gifts
                case PushMessage.STATE_SIGNAL:
                    ZtLiveScStateSignal signal = ZtLiveScStateSignal.Parser.ParseFrom(payload);

                    foreach (var item in signal.Item)
                    {
                        switch (item.SingalType)
                        {
                            case PushMessage.StateSignal.ACFUN_DISPLAY_INFO:
                                //var acInfo = AcfunStateSignalDisplayInfo.Parser.ParseFrom(item.Payload);
                                //Console.WriteLine("Current banada count: {0}", acInfo.BananaCount);
                                break;
                            case PushMessage.StateSignal.DISPLAY_INFO:
                                //var stateInfo = CommonStateSignalDisplayInfo.Parser.ParseFrom(item.Payload);
                                //Console.WriteLine("{0} watching, {1} likes", stateInfo.WatchingCount, stateInfo.LikeCount);
                                break;
                            case PushMessage.StateSignal.TOP_USRES:
                                //var users = CommonStateSignalTopUsers.Parser.ParseFrom(item.Payload);
                                //Console.WriteLine("Top 3 users: {0}", string.Join(", ", users.User.Select(user => user.Detail.Name)));
                                break;
                            case PushMessage.StateSignal.RECENT_COMMENT:
                                var comments = CommonStateSignalRecentComment.Parser.ParseFrom(item.Payload);
                                foreach (var comment in comments.Comment)
                                {
                                    SendMessage(MessageType.Comment, new Comment { Name = comment.UserInfo.Nickname, Content = comment.Content });
                                }
                                break;
                            default:
                                //                            var pi = Parse(item.SingalType, item.Payload);
                                //#if DEBUG
                                //                            Console.WriteLine("Unhandled state type: {0}, content: {1}", item.SingalType, pi);
                                //#endif
                                break;
                        }
                    }
                    break;
            }
        }

        struct Comment
        {
            public string Name { get; set; }
            public string Content { get; set; }
        }
        struct Like { public string Name { get; set; } }
        struct Enter { public string Name { get; set; } }
        struct Follow { public string Name { get; set; } }
        struct Banana
        {
            public string Name { get; set; }
            public int Count { get; set; }
        }
        struct Gift
        {
            public string Name { get; set; }
            public string ComboId { get; set; }
            public int Count { get; set; }
            public GiftInfo Detail { get; set; }

            public struct GiftInfo
            {
                public string Name { get; set; }
                public Uri Pic { get; set; }
            }
        }
    }
}
