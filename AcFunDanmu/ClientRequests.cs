using AcFunDanmu.Enums;
using Google.Protobuf;
using System;
using Serilog;
using static AcFunDanmu.ClientUtils;
using System.Threading;

namespace AcFunDanmu
{
    internal class ClientRequests
    {
        private const string AppName = "link-sdk";
        private const string SdkVersion = "1.2.1";
        private const string KPN = "ACFUN_APP";
        private const string KPF = "PC_WEB";
        private const string SubBiz = "mainApp";
        private const string ClientLiveSdkVersion = "kwai-acfun-live-link";
        private const string LinkVersion = "2.13.8";

        private readonly long UserId;
        private readonly string Did;
        private readonly string ServiceToken;
        private readonly string SecurityKey;
        private readonly string LiveId;
        private readonly string EnterRoomAttach;
        private readonly string[] Tickets;

        private int AppId = 0;
        private long InstanceId = 0;
        public string SessionKey { get; private set; }
        private long Lz4CompressionThreshold;

        private long SeqId = 1;
        private long _HeartbeatSeqId = 0;
        public long HeartbeatSeqId => _HeartbeatSeqId;
        private const uint RetryCount = 1;
        private int TicketIndex = 0;

        private string Ticket => Tickets[TicketIndex];

        public ClientRequests(long userid, string did, string servicetoken, string securitykey, string liveid, string enterroomattach, string[] tickets)
        {
            UserId = userid;
            Did = did;
            ServiceToken = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(servicetoken));
            SecurityKey = securitykey;
            LiveId = liveid;
            EnterRoomAttach = enterroomattach;
            Tickets = tickets;
        }

        public void Register(int appId, long instanceId, string sessionKey, long lz4compressionthreshold)
        {
            AppId = appId;
            InstanceId = instanceId;
            SessionKey = sessionKey;
            Lz4CompressionThreshold = lz4compressionthreshold;
        }

        public void NextTicket()
        {
            TicketIndex = TicketIndex++ % Tickets.Length;
        }

        public byte[] RegisterRequest()
        {
            var register = new RegisterRequest
            {
                AppInfo = new AppInfo
                {
                    SdkVersion = ClientLiveSdkVersion,
                    LinkVersion = LinkVersion,
                },
                DeviceInfo = new DeviceInfo
                {
                    PlatformType = DeviceInfo.Types.PlatformType.H5Windows,
                    DeviceModel = "h5",
                },
                PresenceStatus = AcFunDanmu.RegisterRequest.Types.PresenceStatus.KPresenceOnline,
                AppActiveStatus = AcFunDanmu.RegisterRequest.Types.ActiveStatus.KAppInForeground,
                InstanceId = InstanceId,
                ZtCommonInfo = new ZtCommonInfo
                {
                    Kpn = KPN,
                    Kpf = KPF,
                    Uid = UserId,
                }
            };

            var payload = GeneratePayload(Command.REGISTER, register);

            var body = payload.ToByteString();

            var header = GenerateHeader(body, PacketHeader.Types.EncryptionMode.KEncryptionServiceToken);
            header.TokenInfo = new TokenInfo
            {
                TokenType = TokenInfo.Types.TokenType.KServiceToken,
                Token = ByteString.FromBase64(ServiceToken),
            };

            Interlocked.Increment(ref SeqId);

            Log.Debug("--------");
            Log.Debug("Up\t\t {HeaderSeqId}, {SeqId}, {Command}", header.SeqId, payload.SeqId, payload.Command);
            Log.Debug("Header: {Header}", header);
            Log.Debug("Payload: {Payload}", payload);
            Log.Debug("\t{Register}", register);
            Log.Debug("--------");

            return Encode(header, body, SecurityKey);
        }

        public byte[] KeepAliveRequest()
        {
            var keepalive = new KeepAliveRequest
            {
                PresenceStatus = AcFunDanmu.RegisterRequest.Types.PresenceStatus.KPresenceOnline,
                AppActiveStatus = AcFunDanmu.RegisterRequest.Types.ActiveStatus.KAppInForeground,
            };

            var payload = GeneratePayload(Command.KEEP_ALIVE, keepalive);

            var body = payload.ToByteString();

            var header = GenerateHeader(body);

            Interlocked.Increment(ref SeqId);

            Log.Debug("--------");
            Log.Debug("Up\t\t {HeaderSeqId}, {SeqId}, {Command}", header.SeqId, payload.SeqId, payload.Command);
            Log.Debug("Header: {Header}", header);
            Log.Debug("Payload: {Payload}", payload);
            Log.Debug("\t{KeepAlive}", keepalive);
            Log.Debug("--------");

            return Encode(header, body, SessionKey);
        }

        public byte[] EnterRoomRequest()
        {
            var enterroom = new ZtLiveCsEnterRoom
            {
                EnterRoomAttach = EnterRoomAttach,
                ClientLiveSdkVersion = ClientLiveSdkVersion
            };

            var cmd = GenerateCommand(GlobalCommand.ENTER_ROOM, enterroom);

            var payload = GeneratePayload(Command.GLOBAL_COMMAND, cmd);

            var body = payload.ToByteString();

            var header = GenerateHeader(body);

            Interlocked.Increment(ref SeqId);

            Log.Debug("--------");
            Log.Debug("Up\t\t {HeaderSeqId}, {SeqId}, {Command}", header.SeqId, payload.SeqId, payload.Command);
            Log.Debug("Header: {Header}", header);
            Log.Debug("Payload: {Payload}", payload);
            Log.Debug("\t{Command}", cmd);
            Log.Debug("\t\t{EnterRoom}", enterroom);
            Log.Debug("--------");

            return Encode(header, body, SessionKey);
        }

