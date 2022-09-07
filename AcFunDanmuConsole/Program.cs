using System;
using System.Collections;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Timers;
using AcFunDanmu;
using AcFunDanmu.Enums;
using AcFunDanmu.Im.Basic;
using AcFunDanmu.Im.Cloud.Config;
using AcFunDanmu.Im.Cloud.Message;
using AcFunDanmu.Im.Message;
using Google.Protobuf;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Extensions.Logging;
using static AcFunDanmu.ClientUtils;

namespace AcFunDanmuConsole;

internal class Program
{
    private static async Task Main(string[] args)
    {
        await Start(args);

        //DecodeHar(@".\34195163.har");
    }

    private static byte[] StreamConvert(in string stream)
    {
        Span<byte> data = stackalloc byte[stream.Length >> 1];
        for (var i = 0; i < stream.Length >> 1; i++)
        {
            data[i] = byte.Parse(stream.AsSpan(i << 1, 2), NumberStyles.HexNumber);
        }

        return data.ToArray();
    }

    private static async Task Start(string[] args)
    {
        var retry = 0;


        Log.Logger = new LoggerConfiguration()
#if DEBUG
            .MinimumLevel.Information()
#else
            .MinimumLevel.Information()
#endif
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .WriteTo.Debug()
            .CreateLogger();

        Client client = new(new LoggerFactory().AddSerilog().CreateLogger<Client>());

        switch (args.Length)
        {
            case 1:
                await client.Initialize(args[0]); // Visitor/Anonymous mode
                break;
            case 3:
                await client.InitializeWithLogin(args[0], args[1], args[2]); // User mode
                break;
            default:
                return;
        }

        client.Handler += HandleSignal; // Use your own signal handler

        var resetTimer = new Timer(30000);
        resetTimer.Elapsed += (_, _) => { retry = 0; };

        while (retry < 3 && !await client.Start())
        {
            if (retry > 0) resetTimer.Stop();

            Log.Information("Client closed, retrying");
            retry++;
            resetTimer.Start();
        }

        Log.Information("Client closed, maybe live is end");
    }

