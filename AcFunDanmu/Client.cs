using AcFunDanmu.Enums;
using AcFunDanmu.Im.Basic;
using AcFunDanmu.Models.Client;
using Google.Protobuf;
using Serilog;
using System;
using System.Buffers;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Security;
using System.Net.WebSockets;
using System.Security.Cryptography.X509Certificates;
#if NET5_0_OR_GREATER
using System.Text.Json;
#elif NETSTANDARD2_0_OR_GREATER
using Newtonsoft.Json;
#endif
using System.Threading.Tasks;
using System.Timers;
using static AcFunDanmu.ClientUtils;
using HeartbeatTimer = System.Timers.Timer;

namespace AcFunDanmu
{
    public delegate void SignalHandler(Client sender, string messageType, ByteString payload);

    public class Client
    {
        #region Constants

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
        private static readonly Uri LIVE_URI = new Uri(LIVE_URL);
        private const string LOGIN_URL = "https://id.app.acfun.cn/rest/app/visitor/login";
        private static readonly Uri LOGIN_URI = new Uri(LOGIN_URL);
        private const string GET_TOKEN_URL = "https://id.app.acfun.cn/rest/web/token/get";
        private static readonly Uri GET_TOKEN_URI = new Uri(GET_TOKEN_URL);

        private const string PLAY_URL =
            "https://api.kuaishouzt.com/rest/zt/live/web/startPlay?subBiz=mainApp&kpn=ACFUN_APP&kpf=PC_WEB&userId={0}&did={1}&{2}={3}";

        private const string GIFT_URL =
            "https://api.kuaishouzt.com/rest/zt/live/web/gift/list?subBiz=mainApp&kpn=ACFUN_APP&kpf=PC_WEB&userId={0}&did={1}&{2}={3}";

        private const string WATCHING_URL =
            "https://api.kuaishouzt.com/rest/zt/live/web/watchingList?subBiz=mainApp&kpn=ACFUN_APP&kpf=PC_WEB&userId={0}&did={1}&{2}={3}";

        private const string USER_AGENT =
            "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/98.0.4758.102 Safari/537.36";

        private const string SAFETY_ID_CONTENT =
            "{{\"platform\":5,\"app_version\":\"2.0.32\",\"device_id\":\"null\",\"user_id\":\"{0}\"}}";

        private static readonly Dictionary<string, string> LoginForm = new Dictionary<string, string>()
            { { "sid", "acfun.api.visitor" } };

        private static readonly Dictionary<string, string> GetTokenForm = new Dictionary<string, string>()
            { { "sid", "acfun.midground.api" } };

        private const string WEBSOCKET_HOST = "wss://klink-newproduct-ws2.kwaizt.com/";
        private const string SLINK_HOST = "tcp://slink.gifshow.com:14000"; // TCP Directly
        private static readonly Uri WebsocketHost = new Uri(WEBSOCKET_HOST);

        #endregion

        public SignalHandler Handler { get; set; }

        #region Properties and Fields

        public static readonly ConcurrentDictionary<long, GiftInfo> Gifts =
            new ConcurrentDictionary<long, GiftInfo>(12, 64);

        private static readonly CookieContainer CookieContainer = new CookieContainer();
        private static string DeviceId;
        private static bool IsSignIn;
        private static bool IsPrepared;

        private long UserId = -1;
        public long HostId { get; private set; }
        public string LiveId { get; private set; }
        public string Host => $"{HostId}";
        private string ServiceToken;
        private string SecurityKey;
        private string EnterRoomAttach;
        private string[] Tickets;

        private ClientWebSocket _client;
        private ClientRequestUtils _utils;

        #endregion

        #region Constructor

        public Client()
        {
            Log.Logger = new LoggerConfiguration()
#if DEBUG
                .MinimumLevel.Debug()
#else
                .MinimumLevel.Information()
#endif
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .CreateLogger();
        }

        public Client(long userId, string serviceToken, string securityKey, string[] tickets, string enterRoomAttach,
            string liveId) : this()
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
            if (IsSignIn) return IsSignIn;
            Log.Information("Client signing in");
            try
            {
#if NET5_0_OR_GREATER
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

                using var sidContent = new StringContent(string.Format(SAFETY_ID_CONTENT, user.UserId));
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
                    Value = safetyid.Id
                });

