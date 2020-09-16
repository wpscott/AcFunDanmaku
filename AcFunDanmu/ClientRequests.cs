using AcFunDanmu.Enums;
using Google.Protobuf;
using System;
using static AcFunDanmu.ClientUtils;

namespace AcFunDanmu
{
    internal class ClientRequests
    {
        private const int AppId = 13;
        private const string AppName = "link-sdk";
        private const string SdkVersion = "1.2.1";
        private const string KPN = "ACFUN_APP";
        private const string KPF = "PC_WEB";
        private const string SubBiz = "mainApp";
        private const string ClientLiveSdkVersion = "kwai-acfun-live-link";

        private readonly long UserId;
        private readonly string ServiceToken;
        private readonly string SecurityKey;
        private readonly string LiveId;
        private readonly string EnterRoomAttach;
        private readonly string[] Tickets;

        private long InstanceId = 0;
        public string SessionKey { get; private set; }
        private long Lz4CompressionThreshold;

        private long SeqId = 1;
        private long HeartbeatSeqId = 1;
        private const uint RetryCount = 1;
        private int TicketIndex = 0;

        private string Ticket => Tickets[TicketIndex];

        public ClientRequests(long userid, string servicetoken, string securitykey, string liveid, string enterroomattach, string[] tickets)
        {
            UserId = userid;
            ServiceToken = servicetoken;
            SecurityKey = securitykey;
            LiveId = liveid;
            EnterRoomAttach = enterroomattach;
            Tickets = tickets;
        }

        public void Register(long instanceid, string sessionkey, long lz4compressionthreshold)
        {
            InstanceId = instanceid;
            SessionKey = sessionkey;
            Lz4CompressionThreshold = lz4compressionthreshold;
        }

        public void NextTicket()
        {
            TicketIndex = TicketIndex++ % Tickets.Length;
        }

        internal byte[] RegisterRequest()
        {
            var register = new RegisterRequest
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

            var header = GenerateHeader(body);
            header.EncryptionMode = PacketHeader.Types.EncryptionMode.KEncryptionServiceToken;
            header.TokenInfo = new TokenInfo
            {
                TokenType = TokenInfo.Types.TokenType.KServiceToken,
                Token = ByteString.CopyFromUtf8(ServiceToken),
            };

            SeqId++;

            return Encode(header, body, SecurityKey);
        }

        internal byte[] KeepAliveRequest(bool ShouldIncrease = false)
        {
            var keepalive = new KeepAliveRequest
            {
                PresenceStatus = AcFunDanmu.RegisterRequest.Types.PresenceStatus.KPresenceOnline,
                AppActiveStatus = AcFunDanmu.RegisterRequest.Types.ActiveStatus.KAppInForeground,
            };

            var payload = GeneratePayload(Command.KEEP_ALIVE, keepalive);

            var body = payload.ToByteString();

            var header = GenerateHeader(body);

            if (ShouldIncrease)
            {
                SeqId++;
            }

            return Encode(header, body, SessionKey);
        }

        internal byte[] EnterRoomRequest()
        {
            var enteroroom = new ZtLiveCsEnterRoom
            {
                EnterRoomAttach = EnterRoomAttach,
                ClientLiveSdkVersion = ClientLiveSdkVersion
            };

            var cmd = GenerateCommand(GlobalCommand.ENTER_ROOM, enteroroom);

            var payload = GeneratePayload(Command.GLOBAL_COMMAND, cmd);

            var body = payload.ToByteString();

            var header = GenerateHeader(body);

            SeqId++;

            return Encode(header, body, SessionKey);
        }

        internal byte[] PushMessageResponse(long HeaderSeqId)
        {
            var payload = GeneratePayload(Command.PUSH_MESSAGE);

            var body = payload.ToByteString();

            var header = GenerateHeader(body);
            header.SeqId = HeaderSeqId;

            return Encode(header, body, SessionKey);
        }

        internal byte[] HeartbeatReqeust()
        {
            var hearbeat = new ZtLiveCsHeartbeat
            {
                ClientTimestampMs = DateTimeOffset.Now.ToUnixTimeMilliseconds(),
                Sequence = HeartbeatSeqId,
            };

            var cmd = GenerateCommand(GlobalCommand.HEARTBEAT, hearbeat);

            var payload = GeneratePayload(Command.GLOBAL_COMMAND, cmd);

            var body = payload.ToByteString();

            var header = GenerateHeader(body);

            HeartbeatSeqId++;
            SeqId++;

            return Encode(header, body, SessionKey);
        }

        internal byte[] UserExitRequest()
        {
            var cmd = GenerateCommand(GlobalCommand.USER_EXIT);

            var payload = GeneratePayload(Command.GLOBAL_COMMAND, cmd);

            var body = payload.ToByteString();

            var header = GenerateHeader(body);

            SeqId++;

            return Encode(header, body, SessionKey);
        }

        internal byte[] UnregisterRequest()
        {
            var payload = GeneratePayload(Command.UNREGISTER);

            var body = payload.ToByteString();

            var header = GenerateHeader(body);

            return Encode(header, body, SessionKey);
        }

        internal byte[] PingRequest()
        {
            var ping = new PingRequest
            {
                PingType = AcFunDanmu.PingRequest.Types.PingType.KPostRegister,
            };

            var payload = GeneratePayload(Command.PING, ping);

            var body = payload.ToByteString();

            var header = GenerateHeader(body);

            return Encode(header, body, SessionKey);
        }

        internal ZtLiveCsCmd GenerateCommand(string command)
        {
            return new ZtLiveCsCmd
            {
                CmdType = command,
                Ticket = Ticket,
                LiveId = LiveId,
            };
        }

        internal ZtLiveCsCmd GenerateCommand(string command, IMessage msg)
        {
            var cmd = GenerateCommand(command);
            cmd.Payload = msg.ToByteString();
            return cmd;
        }

        internal UpstreamPayload GeneratePayload(string command)
        {
            return new UpstreamPayload
            {
                Command = command,
                RetryCount = RetryCount,
                SeqId = SeqId,
                SubBiz = SubBiz,
            };
        }

        internal UpstreamPayload GeneratePayload(string command, IMessage msg)
        {
            var payload = GeneratePayload(command);
            payload.PayloadData = msg.ToByteString();
            return payload;
        }

        internal PacketHeader GenerateHeader(ByteString body)
        {
            return new PacketHeader
            {
                AppId = AppId,
                Uid = UserId,
                InstanceId = InstanceId,
                DecodedPayloadLen = Convert.ToUInt32(body.Length),
                EncryptionMode = PacketHeader.Types.EncryptionMode.KEncryptionSessionKey,
                SeqId = SeqId,
                Kpn = KPN
            };
        }
    }
}