    private static void HandleSignal(Client sender, string messageType, ByteString payload)
    {
        switch (messageType)
        {
            // Includes comment, gift, enter room, like, follower
            case PushMessage.ACTION_SIGNAL:
                var actionSignal = ZtLiveScActionSignal.Parser.ParseFrom(payload);

                foreach (var item in actionSignal.Item)
                    switch (item.SignalType)
                    {
                        case PushMessage.ActionSignal.COMMENT:
                            foreach (var pl in item.Payload)
                            {
                                var comment = CommonActionSignalComment.Parser.ParseFrom(pl);
                                Log.Information("{0} - {1}({2}): {3}", comment.SendTimeMs,
                                    comment.UserInfo.Nickname, comment.UserInfo.UserId, comment.Content);
                            }

                            break;
                        case PushMessage.ActionSignal.LIKE:
                            foreach (var pl in item.Payload)
                            {
                                var like = CommonActionSignalLike.Parser.ParseFrom(pl);
                                Log.Information("{0} - {1}({2}) liked", like.SendTimeMs, like.UserInfo.Nickname,
                                    like.UserInfo.UserId);
                            }

                            break;
                        case PushMessage.ActionSignal.ENTER_ROOM:
                            foreach (var pl in item.Payload)
                            {
                                var enter = CommonActionSignalUserEnterRoom.Parser.ParseFrom(pl);
                                Log.Information("{0} - {1}({2}) entered", enter.SendTimeMs, enter.UserInfo.Nickname,
                                    enter.UserInfo.UserId);
                            }

                            break;
                        case PushMessage.ActionSignal.FOLLOW:
                            foreach (var pl in item.Payload)
                            {
                                var follower = CommonActionSignalUserFollowAuthor.Parser.ParseFrom(pl);
                                Log.Information("{0} - {1}({2}) followed", follower.SendTimeMs,
                                    follower.UserInfo.Nickname, follower.UserInfo.UserId);
                            }

                            break;
                        case PushMessage.NotifySignal.KICKED_OUT:
                        case PushMessage.NotifySignal.VIOLATION_ALERT:
                        case PushMessage.NotifySignal.LIVE_MANAGER_STATE:
                            break;
                        case PushMessage.ActionSignal.THROW_BANANA:
                            foreach (var pl in item.Payload)
                            {
                                var enter = AcfunActionSignalThrowBanana.Parser.ParseFrom(pl);
                                Log.Information("{0} - {1}({2}) throwed {3} banana(s)", enter.SendTimeMs,
                                    enter.Visitor.Name, enter.Visitor.UserId, enter.Count);
                            }

                            break;
                        case PushMessage.ActionSignal.GIFT:
                            foreach (var pl in item.Payload)
                            {
                                /*
                                 * Item Id
                                 * 1 - 香蕉
                                 * 2 - 吃瓜
                                 * 3 - 
                                 * 4 - 牛啤
                                 * 5 - 手柄
                                 * 6 - 魔法棒
                                 * 7 - 好人卡
                                 * 8 - 星蕉雨
                                 * 9 - 告白
                                 * 10 - 666
                                 * 11 - 菜鸡
                                 * 12 - 打Call
                                 * 13 - 立FLAG
                                 * 14 - 窜天猴
                                 * 15 - AC机娘
                                 * 16 - 猴岛
                                 * 17 - 快乐水
                                 * 18 - 
                                 * 19 - 
                                 * 20 - 
                                 * 21 - 生日快乐
                                 * 22 - 六一快乐
                                 * 23 - 
                                 * 24 - 
                                 * 25 - 
                                 * 26 - 
                                 * 27 - 
                                 * 28 - 
                                 * 29 - 大触
                                 * 30 - 鸽鸽
                                 * 31 - 金坷垃
                                 * 32 - 变身腰带
                                 * 33 - 情书
                                 * 34 - 狗粮
                                 * 35 - 氧气瓶
                                 */
                                var gift = CommonActionSignalGift.Parser.ParseFrom(pl);
                                if (Client.Gifts.ContainsKey(gift.GiftId))
                                {
                                    var giftInfo = Client.Gifts[gift.GiftId];
                                    Log.Information("{0} - {1}({2}) sent gift {3} × {4}, Combo: {5}, value: {6}",
                                        gift.SendTimeMs, gift.UserInfo.Nickname, gift.UserInfo.UserId,
                                        giftInfo.Name, gift.BatchSize, gift.ComboCount, gift.Rank);
                                }
#if DEBUG
                                else
                                {
                                    Log.Information("ItemId: {0}, Value: {1}", gift.GiftId, gift.Rank);
                                }
#endif
                            }

                            break;
                        default:
                            foreach (var p in item.Payload)
                            {
                                var pi = Parse(item.SignalType, p);
#if DEBUG
                                Log.Information("Unhandled action type: {0}, content: {1}", item.SignalType, pi);
#endif
                            }

                            break;
                    }

                break;
            // Includes current banana counts, watching count, like count and top 3 users sent gifts
            case PushMessage.STATE_SIGNAL:
                var stateSignal = ZtLiveScStateSignal.Parser.ParseFrom(payload);

                foreach (var item in stateSignal.Item)
                    switch (item.SignalType)
                    {
                        case PushMessage.StateSignal.ACFUN_DISPLAY_INFO:
                            var acInfo = AcfunStateSignalDisplayInfo.Parser.ParseFrom(item.Payload);
                            //Log.Information("Current banana count: {0}", acInfo.BananaCount);
                            break;
                        case PushMessage.StateSignal.DISPLAY_INFO:
                            var stateInfo = CommonStateSignalDisplayInfo.Parser.ParseFrom(item.Payload);
                            //Log.Information("{0} watching, {1} likes", stateInfo.WatchingCount, stateInfo.LikeCount);
                            break;
                        case PushMessage.StateSignal.TOP_USRES:
                            var users = CommonStateSignalTopUsers.Parser.ParseFrom(item.Payload);
                            //Log.Information("Top 3 users: {0}", string.Join(", ", users.User.Select(user => user.Detail.Name)));
                            break;
                        case PushMessage.StateSignal.RECENT_COMMENT:
                            var comments = CommonStateSignalRecentComment.Parser.ParseFrom(item.Payload);
                            foreach (var comment in comments.Comment)
                                Log.Information("{0} - {1}({2}): {3}", comment.SendTimeMs,
                                    comment.UserInfo.Nickname, comment.UserInfo.UserId, comment.Content);

                            break;
                        case PushMessage.StateSignal.CHAT_CALL:
                        case PushMessage.StateSignal.CHAT_ACCEPT:
                        case PushMessage.StateSignal.CHAT_READY:
                        case PushMessage.StateSignal.CHAT_END:
                        case PushMessage.StateSignal.CURRENT_RED_PACK_LIST:
                        case PushMessage.StateSignal.AR_LIVE_TREASURE_BOX_STATE:
                            break;
                        default:
                            var pi = Parse(item.SignalType, item.Payload);
#if DEBUG
                            Log.Information("Unhandled state type: {0}, content: {1}", item.SignalType, pi);
#endif
                            break;
                    }

                break;
            case PushMessage.NOTIFY_SIGNAL:
                var notifySignal = ZtLiveScNotifySignal.Parser.ParseFrom(payload);
                foreach (var item in notifySignal.Item)
                    switch (item.SignalType)
                    {
                        case PushMessage.NotifySignal.KICKED_OUT:
                        case PushMessage.NotifySignal.VIOLATION_ALERT:
                        case PushMessage.NotifySignal.LIVE_MANAGER_STATE:
                            break;
                        default:
                            var pi = Parse(item.SignalType, item.Payload);
#if DEBUG
                            Log.Information("Unhandled notify type: {0}, content: {1}", item.SignalType, pi);
#endif
                            break;
                    }

                break;
            default:
                var unknown = Parse(messageType, payload);
#if DEBUG
                Log.Information("Unhandled message type: {0}, content: {1}", messageType, unknown);
#endif
                break;
        }
    }