                IsSignIn = true;
#elif NETSTANDARD2_0_OR_GREATER
                using (var client = CreateHttpClient(ACFUN_LOGIN_URI))
                {
                    using (var login = await client.GetAsync(ACFUN_LOGIN_URI))
                    {
                        if (!login.IsSuccessStatusCode)
                        {
                            Log.Error("Get login error: {Content}", await login.Content.ReadAsStringAsync());
                            return false;
                        }

                        using (var signinContent = new FormUrlEncodedContent(new Dictionary<string, string>
                               {
                                   { "username", username },
                                   { "password", password },
                                   { "key", null },
                                   { "captcha", null }
                               }))
                        {
                            using (var signin = await client.PostAsync(ACFUN_SIGN_IN_URI, signinContent))
                            {
                                if (!signin.IsSuccessStatusCode)
                                {
                                    Log.Error("Post sign in error: {Content}",
                                        await signin.Content.ReadAsStringAsync());
                                    return false;
                                }

                                var user = JsonConvert.DeserializeObject<SignIn>(
                                    await signin.Content.ReadAsStringAsync());
                                if (user == null)
                                {
                                    Log.Error("Unable to deserialize SignIn");
                                    return false;
                                }

                                using (var sidContent =
                                       new StringContent(string.Format(SAFETY_ID_CONTENT, user.UserId)))
                                {
                                    using (var sid = await client.PostAsync(ACFUN_SAFETY_ID_URI, sidContent))
                                    {
                                        if (!sid.IsSuccessStatusCode)
                                        {
                                            Log.Error("Post safety id error: {Content}",
                                                await sid.Content.ReadAsStringAsync());
                                            return false;
                                        }

                                        var safetyid =
                                            JsonConvert.DeserializeObject<SafetyId>(
                                                await sid.Content.ReadAsStringAsync());
                                        if (safetyid == null)
                                        {
                                            Log.Error("Unable to deserialize SignIn");
                                            return false;
                                        }

                                        CookieContainer.Add(new Cookie
                                        {
                                            Domain = ".acfun.cn",
                                            Name = "safety_id",
                                            Value = safetyid.Id
                                        });

                                        IsSignIn = true;
                                    }
                                }
                            }
                        }
                    }
                }
#endif
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

