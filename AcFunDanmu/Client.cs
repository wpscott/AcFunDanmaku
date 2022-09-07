#if NET5_0_OR_GREATER
using System.Text.Json;
using System.Linq;
#elif NETSTANDARD2_0_OR_GREATER
using Newtonsoft.Json;
#endif
using System;
using System.Buffers;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Timers;
using AcFunDanmu.Enums;
using AcFunDanmu.Im.Basic;
using AcFunDanmu.Models.Client;
using Google.Protobuf;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
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
        private const string ACFUN_SIGN_IN_URL = "https://id.app.acfun.cn/rest/app/login/signin";
        private static readonly Uri ACFUN_SIGN_IN_URI = new Uri(ACFUN_SIGN_IN_URL);
        private const string ACFUN_SAFETY_ID_URL = "https://sec-cdn.gifshow.com/safetyid";
        private static readonly Uri ACFUN_SAFETY_ID_URI = new Uri(ACFUN_SAFETY_ID_URL);
        private const string LIVE_URL = "https://live.acfun.cn/live";
        private static readonly Uri LIVE_URI = new Uri(LIVE_URL);
        private const string LOGIN_URL = "https://id.app.acfun.cn/rest/app/visitor/login";
        private static readonly Uri LOGIN_URI = new Uri(LOGIN_URL);
        private const string GET_TOKEN_URL = "https://id.app.acfun.cn/rest/app/token/get";
        private static readonly Uri GET_TOKEN_URI = new Uri(GET_TOKEN_URL);

        private const string PLAY_URL =
            "https://api.kuaishouzt.com/rest/zt/live/web/startPlay?subBiz=mainApp&kpn=ACFUN_APP.LIVE_MATE&kpf=WINDOWS_PC&userId={0}&did={1}&{2}={3}";

        private const string GIFT_URL =
            "https://api.kuaishouzt.com/rest/zt/live/app/gift/list?subBiz=mainApp&kpn=ACFUN_APP.LIVE_MATE&kpf=WINDOWS_PC&userId={0}&did={1}&{2}={3}";

        private const string WATCHING_URL =
            "https://api.kuaishouzt.com/rest/zt/live/app/watchingList?subBiz=mainApp&kpn=ACFUN_APP.LIVE_MATE&kpf=WINDOWS_PC&userId={0}&did={1}&{2}={3}";

        private const string USER_AGENT = "kuaishou 1.9.0.200";

        private const string SAFETY_ID_CONTENT =
            "{{\"platform\":5,\"app_version\":\"2.0.32\",\"device_id\":\"null\",\"user_id\":\"{0}\"}}";

        private static readonly Dictionary<string, string> LoginForm = new Dictionary<string, string>
            { { "sid", "acfun.api.visitor" } };

        private static readonly Dictionary<string, string> GetTokenForm = new Dictionary<string, string>
            { { "sid", "acfun.midground.api" } };

        private const string SLINK_HOST = "slink.gifshow.com"; // TCP Directly
        private const int SLINK_PORT = 14000;

        #endregion

        public SignalHandler Handler { get; set; }

        #region Properties and Fields

        internal static ILogger<Client> Logger { get; private set; }

        public static readonly ConcurrentDictionary<long, GiftInfo> Gifts =
            new ConcurrentDictionary<long, GiftInfo>(12, 64);

        private static readonly CookieContainer CookieContainer = new CookieContainer();
        private static string DeviceId = new Guid().ToString("D");
        private static bool IsSignIn;

        private long UserId = -1;
        public long HostId { get; private set; }
        public string LiveId { get; private set; }
        public string Host => $"{HostId}";
        private string ServiceToken;
        private string SecurityKey;
        private string EnterRoomAttach;
        private string[] Tickets;

        private ClientRequestUtils _utils;

        private TcpClient _tcpClient;
        private NetworkStream _tcpStream;

        #endregion

        #region Constructor

        static Client()
        {
            CookieContainer.Add(new Cookie("_did", DeviceId, "/", ".acfun.cn"));
        }

        public Client(ILogger<Client> logger = null)
        {
            Logger = logger ?? new NullLogger<Client>();
        }

        public Client(long userId, string serviceToken, string securityKey, string[] tickets, string enterRoomAttach,
            string liveId, ILogger<Client> logger = null) : this(logger)
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
            Logger.LogInformation("Client signing in");
            try
            {
#if NET5_0_OR_GREATER
                using var client = CreateHttpClient(ACFUN_LOGIN_URI);

                using var login = await client.GetAsync(ACFUN_LOGIN_URI);
                if (!login.IsSuccessStatusCode)
                {
                    Logger.LogError("Get login error: {Content}", await login.Content.ReadAsStringAsync());
                    return false;
                }

                using var signinContent = new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    { "username", username },
                    { "password", password },
                    { "key", null },
                    { "captcha", null }
                });
                using var signin = await client.PostAsync(ACFUN_SIGN_IN_URI, signinContent);
                if (!signin.IsSuccessStatusCode)
                {
                    Logger.LogError("Post sign in error: {Content}", await signin.Content.ReadAsStringAsync());
                    return false;
                }

                var user = await JsonSerializer.DeserializeAsync<SignIn>(await signin.Content.ReadAsStreamAsync());
                if (user == null)
                {
                    Logger.LogError("Unable to deserialize SignIn");
                    return false;
                }

                using var sidContent = new StringContent(string.Format(SAFETY_ID_CONTENT, user.UserId));
                using var sid = await client.PostAsync(ACFUN_SAFETY_ID_URI, sidContent);
                if (!sid.IsSuccessStatusCode)
                {
                    Logger.LogError("Post safety id error: {Content}", await sid.Content.ReadAsStringAsync());
                    return false;
                }

                var safetyId = await JsonSerializer.DeserializeAsync<SafetyId>(await sid.Content.ReadAsStreamAsync());
                if (safetyId == null)
                {
                    Logger.LogError("Unable to deserialize SignIn");
                    return false;
                }

                CookieContainer.Add(new Cookie
                {
                    Domain = ".acfun.cn",
                    Name = "safety_id",
                    Value = safetyId.Id
                });

                IsSignIn = true;