    #region Others

    private static void DumpCookie(CookieContainer cookiesContainer)
    {
        var hs = (Hashtable)cookiesContainer.GetType().InvokeMember("m_domainTable",
            BindingFlags.NonPublic | BindingFlags.GetField |
            BindingFlags.Instance, null, cookiesContainer, Array.Empty<object>());
        Debug.Assert(hs != null, nameof(hs) + " != null");
        foreach (string key in hs.Keys)
        {
            var uri = new Uri($"http://{key[1..]}/");
            var collection = cookiesContainer.GetCookies(uri);
            foreach (Cookie ck in collection) Log.Information($"{ck.Name} - {ck.Value}");
        }
    }

    private class Message
    {
        [JsonPropertyName("type")] public string Type { get; init; }
        [JsonPropertyName("data")] public string Data { get; init; }
    }

    private static void DecodeHar(string filePath)
    {
        var sessionKey = string.Empty;

        using var file = new StreamReader(filePath);

        using var json = JsonDocument.Parse(file.ReadToEnd());

        var loginResponse = json.RootElement.GetProperty("log").GetProperty("entries").EnumerateArray()
            .First(item =>
                item.GetProperty("request").GetProperty("url").ToString() ==
                "https://id.app.acfun.cn/rest/app/visitor/login");
        using var login = JsonDocument.Parse(loginResponse.GetProperty("response").GetProperty("content")
            .GetProperty("text").ToString());
        var securityKey = login.RootElement.GetProperty("acSecurity").ToString();

        var ws = json.RootElement.GetProperty("log").GetProperty("entries").EnumerateArray().First(item =>
            item.GetProperty("request").GetProperty("url").ToString() == "wss://klink-newproduct-ws3.kwaizt.com/");

        var messages = JsonSerializer.Deserialize<Message[]>(ws.GetProperty("_webSocketMessages").ToString());

        using var writer = new StreamWriter(@".\output.txt");

        Debug.Assert(messages != null, nameof(messages) + " != null");
        foreach (var message in messages)
            switch (message.Type)
            {
                case "send":
                {
#if DEBUG
                    var us = Decode<UpstreamPayload>(Convert.FromBase64String(message.Data), securityKey, sessionKey,
                        out var header);
                    writer.WriteLine("Up\t\tHeaderSeqId {0}, SeqId {1}, Command: {2}", header.SeqId, us.SeqId,
                        us.Command);
                    writer.WriteLine("Header: {0}", header);
                    writer.WriteLine("Payload Base64: {0}", us.ToByteString().ToBase64());
                    writer.WriteLine("Payload: {0}", us);
                    switch (us.Command)
                    {
                        case Command.REGISTER:
                            var register = RegisterRequest.Parser.ParseFrom(us.PayloadData);
                            writer.WriteLine(register);
                            break;
                        case Command.KEEP_ALIVE:
                            var keepalive = KeepAliveRequest.Parser.ParseFrom(us.PayloadData);
                            writer.WriteLine(keepalive);
                            break;
                        case Command.PING:
                            var ping = PingRequest.Parser.ParseFrom(us.PayloadData);
                            writer.WriteLine(ping);
                            break;
                        case Command.MESSAGE_SESSION:
                            var session = SessionListRequest.Parser.ParseFrom(us.PayloadData);
                            writer.WriteLine(session);
                            break;
                        case Command.MESSAGE_PULL_OLD:
                            var pullold = PullOldRequest.Parser.ParseFrom(us.PayloadData);
                            writer.WriteLine(pullold);
                            break;
                        case Command.CLIENT_CONFIG_GET:
                            var configget = ClientConfigGetRequest.Parser.ParseFrom(us.PayloadData);
                            writer.WriteLine(configget);
                            break;
                        case Command.GROUP_USER_GROUP_LIST:
                            var usergrouplist = UserGroupListRequest.Parser.ParseFrom(us.PayloadData);
                            writer.WriteLine(usergrouplist);
                            break;
                        case Command.GLOBAL_COMMAND:
                            var cmd = ZtLiveCsCmd.Parser.ParseFrom(us.PayloadData);
                            writer.WriteLine("\t{0}: {1}", Command.GLOBAL_COMMAND, cmd);
                            switch (cmd.CmdType)
                            {
                                case GlobalCommand.ENTER_ROOM:
                                    var enterRoom = ZtLiveCsEnterRoom.Parser.ParseFrom(cmd.Payload);
                                    writer.WriteLine("\t\t{0}", enterRoom);
                                    break;
                                case GlobalCommand.HEARTBEAT:
                                    var heartbeat = ZtLiveCsHeartbeat.Parser.ParseFrom(cmd.Payload);
                                    writer.WriteLine("\t\t{0}", heartbeat);
                                    break;
                                case GlobalCommand.USER_EXIT:
                                    var userExit = ZtLiveCsUserExit.Parser.ParseFrom(cmd.Payload);
                                    writer.WriteLine("\t\t{0}", userExit);
                                    break;
                                default:
                                    Log.Information("Unhandled Global.ZtLiveInteractive.CsCmd: {0}", cmd.CmdType);
                                    Log.Information(cmd.ToByteString().ToBase64());
                                    break;
                            }

                            break;
                        case Command.PUSH_MESSAGE:
                            writer.WriteLine("\tUpstream Push.Message: {0}", us.PayloadData.ToBase64());
                            break;
                        default:
                            writer.WriteLine("Unknown upstream: {0}", us);
                            break;
                    }

                    writer.WriteLine("--------------------------------");
#endif
                    break;
                }
                case "receive":
                {
                    var ds = Decode<DownstreamPayload>(Convert.FromBase64String(message.Data), securityKey, sessionKey,
                        out var header);
                    writer.WriteLine("Down\tHeaderSeqId {0}, SeqId {1}, Command: {2}", header.SeqId, ds.SeqId,
                        ds.Command);
                    writer.WriteLine("Header: {0}", header);
                    writer.WriteLine("Payload Base64: {0}", ds.ToByteString().ToBase64());
                    writer.WriteLine("Payload: {0}", ds);
                    switch (ds.Command)
                    {
                        case Command.REGISTER:
                            var register = RegisterResponse.Parser.ParseFrom(ds.PayloadData);
                            sessionKey = register.SessKey.ToBase64();
                            writer.WriteLine(register);
                            break;
                        case Command.KEEP_ALIVE:
                            var keepalive = KeepAliveResponse.Parser.ParseFrom(ds.PayloadData);
                            writer.WriteLine(keepalive);
                            break;
                        case Command.PING:
                            var ping = PingResponse.Parser.ParseFrom(ds.PayloadData);
                            writer.WriteLine(ping);
                            break;
                        case Command.MESSAGE_SESSION:
                            var session = SessionListResponse.Parser.ParseFrom(ds.PayloadData);
                            writer.WriteLine(session);
                            break;
                        case Command.MESSAGE_PULL_OLD:
                            var pullold = PullOldResponse.Parser.ParseFrom(ds.PayloadData);
                            writer.WriteLine(pullold);
                            break;
                        case Command.CLIENT_CONFIG_GET:
                            var configget = ClientConfigGetResponse.Parser.ParseFrom(ds.PayloadData);
                            writer.WriteLine(configget);
                            break;
                        case Command.GROUP_USER_GROUP_LIST:
                            var usergrouplist = UserGroupListResponse.Parser.ParseFrom(ds.PayloadData);
                            writer.WriteLine(usergrouplist);
                            break;
                        case Command.GLOBAL_COMMAND:
                            var cmd = ZtLiveCsCmdAck.Parser.ParseFrom(ds.PayloadData);
                            writer.WriteLine("\t{0}: {1}", Command.GLOBAL_COMMAND, cmd);
                            switch (cmd.CmdAckType)
                            {
                                case GlobalCommand.ENTER_ROOM_ACK:
                                    var enterRoom = ZtLiveCsEnterRoomAck.Parser.ParseFrom(cmd.Payload);
                                    writer.WriteLine("\t\t{0}", enterRoom);
                                    break;
                                case GlobalCommand.HEARTBEAT_ACK:
                                    var heartbeat = ZtLiveCsHeartbeatAck.Parser.ParseFrom(cmd.Payload);
                                    writer.WriteLine("\t\t{0}", heartbeat);
                                    break;
                                case GlobalCommand.USER_EXIT_ACK:
                                    var userexit = ZtLiveCsUserExitAck.Parser.ParseFrom(cmd.Payload);
                                    writer.WriteLine("\t\t{0}", userexit);
                                    break;
                                default:
                                    Log.Information("Unhandled Global.ZtLiveInteractive.CsCmd: {0}", cmd.CmdAckType);
                                    Log.Information(ds.PayloadData.ToBase64());
                                    break;
                            }

                            break;
                        case Command.PUSH_MESSAGE:
                            var scmessage = ZtLiveScMessage.Parser.ParseFrom(ds.PayloadData);
                            writer.WriteLine("\t{0}: {1}", Command.PUSH_MESSAGE, scmessage);
                            var payload = scmessage.CompressionType == ZtLiveScMessage.Types.CompressionType.Gzip
                                ? Decompress(scmessage.Payload)
                                : scmessage.Payload;

                            switch (scmessage.MessageType)
                            {
                                case PushMessage.ACTION_SIGNAL:
                                case PushMessage.STATE_SIGNAL:
                                case PushMessage.NOTIFY_SIGNAL:
                                    // Handled by user
                                    HandleSignal(null, scmessage.MessageType, payload);
                                    break;
                                case PushMessage.STATUS_CHANGED:
                                    var statusChanged = ZtLiveScStatusChanged.Parser.ParseFrom(payload);
                                    writer.WriteLine("\t\t{0}", statusChanged);
                                    break;
                                case PushMessage.TICKET_INVALID:
                                    var ticketInvalid = ZtLiveScTicketInvalid.Parser.ParseFrom(payload);
                                    writer.WriteLine("\t\t{0}", ticketInvalid);
                                    break;
                                default:
                                    Log.Information("Unhandled Push.ZtLiveInteractive.Message: {0}",
                                        scmessage.MessageType);
                                    break;
                            }

                            break;
                        default:
                            writer.WriteLine("Unknown downstream: {0}", ds);
                            break;
                    }

                    writer.WriteLine("--------------------------------");
                    break;
                }
            }
    }

    #endregion
}