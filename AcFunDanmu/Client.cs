using AcFunDanmu.Enums;
using AcFunDanmu.Models.Client;
using Google.Protobuf;
using Serilog;
using System;
using System.Buffers;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.WebSockets;
using System.Text.Json;
using System.Threading.Tasks;
using static AcFunDanmu.ClientUtils;

namespace AcFunDanmu
{
    public delegate void SignalHandler(string messageType, ByteString payload);
    public delegate void DedicatedSignalHandler(string userId, string messageType, ByteString payload);
    public class Client
    {
        #region Constants
        private const string ACCEPTED_ENCODING = "gzip, deflate, br";
        private const string VISITOR_ST = "acfun.api.visitor_st";
        private const string MIDGROUND_ST = "acfun.midground.api_st";
        private const string _ACFUN_HOST = "https://live.acfun.cn";
        private static readonly Uri ACFUN_HOST = new(_ACFUN_HOST);
        private const string ACFUN_LOGIN_URL = "https://www.acfun.cn/login";
        private static readonly Uri ACFUN_LOGIN_URI = new(ACFUN_LOGIN_URL);
        private const string ACFUN_SIGN_IN_URL = "https://id.app.acfun.cn/rest/web/login/signin";
        private static readonly Uri ACFUN_SIGN_IN_URI = new(ACFUN_SIGN_IN_URL);
        private const string ACFUN_SAFETY_ID_URL = "https://sec-cdn.gifshow.com/safetyid";
        private static readonly Uri ACFUN_SAFETY_ID_URI = new(ACFUN_SAFETY_ID_URL);
        private const string LIVE_URL = "https://live.acfun.cn/live";
        private const string LOGIN_URL = "https://id.app.acfun.cn/rest/app/visitor/login";
        private static readonly Uri LOGIN_URI = new(LOGIN_URL);
        private const string GET_TOKEN_URL = "https://id.app.acfun.cn/rest/web/token/get";
        private static readonly Uri GET_TOKEN_URI = new(GET_TOKEN_URL);
        private const string PLAY_URL = "https://api.kuaishouzt.com/rest/zt/live/web/startPlay?subBiz=mainApp&kpn=ACFUN_APP&kpf=PC_WEB&userId={0}&did={1}&{2}={3}";
        private const string GIFT_URL = "https://api.kuaishouzt.com/rest/zt/live/web/gift/list?subBiz=mainApp&kpn=ACFUN_APP&kpf=PC_WEB&userId={0}&did={1}&{2}={3}";
        private const string WATCHING_URL = "https://api.kuaishouzt.com/rest/zt/live/web/watchingList?subBiz=mainApp&kpn=ACFUN_APP&kpf=PC_WEB&userId={0}&did={1}&{2}={3}";

        private const string UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.77 Safari/537.36";

        private const string SAFETY_ID_CONTENT = "{{\"platform\":5,\"app_version\":\"2.0.32\",\"device_id\":\"null\",\"user_id\":\"{0}\"}}";
        private static readonly Dictionary<string, string> LOGIN_FORM = new() { { "sid", "acfun.api.visitor" } };
        private static readonly Dictionary<string, string> GET_TOKEN_FORM = new() { { "sid", "acfun.midground.api" } };

        private const string _Host = "wss://klink-newproduct-ws3.kwaizt.com/";
        private static readonly Uri Host = new(_Host);
        #endregion

        public SignalHandler Handler { get; set; }
        public DedicatedSignalHandler DedicatedHandler { get; set; }

        #region Properties and Fields
        public static readonly ConcurrentDictionary<long, GiftInfo> Gifts = new();
        private static DateTimeOffset LastGiftUpdate = DateTimeOffset.MinValue;

        private static readonly CookieContainer CookieContainer = new();
        private static string DeviceId;
        private static bool IsSignIn = false;
        private static bool IsPrepared = false;

        private long UserId = -1;
        private string AVUPId;
        private string ServiceToken;
        private string SecurityKey;
        private string LiveId;
        private string EnterRoomAttach;
        private string[] Tickets;

