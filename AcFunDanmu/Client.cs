using AcFunDanmu.Enums;
using AcFunDanmu.Models.Client;
using Google.Protobuf;
using System;
using System.Buffers;
using System.Collections.Generic;
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
        public static readonly SortedList<long, GiftInfo> Gifts = new SortedList<long, GiftInfo>();
        private static DateTimeOffset _LastGiftUpdate = DateTimeOffset.MinValue;

        private const string ACCEPTED_ENCODING = "gzip, deflate, br";
        private const string VISITOR_ST = "acfun.api.visitor_st";
        private const string MIDGROUND_ST = "acfun.midground.api_st";
        private const string _ACFUN_HOST = "https://live.acfun.cn";
        private static readonly Uri ACFUN_HOST = new Uri(_ACFUN_HOST);
        private const string ACFUN_LOGIN_URL = "https://www.acfun.cn/login";
        private static readonly Uri ACFUN_LOGIN_URI = new Uri(ACFUN_LOGIN_URL);
        private const string ACFUN_SIGN_IN_URL = "https://id.app.acfun.cn/rest/web/login/signin";
        private static readonly Uri ACFUN_SIGN_IN_URI = new Uri(ACFUN_SIGN_IN_URL);
        private const string ACFUN_SAFETY_ID_URL = "https://sec-cdn.gifshow.com/safetyid";
        private static readonly Uri ACFUN_SAFETY_ID_URI = new Uri(ACFUN_SAFETY_ID_URL);
        private const string LIVE_URL = "https://live.acfun.cn/live";
        private const string LOGIN_URL = "https://id.app.acfun.cn/rest/app/visitor/login";
        private static readonly Uri LOGIN_URI = new Uri(LOGIN_URL);
        private const string GET_TOKEN_URL = "https://id.app.acfun.cn/rest/web/token/get";
        private static readonly Uri GET_TOKEN_URI = new Uri(GET_TOKEN_URL);
        private const string PLAY_URL = "https://api.kuaishouzt.com/rest/zt/live/web/startPlay?subBiz=mainApp&kpn=ACFUN_APP&kpf=PC_WEB&userId={0}&did={1}&{2}={3}";
        private const string GIFT_URL = "https://api.kuaishouzt.com/rest/zt/live/web/gift/list?subBiz=mainApp&kpn=ACFUN_APP&kpf=PC_WEB&userId={0}&did={1}&{2}={3}";
        private const string WATCHING_URL = "https://api.kuaishouzt.com/rest/zt/live/web/watchingList?subBiz=mainApp&kpn=ACFUN_APP&kpf=PC_WEB&userId={0}&did={1}&{2}={3}";

        private const string UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/83.0.4103.61 Safari/537.36";

        private const string SAFETY_ID_CONTENT = "{{\"platform\":5,\"app_version\":\"2.0.32\",\"device_id\":\"null\",\"user_id\":\"{0}\"}}";
        private static readonly Dictionary<string, string> LOGIN_FORM = new Dictionary<string, string> { { "sid", "acfun.api.visitor" } };
        private static readonly Dictionary<string, string> GET_TOKEN_FORM = new Dictionary<string, string> { { "sid", "acfun.midground.api" } };

        private const string _Host = "wss://link.xiatou.com/";
        private static readonly Uri Host = new Uri(_Host);
        #endregion

        public SignalHandler Handler { get; set; }
        public DedicatedSignalHandler DedicatedHandler { get; set; }

        #region Properties and Fields
        private static readonly CookieContainer CookieContainer = new CookieContainer();
        private static string DeviceId;
        private static bool IsSignIn = false;

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
        public Client() { }

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

        public async ValueTask<bool> Login(string username, string password)
        {
            if (!IsSignIn)
            {
#if DEBUG
                Console.WriteLine("Client signing in");
#endif
                try
                {
                    using var client = CreateHttpClient(ACFUN_LOGIN_URI);

                    using var login = await client.GetAsync(ACFUN_LOGIN_URI);
                    if (!login.IsSuccessStatusCode)
                    {
                        Console.WriteLine(await login.Content.ReadAsStringAsync());
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
                        Console.WriteLine(await signin.Content.ReadAsStringAsync());
                        return false;
                    }
                    var user = await JsonSerializer.DeserializeAsync<SignIn>(await signin.Content.ReadAsStreamAsync());

                    using var sidContent = new StringContent(string.Format(SAFETY_ID_CONTENT, user.userId));
                    using var sid = await client.PostAsync(ACFUN_SAFETY_ID_URI, sidContent);
                    if (!sid.IsSuccessStatusCode)
                    {
                        Console.WriteLine(await sid.Content.ReadAsStringAsync());
                        return false;
                    }
                    var safetyid = await JsonSerializer.DeserializeAsync<SafetyId>(await sid.Content.ReadAsStreamAsync());

                    CookieContainer.Add(new Cookie
                    {
                        Domain = ".acfun.cn",
                        Name = "safety_id",
                        Value = safetyid.safety_id
                    });

                    IsSignIn = true;
                }
                catch (HttpRequestException e)
                {
#if DEBUG
                    Console.WriteLine("Login Exception: {0}", e.Message);
#endif
                    return await Login(username, password);
                }
            }
            return IsSignIn;
        }

        public async ValueTask<Play.PlayData> InitializeWithLogin(string username, string password, string uid)
        {
            await Login(username, password);
            return await Initialize(uid);
        }

        public async Task<Play.PlayData> Initialize(string uid)
        {
            if (long.TryParse(uid, out _))
            {
                AVUPId = uid;
                Console.WriteLine("Client initializing");
                try
                {
                    using var client = CreateHttpClient($"{LIVE_URL}/{uid}");

                    using var index = await client.GetAsync($"{LIVE_URL}/{uid}");
                    if (!index.IsSuccessStatusCode)
                    {
                        Console.WriteLine(await index.Content.ReadAsStringAsync());
                        return default;
                    }
                    if (string.IsNullOrEmpty(DeviceId))
                    {
                        DeviceId = CookieContainer.GetCookies(ACFUN_HOST).Where(cookie => cookie.Name == "_did").First().Value;
                    }

                    if (IsSignIn)
                    {
                        using var getcontent = new FormUrlEncodedContent(GET_TOKEN_FORM);
                        using var get = await client.PostAsync(GET_TOKEN_URI, getcontent);
                        if (!get.IsSuccessStatusCode)
                        {
                            Console.WriteLine(await get.Content.ReadAsStringAsync());
                            return default;
                        }
                        var token = await JsonSerializer.DeserializeAsync<MidgroundToken>(await get.Content.ReadAsStreamAsync());
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
                            Console.WriteLine(await login.Content.ReadAsStringAsync());
                            return default;
                        }
                        var token = await JsonSerializer.DeserializeAsync<VisitorToken>(await login.Content.ReadAsStreamAsync());

                        UserId = token.userId;
                        ServiceToken = token.service_token;
                        SecurityKey = token.acSecurity;
                    }

                    using var form = new FormUrlEncodedContent(new Dictionary<string, string> { { "authorId", uid } });
                    using var play = await client.PostAsync(string.Format(PLAY_URL, UserId, DeviceId, IsSignIn ? MIDGROUND_ST : VISITOR_ST, ServiceToken), form);

                    if (!play.IsSuccessStatusCode)
                    {
                        Console.WriteLine(await play.Content.ReadAsStringAsync());
                        return default;
                    }

                    var playData = await JsonSerializer.DeserializeAsync<Play>(await play.Content.ReadAsStreamAsync());
                    if (playData.result != 1)
                    {
                        Console.WriteLine(playData.error_msg);
                        return default;
                    }
                    Tickets = playData.data.availableTickets;
                    EnterRoomAttach = playData.data.enterRoomAttach;
                    LiveId = playData.data.liveId;


                    UpdateGiftList();

                    Console.WriteLine("Client initialized");

                    return playData.data;
                }
                catch (HttpRequestException e)
                {
#if DEBUG
                    Console.WriteLine("Initialize exception: {0}", e.Message);
#endif
                    return await Initialize(uid);
                }
            }
            else
            {
                Console.WriteLine($"Invliad user id: {uid}");
                return default;
            }
        }

        private async void UpdateGiftList()
        {
            var now = DateTimeOffset.Now;
            if ((now - _LastGiftUpdate).TotalHours > 1)
            {
                _LastGiftUpdate = now;
                try
                {
                    using var client = CreateHttpClient(LIVE_URL);

                    using var giftContent = new FormUrlEncodedContent(new Dictionary<string, string>
                    {
                        {"visitorId", $"{UserId}" },
                        {"liveId", LiveId }
                    });
                    using var gift = await client.PostAsync(string.Format(GIFT_URL, UserId, DeviceId, IsSignIn ? MIDGROUND_ST : VISITOR_ST, ServiceToken), giftContent);
                    var giftList = await JsonSerializer.DeserializeAsync<GiftList>(await gift.Content.ReadAsStreamAsync());
                    foreach (var item in giftList.data.giftList)
                    {
                        Gifts.Add(item.giftId, new GiftInfo { Name = item.giftName, Pic = new Uri(item.webpPicList[0].url) });
                    }

                    Console.WriteLine("Gift list updated");
                }
                catch (HttpRequestException e)
                {
#if DEBUG
                    Console.WriteLine("Update Gift List Exception: {0}", e.Message);
#endif
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

                return watchingList.data.list;
            }
            catch (HttpRequestException e)
            {
#if DEBUG
                Console.WriteLine("Wathcing List Exception: {0}", e.Message);
#endif
                return await WatchingList();
            }
        }

        public async Task<bool> Start()
        {

            if (UserId == -1 || string.IsNullOrEmpty(ServiceToken) || string.IsNullOrEmpty(SecurityKey) || string.IsNullOrEmpty(LiveId) || string.IsNullOrEmpty(EnterRoomAttach) || Tickets == null)
            {
                Console.WriteLine("Not initialized or live is ended");
                return false;
            }

            using var owner = MemoryPool<byte>.Shared.Rent();
            _requests = new ClientRequests(UserId, ServiceToken, SecurityKey, LiveId, EnterRoomAttach, Tickets);
            using var ws = new ClientWebSocket();
            _client = ws;
            try
            {
                await ws.ConnectAsync(Host, default);

                #region Register & Enter Room
                //Register
                await ws.SendAsync(_requests.RegisterRequest(), WebSocketMessageType.Binary, true, default);
                var resp = owner.Memory;
                await ws.ReceiveAsync(resp, default);
                var registerDown = Decode(typeof(DownstreamPayload), resp.Span, SecurityKey, _requests.SessionKey, out _) as DownstreamPayload;
                var regResp = RegisterResponse.Parser.ParseFrom(registerDown.PayloadData);
                _requests.Register(regResp.InstanceId, regResp.SessKey.ToBase64(), regResp.SdkOption.Lz4CompressionThresholdBytes);

                //Ping
                //await client.SendAsync(Ping(), WebSocketMessageType.Binary, true, default);

                //Keep Alive
                await ws.SendAsync(_requests.KeepAliveRequest(true), WebSocketMessageType.Binary, true, default);

                //Enter room
                await ws.SendAsync(_requests.EnterRoomRequest(), WebSocketMessageType.Binary, true, default);
                #endregion

                #region Timers
                using var heartbeatTimer = new System.Timers.Timer();
                heartbeatTimer.Elapsed += async (s, e) =>
                {
                    if (ws.State == WebSocketState.Open)
                    {
                        try
                        {
                            await ws.SendAsync(
                                _requests.HeartbeatReqeust(),
                                WebSocketMessageType.Binary,
                                true,
                                default
                            );

                            await ws.SendAsync(_requests.KeepAliveRequest(), WebSocketMessageType.Binary, true, default);
                        }
                        catch (WebSocketException ex)
                        {
#if DEBUG
                            Console.WriteLine("Heartbeat - WebSocketException: {0}", ex);
#endif
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

                #region Main loop
                while (ws.State == WebSocketState.Open)
                {
                    var buffer = owner.Memory;
                    try
                    {
                        await ws.ReceiveAsync(buffer, default);

                        var stream = Decode(typeof(DownstreamPayload), buffer.Span, SecurityKey, _requests.SessionKey, out var header) as DownstreamPayload;

                        HandleCommand(header, stream, heartbeatTimer);

                    }
                    catch (WebSocketException e)
                    {
                        heartbeatTimer.Stop();
#if DEBUG
                        Console.WriteLine("Main - WebSocket Exception: {0}", e.Message);
#endif
                        break;
                    }
                }
                heartbeatTimer.Stop();
                #endregion
            }
            catch (HttpRequestException ex)
            {
#if DEBUG
                Console.WriteLine("Start - HttpRequestException: {0}", ex.Message);
#endif
            }
            catch (WebSocketException ex)
            {
#if DEBUG
                Console.WriteLine("Start - WebSocketException: {0}", ex.Message);
#endif 
            }
#if DEBUG
            Console.WriteLine($"Client status: {ws.State}");
#endif
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
#if DEBUG
                Console.WriteLine("Stop - WebSocketException: {0}", ex.Message);
#endif
            }
        }

        private async void HandleCommand(PacketHeader header, DownstreamPayload stream, System.Timers.Timer heartbeatTimer)
        {
            if (stream == null) { return; }
            switch (stream.Command)
            {
                case Command.GLOBAL_COMMAND:
                    ZtLiveCsCmdAck cmd = ZtLiveCsCmdAck.Parser.ParseFrom(stream.PayloadData);

                    switch (cmd.CmdAckType)
                    {
                        case GlobalCommand.ENTER_ROOM_ACK:
                            var enterRoom = ZtLiveCsEnterRoomAck.Parser.ParseFrom(cmd.Payload);
                            heartbeatTimer.Interval = enterRoom.HeartbeatIntervalMs > 0 ? enterRoom.HeartbeatIntervalMs : 10000;
                            heartbeatTimer.Start();
                            break;
                        case GlobalCommand.HEARTBEAT_ACK:
                            var heartbeat = ZtLiveCsHeartbeatAck.Parser.ParseFrom(cmd.Payload);
                            break;
                        case GlobalCommand.USER_EXIT_ACK:
                            break;
                        default:
                            Console.WriteLine("Unhandled Global.ZtLiveInteractive.CsCmdAck: {0}", cmd.CmdAckType);
                            Console.WriteLine(cmd);
                            break;
                    }
                    break;
                case Command.KEEP_ALIVE:
                    var keepalive = KeepAliveResponse.Parser.ParseFrom(stream.PayloadData);
                    break;
                case Command.PING:
                    var ping = PingResponse.Parser.ParseFrom(stream.PayloadData);
                    break;
                case Command.UNREGISTER:
                    var unregister = UnregisterResponse.Parser.ParseFrom(stream.PayloadData);
                    await _client.CloseAsync(WebSocketCloseStatus.NormalClosure, "Unregister", default);
                    break;
                case Command.PUSH_MESSAGE:
                    try
                    {
                        await _client.SendAsync(_requests.PushMessageResponse(header.SeqId), WebSocketMessageType.Binary, true, default);
                    }
                    catch (WebSocketException e)
                    {
#if  DEBUG
                        Console.WriteLine("Push Messsge Response Exception: {0}", e.Message);
#endif
                    }
                    ZtLiveScMessage message = ZtLiveScMessage.Parser.ParseFrom(stream.PayloadData);

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
                            var statusChanged = ZtLiveScStatusChanged.Parser.ParseFrom(payload);
                            if (statusChanged.Type == ZtLiveScStatusChanged.Types.Type.LiveClosed || statusChanged.Type == ZtLiveScStatusChanged.Types.Type.LiveBanned)
                            {
                                heartbeatTimer.Stop();
                                await Stop("Live closed");
                            }
                            break;
                        case PushMessage.TICKET_INVALID:
                            var ticketInvalid = ZtLiveScTicketInvalid.Parser.ParseFrom(payload);
                            //TicketIndex = (TicketIndex + 1) % Tickets.Length;
                            _requests.NextTicket();
                            try
                            {
                                await _client.SendAsync(_requests.EnterRoomRequest(), WebSocketMessageType.Binary, true, default);
                            }
                            catch (WebSocketException e)
                            {
#if DEBUG
                                Console.WriteLine("Ticket Invalid Request Exception: {0}", e.Message);
#endif
                            }
                            break;
                    }
                    break;
                case ImEnums.PUSH_MESSAGE:
                    var msg = Im.Message.Types.Message.Parser.ParseFrom(stream.PayloadData);
                    if (Enum.IsDefined(typeof(Im.Cloud.Types.Message.Types.ImcMessageType), msg.ContentType))
                    {
                        var type = (Im.Cloud.Types.Message.Types.ImcMessageType)msg.ContentType;
                        switch (type)
                        {
                            case Im.Cloud.Types.Message.Types.ImcMessageType.Text:
                                var txt = Im.Cloud.Types.Message.Types.Text.Parser.ParseFrom(msg.Content);
                                break;
                            default:
                                Console.WriteLine("Unhandled IM SDK Push Message Content Type: {0}", type);
                                break;
                        }
                    }
                    else
                    {
#if DEBUG
                        Console.WriteLine("Invalid IM SDK Push Message Content Type: {0}", msg.ContentType);
#endif
                    }
                    break;
                default:
                    if (stream.ErrorCode > 0)
                    {
                        Console.WriteLine("Error： {0} - {1}", stream.ErrorCode, stream.ErrorMsg);
                        if (stream.ErrorCode == 10018)
                        {
                            await Stop("Log out");
                        }
#if DEBUG
                        Console.WriteLine(stream.ErrorData.ToBase64());
#endif
                    }
                    else
                    {
                        Console.WriteLine("Unhandled DownstreamPayload command: {0}", stream.Command);
#if DEBUG
                        Console.WriteLine(stream);
#endif
                    }
                    break;
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
