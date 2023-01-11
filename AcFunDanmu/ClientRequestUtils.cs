using System;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using AcFunDanmu.Enums;
using AcFunDanmu.Im.Basic;
using Google.Protobuf;
using Microsoft.Extensions.Logging;
using static AcFunDanmu.ClientUtils;

namespace AcFunDanmu
{
    internal class ClientRequestUtils
    {
        private const string APP_NAME = "link-sdk";
        private const string SDK_VERSION = "1.4.0.145";
        private const string KPN = "ACFUN_APP.LIVE_MATE";
        private const string KPF = "WINDOWS_PC";
        private const string SUB_BIZ = "mainApp";
        private const string CLIENT_LIVE_SDK_VERSION = "kwai-acfun-live-link";
        private const string LINK_VERSION = "2.13.8";
        private const uint RETRY_COUNT = 1;
        private readonly string _did;
        private readonly string _enterRoomAttach;
        private readonly string _liveId;
        private readonly string _securityKey;
        private readonly string _serviceToken;
        private readonly string[] _tickets;

        private readonly long _userId;

        private int _appId;
        private long _instanceId;
        private long _lz4CompressionThreshold;

        private long _seqId = 1;
        private int _ticketIndex;

        public ClientRequestUtils(long userid, string did, string serviceToken, string securityKey, string liveId,
            string enterRoomAttach, string[] tickets)
        {
            _userId = userid;
            _did = did;
            _serviceToken = Convert.ToBase64String(Encoding.UTF8.GetBytes(serviceToken));
            _securityKey = securityKey;
            _liveId = liveId;
            _enterRoomAttach = enterRoomAttach;
            _tickets = tickets;
        }

        public string SessionKey { get; private set; }
        public long HeartbeatSeqId { get; private set; }

        private string Ticket => _tickets[_ticketIndex];

        public void Register(in int appId, in long instanceId, in string sessionKey, in long lz4CompressionThreshold)
        {
            _appId = appId;
            _instanceId = instanceId;
            SessionKey = sessionKey;
            _lz4CompressionThreshold = lz4CompressionThreshold;
        }

        public void NextTicket()
        {
            _ticketIndex = _ticketIndex++ % _tickets.Length;
        }