        public byte[] PushMessageResponse(long HeaderSeqId)
        {
            var payload = GeneratePayload(Command.PUSH_MESSAGE);

            var body = payload.ToByteString();

            var header = GenerateHeader(body);
            header.SeqId = HeaderSeqId;

            Log.Debug("Up\t\t {HeaderSeqId}, {SeqId}, {Command}", header.SeqId, payload.SeqId, payload.Command);
            Log.Debug("Header: {Header}", header);
            Log.Debug("Payload: {Payload}", payload);
            Log.Debug("\t{PushMessage}");

            return Encode(header, body, SessionKey);
        }

        public byte[] HeartbeatReqeust()
        {
            var heartbeat = new ZtLiveCsHeartbeat
            {
                ClientTimestampMs = DateTimeOffset.Now.ToUnixTimeMilliseconds(),
                Sequence = _HeartbeatSeqId,
            };

            var cmd = GenerateCommand(GlobalCommand.HEARTBEAT, heartbeat);

            var payload = GeneratePayload(Command.GLOBAL_COMMAND, cmd);

            var body = payload.ToByteString();

            var header = GenerateHeader(body);

            _HeartbeatSeqId++;
            Interlocked.Increment(ref SeqId);

            Log.Debug("--------");
            Log.Debug("Up\t\t {HeaderSeqId}, {SeqId}, {Command}", header.SeqId, payload.SeqId, payload.Command);
            Log.Debug("Header: {Header}", header);
            Log.Debug("Payload: {Payload}", payload);
            Log.Debug("\t{Command}", cmd);
            Log.Debug("\t\t{Heartbeat}", heartbeat);
            Log.Debug("--------");

            return Encode(header, body, SessionKey);
        }

        public byte[] UserExitRequest()
        {
            var userexit = GenerateCommand(GlobalCommand.USER_EXIT);

            var payload = GeneratePayload(Command.GLOBAL_COMMAND, userexit);

            var body = payload.ToByteString();

            var header = GenerateHeader(body);

            Interlocked.Increment(ref SeqId);

            Log.Debug("--------");
            Log.Debug("Up\t\t {HeaderSeqId}, {SeqId}, {Command}", header.SeqId, payload.SeqId, payload.Command);
            Log.Debug("Header: {Header}", header);
            Log.Debug("Payload: {Payload}", payload);
            Log.Debug("\t{UserExit}", userexit);
            Log.Debug("--------");

            return Encode(header, body, SessionKey);
        }

        public byte[] UnregisterRequest()
        {
            var payload = GeneratePayload(Command.UNREGISTER);

            var body = payload.ToByteString();

            var header = GenerateHeader(body);

            Log.Debug("--------");
            Log.Debug("Up\t\t {HeaderSeqId}, {SeqId}, {Command}", header.SeqId, payload.SeqId, payload.Command);
            Log.Debug("Header: {Header}", header);
            Log.Debug("Payload: {Payload}", payload);
            Log.Debug("\t{Unregister}");
            Log.Debug("--------");

            return Encode(header, body, SessionKey);
        }

        public byte[] PingRequest()
        {
            var ping = new PingRequest
            {
                PingType = AcFunDanmu.PingRequest.Types.PingType.KPostRegister,
            };

            var payload = GeneratePayload(Command.PING, ping);

            var body = payload.ToByteString();

            var header = GenerateHeader(body);

            Log.Debug("--------");
            Log.Debug("Up\t\t {HeaderSeqId}, {SeqId}, {Command}", header.SeqId, payload.SeqId, payload.Command);
            Log.Debug("Header: {Header}", header);
            Log.Debug("Payload: {Payload}", payload);
            Log.Debug("\t{Ping}", ping);
            Log.Debug("--------");

            return Encode(header, body, SessionKey);
        }

        private ZtLiveCsCmd GenerateCommand(string command)
        {
            return new ZtLiveCsCmd
            {
                CmdType = command,
                Ticket = Ticket,
                LiveId = LiveId,
            };
        }

        private ZtLiveCsCmd GenerateCommand(string command, IMessage msg)
        {
            var cmd = GenerateCommand(command);
            cmd.Payload = msg.ToByteString();
            return cmd;
        }

        private UpstreamPayload GeneratePayload(string command)
        {
            return new UpstreamPayload
            {
                Command = command,
                RetryCount = RetryCount,
                SeqId = SeqId,
                SubBiz = SubBiz,
            };
        }

        private UpstreamPayload GeneratePayload(string command, IMessage msg)
        {
            var payload = GeneratePayload(command);
            payload.PayloadData = msg.ToByteString();
            return payload;
        }

        private PacketHeader GenerateHeader(ByteString body, PacketHeader.Types.EncryptionMode encryptionMode = PacketHeader.Types.EncryptionMode.KEncryptionSessionKey)
        {
            return new PacketHeader
            {
                AppId = AppId,
                Uid = UserId,
                InstanceId = InstanceId,
                DecodedPayloadLen = Convert.ToUInt32(body.Length),
                EncryptionMode = encryptionMode,
                SeqId = SeqId,
                Kpn = KPN
            };
        }
    }
}
