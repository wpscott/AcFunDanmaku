#if NETSTANDARD2_1
using System;
using System.Buffers;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Timers;
using AcFunDanmu.Im.Basic;
using AcFunDanmu.Models.Client;
using Google.Protobuf;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using static AcFunDanmu.ClientUtils;
using HeartbeatTimer = System.Timers.Timer;

namespace AcFunDanmu
{
    public partial class Client
    {
        public async Task<bool> Login(string username, string password)
        {
            if (IsSignIn) return IsSignIn;
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

        private async Task<PlayData> Initialize(long hostId)
        {
            OnInitialize?.Invoke();
            HostId = hostId;
            Logger.LogInformation("Client initializing");
            try
            {
                using var client = CreateHttpClient(LIVE_URI);
                if (IsSignIn)
                {
                    using var getContent = new FormUrlEncodedContent(GetTokenForm);
                    using var get = await client.PostAsync(GET_TOKEN_URI, getContent);
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
                else
                {
                    using var loginContent = new FormUrlEncodedContent(LoginForm);
                    using var login = await client.PostAsync(LOGIN_URI, loginContent);
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

                using var form = new FormUrlEncodedContent(new Dictionary<string, string>
                    { { "authorId", $"{hostId}" }, { "pullStreamType", "FLV" } });
                using var play = await client.PostAsync(
                    string.Format(PLAY_URL, UserId, DeviceId, IsSignIn ? MIDGROUND_ST : VISITOR_ST,
                        ServiceToken), form);
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
                    Logger.LogError("PlayData error message: {Message}", playData.ErrorMsg);
                    return null;
                }

                Tickets = playData.Data?.AvailableTickets ?? Array.Empty<string>();
                EnterRoomAttach = playData.Data?.EnterRoomAttach;
                LiveId = playData.Data?.LiveId;

                if (Gifts.Count == 0) UpdateGiftList();

                Logger.LogInformation("Client initialized");

                return playData.Data;
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
                using var client = CreateHttpClient(LIVE_URI);
                var sign = Sign(GiftAll, SecurityKey);

                using var gift = await client.PostAsync($"{KuaishouZt}{GiftAll}?{Query}&__clientSign={sign}",
                    null);
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
                using var client = CreateHttpClient(LIVE_URL);
                using var watchingContent = new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    { "visitorId", $"{UserId}" },
                    { "uperId", LiveId }
                });
                using var watching = await client.PostAsync(
                    string.Format(WATCHING_URL, UserId, DeviceId, IsSignIn ? MIDGROUND_ST : VISITOR_ST,
                        ServiceToken), watchingContent);
                if (!watching.IsSuccessStatusCode) return Array.Empty<WatchingUser>();

                var watchingList =
                    JsonConvert.DeserializeObject<WatchingList>(await watching.Content.ReadAsStringAsync());

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
            if (UserId == -1 || string.IsNullOrEmpty(ServiceToken) || string.IsNullOrEmpty(SecurityKey) ||
                string.IsNullOrEmpty(LiveId) ||
                string.IsNullOrEmpty(EnterRoomAttach) || Tickets == null ||
                Tickets.Length == 0 || HostId != hostId)
            {
                var data = await Initialize(hostId);
                if (data == null)
                {
                    Logger.LogInformation("Client initialize failed, maybe live is end");
                    return;
                }
            }

            var owner = ArrayPool<byte>.Shared;

            _utils = new ClientRequestUtils(UserId, DeviceId, ServiceToken, SecurityKey, LiveId, EnterRoomAttach,
                Tickets);
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

            using HeartbeatTimer heartbeatTimer = new HeartbeatTimer(), deathTimer = new HeartbeatTimer();
            heartbeatTimer.Elapsed += Heartbeat;
            heartbeatTimer.AutoReset = true;

            deathTimer.Interval = TimeSpan.FromSeconds(10).TotalMilliseconds;
            deathTimer.AutoReset = false;
            deathTimer.Elapsed += (s, e) => { Stop("dead"); };

            #endregion

            try
            {
                IsRunning = true;
                OnStart?.Invoke();

                _tcpStream = _tcpClient.GetStream();

                _utils.HandshakeRequest(_tcpStream);

                #region Main loop

                while (_tcpClient is { Connected: true } && _tcpStream != null)
                {
                    var buffer = owner.Rent(BUFFER_SIZE);

                    try
                    {
                        var _ = await _tcpStream.ReadAsync(buffer);

                        var downstream = Decode(DownstreamPayload.Parser, buffer, SecurityKey, _utils.SessionKey,
                            out var header);
                        owner.Return(buffer);

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
            finally
            {
                IsRunning = false;
                OnEnd?.Invoke();
            }
        }

        public async void Stop(string reason)
        {
            Logger.LogInformation("Stopping client, reason: {Reason}", reason);
            try
            {
                if (!(_tcpClient is { Connected: true }) || _tcpStream == null) return;

                _utils.UserExitRequest(_tcpStream);
                _utils.UnRegisterRequest(_tcpStream);

                _tcpStream.Close();
                await _tcpStream.DisposeAsync();

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

                UserId = -1;
            }
        }

        private void Heartbeat(object sender, ElapsedEventArgs e)
        {
            if (_tcpClient is { Connected: true })
            {
                Logger.LogDebug("HEARTBEAT");
                try
                {
                    _utils.HeartbeatRequest(_tcpStream);

                    if (_utils.HeartbeatSeqId % 5 == 4)
                        _utils.KeepAliveRequest(_tcpStream);
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

        private void HandleHandshake(DownstreamPayload payload)
        {
            var handshake = HandshakeResponse.Parser.ParseFrom(payload.PayloadData);
            Logger.LogDebug("\t{HandShake}", handshake);

            _utils.RegisterRequest(_tcpStream);
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

        private void HandleRegister(int appId, DownstreamPayload payload)
        {
            var register = RegisterResponse.Parser.ParseFrom(payload.PayloadData);
            _utils.Register(appId, register.InstanceId, register.SessKey.ToBase64(),
                register.SdkOption.Lz4CompressionThresholdBytes);
            Logger.LogDebug("\t{Register}", register);

            try
            {
                _utils.KeepAliveRequest(_tcpStream);
                _utils.EnterRoomRequest(_tcpStream);
            }
            catch (Exception ex)
            {
                Logger.LogDebug(ex, "Register response");
            }
        }

        private void SendPushMessageResponse(PacketHeader header)
        {
            try
            {
                _utils.PushMessageResponse(_tcpStream, header.SeqId);
            }
            catch (Exception ex)
            {
                Logger.LogDebug(ex, "Push message response");
            }
        }

        private void HandleTicketInvalid(ByteString payload)
        {
            var ticketInvalid = ZtLiveScTicketInvalid.Parser.ParseFrom(payload);
            Logger.LogDebug("\t\t{TicketInvalid}", ticketInvalid);

            _utils.NextTicket();
            try
            {
                _utils.EnterRoomRequest(_tcpStream);
            }
            catch (Exception ex)
            {
                Logger.LogDebug(ex, "Ticket invalid request");
            }
        }

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
    }
}
#endif