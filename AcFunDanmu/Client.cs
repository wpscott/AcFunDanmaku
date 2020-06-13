using AcFunDanmu.Enums;
using AcFunDanmu.Models.Client;
using Google.Protobuf;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.WebSockets;
using System.Security.Cryptography;
using System.Text.Json;
using System.Threading.Tasks;

namespace AcFunDanmu
{
    public delegate void SignalHandler(string messageType, byte[] payload);
    public class Client
    {
        #region Constants
        public static readonly string[] Gifts = new string[] {
            "?",
            "香蕉",
            "吃瓜",
            "?",
            "牛啤",
            "手柄",
            "魔法棒",
            "好人卡",
            "星蕉雨",
            "告白",
            "666",
            "菜鸡",
            "打Call",
            "立FLAG",
            "窜天猴",
            "AC机娘",
            "猴岛",
            "快乐水",
            "?",
            "?",
            "?",
            "生日快乐",
            "六一快乐",
            "?",
            "?",
            "?",
            "?",
            "?",
            "?",
            "?",
            "?",
            "?",
        };

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
        private const int Offset = 12;
        private const int BufferSize = 1 << 16;
        private const int AppId = 13;
        private const string AppName = "link-sdk";
        private const string SdkVersion = "1.2.1";
        private const string KPN = "ACFUN_APP";
        private const string KPF = "PC_WEB";
        private const string SubBiz = "mainApp";
        private const string ClientLiveSdkVersion = "kwai-acfun-live-link";
        #endregion

        public SignalHandler Handler { get; set; }

        #region Properties and Fields
        private static readonly CookieContainer CookieContainer = new CookieContainer();
        private static string DeviceId;
        private static bool IsSignIn = false;

        private long UserId = -1;
        private string ServiceToken;
        private string SecurityKey;
        private string LiveId;
        private string EnterRoomAttach;
        private string[] Tickets;

        private ClientWebSocket client;

        private long InstanceId = 0;
        private string SessionKey;
        private long Lz4CompressionThreshold;

        private long SeqId = 1;
        private long HeaderSeqId = 1;
        private long HeartbeatSeqId = 1;
        private uint RetryCount = 1;
        private int TicketIndex = 0;

        private bool PrintHeader = false;
        #endregion