        private ClientWebSocket _client;
        private ClientRequests _requests;
        #endregion

        #region Constructor
        public Client()
        {
            Log.Logger = new LoggerConfiguration().WriteTo.Console().CreateLogger();
        }

        public Client(long userId, string serviceToken, string securityKey, string[] tickets, string enterRoomAttach, string liveId) : this()
        {
            UserId = userId;
            ServiceToken = serviceToken;
            SecurityKey = securityKey;
            Tickets = tickets;
            EnterRoomAttach = enterRoomAttach;
            LiveId = liveId;
        }
        #endregion

        public async Task<bool> Login(string username, string password)
        {
            if (!IsSignIn)
            {
                Log.Information("Client signing in");
                try
                {
                    using var client = CreateHttpClient(ACFUN_LOGIN_URI);

                    using var login = await client.GetAsync(ACFUN_LOGIN_URI);
                    if (!login.IsSuccessStatusCode)
                    {
                        Log.Error("Get login error: {Content}", await login.Content.ReadAsStringAsync());
                        return false;
                    }

                    using var signinContent = new FormUrlEncodedContent(new Dictionary<string, string>
                                                                        {
                                                                            {"username", username },
                                                                            {"password", password },
                                                                            {"key", null },
                                                                            {"captcha", null}
                                                                        });
                    using var signin = await client.PostAsync(ACFUN_SIGN_IN_URI, signinContent);
                    if (!signin.IsSuccessStatusCode)
                    {
                        Log.Error("Post sign in error: {Content}", await signin.Content.ReadAsStringAsync());
                        return false;
                    }
                    var user = await JsonSerializer.DeserializeAsync<SignIn>(await signin.Content.ReadAsStreamAsync());
                    if (user == null)
                    {
                        Log.Error("Unable to deserialize SignIn");
                        return false;
                    }

                    using var sidContent = new StringContent(string.Format(SAFETY_ID_CONTENT, user.userId));
                    using var sid = await client.PostAsync(ACFUN_SAFETY_ID_URI, sidContent);
                    if (!sid.IsSuccessStatusCode)
                    {
                        Log.Error("Post safety id error: {Content}", await sid.Content.ReadAsStringAsync());
                        return false;
                    }
                    var safetyid = await JsonSerializer.DeserializeAsync<SafetyId>(await sid.Content.ReadAsStreamAsync());
                    if (safetyid == null)
                    {
                        Log.Error("Unable to deserialize SignIn");
                        return false;
                    }
                    CookieContainer.Add(new Cookie
                    {
                        Domain = ".acfun.cn",
                        Name = "safety_id",
                        Value = safetyid.safety_id
                    });

                    IsSignIn = true;
                }
                catch (HttpRequestException ex)
                {
                    Log.Error(ex, "Login Exception");
                    return await Login(username, password);
                }
                catch (TaskCanceledException ex)
                {
                    Log.Error(ex, "Login Exception");
                    return await Login(username, password);
                }
            }
            return IsSignIn;
        }

        public async Task<Play.PlayData> InitializeWithLogin(string username, string password, string uid, bool refreshGiftList = false)
        {
            await Login(username, password);
            return await Initialize(uid, refreshGiftList);
        }

        public static async Task<bool> Prepare()
        {
            try
            {
                if (!IsPrepared)
                {
                    using var client = CreateHttpClient($"{LIVE_URL}");

                    using var index = await client.GetAsync($"{LIVE_URL}");
                    if (!index.IsSuccessStatusCode)
                    {
                        Log.Error("Get live info error: {Content}", await index.Content.ReadAsStringAsync());
                    }
                    if (string.IsNullOrEmpty(DeviceId))
                    {
                        DeviceId = CookieContainer.GetCookies(ACFUN_HOST).First(cookie => cookie.Name == "_did").Value;
                    }
                    IsPrepared = true;
                }
                return IsPrepared;
            }
            catch (HttpRequestException) { return await Prepare(); }
            catch (TaskCanceledException) { return await Prepare(); }
        }