            return IsSignIn;
        }

        public async Task<PlayData> InitializeWithLogin(string username, string password, string uid,
            bool refreshGiftList = false)
        {
            await Login(username, password);
            return await Initialize(uid, refreshGiftList);
        }

        public static async Task<bool> Prepare()
        {
            try
            {
                if (IsPrepared) return IsPrepared;
#if NET5_0_OR_GREATER
                using var client = CreateHttpClient(LIVE_URI);

                using var index = await client.GetAsync(LIVE_URI);
                if (!index.IsSuccessStatusCode)
                {
                    Log.Error("Get live info error: {Content}", await index.Content.ReadAsStringAsync());
                }
                if (string.IsNullOrEmpty(DeviceId))
                {
                    DeviceId = CookieContainer.GetCookies(ACFUN_HOST).First(cookie => cookie.Name == "_did").Value;
                }
                IsPrepared = true;
#elif NETSTANDARD2_0_OR_GREATER
                using (var client = CreateHttpClient(LIVE_URI))
                {
                    using (var index = await client.GetAsync(LIVE_URI))
                    {
                        if (!index.IsSuccessStatusCode)
                        {
                            Log.Error("Get live info error: {Content}", await index.Content.ReadAsStringAsync());
                        }

                        if (string.IsNullOrEmpty(DeviceId))
                        {
                            DeviceId = CookieContainer.GetCookies(ACFUN_HOST)["_did"]?.Value;
                        }

                        IsPrepared = true;
                    }
                }
#endif
                return IsPrepared;
            }
            catch (HttpRequestException)
            {
                return await Prepare();
            }
            catch (TaskCanceledException)
            {
                return await Prepare();
            }
        }

        public async Task<PlayData> Initialize(string hostId, bool refreshGiftList = false)
        {
            if (!IsPrepared)
            {
                Log.Error("Client not prepared, please call Client.Prepare() first");
                return null;
            }

            if (long.TryParse(hostId, out var HostId))
            {
                return await Initialize(HostId, refreshGiftList);
            }
            else
            {
                Log.Error($"Invalid user id: {hostId}");
                return null;
            }
        }

        public async Task<PlayData> Initialize(long hostId, bool refreshGiftList = false)
        {
            if (!IsPrepared)
            {
                Log.Error("Client not prepared, please call Client.Prepare() first");
                return null;
            }

            HostId = hostId;
            Log.Information("Client initializing");
            try
            {
#if NET5_0_OR_GREATER
                using var client = CreateHttpClient(LIVE_URI);

                if (IsSignIn)
                {
                    using var getcontent = new FormUrlEncodedContent(GetTokenForm);
                    using var get = await client.PostAsync(GET_TOKEN_URI, getcontent);
                    if (!get.IsSuccessStatusCode)
                    {
                        Log.Error("Get token error: {Content}", await get.Content.ReadAsStringAsync());
                        return null;
                    }
                    var token =
 await JsonSerializer.DeserializeAsync<MidgroundToken>(await get.Content.ReadAsStreamAsync());
                    if (token == null)
                    {

                        Log.Error("Unable to deserialize MidgroundToken");
                        return null;
                    }
                    UserId = token.UserId;
                    ServiceToken = token.ServiceToken;
                    SecurityKey = token.SecurityKey;
                }
                else
                {
                    using var loginContent = new FormUrlEncodedContent(LoginForm);
                    using var login = await client.PostAsync(LOGIN_URI, loginContent);
                    if (!login.IsSuccessStatusCode)
                    {
                        Log.Error("Get token error: {Content}", await login.Content.ReadAsStringAsync());
                        return null;
                    }
                    var token =
 await JsonSerializer.DeserializeAsync<VisitorToken>(await login.Content.ReadAsStreamAsync());
                    if (token == null)
                    {

                        Log.Error("Unable to deserialize VisitorToken");
                        return null;
                    }
                    UserId = token.UserId;
                    ServiceToken = token.ServiceToken;
                    SecurityKey = token.SecurityKey;
                }

                using var form =
 new FormUrlEncodedContent(new Dictionary<string, string> { { "authorId", $"{hostId}" }, { "pullStreamType", "FLV" } });
                using var play =
 await client.PostAsync(string.Format(PLAY_URL, UserId, DeviceId, IsSignIn ? MIDGROUND_ST : VISITOR_ST, ServiceToken), form);

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
                if (playData.Result > 1)
                {
                    Log.Error(playData.ErrorMsg);
                    return null;
                }
                Tickets = playData.Data?.AvailableTickets ?? Array.Empty<string>();
                EnterRoomAttach = playData.Data?.EnterRoomAttach;
                LiveId = playData.Data?.LiveId;

                if (refreshGiftList) { UpdateGiftList(); }

                Log.Information("Client initialized");

                return playData.Data;
#elif NETSTANDARD2_0_OR_GREATER
                using (var client = CreateHttpClient(LIVE_URI))
                {
                    if (IsSignIn)
                    {
                        using (var getcontent = new FormUrlEncodedContent(GetTokenForm))
                        {
                            using (var get = await client.PostAsync(GET_TOKEN_URI, getcontent))
                            {
                                if (!get.IsSuccessStatusCode)
                                {
                                    Log.Error("Get token error: {Content}", await get.Content.ReadAsStringAsync());
                                    return null;
                                }

                                var token = JsonConvert.DeserializeObject<MidgroundToken>(
                                    await get.Content.ReadAsStringAsync());
                                if (token == null)
                                {
                                    Log.Error("Unable to deserialize MidgroundToken");
                                    return null;
                                }

                                UserId = token.UserId;
                                ServiceToken = token.ServiceToken;
                                SecurityKey = token.SecurityKey;
                            }
                        }
                    }
                    else
                    {
                        using (var loginContent = new FormUrlEncodedContent(LoginForm))
                        {
                            using (var login = await client.PostAsync(LOGIN_URI, loginContent))
                            {
                                if (!login.IsSuccessStatusCode)
                                {
                                    Log.Error("Get token error: {Content}", await login.Content.ReadAsStringAsync());
                                    return null;
                                }

                                var token = JsonConvert.DeserializeObject<VisitorToken>(
                                    await login.Content.ReadAsStringAsync());
                                if (token == null)
                                {
                                    Log.Error("Unable to deserialize VisitorToken");
                                    return null;
                                }

                                UserId = token.UserId;
                                ServiceToken = token.ServiceToken;
                                SecurityKey = token.SecurityKey;
                            }
                        }
                    }

                    using (var form = new FormUrlEncodedContent(new Dictionary<string, string>
                               { { "authorId", $"{hostId}" }, { "pullStreamType", "FLV" } }))
                    {
                        using (var play = await client.PostAsync(
                                   string.Format(PLAY_URL, UserId, DeviceId, IsSignIn ? MIDGROUND_ST : VISITOR_ST,
                                       ServiceToken), form))
                        {
                            if (!play.IsSuccessStatusCode)
                            {
                                Log.Error("Get play info error: {Content}", await play.Content.ReadAsStringAsync());
                                return null;
                            }

                            var playData = JsonConvert.DeserializeObject<Play>(await play.Content.ReadAsStringAsync());
                            if (playData == null)
                            {
                                Log.Error("Unable to deserialize Play");
                                return null;
                            }

                            if (playData.Result > 1)
                            {
                                Log.Error(playData.ErrorMsg);
                                return null;
                            }

                            Tickets = playData.Data?.AvailableTickets ?? Array.Empty<string>();
                            EnterRoomAttach = playData.Data?.EnterRoomAttach;
                            LiveId = playData.Data?.LiveId;

                            if (refreshGiftList)
                            {
                                UpdateGiftList();
                            }

                            Log.Information("Client initialized");

                            return playData.Data;
                        }
                    }
                }
#endif
            }
            catch (HttpRequestException ex)
            {
                Log.Error(ex, "Initialize exception");
                return await Initialize(hostId);
            }
            catch (TaskCanceledException ex)
            {
                Log.Error(ex, "Initialize exception");
                return await Initialize(hostId);
            }
        }


        private async void UpdateGiftList()
        {
            if (UserId == -1 || string.IsNullOrEmpty(ServiceToken) || string.IsNullOrEmpty(LiveId))
            {
                return;
            }

            try
            {
#if NET5_0_OR_GREATER
                using var client = CreateHttpClient(LIVE_URI);

                using var giftContent = new FormUrlEncodedContent(new Dictionary<string, string>
                    {
                        {"visitorId", $"{UserId}" },
                        {"liveId", LiveId }
                    });
                using var gift =
 await client.PostAsync(string.Format(GIFT_URL, UserId, DeviceId, IsSignIn ? MIDGROUND_ST : VISITOR_ST, ServiceToken), giftContent);
                if (gift.IsSuccessStatusCode)
                {
                    var giftList =
 await JsonSerializer.DeserializeAsync<GiftList>(await gift.Content.ReadAsStreamAsync());
                    foreach (var item in giftList?.Data?.GiftList ?? Array.Empty<Gift>())
                    {
                        var giftInfo = new GiftInfo
                        {
                            Name = item.GiftName,
                            Value = item.GiftPrice,
                            Pic = new Uri(item.WebpPicList[0].Url)
                        };
                        Gifts[item.GiftId] = giftInfo;
                    }
                }
#elif NETSTANDARD2_0_OR_GREATER
                using (var client = CreateHttpClient(LIVE_URI))
                {
                    using (var giftContent = new FormUrlEncodedContent(new Dictionary<string, string>
                           {
                               { "visitorId", $"{UserId}" },
                               { "liveId", LiveId }
                           }))
                    {
                        using (var gift = await client.PostAsync(
                                   string.Format(GIFT_URL, UserId, DeviceId, IsSignIn ? MIDGROUND_ST : VISITOR_ST,
                                       ServiceToken), giftContent))
                        {
                            if (!gift.IsSuccessStatusCode) return;
                            var giftList =
                                JsonConvert.DeserializeObject<GiftList>(await gift.Content.ReadAsStringAsync());
                            foreach (var item in giftList?.Data?.GiftList ?? Array.Empty<Gift>())
                            {
                                var giftInfo = new GiftInfo
                                {
                                    Name = item.GiftName,
                                    Value = item.GiftPrice,
                                    Pic = new Uri(item.WebpPicList[0].Url)
                                };
                                Gifts[item.GiftId] = giftInfo;
                            }
                        }
                    }
                }
#endif
            }
            catch (HttpRequestException ex)
            {
                Log.Error(ex, "Update gift list exception");
                UpdateGiftList();
            }
            catch (TaskCanceledException ex)
            {
                Log.Error(ex, "Update gift list exception");
                UpdateGiftList();
            }
        }

        public async Task<WatchingUser[]> WatchingList()
        {
            if (UserId == -1 || string.IsNullOrEmpty(ServiceToken) || string.IsNullOrEmpty(LiveId))
            {
                return Array.Empty<WatchingUser>();
            }

            try
            {
#if NET5_0_OR_GREATER
                using var client = CreateHttpClient(LIVE_URL);

                using var watchingContent = new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    {"visitorId", $"{UserId}" },
                    {"liveId", LiveId }
                });
                using var watching =
 await client.PostAsync(string.Format(WATCHING_URL, UserId, DeviceId, IsSignIn ? MIDGROUND_ST : VISITOR_ST, ServiceToken), watchingContent);
                if (!watching.IsSuccessStatusCode)
                {
                    return Array.Empty<WatchingUser>();
                }
                var watchingList =
 await JsonSerializer.DeserializeAsync<WatchingList>(await watching.Content.ReadAsStreamAsync());

                return watchingList?.Data?.List ?? Array.Empty<WatchingUser>();
