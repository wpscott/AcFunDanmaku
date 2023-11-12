using AcFunDanmu.Im.Basic;
using AcFunDanmu.Models.Client;
using Google.Protobuf;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System.Buffers;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Timers;
using static AcFunDanmu.ClientUtils;
using HeartbeatTimer = System.Timers.Timer;

namespace AcFunDanmu
{
    public partial class Client
    {
        public event SignalHandler? Handler;
        public event Initialize? OnInitialize;
        public event Start? OnStart;
        public event End? OnEnd;

        public async Task<bool> InitializeWithLogin(string username, string password, string uid)
        {
            await Login(username, password);
            return await Initialize(uid);
        }

        private async Task<bool> Initialize(string hostId)
        {
            if (long.TryParse(hostId, out var id)) return await Initialize(id);

            Logger.LogError("Invalid user id: {HostId}", hostId);
            return false;
        }

        private async Task<bool> Initialize(long hostId)
        {
            OnInitialize?.Invoke();
            HostId = hostId;
            Logger.LogInformation("Client initializing");
            try
            {
                using var client = CreateHttpClient(LIVE_URI);
                if (_userId == -1 || string.IsNullOrEmpty(_serviceToken) || _securityKey == null)
                {
                    if (_isSignIn)
                    {
                        using var getContent = new FormUrlEncodedContent(GetTokenForm);
                        using var get = await client.PostAsync(GET_TOKEN_URI, getContent);
                        if (!get.IsSuccessStatusCode)
                        {
                            Logger.LogError("Get token error: {Content}",
                                await get.Content.ReadAsStringAsync());
                            return false;
                        }

                        var token = await JsonSerializer.DeserializeAsync(await get.Content.ReadAsStreamAsync(),
                            ClientModelsContext.Default.MidgroundToken);
                        if (token == null)
                        {
                            Logger.LogError("Unable to deserialize MidgroundToken");
                            return false;
                        }

                        _userId = token.UserId;
                        _serviceToken = token.ServiceToken;
                        _securityKey = Encoding.UTF8.GetBytes(token.SecurityKey);
                    }
                    else
                    {
                        using var loginContent = new FormUrlEncodedContent(LoginForm);
                        using var login = await client.PostAsync(LOGIN_URI, loginContent);
                        if (!login.IsSuccessStatusCode)
                        {
                            Logger.LogError("Get token error: {Content}",
                                await login.Content.ReadAsStringAsync());
                            return false;
                        }

                        var token = await JsonSerializer.DeserializeAsync(await login.Content.ReadAsStreamAsync(),
                            ClientModelsContext.Default.VisitorToken);
                        if (token == null)
                        {
                            Logger.LogError("Unable to deserialize VisitorToken");
                            return false;
                        }

                        _userId = token.UserId;
                        _serviceToken = token.ServiceToken;
                        _securityKey = Encoding.UTF8.GetBytes(token.SecurityKey);
                    }
                }

                using var form = new FormUrlEncodedContent(new Dictionary<string, string>
                    { { "authorId", $"{hostId}" }, { "pullStreamType", "FLV" } });
                using var play = await client.PostAsync(
                    string.Format(PLAY_URL, _userId, DeviceId, _isSignIn ? MIDGROUND_ST : VISITOR_ST,
                        _serviceToken), form);
                if (!play.IsSuccessStatusCode)
                {
                    Logger.LogError("Get play info error: {Content}",
                        await play.Content.ReadAsStringAsync());
                    return false;
                }

                var playData =
                    await JsonSerializer.DeserializeAsync(await play.Content.ReadAsStreamAsync(),
                        ClientModelsContext.Default.Play);
                if (playData == null)
                {
                    Logger.LogError("Unable to deserialize Play");
                    return false;
                }

                if (playData.Result > 1)
                {
                    Logger.LogError("PlayData error message: {Message}", playData.ErrorMsg);
                    return false;
                }

                _tickets = playData.Data?.AvailableTickets ?? Array.Empty<string>();
                _enterRoomAttach = playData.Data?.EnterRoomAttach ?? string.Empty;
                LiveId = playData.Data?.LiveId ?? string.Empty;

                if (Gifts.Count == 0) UpdateGiftList();

                Logger.LogInformation("Client initialized");

                return true;
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

        public static async Task<bool> Login(string username, string password)
        {
            if (_isSignIn) return _isSignIn;
            Logger.LogInformation("Client signing in");
            try
            {
                using var client = CreateHttpClient(ACFUN_LOGIN_URI);
                using var login = await client.GetAsync(ACFUN_LOGIN_URI);
                if (!login.IsSuccessStatusCode)
                {
                    Logger.LogError("Get login error: {Content}", await login.Content.ReadAsStringAsync());
                    return false;
                }

                using var signinContent = new FormUrlEncodedContent(new Dictionary<string, string?>
                {
                    { "username", username },
                    { "password", password },
                    { "key", null },
                    { "captcha", null }
                });
                using var signin = await client.PostAsync(ACFUN_SIGN_IN_URI, signinContent);
                if (!signin.IsSuccessStatusCode)
                {
                    Logger.LogError("Post sign in error: {Content}",
                        await signin.Content.ReadAsStringAsync());
                    return false;
                }

                var user = await JsonSerializer.DeserializeAsync(await signin.Content.ReadAsStreamAsync(),
                    ClientModelsContext.Default.SignIn);
                if (user == null)
                {
                    Logger.LogError("Unable to deserialize SignIn");
                    return false;
                }

                using var sidContent =
                    new StringContent(string.Format(SAFETY_ID_CONTENT, user.UserId));
                using var sid = await client.PostAsync(ACFUN_SAFETY_ID_URI, sidContent);
                if (!sid.IsSuccessStatusCode)
                {
                    Logger.LogError("Post safety id error: {Content}",
                        await sid.Content.ReadAsStringAsync());
                    return false;
                }

                var safetyId =
                    await JsonSerializer.DeserializeAsync(await sid.Content.ReadAsStreamAsync(),
                        ClientModelsContext.Default.SafetyId);
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

                _isSignIn = true;
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

            return _isSignIn;
        }

        private async void UpdateGiftList()
        {
            if (_securityKey == null) return;

            try
            {
                using var client = CreateHttpClient(LIVE_URI);
                var sign = Sign(GiftAll, _securityKey);

                using var gift = await client.PostAsync($"{KuaishouZt}{GiftAll}?{Query}&__clientSign={sign}",
                    null);
                if (!gift.IsSuccessStatusCode) return;
                var giftList =
                    await JsonSerializer.DeserializeAsync(await gift.Content.ReadAsStreamAsync(),
                        ClientModelsContext.Default.GiftList);
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
            if (_userId == -1 || string.IsNullOrEmpty(_serviceToken) || string.IsNullOrEmpty(LiveId))
                return Array.Empty<WatchingUser>();

            try
            {
                using var client = CreateHttpClient(LIVE_URL);
                using var watchingContent = new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    { "visitorId", $"{_userId}" },
                    { "uperId", LiveId }
                });
                using var watching = await client.PostAsync(
                    string.Format(WATCHING_URL, _userId, DeviceId, _isSignIn ? MIDGROUND_ST : VISITOR_ST,
                        _serviceToken), watchingContent);
                if (!watching.IsSuccessStatusCode) return Array.Empty<WatchingUser>();

                var watchingList =
                    await JsonSerializer.DeserializeAsync(await watching.Content.ReadAsStreamAsync(),
                        ClientModelsContext.Default.WatchingList);

                return watchingList?.Data?.List ?? Array.Empty<WatchingUser>();
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

        public async void Start(long hostId)
        {
            if (string.IsNullOrEmpty(LiveId) || string.IsNullOrEmpty(_enterRoomAttach) || _tickets == null ||
                _tickets.Length == 0 ||
                HostId != hostId)
            {
                if (!await Initialize(hostId))
                {
                    Logger.LogInformation("Client initialize failed, maybe live is end");
                    return;
                }
            }

            var owner = ArrayPool<byte>.Shared;

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

            #endregion

            try
            {
                IsRunning = true;
                OnStart?.Invoke();

                _tcpStream = _tcpClient.GetStream();

                HandshakeRequest(_tcpStream);

                #region Main loop

                while (_tcpClient is { Connected: true } && _tcpStream != null)
                {
                    var buffer = owner.Rent(BUFFER_SIZE);

                    try
                    {
                        var _ = await _tcpStream.ReadAsync(buffer, 0, 1024 * 1024);

                        var downstream = Decode(DownstreamPayload.Parser, buffer, _securityKey, _sessionKey,
                            out var header);
                        owner.Return(buffer);

                        if (downstream == null)
                        {
                            Logger.LogError("Downstream is null: {Content}", Convert.ToBase64String(buffer));
                            continue;
                        }

                        HandleCommand(header, downstream, heartbeatTimer);
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

                #endregion
            }
            catch (Exception ex)
            {
                Logger.LogDebug(ex, "Start");
            }
            finally
            {
                IsRunning = false;
                OnEnd?.Invoke();
            }
        }

        public void Stop(string? reason = null)
        {
            Logger.LogInformation("Stopping client, reason: {Reason}", reason);
            try
            {
                if (_tcpClient is not { Connected: true } || _tcpStream == null) return;

                UserExitRequest(_tcpStream);
                UnRegisterRequest(_tcpStream);

                _tcpStream.Close();
                _tcpStream.Dispose();

                _tcpClient.Close();
                _tcpClient.Dispose();
            }
            catch (Exception ex)
            {
                Logger.LogDebug(ex, "Stop");
            }
            finally
            {
                _tcpStream = null;
                _tcpClient = null;
                GC.Collect();

                IsRunning = false;

                _enterRoomAttach = null;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void Heartbeat(object sender, ElapsedEventArgs e)
        {
            if (_tcpClient is { Connected: true })
            {
                Logger.LogTrace("HEARTBEAT");
                try
                {
                    HeartbeatRequest(_tcpStream);

                    if (_heartbeatSeqId % 5 == 4)
                        KeepAliveRequest(_tcpStream);
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void HandleHandshake(DownstreamPayload payload)
        {
            var handshake = HandshakeResponse.Parser.ParseFrom(payload.PayloadData);
            Logger.LogTrace("\t{HandShake}", handshake);

            RegisterRequest(_tcpStream);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void HandleKeepAlive(DownstreamPayload payload)
        {
            var keepAlive = KeepAliveResponse.Parser.ParseFrom(payload.PayloadData);
            Logger.LogTrace("\t{KeepAlive}", keepAlive);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void HandlePing(DownstreamPayload payload)
        {
            var ping = PingResponse.Parser.ParseFrom(payload.PayloadData);
            Logger.LogTrace("\t{Ping}", ping);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void HandleRegister(int appId, DownstreamPayload payload)
        {
            var register = RegisterResponse.Parser.ParseFrom(payload.PayloadData);
            Register(appId, register);
            Logger.LogTrace("\t{Register}", register);

            try
            {
                KeepAliveRequest(_tcpStream);
                EnterRoomRequest(_tcpStream);
            }
            catch (Exception ex)
            {
                Logger.LogDebug(ex, "Register response");
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void SendPushMessageResponse(PacketHeader header)
        {
            try
            {
                PushMessageResponse(_tcpStream, header.SeqId);
            }
            catch (Exception ex)
            {
                Logger.LogDebug(ex, "Push message response");
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void HandleTicketInvalid(ByteString payload)
        {
            var ticketInvalid = ZtLiveScTicketInvalid.Parser.ParseFrom(payload);
            Logger.LogTrace("\t\t{TicketInvalid}", ticketInvalid);

            NextTicket();
            try
            {
                EnterRoomRequest(_tcpStream);
            }
            catch (Exception ex)
            {
                Logger.LogDebug(ex, "Ticket invalid request");
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static HttpClient CreateHttpClient(Uri referer)
        {
            var client = new HttpClient(
                new HttpClientHandler
                {
                    AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate,
                    UseCookies = true,
                    CookieContainer = CookieContainer
                });
            client.DefaultRequestHeaders.UserAgent.ParseAdd(USER_AGENT);
            client.DefaultRequestHeaders.AcceptEncoding.ParseAdd(ACCEPTED_ENCODING);
            client.DefaultRequestHeaders.Referrer = referer;
            return client;
        }

        internal static ILogger<Client> Logger { get; private set; } = new NullLogger<Client>();

        public Client(ILogger<Client>? logger = null)
        {
            if (logger != null)
                Logger = logger;
        }

        public Client(long userId, string serviceToken, byte[]? securityKey, string[]? tickets, string? enterRoomAttach,
            string liveId, ILogger<Client>? logger = null) : this(logger)
        {
            _userId = userId;
            _serviceToken = serviceToken;
            _securityKey = securityKey;
            _tickets = tickets;
            _enterRoomAttach = enterRoomAttach;
            LiveId = liveId;
        }


        public string LiveId { get; private set; } = string.Empty;

        private string _serviceToken = string.Empty;
        private byte[]? _securityKey;
        private string? _enterRoomAttach;
        private string[]? _tickets;

        private TcpClient? _tcpClient;
        private NetworkStream? _tcpStream;
    }
}