        public async Task<Play.PlayData> Initialize(string uid, bool refreshGiftList = false)
        {
            if (long.TryParse(uid, out _))
            {
                AVUPId = uid;
                if (!IsPrepared) { Log.Error("Client not prepared, please call Client.Prepare() first"); return null; }
                Log.Information("Client initializing");
                try
                {
                    using var client = CreateHttpClient($"{LIVE_URL}/{uid}");

                    if (IsSignIn)
                    {
                        using var getcontent = new FormUrlEncodedContent(GET_TOKEN_FORM);
                        using var get = await client.PostAsync(GET_TOKEN_URI, getcontent);
                        if (!get.IsSuccessStatusCode)
                        {
                            Log.Error("Get token error: {Content}", await get.Content.ReadAsStringAsync());
                            return null;
                        }
                        var token = await JsonSerializer.DeserializeAsync<MidgroundToken>(await get.Content.ReadAsStreamAsync());
                        if (token == null)
                        {

                            Log.Error("Unable to deserialize MidgroundToken");
                            return null;
                        }
                        UserId = token.userId;
                        ServiceToken = token.service_token;
                        SecurityKey = token.ssecurity;
                    }
                    else
                    {
                        using var loginContent = new FormUrlEncodedContent(LOGIN_FORM);
                        using var login = await client.PostAsync(LOGIN_URI, loginContent);
                        if (!login.IsSuccessStatusCode)
                        {
                            Log.Error("Get token error: {Content}", await login.Content.ReadAsStringAsync());
                            return null;
                        }
                        var token = await JsonSerializer.DeserializeAsync<VisitorToken>(await login.Content.ReadAsStreamAsync());
                        if (token == null)
                        {

                            Log.Error("Unable to deserialize VisitorToken");
                            return null;
                        }
                        UserId = token.userId;
                        ServiceToken = token.service_token;
                        SecurityKey = token.acSecurity;
                    }

                    using var form = new FormUrlEncodedContent(new Dictionary<string, string> { { "authorId", uid } });
                    using var play = await client.PostAsync(string.Format(PLAY_URL, UserId, DeviceId, IsSignIn ? MIDGROUND_ST : VISITOR_ST, ServiceToken), form);

                    if (!play.IsSuccessStatusCode)
                    {
                        Log.Error("Get play info error: {Content}", await play.Content.ReadAsStringAsync());
                        return null;
                    }

                    var playData = await JsonSerializer.DeserializeAsync<Play>(await play.Content.ReadAsStreamAsync());
                    if (playData == null)
                    {
                        Log.Error("Unable to deserialize Play");
                        return null;
                    }
                    if (playData.result != 1)
                    {
                        Log.Error(playData.error_msg);
                        return null;
                    }
                    Tickets = playData.data?.availableTickets ?? Array.Empty<string>();
                    EnterRoomAttach = playData.data?.enterRoomAttach;
                    LiveId = playData.data?.liveId;

                    UpdateGiftList(refreshGiftList);

                    Log.Information("Client initialized");

                    return playData.data;
                }
                catch (HttpRequestException ex)
                {
                    Log.Error(ex, "Initialize exception");
                    return await Initialize(uid);
                }
                catch (TaskCanceledException ex)
                {
                    Log.Error(ex, "Initialize exception");
                    return await Initialize(uid);
                }
            }
            else
            {
                Log.Error($"Invliad user id: {uid}");
                return null;
            }
        }