#elif NETSTANDARD2_0_OR_GREATER
                using (var client = CreateHttpClient(LIVE_URL))
                {
                    using (var watchingContent = new FormUrlEncodedContent(new Dictionary<string, string>
                           {
                               { "visitorId", $"{UserId}" },
                               { "liveId", LiveId }
                           }))
                    {
                        using (var watching = await client.PostAsync(
                                   string.Format(WATCHING_URL, UserId, DeviceId, IsSignIn ? MIDGROUND_ST : VISITOR_ST,
                                       ServiceToken), watchingContent))
                        {
                            if (!watching.IsSuccessStatusCode)
                            {
                                return Array.Empty<WatchingUser>();
                            }

                            var watchingList =
                                JsonConvert.DeserializeObject<WatchingList>(await watching.Content.ReadAsStringAsync());

                            return watchingList?.Data?.List ?? Array.Empty<WatchingUser>();
                        }
                    }
                }
#endif
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

#if NET5_0_OR_GREATER
        public async Task<bool> Start()
        {
            if (UserId == -1 || string.IsNullOrEmpty(ServiceToken) || string.IsNullOrEmpty(SecurityKey) ||
                string.IsNullOrEmpty(LiveId) || string.IsNullOrEmpty(EnterRoomAttach) || Tickets == null ||
                Tickets.Length == 0)
            {
                Log.Information("Not initialized or live is ended");
                return false;
            }

            using var owner = MemoryPool<byte>.Shared.Rent();

            if (_utils != null)
            {
                _utils = null;
            }

            _utils =
                new ClientRequestUtils(UserId, DeviceId, ServiceToken, SecurityKey, LiveId, EnterRoomAttach, Tickets);
            if (_client != null)
            {
                _client.Dispose();
                _client = null;
                GC.Collect();
            }

            _client = CreateWebsocketClient();

            #region Timers

            using var heartbeatTimer = new HeartbeatTimer();
            heartbeatTimer.Elapsed += Heartbeat;
            heartbeatTimer.AutoReset = true;
            using var deathTimer = new HeartbeatTimer();
            deathTimer.Interval = TimeSpan.FromSeconds(10).TotalMilliseconds;
            deathTimer.Elapsed += async (s, e) => { await Stop("dead"); };

            #endregion

            try
            {
                await _client.ConnectAsync(WebsocketHost, default);

                #region Register

                await _client.SendAsync(_utils.RegisterRequest(), WebSocketMessageType.Binary, true, default);

                #endregion

                #region Main loop

                while (_client.State == WebSocketState.Open)
                {
                    var buffer = owner.Memory;
                    try
                    {
                        await _client.ReceiveAsync(buffer, default);

                        var stream =
                            Decode<DownstreamPayload>(buffer.Span, SecurityKey, _utils.SessionKey, out var header);
                        if (stream == null)
                        {
                            Log.Error("Downstream is null: {Content}", Convert.ToBase64String(buffer.Span));
                            continue;
                        }

                        HandleCommand(header, stream, heartbeatTimer, deathTimer);
                    }
                    catch (Exception ex)
                    {
                        Log.Debug(ex, "Main");
                        heartbeatTimer.Stop();
                        break;
                    }
                }

                Log.Debug("Client status: {State}", _client.State);
                heartbeatTimer.Stop();
                deathTimer.Stop();

                #endregion
            }
            catch (Exception ex)
            {
                Log.Debug(ex, "Start");
            }

            return _client.State != WebSocketState.Aborted;
        }