#elif NETSTANDARD2_0_OR_GREATER
                using (var client = CreateHttpClient(ACFUN_LOGIN_URI))
                {
                    using (var login = await client.GetAsync(ACFUN_LOGIN_URI))
                    {
                        if (!login.IsSuccessStatusCode)
                        {
                            Logger.LogError("Get login error: {Content}", await login.Content.ReadAsStringAsync());
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
                                    Logger.LogError("Post sign in error: {Content}",
                                        await signin.Content.ReadAsStringAsync());
                                    return false;
                                }

                                var user = JsonConvert.DeserializeObject<SignIn>(
                                    await signin.Content.ReadAsStringAsync());
                                if (user == null)
                                {
                                    Logger.LogError("Unable to deserialize SignIn");
                                    return false;
                                }

                                using (var sidContent =
                                       new StringContent(string.Format(SAFETY_ID_CONTENT, user.UserId)))
                                {
                                    using (var sid = await client.PostAsync(ACFUN_SAFETY_ID_URI, sidContent))
                                    {
                                        if (!sid.IsSuccessStatusCode)
                                        {
                                            Logger.LogError("Post safety id error: {Content}",
                                                await sid.Content.ReadAsStringAsync());
                                            return false;
                                        }

                                        var safetyId =
                                            JsonConvert.DeserializeObject<SafetyId>(
                                                await sid.Content.ReadAsStringAsync());
                                        if (safetyId == null)
                                        {
                                            Logger.LogError("Unable to deserialize SignIn");
                                            return false;
                                        }

                                        CookieContainer.Add(new Cookie
                                        {
                                            Domain = ".acfun.cn",
                                            Name = "safety_id",
                                            Value = safetyId.Id
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
                Logger.LogError(ex, "Login Exception");
                return await Login(username, password);
            }
            catch (TaskCanceledException ex)
            {
                Logger.LogError(ex, "Login Exception");
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

        public async Task<PlayData> Initialize(string hostId, bool refreshGiftList = false)
        {
            if (long.TryParse(hostId, out var id)) return await Initialize(id, refreshGiftList);

            Logger.LogError($"Invalid user id: {hostId}");
            return null;
        }

        public async Task<PlayData> Initialize(long hostId, bool refreshGiftList = false)
        {
            HostId = hostId;
            Logger.LogInformation("Client initializing");
            try
            {
#if NET5_0_OR_GREATER
                using var client = CreateHttpClient(LIVE_URI);

                if (IsSignIn)
                {
                    using var getContent = new FormUrlEncodedContent(GetTokenForm);
                    using var get = await client.PostAsync(GET_TOKEN_URI, getContent);
                    if (!get.IsSuccessStatusCode)
                    {
                        Logger.LogError("Get token error: {Content}", await get.Content.ReadAsStringAsync());
                        return null;
                    }

                    var token =
                        await JsonSerializer.DeserializeAsync<MidgroundToken>(await get.Content.ReadAsStreamAsync());
                    if (token == null)
                    {
                        Logger.LogError("Unable to deserialize MidgroundToken");
                        return null;
                    }

                    if (token.Result != 1) Logger.LogError("Get token error: {Message}", token.ErrorMsg);

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
                        Logger.LogError("Get token error: {Content}", await login.Content.ReadAsStringAsync());
                        return null;
                    }

                    var token =
                        await JsonSerializer.DeserializeAsync<VisitorToken>(await login.Content.ReadAsStreamAsync());
                    if (token == null)
                    {
                        Logger.LogError("Unable to deserialize VisitorToken");
                        return null;
                    }

                    if (token.Result != 0) Logger.LogError("Get token error: {Message}", token.ErrorMsg);

                    UserId = token.UserId;
                    ServiceToken = token.ServiceToken;
                    SecurityKey = token.SecurityKey;
                }

                using var form =
                    new FormUrlEncodedContent(new Dictionary<string, string>
                        { { "authorId", $"{hostId}" }, { "pullStreamType", "FLV" } });
                using var play =
                    await client.PostAsync(
                        string.Format(PLAY_URL, UserId, DeviceId, IsSignIn ? MIDGROUND_ST : VISITOR_ST, ServiceToken),
                        form);

                if (!play.IsSuccessStatusCode)
                {
                    Logger.LogError("Get play info error: {Content}", await play.Content.ReadAsStringAsync());
                    return null;
                }

                var playData = await JsonSerializer.DeserializeAsync<Play>(await play.Content.ReadAsStreamAsync());
                if (playData == null)
                {
                    Logger.LogError("Unable to deserialize Play");
                    return null;
                }

                if (playData.Result > 1)
                {
                    Logger.LogError(playData.ErrorMsg);
                    return null;
                }

                Tickets = playData.Data?.AvailableTickets ?? Array.Empty<string>();
                EnterRoomAttach = playData.Data?.EnterRoomAttach;
                LiveId = playData.Data?.LiveId;

                if (Gifts.Count == 0 || refreshGiftList) UpdateGiftList();

                Logger.LogInformation("Client initialized");

                return playData.Data;
#elif NETSTANDARD2_0_OR_GREATER
                using (var client = CreateHttpClient(LIVE_URI))
                {
                    if (IsSignIn)
                        using (var getContent = new FormUrlEncodedContent(GetTokenForm))
                        {
                            using (var get = await client.PostAsync(GET_TOKEN_URI, getContent))
                            {
                                if (!get.IsSuccessStatusCode)
                                {
                                    Logger.LogError("Get token error: {Content}",
                                        await get.Content.ReadAsStringAsync());
                                    return null;
                                }

                                var token = JsonConvert.DeserializeObject<MidgroundToken>(
                                    await get.Content.ReadAsStringAsync());
                                if (token == null)
                                {
                                    Logger.LogError("Unable to deserialize MidgroundToken");
                                    return null;
                                }

                                UserId = token.UserId;
                                ServiceToken = token.ServiceToken;
                                SecurityKey = token.SecurityKey;
                            }
                        }
                    else
                        using (var loginContent = new FormUrlEncodedContent(LoginForm))
                        {
                            using (var login = await client.PostAsync(LOGIN_URI, loginContent))
                            {
                                if (!login.IsSuccessStatusCode)
                                {
                                    Logger.LogError("Get token error: {Content}",
                                        await login.Content.ReadAsStringAsync());
                                    return null;
                                }

                                var token = JsonConvert.DeserializeObject<VisitorToken>(
                                    await login.Content.ReadAsStringAsync());
                                if (token == null)
                                {
                                    Logger.LogError("Unable to deserialize VisitorToken");
                                    return null;
                                }

                                UserId = token.UserId;
                                ServiceToken = token.ServiceToken;
                                SecurityKey = token.SecurityKey;
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
                                Logger.LogError("Get play info error: {Content}",
                                    await play.Content.ReadAsStringAsync());
                                return null;
                            }

                            var playData = JsonConvert.DeserializeObject<Play>(await play.Content.ReadAsStringAsync());
                            if (playData == null)
                            {
                                Logger.LogError("Unable to deserialize Play");
                                return null;
                            }

                            if (playData.Result > 1)
                            {
                                Logger.LogError(playData.ErrorMsg);
                                return null;
                            }

                            Tickets = playData.Data?.AvailableTickets ?? Array.Empty<string>();
                            EnterRoomAttach = playData.Data?.EnterRoomAttach;
                            LiveId = playData.Data?.LiveId;

                            if (Gifts.Count == 0 || refreshGiftList) UpdateGiftList();

                            Logger.LogInformation("Client initialized");

                            return playData.Data;
                        }
                    }
                }
#endif
            }
            catch (HttpRequestException ex)
            {
                Logger.LogError(ex, "Initialize exception");
                return await Initialize(hostId);
            }
            catch (TaskCanceledException ex)
            {
                Logger.LogError(ex, "Initialize exception");
                return await Initialize(hostId);
            }
        }

        private async void UpdateGiftList()
        {
            if (string.IsNullOrEmpty(SecurityKey)) return;

            try
            {
#if NET5_0_OR_GREATER
                using var client = CreateHttpClient(LIVE_URI);

                var sign = Sign(GIFT_ALL, SecurityKey);

                using var gift = await client.PostAsync($"{KUAISHOU_ZT}{GIFT_ALL}?{Query}&__clientSign={sign}", null);

                if (!gift.IsSuccessStatusCode) return;
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
#elif NETSTANDARD2_0_OR_GREATER
                using (var client = CreateHttpClient(LIVE_URI))
                {
                    var sign = Sign(GIFT_ALL, SecurityKey);

                    using (var gift = await client.PostAsync($"{KUAISHOU_ZT}{GIFT_ALL}?{Query}&__clientSign={sign}",
                               null))
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
#endif
            }
            catch (HttpRequestException ex)
            {
                Logger.LogError(ex, "Update gift list exception");
                UpdateGiftList();
            }
            catch (TaskCanceledException ex)
            {
                Logger.LogError(ex, "Update gift list exception");
                UpdateGiftList();
            }
        }

        public async Task<WatchingUser[]> WatchingList()
        {
            if (UserId == -1 || string.IsNullOrEmpty(ServiceToken) || string.IsNullOrEmpty(LiveId))
                return Array.Empty<WatchingUser>();

            try
            {
#if NET5_0_OR_GREATER
                using var client = CreateHttpClient(LIVE_URL);

                using var watchingContent = new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    { "visitorId", $"{UserId}" },
                    { "liveId", LiveId }
                });
                using var watching =
                    await client.PostAsync(
                        string.Format(WATCHING_URL, UserId, DeviceId, IsSignIn ? MIDGROUND_ST : VISITOR_ST,
                            ServiceToken), watchingContent);
                if (!watching.IsSuccessStatusCode) return Array.Empty<WatchingUser>();

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
                            if (!watching.IsSuccessStatusCode) return Array.Empty<WatchingUser>();

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
                Logger.LogError(ex, "Watching list exception");
                return await WatchingList();
            }
            catch (TaskCanceledException ex)
            {
                Logger.LogError(ex, "Watching list exception");
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
                Logger.LogInformation("Not initialized or live is ended");
                return false;
            }

            using var owner = MemoryPool<byte>.Shared.Rent();

            if (_utils != null) _utils = null;

            _utils =
                new ClientRequestUtils(UserId, DeviceId, ServiceToken, SecurityKey, LiveId, EnterRoomAttach, Tickets);
            if (_tcpClient != null)
            {
                if (_tcpStream != null)
                {
                    _tcpStream.Close();
                    await _tcpStream.DisposeAsync();
                    _tcpStream = null;
                }

                _tcpClient.Close();
                _tcpClient.Dispose();
                _tcpClient = null;
                GC.Collect();
            }

            _tcpClient = CreateTcpClient();

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
                _tcpStream = _tcpClient.GetStream();

                await _tcpStream.WriteAsync(_utils.HandshakeRequest());

                #region Main loop

                while (_tcpClient.Connected)
                {
                    var buffer = owner.Memory;

                    try
                    {
                        var _ = await _tcpStream.ReadAsync(buffer);
                        var downstream = Decode<DownstreamPayload>(buffer.Span, SecurityKey, _utils.SessionKey,
                            out var header);

                        if (downstream == null)
                        {
                            Logger.LogError("Downstream is null: {Content}", Convert.ToBase64String(buffer.Span));
                            continue;
                        }

                        HandleCommand(header, downstream, heartbeatTimer, deathTimer);
                    }
                    catch (Exception ex)
                    {
                        Logger.LogDebug(ex, "Main");
                        heartbeatTimer.Stop();
                        break;
                    }
                }

                Logger.LogDebug("Client disconnected");
                heartbeatTimer.Stop();
                deathTimer.Stop();

                #endregion
            }
            catch (Exception ex)
            {
                Logger.LogDebug(ex, "Start");
            }

            return true;
        }
#elif NETSTANDARD2_0_OR_GREATER
        public async Task<bool> Start()
        {
            if (UserId == -1 || string.IsNullOrEmpty(ServiceToken) || string.IsNullOrEmpty(SecurityKey) ||
                string.IsNullOrEmpty(LiveId) || string.IsNullOrEmpty(EnterRoomAttach) || Tickets == null ||
                Tickets.Length == 0)
            {
                Logger.LogInformation("Not initialized or live is ended");
                return false;
            }

            var owner = ArrayPool<byte>.Shared;

            if (_utils != null) _utils = null;

            _utils = new ClientRequestUtils(UserId, DeviceId, ServiceToken, SecurityKey, LiveId, EnterRoomAttach,
                Tickets);
            if (_tcpClient != null)
            {
                if (_tcpStream != null)
                {
                    _tcpStream.Close();
                    _tcpStream.Dispose();
                    _tcpStream = null;
                }

                _tcpClient.Close();
                _tcpClient.Dispose();
                _tcpClient = null;
                GC.Collect();
            }

            _tcpClient = CreateTcpClient();

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
                    _tcpStream = _tcpClient.GetStream();

                    var handshake = _utils.HandshakeRequest();
                    await _tcpStream.WriteAsync(handshake, 0, handshake.Length);

                    #region Main loop

                    while (_tcpClient.Connected)
                    {
                        var buffer = owner.Rent(1024 * 1024);

                        try
                        {
                            var _ = await _tcpStream.ReadAsync(buffer, 0, 1024 * 1024);
                            var downstream = Decode<DownstreamPayload>(buffer, SecurityKey, _utils.SessionKey,
                                out var header);

                            if (downstream == null)
                            {
                                Logger.LogError("Downstream is null: {Content}", Convert.ToBase64String(buffer));
                                continue;
                            }

                            HandleCommand(header, downstream, heartbeatTimer, deathTimer);
                        }
                        catch (Exception ex)
                        {
                            Logger.LogDebug(ex, "Main");
                            heartbeatTimer.Stop();
                            break;
                        }

                        owner.Return(buffer);
                    }

                    Logger.LogDebug("Client disconnected");
                    heartbeatTimer.Stop();
                    deathTimer.Stop();

                    #endregion
                }
                catch (Exception ex)
                {
                    Logger.LogDebug(ex, "Start");
                }

                return true;
            }
        }