        private async void UpdateGiftList(bool refresh)
        {
            var now = DateTimeOffset.Now;
            if (refresh || (now - LastGiftUpdate).TotalHours > 1)
            {
                if (!refresh) { LastGiftUpdate = now; }
                try
                {
                    using var client = CreateHttpClient(LIVE_URL);

                    using var giftContent = new FormUrlEncodedContent(new Dictionary<string, string>
                    {
                        {"visitorId", $"{UserId}" },
                        {"liveId", LiveId }
                    });
                    using var gift = await client.PostAsync(string.Format(GIFT_URL, UserId, DeviceId, IsSignIn ? MIDGROUND_ST : VISITOR_ST, ServiceToken), giftContent);
                    if (gift.IsSuccessStatusCode)
                    {
                        var giftList = await JsonSerializer.DeserializeAsync<GiftList>(await gift.Content.ReadAsStreamAsync());
                        foreach (var item in giftList?.data?.giftList ?? Array.Empty<GiftList.GiftData.Gift>())
                        {
                            var giftInfo = new GiftInfo
                            {
                                Name = item.giftName,
                                Value = item.giftPrice,
                                Pic = new Uri(item.webpPicList[0].url)
                            };
                            Gifts[item.giftId] = giftInfo;
                        }

                        if (!refresh) { Log.Information("Gift list updated"); }
                    }
                }
                catch (HttpRequestException ex)
                {
                    Log.Error(ex, "Update gift list exception");
                    UpdateGiftList(refresh);
                }
                catch (TaskCanceledException ex)
                {
                    Log.Error(ex, "Update gift list exception");
                    UpdateGiftList(refresh);
                }
            }
        }

        public async Task<WatchingList.WatchingData.User[]> WatchingList()
        {
            if (UserId == -1 || string.IsNullOrEmpty(ServiceToken) || string.IsNullOrEmpty(SecurityKey) || string.IsNullOrEmpty(LiveId) || string.IsNullOrEmpty(EnterRoomAttach) || Tickets == null)
            {
                return Array.Empty<WatchingList.WatchingData.User>();
            }
            try
            {
                using var client = CreateHttpClient(LIVE_URL);

                using var watchingContent = new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    {"visitorId", $"{UserId}" },
                    {"liveId", LiveId }
                });
                using var watching = await client.PostAsync(string.Format(WATCHING_URL, UserId, DeviceId, IsSignIn ? MIDGROUND_ST : VISITOR_ST, ServiceToken), watchingContent);
                if (!watching.IsSuccessStatusCode)
                {
                    return Array.Empty<WatchingList.WatchingData.User>();
                }
                var watchingList = await JsonSerializer.DeserializeAsync<WatchingList>(await watching.Content.ReadAsStreamAsync());