        public void HandshakeRequest(in NetworkStream tcpStream)
        {
            var handshake = new HandshakeRequest
            {
                Unknown1 = 1,
                Unknown2 = 1
            };

            var payload = GeneratePayload(Command.HANDSHAKE, handshake);

            var header = GenerateHeader(payload.CalculateSize(),
                PacketHeader.Types.EncryptionMode.KEncryptionServiceToken);
            header.TokenInfo = new TokenInfo
            {
                TokenType = TokenInfo.Types.TokenType.KServiceToken,
                Token = ByteString.FromBase64(_serviceToken)
            };
#if DEBUG
            Client.Logger.LogDebug("--------");
            Client.Logger.LogDebug("Up\t\t {HeaderSeqId}, {SeqId}, {Command}", header.SeqId, payload.SeqId,
                payload.Command);
            Client.Logger.LogDebug("Header: {Header}", header);
            Client.Logger.LogDebug("Payload Base64: {Payload}", payload.ToByteString().ToBase64());
            Client.Logger.LogDebug("Payload: {Payload}", payload);
            Client.Logger.LogDebug("\t{Handshake}", handshake);
            Client.Logger.LogDebug("--------");
#endif
            EncodeAndSend(tcpStream, header, payload, _securityKey);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void RegisterRequest(in NetworkStream tcpStream)
        {
            var register = new RegisterRequest
            {
                AppInfo = new AppInfo
                {
                    SdkVersion = CLIENT_LIVE_SDK_VERSION,
                    LinkVersion = LINK_VERSION
                },
                DeviceInfo = new DeviceInfo
                {
                    PlatformType = DeviceInfo.Types.PlatformType.H5Windows,
                    DeviceModel = "h5"
                },
                PresenceStatus = Im.Basic.RegisterRequest.Types.PresenceStatus.KPresenceOnline,
                AppActiveStatus = Im.Basic.RegisterRequest.Types.ActiveStatus.KAppInForeground,
                InstanceId = _instanceId,
                ZtCommonInfo = new ZtCommonInfo
                {
                    Kpn = KPN,
                    Kpf = KPF,
                    Uid = _userId
                }
            };

            var payload = GeneratePayload(Command.REGISTER, register);

            var header = GenerateHeader(payload.CalculateSize(),
                PacketHeader.Types.EncryptionMode.KEncryptionServiceToken);
            header.TokenInfo = new TokenInfo
            {
                TokenType = TokenInfo.Types.TokenType.KServiceToken,
                Token = ByteString.FromBase64(_serviceToken)
            };

            Interlocked.Increment(ref _seqId);
#if DEBUG
            Client.Logger.LogDebug("--------");
            Client.Logger.LogDebug("Up\t\t {HeaderSeqId}, {SeqId}, {Command}", header.SeqId, payload.SeqId,
                payload.Command);
            Client.Logger.LogDebug("Header: {Header}", header);
            Client.Logger.LogDebug("Payload Base64: {Payload}", payload.ToByteString().ToBase64());
            Client.Logger.LogDebug("Payload: {Payload}", payload);
            Client.Logger.LogDebug("\t{Register}", register);
            Client.Logger.LogDebug("--------");
#endif
            EncodeAndSend(tcpStream, header, payload, _securityKey);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void KeepAliveRequest(in NetworkStream tcpStream)
        {
            var keepAlive = new KeepAliveRequest
            {
                PresenceStatus = Im.Basic.RegisterRequest.Types.PresenceStatus.KPresenceOnline,
                AppActiveStatus = Im.Basic.RegisterRequest.Types.ActiveStatus.KAppInForeground
            };

            var payload = GeneratePayload(Command.KEEP_ALIVE, keepAlive);

            var header = GenerateHeader(payload.CalculateSize());

            Interlocked.Increment(ref _seqId);
#if DEBUG
            Client.Logger.LogDebug("--------");
            Client.Logger.LogDebug("Up\t\t {HeaderSeqId}, {SeqId}, {Command}", header.SeqId, payload.SeqId,
                payload.Command);
            Client.Logger.LogDebug("Header: {Header}", header);
            Client.Logger.LogDebug("Payload Base64: {Payload}", payload.ToByteString().ToBase64());
            Client.Logger.LogDebug("Payload: {Payload}", payload);
            Client.Logger.LogDebug("\t{KeepAlive}", keepAlive);
            Client.Logger.LogDebug("--------");
#endif
            EncodeAndSend(tcpStream, header, payload, SessionKey);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void EnterRoomRequest(in NetworkStream tcpStream)
        {
            var enterRoom = new ZtLiveCsEnterRoom
            {
                EnterRoomAttach = _enterRoomAttach,
                ClientLiveSdkVersion = CLIENT_LIVE_SDK_VERSION
            };

            var cmd = GenerateCommand(GlobalCommand.ENTER_ROOM, enterRoom);

            var payload = GeneratePayload(Command.GLOBAL_COMMAND, cmd);

            var header = GenerateHeader(payload.CalculateSize());

            Interlocked.Increment(ref _seqId);
#if DEBUG
            Client.Logger.LogDebug("--------");
            Client.Logger.LogDebug("Up\t\t {HeaderSeqId}, {SeqId}, {Command}", header.SeqId, payload.SeqId,
                payload.Command);
            Client.Logger.LogDebug("Header: {Header}", header);
            Client.Logger.LogDebug("Payload Base64: {Payload}", payload.ToByteString().ToBase64());
            Client.Logger.LogDebug("Payload: {Payload}", payload);
            Client.Logger.LogDebug("\t{Command}", cmd);
            Client.Logger.LogDebug("\t\t{EnterRoom}", enterRoom);
            Client.Logger.LogDebug("--------");
#endif
            EncodeAndSend(tcpStream, header, payload, SessionKey);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void PushMessageResponse(in NetworkStream tcpStream, in long headerSeqId)
        {
            var payload = GeneratePayload(Command.PUSH_MESSAGE);

            var header = GenerateHeader(payload.CalculateSize());
            header.SeqId = headerSeqId;
#if DEBUG
            Client.Logger.LogDebug("Up\t\t {HeaderSeqId}, {SeqId}, {Command}", header.SeqId, payload.SeqId,
                payload.Command);
            Client.Logger.LogDebug("Header: {Header}", header);
            Client.Logger.LogDebug("Payload Base64: {Payload}", payload.ToByteString().ToBase64());
            Client.Logger.LogDebug("Payload: {Payload}", payload);
            Client.Logger.LogDebug("\t{PushMessage}", "Empty");
#endif
            EncodeAndSend(tcpStream, header, payload, SessionKey);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HeartbeatRequest(in NetworkStream tcpStream)
        {
            var heartbeat = new ZtLiveCsHeartbeat
            {
                ClientTimestampMs = DateTimeOffset.Now.ToUnixTimeMilliseconds(),
                Sequence = HeartbeatSeqId
            };

            var cmd = GenerateCommand(GlobalCommand.HEARTBEAT, heartbeat);

            var payload = GeneratePayload(Command.GLOBAL_COMMAND, cmd);

            var header = GenerateHeader(payload.CalculateSize());

            HeartbeatSeqId++;
            Interlocked.Increment(ref _seqId);
#if DEBUG
            Client.Logger.LogDebug("--------");
            Client.Logger.LogDebug("Up\t\t {HeaderSeqId}, {SeqId}, {Command}", header.SeqId, payload.SeqId,
                payload.Command);
            Client.Logger.LogDebug("Header: {Header}", header);
            Client.Logger.LogDebug("Payload Base64: {Payload}", payload.ToByteString().ToBase64());
            Client.Logger.LogDebug("Payload: {Payload}", payload);
            Client.Logger.LogDebug("\t{Command}", cmd);
            Client.Logger.LogDebug("\t\t{Heartbeat}", heartbeat);
            Client.Logger.LogDebug("--------");
#endif
            EncodeAndSend(tcpStream, header, payload, SessionKey);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void UserExitRequest(in NetworkStream tcpStream)
        {
            var userExit = GenerateCommand(GlobalCommand.USER_EXIT);

            var payload = GeneratePayload(Command.GLOBAL_COMMAND, userExit);

            var header = GenerateHeader(payload.CalculateSize());

            Interlocked.Increment(ref _seqId);
#if DEBUG
            Client.Logger.LogDebug("--------");
            Client.Logger.LogDebug("Up\t\t {HeaderSeqId}, {SeqId}, {Command}", header.SeqId, payload.SeqId,
                payload.Command);
            Client.Logger.LogDebug("Header: {Header}", header);
            Client.Logger.LogDebug("Payload Base64: {Payload}", payload.ToByteString().ToBase64());
            Client.Logger.LogDebug("Payload: {Payload}", payload);
            Client.Logger.LogDebug("\t{UserExit}", userExit);
            Client.Logger.LogDebug("--------");
#endif
            EncodeAndSend(tcpStream, header, payload, SessionKey);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void UnRegisterRequest(in NetworkStream tcpStream)
        {
            var payload = GeneratePayload(Command.UNREGISTER);

            var header = GenerateHeader(payload.CalculateSize());
#if DEBUG
            Client.Logger.LogDebug("--------");
            Client.Logger.LogDebug("Up\t\t {HeaderSeqId}, {SeqId}, {Command}", header.SeqId, payload.SeqId,
                payload.Command);
            Client.Logger.LogDebug("Header: {Header}", header);
            Client.Logger.LogDebug("Payload Base64: {Payload}", payload.ToByteString().ToBase64());
            Client.Logger.LogDebug("Payload: {Payload}", payload);
            Client.Logger.LogDebug("\t{UnRegister}", "Empty");
            Client.Logger.LogDebug("--------");
#endif
            EncodeAndSend(tcpStream, header, payload, SessionKey);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void PingRequest(in NetworkStream tcpStream)
        {
            var ping = new PingRequest
            {
                PingType = Im.Basic.PingRequest.Types.PingType.KPostRegister
            };

            var payload = GeneratePayload(Command.PING, ping);

            var header = GenerateHeader(payload.CalculateSize());
#if DEBUG
            Client.Logger.LogDebug("--------");
            Client.Logger.LogDebug("Up\t\t {HeaderSeqId}, {SeqId}, {Command}", header.SeqId, payload.SeqId,
                payload.Command);
            Client.Logger.LogDebug("Header: {Header}", header);
            Client.Logger.LogDebug("Payload Base64: {Payload}", payload.ToByteString().ToBase64());
            Client.Logger.LogDebug("Payload: {Payload}", payload);
            Client.Logger.LogDebug("\t{Ping}", ping);
            Client.Logger.LogDebug("--------");
#endif
            EncodeAndSend(tcpStream, header, payload, SessionKey);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private ZtLiveCsCmd GenerateCommand(in string command, in IMessage msg = null)
        {
            return new ZtLiveCsCmd
            {
                CmdType = command,
                Ticket = Ticket,
                LiveId = _liveId,
                Payload = msg?.ToByteString() ?? ByteString.Empty
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private UpstreamPayload GeneratePayload(in string command, in IMessage msg = null)
        {
            return new UpstreamPayload
            {
                Command = command,
                RetryCount = RETRY_COUNT,
                SeqId = _seqId,
                SubBiz = SUB_BIZ,
                PayloadData = msg?.ToByteString() ?? ByteString.Empty
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private PacketHeader GenerateHeader(in int bodyLength,
            in PacketHeader.Types.EncryptionMode encryptionMode =
                PacketHeader.Types.EncryptionMode.KEncryptionSessionKey)
        {
            return new PacketHeader
            {
                AppId = _appId,
                Uid = _userId,
                InstanceId = _instanceId,
                DecodedPayloadLen = Convert.ToUInt32(bodyLength),
                EncryptionMode = encryptionMode,
                SeqId = _seqId,
                Kpn = KPN
            };
        }
    }
}