#elif NETSTANDARD2_0_OR_GREATER
        public async Task<bool> Start()
        {
            if (UserId == -1 || string.IsNullOrEmpty(ServiceToken) || string.IsNullOrEmpty(SecurityKey) ||
                string.IsNullOrEmpty(LiveId) || string.IsNullOrEmpty(EnterRoomAttach) || Tickets == null ||
                Tickets.Length == 0)
            {
                Log.Information("Not initialized or live is ended");
                return false;
            }

            var owner = ArrayPool<byte>.Shared;

            if (_utils != null)
            {
                _utils = null;
            }

            _utils = new ClientRequestUtils(UserId, DeviceId, ServiceToken, SecurityKey, LiveId, EnterRoomAttach,
                Tickets);
            if (_client != null)
            {
                _client.Dispose();
                _client = null;
                GC.Collect();
            }

            _client = CreateWebsocketClient();

            #region Timers

            using (HeartbeatTimer heartbeatTimer = new HeartbeatTimer(), deathTimer = new HeartbeatTimer())
            {
                heartbeatTimer.Elapsed += Heartbeat;
                heartbeatTimer.AutoReset = true;

                deathTimer.Interval = TimeSpan.FromSeconds(10).TotalMilliseconds;
                deathTimer.Elapsed += async (s, e) => { await Stop("dead"); };

                #endregion

                try
                {
                    await _client.ConnectAsync(WebsocketHost, default);

                    #region Register

                    await _client.SendAsync(new ArraySegment<byte>(_utils.RegisterRequest()),
                        WebSocketMessageType.Binary, true, default);

                    #endregion

                    #region Main loop

                    while (_client.State == WebSocketState.Open)
                    {
                        try
                        {
                            var buffer = owner.Rent(1024 * 1024);
                            var segment = new ArraySegment<byte>(buffer);
                            var result = await _client.ReceiveAsync(segment, default);

                            var stream = Decode<DownstreamPayload>(buffer, SecurityKey, _utils.SessionKey,
                                out var header);
                            if (stream == null)
                            {
                                Log.Error("Downstream is null: {Content}", Convert.ToBase64String(buffer));
                                continue;
                            }

                            HandleCommand(header, stream, heartbeatTimer, deathTimer);

                            owner.Return(buffer);
                        }
                        catch (Exception ex)
                        {
                            Log.Debug(ex, "Main");
                            heartbeatTimer.Stop();
                            break;
                        }
                    }

                    Log.Debug("Client status: {State}", _client.State);
                    heartbeatTimer.Stop();
                    deathTimer.Stop();

                    #endregion
                }
                catch (Exception ex)
                {
                    Log.Debug(ex, "Start");
                }

                return _client.State != WebSocketState.Aborted;
            }
        }