                return watchingList?.data?.list ?? Array.Empty<WatchingList.WatchingData.User>();
            }
            catch (HttpRequestException ex)
            {
                Log.Error(ex, "Watching list exception");
                return await WatchingList();
            }
            catch (TaskCanceledException ex)
            {
                Log.Error(ex, "Watching list exception");
                return await WatchingList();
            }
        }

        public async Task<bool> Start()
        {

            if (UserId == -1 || string.IsNullOrEmpty(ServiceToken) || string.IsNullOrEmpty(SecurityKey) || string.IsNullOrEmpty(LiveId) || string.IsNullOrEmpty(EnterRoomAttach) || Tickets == null || Tickets.Length == 0)
            {
                Log.Information("Not initialized or live is ended");
                return false;
            }

            using var owner = MemoryPool<byte>.Shared.Rent();
            _requests = new ClientRequests(UserId, DeviceId, ServiceToken, SecurityKey, LiveId, EnterRoomAttach, Tickets);
            using var ws = new ClientWebSocket();
            _client = ws;
            try
            {
                #region Timers
                using var heartbeatTimer = new System.Timers.Timer();
                heartbeatTimer.Elapsed += async (s, e) =>
                {
                    if (ws.State == WebSocketState.Open)
                    {
                        Log.Debug("HEARTBEAT");
                        try
                        {
                            await ws.SendAsync(
                                _requests.HeartbeatReqeust(),
                                WebSocketMessageType.Binary,
                                true,
                                default
                            );

                            if (_requests.HeartbeatSeqId % 5 == 4)
                            {
                                await ws.SendAsync(_requests.KeepAliveRequest(), WebSocketMessageType.Binary, true, default);
                            }
                        }
                        catch (WebSocketException ex)
                        {
                            Log.Debug(ex, "Heartbeat");
                            heartbeatTimer.Stop();
                        }
                        catch (OperationCanceledException ex)
                        {
                            Log.Debug(ex, "Heartbeat");
                            heartbeatTimer.Stop();
                        }
                        catch (IOException ex)
                        {
                            Log.Debug(ex, "Heartbeat");
                            heartbeatTimer.Stop();
                        }
                    }
                    else
                    {
                        heartbeatTimer.Stop();
                    }
                };
                heartbeatTimer.AutoReset = true;
                #endregion

                await ws.ConnectAsync(Host, default);

                #region Register
                await ws.SendAsync(_requests.RegisterRequest(), WebSocketMessageType.Binary, true, default);
                #endregion

                #region Main loop
                while (ws.State == WebSocketState.Open)
                {
                    var buffer = owner.Memory;
                    try
                    {
                        await ws.ReceiveAsync(buffer, default);

                        var stream = Decode<DownstreamPayload>(buffer.Span, SecurityKey, _requests.SessionKey, out var header);
                        if (stream == null) { Log.Error("Downstream is null: {Content}", Convert.ToBase64String(buffer.Span)); continue; }
                        HandleCommand(header, stream, heartbeatTimer);
                    }
                    catch (WebSocketException ex)
                    {
                        Log.Debug(ex, "Main");
                        heartbeatTimer.Stop();
                        break;
                    }
                    catch (OperationCanceledException ex)
                    {
                        Log.Debug(ex, "Main");
                        heartbeatTimer.Stop();
                        break;
                    }
                    catch (IOException ex)
                    {
                        Log.Debug(ex, "Main");
                        heartbeatTimer.Stop();
                        break;
                    }
                }
                heartbeatTimer.Stop();
                #endregion
            }
            catch (HttpRequestException ex)
            {
                Log.Debug(ex, "Start");
            }
            catch (WebSocketException ex)
            {
                Log.Debug(ex, "Start");
            }
            catch (OperationCanceledException ex)
            {
                Log.Debug(ex, "Start");
            }
            catch (IOException ex)
            {
                Log.Debug(ex, "Start");
            }
            Log.Debug("Client status: {State}", ws.State);
            return ws.State != WebSocketState.Aborted;
        }

        public async Task Stop(string message)
        {
            try
            {
                if (_client != null && _client.State == WebSocketState.Open)
                {
                    await _client.SendAsync(_requests.UserExitRequest(), WebSocketMessageType.Binary, true, default);
                    await _client.SendAsync(_requests.UnregisterRequest(), WebSocketMessageType.Binary, true, default);
                    await _client.CloseAsync(WebSocketCloseStatus.NormalClosure, message, default);
                }
            }
            catch (WebSocketException ex)
            {
                Log.Debug(ex, "Stop");
            }
            catch (OperationCanceledException ex)
            {
                Log.Debug(ex, "Stop");
            }
            catch (IOException ex)
            {
                Log.Debug(ex, "Stop");
            }
        }

        private async void HandleCommand(PacketHeader header, DownstreamPayload stream, System.Timers.Timer heartbeatTimer)
        {
            Log.Debug("--------");
            Log.Debug("Down\t\t {HeaderSeqId}, {SeqId}, {Command}", header.SeqId, stream.SeqId, stream.Command);
            Log.Debug("Header: {Header}", header);
            Log.Debug("Payload: {Payload}", stream);
            switch (stream.Command)
            {
                case Command.GLOBAL_COMMAND:
                    HandleGlobalCommand(stream, heartbeatTimer);
                    break;
                case Command.KEEP_ALIVE:
                    HandleKeepAlive(stream);
                    break;
                case Command.PING:
                    HandlePing(stream);
                    break;
                case Command.REGISTER:
                    await HandleRegister(header.AppId, stream);
                    break;
                case Command.UNREGISTER:
                    await HandleUnregister(stream);
                    break;
                case Command.PUSH_MESSAGE:
                    await HandlePushMessage(header, stream, heartbeatTimer);
                    break;
                case ImEnums.PUSH_MESSAGE:
                    break;
                default:
                    if (stream.ErrorCode > 0)
                    {
                        Log.Warning("Error： {ErrorCode} - {ErrorMsg}", stream.ErrorCode, stream.ErrorMsg);
                        if (stream.ErrorCode == 10018)
                        {
                            await Stop("Log out");
                        }
                        Log.Debug("Error Data: {Data}", stream.ErrorData.ToBase64());
                    }
                    else
                    {
                        Log.Information("Unhandled DownstreamPayload Command: {Command}", stream.Command);
                        Log.Debug("Command Data: {Data}", stream.ToByteString().ToBase64());
                    }
                    break;
            }
            Log.Debug("--------");
        }

        private static void HandleGlobalCommand(DownstreamPayload payload, System.Timers.Timer heartbeatTimer)
        {
            ZtLiveCsCmdAck cmd = ZtLiveCsCmdAck.Parser.ParseFrom(payload.PayloadData);
            Log.Debug("\t{Command}", cmd);
            switch (cmd.CmdAckType)
            {
                case GlobalCommand.ENTER_ROOM_ACK:
                    var enterRoom = ZtLiveCsEnterRoomAck.Parser.ParseFrom(cmd.Payload);
                    heartbeatTimer.Interval = enterRoom.HeartbeatIntervalMs > 0 ? enterRoom.HeartbeatIntervalMs : 10000;
                    heartbeatTimer.Start();
                    Log.Debug("\t\t{EnterRoom}", enterRoom);
                    break;
                case GlobalCommand.HEARTBEAT_ACK:
                    var heartbeat = ZtLiveCsHeartbeatAck.Parser.ParseFrom(cmd.Payload);
                    Log.Debug("\t\t{Heartbeat}", heartbeat);
                    break;
                case GlobalCommand.USER_EXIT_ACK:
                    var userexit = ZtLiveCsUserExitAck.Parser.ParseFrom(cmd.Payload);
                    Log.Debug("\t\t{UserExit}", userexit);
                    break;
                default:
                    Log.Information("Unhandled Global.ZtLiveInteractive.CsCmdAck: {Type}", cmd.CmdAckType ?? string.Empty);
                    Log.Debug("CsCmdAck Data: {Data}", payload.PayloadData.ToBase64());
                    break;
            }
        }

        private static void HandleKeepAlive(DownstreamPayload payload)
        {
            var keepalive = KeepAliveResponse.Parser.ParseFrom(payload.PayloadData);
            Log.Debug("\t{KeepAlive}", keepalive);
        }

        private static void HandlePing(DownstreamPayload payload)
        {
            var ping = PingResponse.Parser.ParseFrom(payload.PayloadData);
            Log.Debug("\t{Ping}", ping);
        }

        private async Task HandleRegister(int appId, DownstreamPayload payload)
        {
            var register = RegisterResponse.Parser.ParseFrom(payload.PayloadData);
            _requests.Register(appId, register.InstanceId, register.SessKey.ToBase64(), register.SdkOption.Lz4CompressionThresholdBytes);
            Log.Debug("\t{Register}", register);
            await _client.SendAsync(_requests.KeepAliveRequest(), WebSocketMessageType.Binary, true, default);
            await _client.SendAsync(_requests.EnterRoomRequest(), WebSocketMessageType.Binary, true, default);
        }

        private async Task HandleUnregister(DownstreamPayload payload)
        {
            var unregister = UnregisterResponse.Parser.ParseFrom(payload.PayloadData);
            Log.Debug("\t{Unregister}", unregister);
            try
            {
                await _client.CloseAsync(WebSocketCloseStatus.NormalClosure, "Unregister", default);
            }
            catch (WebSocketException ex)
            {
                Log.Debug(ex, "Unregister response");
            }
            catch (OperationCanceledException ex)
            {
                Log.Debug(ex, "Unregister response");
            }
            catch (IOException ex)
            {
                Log.Debug(ex, "Unregister response");
            }
        }

        private async Task HandlePushMessage(PacketHeader header, DownstreamPayload stream, System.Timers.Timer heartbeatTimer)
        {
            ZtLiveScMessage message = ZtLiveScMessage.Parser.ParseFrom(stream.PayloadData);
            Log.Debug("\t{message}", message);
            var payload = message.CompressionType == ZtLiveScMessage.Types.CompressionType.Gzip ? Decompress(message.Payload) : message.Payload;

            switch (message.MessageType)
            {
                case PushMessage.ACTION_SIGNAL:
                case PushMessage.STATE_SIGNAL:
                case PushMessage.NOTIFY_SIGNAL:
                    // Handled by user
                    Handler?.Invoke(message.MessageType, payload);
                    DedicatedHandler?.Invoke(AVUPId, message.MessageType, payload);
                    break;
                case PushMessage.STATUS_CHANGED:
                    await HandleStatusChanged(payload, heartbeatTimer);
                    break;
                case PushMessage.TICKET_INVALID:
                    await HandleTicketInvalid(payload);
                    break;
                default:
                    Log.Information("Unhandled Push.ZtLiveInteractive.Message: {Type}", message.MessageType ?? string.Empty);
                    Log.Debug("CsCmdAck Data: {Data}", stream.PayloadData.ToBase64());
                    break;
            }
            try
            {
                await _client.SendAsync(_requests.PushMessageResponse(header.SeqId), WebSocketMessageType.Binary, true, default);
            }
            catch (WebSocketException ex)
            {
                Log.Debug(ex, "Push message response");
            }
            catch (OperationCanceledException ex)
            {
                Log.Debug(ex, "Push message response");
            }
            catch (IOException ex)
            {
                Log.Debug(ex, "Push message response");
            }
        }


        private async Task HandleStatusChanged(ByteString payload, System.Timers.Timer heartbeatTimer)
        {
            var statusChanged = ZtLiveScStatusChanged.Parser.ParseFrom(payload);
            Log.Debug("\t\t{StatusChanged}", statusChanged);
            if (statusChanged.Type == ZtLiveScStatusChanged.Types.Type.LiveClosed || statusChanged.Type == ZtLiveScStatusChanged.Types.Type.LiveBanned)
            {
                heartbeatTimer.Stop();
                await Stop("Live closed");
            }
        }

        private async Task HandleTicketInvalid(ByteString payload)
        {
            var ticketInvalid = ZtLiveScTicketInvalid.Parser.ParseFrom(payload);
            Log.Debug("\t\t{TicketInvalid}", ticketInvalid);
            _requests.NextTicket();
            try
            {
                await _client.SendAsync(_requests.EnterRoomRequest(), WebSocketMessageType.Binary, true, default);
            }
            catch (WebSocketException ex)
            {
                Log.Debug(ex, "Ticket invalid request");
            }
            catch (OperationCanceledException ex)
            {
                Log.Debug(ex, "Ticket invalid request");
            }
            catch (IOException ex)
            {
                Log.Debug(ex, "Ticket invalid request");
            }
        }

        private static HttpClient CreateHttpClient(string referer)
        {
            return CreateHttpClient(new Uri(referer));
        }

        private static HttpClient CreateHttpClient(Uri referer)
        {
            var client = new HttpClient(
                   new HttpClientHandler
                   {
                       AutomaticDecompression = DecompressionMethods.All,
                       UseCookies = true,
                       CookieContainer = CookieContainer
                   }
                );
            client.DefaultRequestHeaders.UserAgent.ParseAdd(UserAgent);
            client.DefaultRequestHeaders.AcceptEncoding.ParseAdd(ACCEPTED_ENCODING);
            client.DefaultRequestHeaders.Referrer = referer;
            return client;
        }
    }
}
