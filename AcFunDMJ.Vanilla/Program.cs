using AcFunDanmu;
using AcFunDanmu.Enums;
using Google.Protobuf;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AcFunDMJ.Vanilla
{
    class Program
    {
        private static readonly Encoding Encoding = Encoding.UTF8;
        private static readonly JsonSerializerOptions Options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

        private static WebSocket ws;
        private static Client danmaku;

        private static Config config;

        static async Task Main(string[] args)
        {
            config = await Config.LoadConfig();
            var address = $"http://localhost:{config.Port}/";
            using var server = new HttpListener();
            server.Prefixes.Add(address);
            server.Start();
            Console.WriteLine($"弹幕姬已启动，地址为{address}");
            while (true)
            {
                var ctx = await server.GetContextAsync();
                var path = ctx.Request.Url.LocalPath.Substring(1);

                switch (path)
                {
                    case string uid when long.TryParse(path, out _):
                        if (ctx.Request.IsWebSocketRequest)
                        {
                            var wsCtx = await ctx.AcceptWebSocketAsync(null);
                            Start(wsCtx.WebSocket, uid);
                        }
                        else
                        {
                            StaticFile(ctx.Response, "index.html", "text/html; charset=utf-8");
                        }
                        break;
                    case string css when path.EndsWith(".css"):
                        StaticFile(ctx.Response, css, "text/css");
                        break;
                    case string js when path.EndsWith(".js"):
                        StaticFile(ctx.Response, js, "application/javascript");
                        break;
                    case string json when path.EndsWith(".json"):
                        StaticFile(ctx.Response, json, "application/json; charset=utf-8");
                        break;
                    default:
                        ctx.Response.StatusCode = 404;
                        ctx.Response.Close();
                        break;
                }
            }
        }

        private static async void StaticFile(HttpListenerResponse response, string file, string contentType)
        {
            if (File.Exists($@".\{file}"))
            {
                using var stream = File.Open($@".\{file}", FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

                response.StatusCode = 200;
                response.ContentType = contentType;
                response.ContentEncoding = Encoding;
                await stream.CopyToAsync(response.OutputStream);

            }
            else
            {
                response.StatusCode = 404;
            }
            response.Close();
        }

        private static async void Start(WebSocket websocket, string uid)
        {
            if (danmaku != null)
            {
                await danmaku.Stop("Disconnect");
                danmaku = null;
                await ws.CloseAsync(WebSocketCloseStatus.Empty, string.Empty, default);
                ws.Dispose();
                ws = null;
            }
            ws = websocket;
            danmaku = new Client();
            SendMessage(MessageType.Text, $"正在连接到直播间：{uid}");
            danmaku.Handler += HandleSignal;
            SendMessage(MessageType.Text, $"正在初始化弹幕姬");
            await danmaku.Initialize(uid);
            SendMessage(MessageType.Text, $"正在启动弹幕姬");
            await danmaku.Start();
            SendMessage(MessageType.Text, $"直播已结束");
        }

        enum MessageType
        {
            Comment,
            Follow,
            Like,
            Enter,
            Gift,
            Banana,
            Text = 99
        }

        private static async void SendMessage(MessageType type, object obj)
        {
            if (ws.State == WebSocketState.Open)
            {
                var text = JsonSerializer.Serialize(new { Type = type, Obj = obj }, Options);
                await ws.SendAsync(Encoding.GetBytes(text), WebSocketMessageType.Text, true, default);
            }
            else
            {
                await danmaku.Stop("Disconnect");
            }
        }

        private static void HandleSignal(string messagetType, ByteString payload)
        {
            switch (messagetType)
            {
                // Includes comment, gift, enter room, like, follower
                case PushMessage.ACTION_SIGNAL:
                    var actionSignal = ZtLiveScActionSignal.Parser.ParseFrom(payload);
                    foreach (var item in actionSignal.Item)
                    {
                        switch (item.SignalType)
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
                                        var follow = CommonActionSignalUserFollowAuthor.Parser.ParseFrom(pl);
                                        SendMessage(MessageType.Follow, new Follow { Name = follow.UserInfo.Nickname });
                                    }
                                }
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
                                        SendMessage(MessageType.Gift, new Gift { Name = gift.User.Nickname, ComboId = gift.ComboId, Count = gift.Count, Value = gift.Value, Combo = gift.Combo, Detail = info });
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
                        switch (item.SignalType)
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
                                //                            var pi = Parse(item.SignalType, item.Payload);
                                //#if DEBUG
                                //                            Console.WriteLine("Unhandled state type: {0}, content: {1}", item.SignalType, pi);
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
            public int Combo { get; set; }
            public long Value { get; set; }
            public AcFunDanmu.Models.Client.GiftInfo Detail { get; set; }
        }
    }
}
