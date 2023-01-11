using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Threading.Tasks;
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

    public delegate void Initialize();

    public delegate void Start();

    public delegate void End();

    public partial class Client
    {
        public event SignalHandler Handler;
        public event Initialize OnInitialize;
        public event Start OnStart;
        public event End OnEnd;

        public async Task<PlayData> InitializeWithLogin(string username, string password, string uid)
        {
            await Login(username, password);
            return await Initialize(uid);
        }

        private async Task<PlayData> Initialize(string hostId)
        {
            if (long.TryParse(hostId, out var id)) return await Initialize(id);

            Logger.LogError("Invalid user id: {HostId}", hostId);
            return null;
        }

        public void Start(string hostId)
        {
            if (string.IsNullOrEmpty(hostId) || !long.TryParse(hostId, out var id))
            {
                Logger.LogError("Invalid Host id", hostId);
                return;
            }

            Start(id);
        }

        private void HandleCommand(PacketHeader header, DownstreamPayload stream, HeartbeatTimer heartbeatTimer,
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
                    HandleHandshake(stream);
                    break;
                case Command.REGISTER:
                    HandleRegister(header.AppId, stream);
                    break;
                case Command.UNREGISTER:
                    HandleUnRegister(stream);
                    break;
                case Command.PUSH_MESSAGE:
                    HandlePushMessage(header, stream, heartbeatTimer, deathTimer);
                    break;
                case ImEnums.PUSH_MESSAGE:
                    break;
                default:
                    if (stream.ErrorCode > 0)
                    {
                        Logger.LogWarning("Error： {ErrorCode} - {ErrorMsg}", stream.ErrorCode, stream.ErrorMsg);
                        if (stream.ErrorCode == 10018) Stop("Log out");

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

        private void HandleGlobalCommand(DownstreamPayload payload, HeartbeatTimer heartbeatTimer)
        {
            var cmd = ZtLiveCsCmdAck.Parser.ParseFrom(payload.PayloadData);
            Logger.LogDebug("\t{Command}", cmd);
            if (cmd.ErrorCode != 0)
            {
                Stop(cmd.ErrorMsg);
                return;
            }

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
                    var userExit = ZtLiveCsUserExitAck.Parser.ParseFrom(cmd.Payload);
                    Logger.LogDebug("\t\t{UserExit}", userExit);
                    break;
                default:
                    Logger.LogInformation("Unhandled Global.ZtLiveInteractive.CsCmdAck: {Type}",
                        cmd.CmdAckType ?? "Empty");
                    Logger.LogDebug("CsCmdAck Data: {Data}", payload.PayloadData.ToBase64());
                    break;
            }
        }

        private void HandleUnRegister(DownstreamPayload payload)
        {
            var unRegister = UnregisterResponse.Parser.ParseFrom(payload.PayloadData);
            Logger.LogDebug("\t{UnRegister}", unRegister);
            try
            {
                _tcpStream.Close();
                _tcpClient.Close();
            }
            catch (Exception ex)
            {
                Logger.LogDebug(ex, "UnRegister response");
            }
        }

        private void HandlePushMessage(PacketHeader header, DownstreamPayload stream,
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
                    HandleStatusChanged(payload, heartbeatTimer);
                    break;
                case PushMessage.TICKET_INVALID:
                    HandleTicketInvalid(payload);
                    break;
                default:
                    Logger.LogInformation("Unhandled Push.ZtLiveInteractive.Message: {Type}",
                        message.MessageType ?? "Empty");
                    Logger.LogDebug("CsCmdAck Data: {Data}", stream.PayloadData.ToBase64());
                    break;
            }

            SendPushMessageResponse(header);
        }

        private void HandleStatusChanged(ByteString payload, HeartbeatTimer heartbeatTimer)
        {
            var statusChanged = ZtLiveScStatusChanged.Parser.ParseFrom(payload);
            Logger.LogDebug("\t\t{StatusChanged}", statusChanged);
            if (statusChanged.Type != ZtLiveScStatusChanged.Types.Type.LiveClosed &&
                statusChanged.Type != ZtLiveScStatusChanged.Types.Type.LiveBanned) return;
            heartbeatTimer.Stop();
            Stop("Live closed");
        }

        private static HttpClient CreateHttpClient(string referer)
        {
            return CreateHttpClient(new Uri(referer));
        }

        private static TcpClient CreateTcpClient()
        {
            return new TcpClient(SLINK_HOST, SLINK_PORT);
        }

        #region Constants

        private const int BUFFER_SIZE = 1024 * 1024;

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

        #region Properties and Fields

        internal static ILogger<Client> Logger { get; private set; }

        public static readonly Dictionary<long, GiftInfo>
            Gifts = new Dictionary<long, GiftInfo>(128);

        private static readonly CookieContainer CookieContainer = new CookieContainer();
        private static readonly string DeviceId = new Guid().ToString("D");
        private static bool IsSignIn;

        private long UserId = -1;
        public long HostId { get; private set; }
        public string LiveId { get; private set; }
        public string Host => $"{HostId}";
        public bool IsRunning { get; private set; }
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
    }
}