#endif

        public async Task Stop(string message)
        {
            try
            {
                if (_tcpClient != null && _tcpClient.Connected)
                {
#if NET5_0_OR_GREATER
                    await _tcpStream.WriteAsync(_utils.UserExitRequest());
                    await _tcpStream.WriteAsync(_utils.UnregisterRequest());
#elif NETSTANDARD2_0_OR_GREATER
                    var exit = _utils.UserExitRequest();
                    var unregister = _utils.UnregisterRequest();
                    await _tcpStream.WriteAsync(exit, 0, exit.Length);
                    await _tcpStream.WriteAsync(unregister, 0, unregister.Length);
#endif
                }
            }
            catch (Exception ex)
            {
                Logger.LogDebug(ex, "Stop");
            }
        }

        private async void HandleCommand(PacketHeader header, DownstreamPayload stream, HeartbeatTimer heartbeatTimer,
            HeartbeatTimer deathTimer)
        {
            Logger.LogDebug("--------");
            Logger.LogDebug("Down\t\t {HeaderSeqId}, {SeqId}, {Command}", header.SeqId, stream.SeqId, stream.Command);
            Logger.LogDebug("Header: {Header}", header);
            Logger.LogDebug("Payload: {Payload}", stream);
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
                case Command.HANDSHAKE:
#if NET5_0_OR_GREATER
                    await _tcpStream.WriteAsync(_utils.RegisterRequest());
#elif NETSTANDARD2_0_OR_GREATER
                    var register = _utils.RegisterRequest();
                    await _tcpStream.WriteAsync(register, 0, register.Length);
#endif
                    break;
                case Command.REGISTER:
                    await HandleRegister(header.AppId, stream);
                    break;
                case Command.UNREGISTER:
                    HandleUnregister(stream);
                    break;
                case Command.PUSH_MESSAGE:
                    await HandlePushMessage(header, stream, heartbeatTimer, deathTimer);
                    break;
                case ImEnums.PUSH_MESSAGE:
                    break;
                default:
                    if (stream.ErrorCode > 0)
                    {
                        Logger.LogWarning("Error： {ErrorCode} - {ErrorMsg}", stream.ErrorCode, stream.ErrorMsg);
                        if (stream.ErrorCode == 10018) await Stop("Log out");

                        Logger.LogDebug("Error Data: {Data}", stream.ErrorData.ToBase64());
                    }
                    else
                    {
                        Logger.LogInformation("Unhandled DownstreamPayload Command: {Command}",
                            stream.Command ?? "Empty");
                        Logger.LogDebug("Command Data: {Data}", stream.ToByteString().ToBase64());
                    }

                    break;
            }

            Logger.LogDebug("--------");
        }

        private static void HandleGlobalCommand(DownstreamPayload payload, HeartbeatTimer heartbeatTimer)
        {
            var cmd = ZtLiveCsCmdAck.Parser.ParseFrom(payload.PayloadData);
            Logger.LogDebug("\t{Command}", cmd);
            switch (cmd.CmdAckType)
            {
                case GlobalCommand.ENTER_ROOM_ACK:
                    var enterRoom = ZtLiveCsEnterRoomAck.Parser.ParseFrom(cmd.Payload);
                    heartbeatTimer.Interval = enterRoom.HeartbeatIntervalMs > 0
                        ? enterRoom.HeartbeatIntervalMs
                        : TimeSpan.FromSeconds(10).TotalMilliseconds;
                    heartbeatTimer.Start();
                    Logger.LogDebug("\t\t{EnterRoom}", enterRoom);
                    break;
                case GlobalCommand.HEARTBEAT_ACK:
                    var heartbeat = ZtLiveCsHeartbeatAck.Parser.ParseFrom(cmd.Payload);
                    Logger.LogDebug("\t\t{Heartbeat}", heartbeat);
                    break;
                case GlobalCommand.USER_EXIT_ACK:
                    var userexit = ZtLiveCsUserExitAck.Parser.ParseFrom(cmd.Payload);
                    Logger.LogDebug("\t\t{UserExit}", userexit);
                    break;
                default:
                    Logger.LogInformation("Unhandled Global.ZtLiveInteractive.CsCmdAck: {Type}",
                        cmd.CmdAckType ?? "Empty");
                    Logger.LogDebug("CsCmdAck Data: {Data}", payload.PayloadData.ToBase64());
                    break;
            }
        }

        private static void HandleKeepAlive(DownstreamPayload payload)
        {
            var keepalive = KeepAliveResponse.Parser.ParseFrom(payload.PayloadData);
            Logger.LogDebug("\t{KeepAlive}", keepalive);
        }

        private static void HandlePing(DownstreamPayload payload)
        {
            var ping = PingResponse.Parser.ParseFrom(payload.PayloadData);
            Logger.LogDebug("\t{Ping}", ping);
        }

        private async Task HandleRegister(int appId, DownstreamPayload payload)
        {
            var register = RegisterResponse.Parser.ParseFrom(payload.PayloadData);
            _utils.Register(appId, register.InstanceId, register.SessKey.ToBase64(),
                register.SdkOption.Lz4CompressionThresholdBytes);
            Logger.LogDebug("\t{Register}", register);
            try
            {
#if NET5_0_OR_GREATER
                await _tcpStream.WriteAsync(_utils.KeepAliveRequest());
                await _tcpStream.WriteAsync(_utils.EnterRoomRequest());
#elif NETSTANDARD2_0_OR_GREATER
                var keepAlive = _utils.KeepAliveRequest();
                var enterRoom = _utils.EnterRoomRequest();
                await _tcpStream.WriteAsync(keepAlive, 0, keepAlive.Length);
                await _tcpStream.WriteAsync(enterRoom, 0, enterRoom.Length);
#endif
            }
            catch (Exception ex)
            {
                Logger.LogDebug(ex, "Register response");
            }
        }


        private void HandleUnregister(DownstreamPayload payload)
        {
            var unregister = UnregisterResponse.Parser.ParseFrom(payload.PayloadData);
            Logger.LogDebug("\t{Unregister}", unregister);
            try
            {
                _tcpStream.Close();
                _tcpClient.Close();
            }
            catch (Exception ex)
            {
                Logger.LogDebug(ex, "Unregister response");
            }
        }

        private async Task HandlePushMessage(PacketHeader header, DownstreamPayload stream,
            HeartbeatTimer heartbeatTimer, HeartbeatTimer deathTimer)
        {
            var message = ZtLiveScMessage.Parser.ParseFrom(stream.PayloadData);
            Logger.LogDebug("\t{message}", message);
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
                    Logger.LogInformation("Unhandled Push.ZtLiveInteractive.Message: {Type}",
                        message.MessageType ?? "Empty");
                    Logger.LogDebug("CsCmdAck Data: {Data}", stream.PayloadData.ToBase64());
                    break;
            }

            try
            {
#if NET5_0_OR_GREATER
                await _tcpStream.WriteAsync(_utils.PushMessageResponse(header.SeqId));
#elif NETSTANDARD2_0_OR_GREATER
                var push = _utils.PushMessageResponse(header.SeqId);
                await _tcpStream.WriteAsync(push, 0, push.Length);
#endif
            }
            catch (Exception ex)
            {
                Logger.LogDebug(ex, "Push message response");
            }
        }


        private async Task HandleStatusChanged(ByteString payload, HeartbeatTimer heartbeatTimer)
        {
            var statusChanged = ZtLiveScStatusChanged.Parser.ParseFrom(payload);
            Logger.LogDebug("\t\t{StatusChanged}", statusChanged);
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
            Logger.LogDebug("\t\t{TicketInvalid}", ticketInvalid);
            _utils.NextTicket();
            try
            {
#if NET5_0_OR_GREATER
                await _tcpStream.WriteAsync(_utils.EnterRoomRequest());
#elif NETSTANDARD2_0_OR_GREATER
                var enterRoom = _utils.EnterRoomRequest();
                await _tcpStream.WriteAsync(enterRoom, 0, enterRoom.Length);
#endif
            }
            catch (Exception ex)
            {
                Logger.LogDebug(ex, "Ticket invalid request");
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

        private static TcpClient CreateTcpClient()
        {
            return new TcpClient(SLINK_HOST, SLINK_PORT);
        }

        private async void Heartbeat(object sender, ElapsedEventArgs e)
        {
            if (_tcpClient.Connected)
            {
                Logger.LogDebug("HEARTBEAT");
                try
                {
#if NET5_0_OR_GREATER
                    await _tcpStream.WriteAsync(_utils.HeartbeatReqeust());

                    if (_utils.HeartbeatSeqId % 5 == 4) await _tcpStream.WriteAsync(_utils.KeepAliveRequest());
#elif NETSTANDARD2_0_OR_GREATER
                    var heartbeat = _utils.HeartbeatReqeust();
                    await _tcpStream.WriteAsync(heartbeat, 0, heartbeat.Length);

                    if (_utils.HeartbeatSeqId % 5 == 4)
                    {
                        var keepAlive = _utils.KeepAliveRequest();
                        await _tcpStream.WriteAsync(keepAlive, 0, keepAlive.Length);
                    }
#endif
                }
                catch (Exception ex)
                {
                    Logger.LogDebug(ex, "Heartbeat");
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