#endif

        public async Task Stop(string message)
        {
            try
            {
#if NET5_0_OR_GREATER
                if (_client is { State: WebSocketState.Open })
                {
                    await _client.SendAsync(_utils.UserExitRequest(), WebSocketMessageType.Binary, true, default);
                    await _client.SendAsync(_utils.UnregisterRequest(), WebSocketMessageType.Binary, true, default);
#elif NETSTANDARD2_0_OR_GREATER
                if (_client != null && _client.State == WebSocketState.Open)
                {
                    await _client.SendAsync(new ArraySegment<byte>(_utils.UserExitRequest()),
                        WebSocketMessageType.Binary, true, default);
                    await _client.SendAsync(new ArraySegment<byte>(_utils.UnregisterRequest()),
                        WebSocketMessageType.Binary, true, default);
#endif
                    //await _client.CloseAsync(WebSocketCloseStatus.NormalClosure, message, default);
                }
            }
            catch (Exception ex)
            {
                Log.Debug(ex, "Stop");
            }
        }

        private async void HandleCommand(PacketHeader header, DownstreamPayload stream, HeartbeatTimer heartbeatTimer,
            HeartbeatTimer deathTimer)
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
                    await HandlePushMessage(header, stream, heartbeatTimer, deathTimer);
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
                        Log.Information("Unhandled DownstreamPayload Command: {Command}", stream.Command ?? "Empty");
                        Log.Debug("Command Data: {Data}", stream.ToByteString().ToBase64());
                    }

                    break;
            }

            Log.Debug("--------");
        }

        private static void HandleGlobalCommand(DownstreamPayload payload, HeartbeatTimer heartbeatTimer)
        {
            var cmd = ZtLiveCsCmdAck.Parser.ParseFrom(payload.PayloadData);
            Log.Debug("\t{Command}", cmd);
            switch (cmd.CmdAckType)
            {
                case GlobalCommand.ENTER_ROOM_ACK:
                    var enterRoom = ZtLiveCsEnterRoomAck.Parser.ParseFrom(cmd.Payload);
                    heartbeatTimer.Interval = enterRoom.HeartbeatIntervalMs > 0
                        ? enterRoom.HeartbeatIntervalMs
                        : TimeSpan.FromSeconds(10).TotalMilliseconds;
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
                    Log.Information("Unhandled Global.ZtLiveInteractive.CsCmdAck: {Type}", cmd.CmdAckType ?? "Empty");
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
            _utils.Register(appId, register.InstanceId, register.SessKey.ToBase64(),
                register.SdkOption.Lz4CompressionThresholdBytes);
            Log.Debug("\t{Register}", register);
            try
            {
#if NET5_0_OR_GREATER
                await _client.SendAsync(_utils.KeepAliveRequest(), WebSocketMessageType.Binary, true, default);
                await _client.SendAsync(_utils.EnterRoomRequest(), WebSocketMessageType.Binary, true, default);
#elif NETSTANDARD2_0_OR_GREATER
                await _client.SendAsync(new ArraySegment<byte>(_utils.KeepAliveRequest()), WebSocketMessageType.Binary,
                    true, default);
                await _client.SendAsync(new ArraySegment<byte>(_utils.EnterRoomRequest()), WebSocketMessageType.Binary,
                    true, default);
#endif
            }
            catch (Exception ex)
            {
                Log.Debug(ex, "Register response");
            }
        }

        private async Task HandleUnregister(DownstreamPayload payload)
        {
            var unregister = UnregisterResponse.Parser.ParseFrom(payload.PayloadData);
            Log.Debug("\t{Unregister}", unregister);
            try
            {
                await _client.CloseAsync(WebSocketCloseStatus.NormalClosure, "Unregister", default);
            }
            catch (Exception ex)
            {
                Log.Debug(ex, "Unregister response");
            }
        }

        private async Task HandlePushMessage(PacketHeader header, DownstreamPayload stream,
            HeartbeatTimer heartbeatTimer, HeartbeatTimer deathTimer)
        {
            var message = ZtLiveScMessage.Parser.ParseFrom(stream.PayloadData);
            Log.Debug("\t{message}", message);
            var payload = message.CompressionType == ZtLiveScMessage.Types.CompressionType.Gzip
                ? Decompress(message.Payload)
                : message.Payload;

            deathTimer.Stop();
            deathTimer.Start();

            switch (message.MessageType)
            {
                case PushMessage.ACTION_SIGNAL:
                case PushMessage.STATE_SIGNAL:
                case PushMessage.NOTIFY_SIGNAL:
                    // Handled by user
                    Handler?.Invoke(this, message.MessageType, payload);
                    break;
                case PushMessage.STATUS_CHANGED:
                    await HandleStatusChanged(payload, heartbeatTimer);
                    break;
                case PushMessage.TICKET_INVALID:
                    await HandleTicketInvalid(payload);
                    break;
                default:
                    Log.Information("Unhandled Push.ZtLiveInteractive.Message: {Type}", message.MessageType ?? "Empty");
                    Log.Debug("CsCmdAck Data: {Data}", stream.PayloadData.ToBase64());
                    break;
            }

            try
            {
#if NET5_0_OR_GREATER
                await _client.SendAsync(_utils.PushMessageResponse(header.SeqId), WebSocketMessageType.Binary, true, default);
#elif NETSTANDARD2_0_OR_GREATER
                await _client.SendAsync(new ArraySegment<byte>(_utils.PushMessageResponse(header.SeqId)),
                    WebSocketMessageType.Binary, true, default);
#endif
            }
            catch (Exception ex)
            {
                Log.Debug(ex, "Push message response");
            }
        }


        private async Task HandleStatusChanged(ByteString payload, HeartbeatTimer heartbeatTimer)
        {
            var statusChanged = ZtLiveScStatusChanged.Parser.ParseFrom(payload);
            Log.Debug("\t\t{StatusChanged}", statusChanged);
            if (statusChanged.Type == ZtLiveScStatusChanged.Types.Type.LiveClosed ||
                statusChanged.Type == ZtLiveScStatusChanged.Types.Type.LiveBanned)
            {
                heartbeatTimer.Stop();
                await Stop("Live closed");
            }
        }

        private async Task HandleTicketInvalid(ByteString payload)
        {
            var ticketInvalid = ZtLiveScTicketInvalid.Parser.ParseFrom(payload);
            Log.Debug("\t\t{TicketInvalid}", ticketInvalid);
            _utils.NextTicket();
            try
            {
#if NET5_0_OR_GREATER
                await _client.SendAsync(_utils.EnterRoomRequest(), WebSocketMessageType.Binary, true, default);
#elif NETSTANDARD2_0_OR_GREATER
                await _client.SendAsync(new ArraySegment<byte>(_utils.EnterRoomRequest()), WebSocketMessageType.Binary,
                    true, default);
#endif
            }
            catch (Exception ex)
            {
                Log.Debug(ex, "Ticket invalid request");
            }
        }

        private static HttpClient CreateHttpClient(string referer) => CreateHttpClient(new Uri(referer));

        private static HttpClient CreateHttpClient(Uri referer)
        {
            var client = new HttpClient(
                new HttpClientHandler
                {
#if NET5_0_OR_GREATER
                    AutomaticDecompression = DecompressionMethods.All,
#elif NETSTANDARD2_0_OR_GREATER
                    AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate,
#endif
                    UseCookies = true,
                    CookieContainer = CookieContainer
                });
            client.DefaultRequestHeaders.UserAgent.ParseAdd(USER_AGENT);
            client.DefaultRequestHeaders.AcceptEncoding.ParseAdd(ACCEPTED_ENCODING);
            client.DefaultRequestHeaders.Referrer = referer;
            return client;
        }

        private static ClientWebSocket CreateWebsocketClient()
        {
            var client = new ClientWebSocket();
            client.Options.SetRequestHeader("Origin", _ACFUN_HOST);
            client.Options.SetRequestHeader("User-Agent", USER_AGENT);
            client.Options.KeepAliveInterval = TimeSpan.Zero; // ????WTF????
            client.Options.Cookies = CookieContainer;
#if NETSTANDARD2_1_OR_GREATER
            client.Options.RemoteCertificateValidationCallback = RemoteCertificateValidationCallback;
#endif
            return client;
        }

        private static bool RemoteCertificateValidationCallback(object sender, X509Certificate certificate,
            X509Chain chain, SslPolicyErrors sslPolicyErrors) => true;

        private async void Heartbeat(object sender, ElapsedEventArgs e)
        {
            if (_client.State == WebSocketState.Open)
            {
                Log.Debug("HEARTBEAT");
                try
                {
#if NET5_0_OR_GREATER
                    await _client.SendAsync(_utils.HeartbeatReqeust(), WebSocketMessageType.Binary, true, default);

                    if (_utils.HeartbeatSeqId % 5 == 4)
                    {
                        await _client.SendAsync(_utils.KeepAliveRequest(), WebSocketMessageType.Binary, true, default);
                    }
#elif NETSTANDARD2_0_OR_GREATER
                    await _client.SendAsync(new ArraySegment<byte>(_utils.HeartbeatReqeust()),
                        WebSocketMessageType.Binary, true, default);

                    if (_utils.HeartbeatSeqId % 5 == 4)
                    {
                        await _client.SendAsync(new ArraySegment<byte>(_utils.KeepAliveRequest()),
                            WebSocketMessageType.Binary, true, default);
                    }
#endif
                }
                catch (Exception ex)
                {
                    Log.Debug(ex, "Heartbeat");
                    (sender as HeartbeatTimer)?.Stop();
                }
            }
            else
            {
                (sender as HeartbeatTimer)?.Stop();
            }
        }
    }
}