        #region Constructor
        public Client()
        {
            client = new ClientWebSocket();
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

        public Client(long userId, string serviceToken, string securityKey, string[] tickets, string enterRoomAttach, string liveId, string sessionKey) : this(userId, serviceToken, securityKey, tickets, enterRoomAttach, liveId)
        {
            SessionKey = sessionKey;
            PrintHeader = true;
        }
        #endregion

        public async ValueTask<bool> Login(string username, string password)
        {
            if (!IsSignIn)
            {
#if DEBUG
                Console.WriteLine("Client signing in");
#endif
                using var client = new HttpClient(
                    new HttpClientHandler
                    {
                        AutomaticDecompression = DecompressionMethods.All,
                        UseCookies = true,
                        CookieContainer = CookieContainer
                    }
                );
                client.DefaultRequestHeaders.UserAgent.ParseAdd(UserAgent);
                client.DefaultRequestHeaders.AcceptEncoding.ParseAdd(ACCEPTED_ENCODING);

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
            return true;
        }

        public async ValueTask<Play.PlayData> InitializeWithLogin(string username, string password, string uid)
        {
            await Login(username, password);
            return await Initialize(uid);
        }

        public async Task<Play.PlayData> Initialize(string uid)
        {
            Console.WriteLine("Client initializing");

            using var client = new HttpClient(
                new HttpClientHandler
                {
                    AutomaticDecompression = DecompressionMethods.All,
                    UseCookies = true,
                    CookieContainer = CookieContainer
                }
            );
            client.DefaultRequestHeaders.UserAgent.ParseAdd(UserAgent);
            client.DefaultRequestHeaders.AcceptEncoding.ParseAdd(ACCEPTED_ENCODING);

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

        private async void UpdateGiftList()
        {
            using var client = new HttpClient(
               new HttpClientHandler
               {
                   AutomaticDecompression = DecompressionMethods.All,
                   UseCookies = true,
                   CookieContainer = CookieContainer
               }
            );
            client.DefaultRequestHeaders.UserAgent.ParseAdd(UserAgent);
            client.DefaultRequestHeaders.AcceptEncoding.ParseAdd(ACCEPTED_ENCODING);

            using var giftContent = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                {"visitorId", $"{UserId}" },
                {"liveId", LiveId }
            });
            using var gift = await client.PostAsync(string.Format(GIFT_URL, UserId, DeviceId, IsSignIn ? MIDGROUND_ST : VISITOR_ST, ServiceToken), giftContent);
            var giftList = await JsonSerializer.DeserializeAsync<GiftList>(await gift.Content.ReadAsStreamAsync());
            foreach (var item in giftList.data.giftList)
            {
                Gifts[item.giftId] = item.giftName;
            }

            Console.WriteLine("Gift list updated");
        }

        public async Task<WatchingList.WatchingData.User[]> WatchingList()
        {
            if (UserId == -1 || string.IsNullOrEmpty(ServiceToken) || string.IsNullOrEmpty(SecurityKey) || string.IsNullOrEmpty(LiveId) || string.IsNullOrEmpty(EnterRoomAttach) || Tickets == null)
            {
                return Array.Empty<WatchingList.WatchingData.User>();
            }
            using var client = new HttpClient(
               new HttpClientHandler
               {
                   AutomaticDecompression = DecompressionMethods.All,
                   UseCookies = true,
                   CookieContainer = CookieContainer
               }
            );
            client.DefaultRequestHeaders.UserAgent.ParseAdd(UserAgent);
            client.DefaultRequestHeaders.AcceptEncoding.ParseAdd(ACCEPTED_ENCODING);

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

        public async Task<bool> Start()
        {
            if (UserId == -1 || string.IsNullOrEmpty(ServiceToken) || string.IsNullOrEmpty(SecurityKey) || string.IsNullOrEmpty(LiveId) || string.IsNullOrEmpty(EnterRoomAttach) || Tickets == null)
            {
                Console.WriteLine("Not initialized or live is ended");
                return false;
            }
            using var ws = new ClientWebSocket();
            client = ws;
            try
            {
                await client.ConnectAsync(Host, default);
                if (client.State == WebSocketState.Open)
                {
                    #region Register & Enter Room
                    //Register
                    await client.SendAsync(Register(), WebSocketMessageType.Binary, true, default);
                    var resp = new byte[BufferSize];
                    await client.ReceiveAsync(resp, default);
                    var registerDown = Decode(resp);
                    var regResp = RegisterResponse.Parser.ParseFrom(registerDown.PayloadData);
                    InstanceId = regResp.InstanceId;
                    SessionKey = regResp.SessKey.ToBase64();
                    Lz4CompressionThreshold = regResp.SdkOption.Lz4CompressionThresholdBytes;

                    //Ping
                    //await client.SendAsync(Ping(), WebSocketMessageType.Binary, true, default);

                    //Keep Alive
                    await client.SendAsync(KeepAlive(), WebSocketMessageType.Binary, true, default);
                    SeqId++;
                    HeaderSeqId++;

                    //Enter room
                    await client.SendAsync(EnterRoom(), WebSocketMessageType.Binary, true, default);
                    #endregion

                    #region Timers
                    using var heartbeatTimer = new System.Timers.Timer();
                    heartbeatTimer.Elapsed += async (s, e) =>
                    {
                        if (client.State == WebSocketState.Open)
                        {
                            try
                            {
                                await client.SendAsync(
                                    Heartbeat(),
                                    WebSocketMessageType.Binary,
                                    true,
                                    default
                                );

                                await client.SendAsync(KeepAlive(), WebSocketMessageType.Binary, true, default);
                            }
                            catch (WebSocketException ex)
                            {
                                Console.WriteLine("Heartbeat - WebSocket Exception: {0}", ex);
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
                    while (client.State == WebSocketState.Open)
                    {
                        try
                        {
                            var buffer = new byte[BufferSize];
                            await client.ReceiveAsync(buffer, default);

                            var stream = Decode(buffer);

                            HandleCommand(stream, heartbeatTimer);

                        }
                        catch (WebSocketException e)
                        {
                            Console.WriteLine("Main - WebSocket Exception: {0}", e.Message);
                            break;
                        }
                    }
                    #endregion
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine("Start - HttpRequestException: {0}", ex.Message);
            }
            catch (WebSocketException ex)
            {
                Console.WriteLine("Start - WebSocketException: {0}", ex.Message);
            }
            return client.State == WebSocketState.Closed;
        }

        public async Task Stop(string message)
        {
            await client.SendAsync(Unregister(), WebSocketMessageType.Binary, true, default);
            try
            {
                await client.CloseAsync(WebSocketCloseStatus.NormalClosure, message, default);
            }
            catch (WebSocketException ex)
            {
                Console.WriteLine("WebSocketException: {0}", ex.Message);
            }
        }

        async void HandleCommand(DownstreamPayload stream, System.Timers.Timer heartbeatTimer)
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
                        default:
                            Console.WriteLine("Unhandled Global.ZtLiveInteractive.CsCmd: {0}", cmd.CmdAckType);
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
                    await client.CloseAsync(WebSocketCloseStatus.NormalClosure, "Unregister", default);
                    break;
                case Command.PUSH_MESSAGE:
                    if (client.State == WebSocketState.Open)
                    {
                        await client.SendAsync(PushMessage(), WebSocketMessageType.Binary, true, default);
                    }
                    ZtLiveScMessage message = ZtLiveScMessage.Parser.ParseFrom(stream.PayloadData);

                    var payload = message.CompressionType == ZtLiveScMessage.Types.CompressionType.Gzip ? Decompress(message.Payload) : message.Payload;

                    switch (message.MessageType)
                    {
                        case Enums.PushMessage.ACTION_SIGNAL:
                            // Handled by user
                            Handler?.Invoke(message.MessageType, payload.ToByteArray());
                            break;
                        case Enums.PushMessage.STATE_SIGNAL:
                            // Handled by user
                            Handler?.Invoke(message.MessageType, payload.ToByteArray());
                            break;
                        case Enums.PushMessage.STATUS_CHANGED:
                            var statusChanged = ZtLiveScStatusChanged.Parser.ParseFrom(payload);
                            if (statusChanged.Type == ZtLiveScStatusChanged.Types.Type.LiveClosed || statusChanged.Type == ZtLiveScStatusChanged.Types.Type.LiveBanned)
                            {
                                await Stop("Live closed");
                            }
                            break;
                        case Enums.PushMessage.TICKET_INVALID:
                            var ticketInvalid = ZtLiveScTicketInvalid.Parser.ParseFrom(payload);
                            TicketIndex = (TicketIndex + 1) % Tickets.Length;
                            await client.SendAsync(EnterRoom(), WebSocketMessageType.Binary, true, default);
                            break;
                    }
                    break;
                default:
                    if (stream.ErrorCode > 0)
                    {
                        Console.WriteLine("Error： {0} - {1}", stream.ErrorCode, stream.ErrorMsg);
                        Console.WriteLine(stream.ErrorData.ToBase64());
                    }
                    else
                    {
                        Console.WriteLine("Unhandled DownstreamPayload command: {0}", stream.Command);
                        Console.WriteLine(stream);
                    }
                    break;
            }
        }

        #region Common Functions
        byte[] Register()
        {
            var request = new RegisterRequest
            {
                AppInfo = new AppInfo
                {
                    AppName = AppName,
                    SdkVersion = SdkVersion,
                },
                DeviceInfo = new DeviceInfo
                {
                    PlatformType = DeviceInfo.Types.PlatformType.H5,
                    DeviceModel = "h5",
                },
                PresenceStatus = RegisterRequest.Types.PresenceStatus.KPresenceOnline,
                AppActiveStatus = RegisterRequest.Types.ActiveStatus.KAppInForeground,
                InstanceId = InstanceId,
                ZtCommonInfo = new ZtCommonInfo
                {
                    Kpn = KPN,
                    Kpf = KPF,
                    Uid = UserId,
                }
            };

            var payload = new UpstreamPayload
            {
                Command = Command.REGISTER,
                SeqId = SeqId++,
                RetryCount = RetryCount,
                PayloadData = request.ToByteString(),
                SubBiz = SubBiz,
            };

            var body = payload.ToByteString();

            var header = new PacketHeader
            {
                AppId = AppId,
                Uid = UserId,
                EncryptionMode = PacketHeader.Types.EncryptionMode.KEncryptionServiceToken,
                DecodedPayloadLen = body.Length,
                TokenInfo = new TokenInfo
                {
                    TokenType = TokenInfo.Types.TokenType.KServiceToken,
                    Token = ByteString.CopyFromUtf8(ServiceToken),
                },
                SeqId = HeaderSeqId++,
                Kpn = KPN,
            };

            return Encode(header, body);
        }

        byte[] Unregister()
        {
            var unregister = new UnregisterRequest();

            var payload = new UpstreamPayload
            {
                Command = Command.UNREGISTER,
                RetryCount = RetryCount,
                PayloadData = unregister.ToByteString(),
                SubBiz = SubBiz
            };

            var body = payload.ToByteString();

            var header = new PacketHeader
            {
                AppId = AppId,
                Uid = UserId,
                EncryptionMode = PacketHeader.Types.EncryptionMode.KEncryptionSessionKey,
                DecodedPayloadLen = body.Length,
                Kpn = KPN
            };

            return Encode(header, body);
        }

        byte[] Ping()
        {
            var ping = new PingRequest
            {
                PingType = PingRequest.Types.PingType.KPostRegister,
            };

            var payload = new UpstreamPayload
            {
                Command = Command.PING,
                SeqId = SeqId,
                RetryCount = RetryCount,
                PayloadData = ping.ToByteString(),
                SubBiz = SubBiz
            };

            var body = payload.ToByteString();

            var header = new PacketHeader
            {
                AppId = AppId,
                Uid = UserId,
                InstanceId = InstanceId,
                DecodedPayloadLen = body.Length,
                EncryptionMode = PacketHeader.Types.EncryptionMode.KEncryptionSessionKey,
                Kpn = KPN
            };

            return Encode(header, body);
        }

        byte[] EnterRoom()
        {
            var request = new ZtLiveCsEnterRoom
            {
                EnterRoomAttach = EnterRoomAttach,
                ClientLiveSdkVersion = ClientLiveSdkVersion
            };

            var cmd = new ZtLiveCsCmd
            {
                CmdType = GlobalCommand.ENTER_ROOM,
                Payload = request.ToByteString(),
                Ticket = Tickets[TicketIndex],
                LiveId = LiveId,
            };

            var payload = new UpstreamPayload
            {
                Command = Command.GLOBAL_COMMAND,
                SeqId = SeqId++,
                RetryCount = RetryCount,
                PayloadData = cmd.ToByteString(),
                SubBiz = SubBiz,
            };

            var body = payload.ToByteString();

            var header = new PacketHeader
            {
                AppId = AppId,
                Uid = UserId,
                InstanceId = InstanceId,
                DecodedPayloadLen = body.Length,
                EncryptionMode = PacketHeader.Types.EncryptionMode.KEncryptionSessionKey,
                SeqId = HeaderSeqId++,
                Kpn = KPN
            };

            return Encode(header, body);
        }

        byte[] KeepAlive()
        {
            var keepalive = new KeepAliveRequest
            {
                PresenceStatus = RegisterRequest.Types.PresenceStatus.KPresenceOnline,
                AppActiveStatus = RegisterRequest.Types.ActiveStatus.KAppInForeground,
            };

            var payload = new UpstreamPayload
            {
                Command = Command.KEEP_ALIVE,
                SeqId = SeqId,
                RetryCount = RetryCount,
                PayloadData = keepalive.ToByteString(),
                SubBiz = SubBiz
            };

            var body = payload.ToByteString();

            var header = new PacketHeader
            {
                AppId = AppId,
                Uid = UserId,
                InstanceId = InstanceId,
                DecodedPayloadLen = body.Length,
                EncryptionMode = PacketHeader.Types.EncryptionMode.KEncryptionSessionKey,
                SeqId = SeqId,
                Kpn = KPN,
            };

            return Encode(header, body);
        }

        byte[] PushMessage()
        {
            var msg = new UpstreamPayload
            {
                Command = Command.PUSH_MESSAGE,
                SeqId = SeqId,
                RetryCount = RetryCount,
                SubBiz = SubBiz
            };

            var body = msg.ToByteString();

            var header = new PacketHeader
            {
                AppId = AppId,
                Uid = UserId,
                InstanceId = InstanceId,
                DecodedPayloadLen = body.Length,
                EncryptionMode = PacketHeader.Types.EncryptionMode.KEncryptionSessionKey,
                SeqId = HeaderSeqId,
                Kpn = KPN,
            };

            return Encode(header, body);
        }

        byte[] Heartbeat()
        {
            var hearbeat = new ZtLiveCsHeartbeat
            {
                ClientTimestampMs = DateTimeOffset.Now.ToUnixTimeMilliseconds(),
                Sequence = HeartbeatSeqId++,
            };

            var cmd = new ZtLiveCsCmd
            {
                CmdType = GlobalCommand.HEARTBEAT,
                Payload = hearbeat.ToByteString(),
                Ticket = Tickets[TicketIndex],
                LiveId = LiveId,
            };

            var payload = new UpstreamPayload
            {
                Command = Command.GLOBAL_COMMAND,
                SeqId = SeqId,
                RetryCount = RetryCount,
                PayloadData = cmd.ToByteString(),
                SubBiz = SubBiz,
            };

            var body = payload.ToByteString();

            var header = new PacketHeader
            {
                AppId = AppId,
                Uid = UserId,
                InstanceId = InstanceId,
                DecodedPayloadLen = body.Length,
                EncryptionMode = PacketHeader.Types.EncryptionMode.KEncryptionSessionKey,
                SeqId = SeqId++,
                Kpn = KPN
            };

            return Encode(header, body);
        }
        #endregion

        #region Utils
        byte[] Encode(PacketHeader header, ByteString body)
        {
            var bHeader = header.ToByteString();

            var key = header.EncryptionMode == PacketHeader.Types.EncryptionMode.KEncryptionServiceToken ? SecurityKey : SessionKey;
            var encrypt = Encrypt(key, body);

            var data = new byte[Offset + bHeader.Length + encrypt.Length];
            data[0] = 0xAB;
            data[1] = 0xCD;
            data[2] = 0x0;
            data[3] = 0x1;

            var packetLength = BitConverter.GetBytes(bHeader.Length);
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(packetLength);
            }
            Array.Copy(packetLength, 0, data, 4, packetLength.Length);

            var bodyLength = BitConverter.GetBytes(encrypt.Length);
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(bodyLength);
            }
            Array.Copy(bodyLength, 0, data, 8, bodyLength.Length);

            bHeader.CopyTo(data, Offset);

            Array.Copy(encrypt, 0, data, Offset + bHeader.Length, encrypt.Length);

            return data;
        }

#if DEBUG
        public UpstreamPayload DecodeUpstream(byte[] bytes)
        {
            var (headerLength, payloadLength) = DecodeLengths(bytes);

            PacketHeader header = DecodeHeader(bytes, headerLength, PrintHeader);

            byte[] payload;
            if (header.EncryptionMode != PacketHeader.Types.EncryptionMode.KEncryptionNone)
            {
                var key = header.EncryptionMode == PacketHeader.Types.EncryptionMode.KEncryptionServiceToken ? SecurityKey : SessionKey;

                payload = Decrypt(bytes, headerLength, payloadLength, key);
            }
            else
            {
                payload = new byte[payloadLength];
                Array.Copy(bytes, Offset + headerLength, payload, 0, payloadLength);
            }


            if (payload.Length != header.DecodedPayloadLen)
            {
#if DEBUG
                Console.WriteLine("Payload length does not match");
                Console.WriteLine(Convert.ToBase64String(payload));
#endif
                return null;
            }

            UpstreamPayload upstream = UpstreamPayload.Parser.ParseFrom(payload);

            return upstream;
        }
#endif

        public DownstreamPayload Decode(byte[] bytes)
        {
            var (headerLength, payloadLength) = DecodeLengths(bytes);

            PacketHeader header = DecodeHeader(bytes, headerLength, PrintHeader);

            byte[] payload;
            if (header.EncryptionMode == PacketHeader.Types.EncryptionMode.KEncryptionNone)
            {
                payload = new byte[payloadLength];
                Array.Copy(bytes, Offset + headerLength, payload, 0, payloadLength);
            }
            else
            {
                var key = header.EncryptionMode == PacketHeader.Types.EncryptionMode.KEncryptionServiceToken ? SecurityKey : SessionKey;

                payload = Decrypt(bytes, headerLength, payloadLength, key);
            }


            if (payload.Length != header.DecodedPayloadLen)
            {
#if DEBUG
                Console.WriteLine("Payload length does not match");
                Console.WriteLine(Convert.ToBase64String(payload));
#endif
                return null;
            }

            DownstreamPayload downstream = DownstreamPayload.Parser.ParseFrom(payload);

            if (downstream.Command == "Push.ZtLiveInteractive.Message")
            {
                HeaderSeqId = header.SeqId;
            }


            return downstream;
        }

        internal static PacketHeader DecodeHeader(byte[] bytes, int headerLength, bool print)
        {
            PacketHeader header = PacketHeader.Parser.ParseFrom(bytes, Offset, headerLength);

            if (print)
            {
                Console.WriteLine("Header SeqId: {0}", header.SeqId);
            }

            return header;
        }

        internal static (int, int) DecodeLengths(byte[] bytes)
        {
            int headerLength, payloadLength;
            if (BitConverter.IsLittleEndian)
            {
                var header = new byte[4];
                var payload = new byte[4];

                Array.Copy(bytes, 4, header, 0, 4);
                Array.Reverse(header);
                headerLength = BitConverter.ToInt32(header);

                Array.Copy(bytes, 8, payload, 0, 4);
                Array.Reverse(payload);
                payloadLength = BitConverter.ToInt32(payload);
            }
            else
            {
                headerLength = BitConverter.ToInt32(bytes, 4);

                payloadLength = BitConverter.ToInt32(bytes, 8);
            }

            return (headerLength, payloadLength);
        }

        internal static byte[] Encrypt(string key, ByteString body)
        {
            using var aes = Aes.Create();

            using var encryptor = aes.CreateEncryptor(Convert.FromBase64String(key), aes.IV);
            using var ms = new MemoryStream();
            using var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write);
            body.WriteTo(cs);
            cs.FlushFinalBlock();

            var encrypted = ms.ToArray();

            var payload = new byte[aes.IV.Length + encrypted.Length];
            Array.Copy(aes.IV, 0, payload, 0, aes.IV.Length);
            Array.Copy(encrypted, 0, payload, aes.IV.Length, encrypted.Length);

            return payload;
        }

        internal static byte[] Decrypt(byte[] bytes, int headerLength, int payloadLength, string key)
        {
            var payload = new byte[payloadLength];
            Array.Copy(bytes, Offset + headerLength, payload, 0, payloadLength);
            var IV = new byte[16];
            Array.Copy(payload, 0, IV, 0, 16);

            using var aes = Aes.Create();
            using var decryptor = aes.CreateDecryptor(Convert.FromBase64String(key), IV);
            using var ms = new MemoryStream();
            using var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Write);
            cs.Write(payload, 16, payloadLength - 16);
            cs.FlushFinalBlock();

            return ms.ToArray();
        }

        internal static ByteString Compress(ByteString payload)
        {
            return GZip(CompressionMode.Compress, payload);
        }

        public static ByteString Decompress(ByteString payload)
        {
            return GZip(CompressionMode.Decompress, payload);
        }

        internal static ByteString GZip(CompressionMode mode, ByteString payload)
        {
            using var input = new MemoryStream(payload.ToByteArray());
            using var gzip = new GZipStream(input, mode);
            using var output = new MemoryStream();

            gzip.CopyTo(output);

            output.Position = 0;

            return ByteString.FromStream(output);
        }

        public static object Parse(string type, ByteString payload)
        {
            var t = Type.GetType($"AcFunDanmu.{type}");
            if (t != null)
            {
                var pt = typeof(MessageParser<>).MakeGenericType(new Type[] { t });

                var parser = t.GetProperty("Parser", pt).GetValue(null);
                var method = pt.GetMethod("ParseFrom", new Type[] { typeof(ByteString) });

                var ack = method.Invoke(parser, new object[] { payload });
                return ack;
            }
            else
            {
                Console.WriteLine("Unhandled type: {0}", type);
                Console.WriteLine(payload.ToBase64());
                return null;
            }
        }
        #endregion
    }
}
