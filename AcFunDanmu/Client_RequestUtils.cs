using AcFunDanmu.Enums;
using AcFunDanmu.Im.Basic;
using Google.Protobuf;
using Microsoft.Extensions.Logging;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using static AcFunDanmu.ClientUtils;

namespace AcFunDanmu
{
    public partial class Client
    {
        private const string APP_NAME = "link-sdk";
        private const string SDK_VERSION = "1.4.0.145";
        private const string KPN = "ACFUN_APP.LIVE_MATE";
        private const string KPF = "WINDOWS_PC";
        private const string SUB_BIZ = "mainApp";
        private const string CLIENT_LIVE_SDK_VERSION = "kwai-acfun-live-link";
        private const string LINK_VERSION = "2.13.8";
        private const uint RETRY_COUNT = 1;

        private int _appId;
        private long _heartbeatSeqId;
        private long _instanceId;
        private long _lz4CompressionThreshold;

        private long _seqId = 1;
        private byte[]? _sessionKey;
        private int _ticketIndex;

        private string Ticket => _tickets == null ? string.Empty : _tickets[_ticketIndex];

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void Register(in int appId, in RegisterResponse register)
        {
            _appId = appId;
            _instanceId = register.InstanceId;
            _sessionKey = register.SessKey.ToByteArray();
            _lz4CompressionThreshold = register.SdkOption.Lz4CompressionThresholdBytes;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void NextTicket()
        {
            if (_tickets == null) return;
            _ticketIndex = _ticketIndex++ % _tickets.Length;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void HandshakeRequest(in NetworkStream? tcpStream)
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
            Logger.LogTrace("--------");
            Logger.LogTrace("Up\t\t {HeaderSeqId}, {SeqId}, {Command}", header.SeqId, payload.SeqId,
                payload.Command);
            Logger.LogTrace("Header: {Header}", header);
            Logger.LogTrace("Payload Base64: {Payload}", payload.ToByteString().ToBase64());
            Logger.LogTrace("Payload: {Payload}", payload);
            Logger.LogTrace("\t{Handshake}", handshake);
            Logger.LogTrace("--------");
#endif
            EncodeAndSend(tcpStream, header, payload, _securityKey);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void RegisterRequest(in NetworkStream? tcpStream)
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
            Logger.LogTrace("--------");
            Logger.LogTrace("Up\t\t {HeaderSeqId}, {SeqId}, {Command}", header.SeqId, payload.SeqId,
                payload.Command);
            Logger.LogTrace("Header: {Header}", header);
            Logger.LogTrace("Payload Base64: {Payload}", payload.ToByteString().ToBase64());
            Logger.LogTrace("Payload: {Payload}", payload);
            Logger.LogTrace("\t{Register}", register);
            Logger.LogTrace("--------");
#endif
            EncodeAndSend(tcpStream, header, payload, _securityKey);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void KeepAliveRequest(in NetworkStream? tcpStream)
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
            Logger.LogTrace("--------");
            Logger.LogTrace("Up\t\t {HeaderSeqId}, {SeqId}, {Command}", header.SeqId, payload.SeqId,
                payload.Command);
            Logger.LogTrace("Header: {Header}", header);
            Logger.LogTrace("Payload Base64: {Payload}", payload.ToByteString().ToBase64());
            Logger.LogTrace("Payload: {Payload}", payload);
            Logger.LogTrace("\t{KeepAlive}", keepAlive);
            Logger.LogTrace("--------");
#endif
            EncodeAndSend(tcpStream, header, payload, _sessionKey);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void EnterRoomRequest(in NetworkStream? tcpStream)
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
            Logger.LogTrace("--------");
            Logger.LogTrace("Up\t\t {HeaderSeqId}, {SeqId}, {Command}", header.SeqId, payload.SeqId,
                payload.Command);
            Logger.LogTrace("Header: {Header}", header);
            Logger.LogTrace("Payload Base64: {Payload}", payload.ToByteString().ToBase64());
            Logger.LogTrace("Payload: {Payload}", payload);
            Logger.LogTrace("\t{Command}", cmd);
            Logger.LogTrace("\t\t{EnterRoom}", enterRoom);
            Logger.LogTrace("--------");
#endif
            EncodeAndSend(tcpStream, header, payload, _sessionKey);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void PushMessageResponse(in NetworkStream? tcpStream, in long headerSeqId)
        {
            var payload = GeneratePayload(Command.PUSH_MESSAGE);

            var header = GenerateHeader(payload.CalculateSize());
            header.SeqId = headerSeqId;
#if DEBUG
            Logger.LogTrace("--------");
            Logger.LogTrace("Up\t\t {HeaderSeqId}, {SeqId}, {Command}", header.SeqId, payload.SeqId,
                payload.Command);
            Logger.LogTrace("Header: {Header}", header);
            Logger.LogTrace("Payload Base64: {Payload}", payload.ToByteString().ToBase64());
            Logger.LogTrace("Payload: {Payload}", payload);
            Logger.LogTrace("\t{PushMessage}", "Empty");
            Logger.LogTrace("--------");
#endif
            EncodeAndSend(tcpStream, header, payload, _sessionKey);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void HeartbeatRequest(in NetworkStream? tcpStream)
        {
            var heartbeat = new ZtLiveCsHeartbeat
            {
                ClientTimestampMs = DateTimeOffset.Now.ToUnixTimeMilliseconds(),
                Sequence = _heartbeatSeqId
            };

            var cmd = GenerateCommand(GlobalCommand.HEARTBEAT, heartbeat);

            var payload = GeneratePayload(Command.GLOBAL_COMMAND, cmd);

            var header = GenerateHeader(payload.CalculateSize());

            _heartbeatSeqId++;
            Interlocked.Increment(ref _seqId);
#if DEBUG
            Logger.LogTrace("--------");
            Logger.LogTrace("Up\t\t {HeaderSeqId}, {SeqId}, {Command}", header.SeqId, payload.SeqId,
                payload.Command);
            Logger.LogTrace("Header: {Header}", header);
            Logger.LogTrace("Payload Base64: {Payload}", payload.ToByteString().ToBase64());
            Logger.LogTrace("Payload: {Payload}", payload);
            Logger.LogTrace("\t{Command}", cmd);
            Logger.LogTrace("\t\t{Heartbeat}", heartbeat);
            Logger.LogTrace("--------");
#endif
            EncodeAndSend(tcpStream, header, payload, _sessionKey);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void UserExitRequest(in NetworkStream? tcpStream)
        {
            var userExit = GenerateCommand(GlobalCommand.USER_EXIT);

            var payload = GeneratePayload(Command.GLOBAL_COMMAND, userExit);

            var header = GenerateHeader(payload.CalculateSize());

            Interlocked.Increment(ref _seqId);
#if DEBUG
            Logger.LogTrace("--------");
            Logger.LogTrace("Up\t\t {HeaderSeqId}, {SeqId}, {Command}", header.SeqId, payload.SeqId,
                payload.Command);
            Logger.LogTrace("Header: {Header}", header);
            Logger.LogTrace("Payload Base64: {Payload}", payload.ToByteString().ToBase64());
            Logger.LogTrace("Payload: {Payload}", payload);
            Logger.LogTrace("\t{UserExit}", userExit);
            Logger.LogTrace("--------");
#endif
            EncodeAndSend(tcpStream, header, payload, _sessionKey);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void UnRegisterRequest(in NetworkStream? tcpStream)
        {
            var payload = GeneratePayload(Command.UNREGISTER);

            var header = GenerateHeader(payload.CalculateSize());
#if DEBUG
            Logger.LogTrace("--------");
            Logger.LogTrace("Up\t\t {HeaderSeqId}, {SeqId}, {Command}", header.SeqId, payload.SeqId,
                payload.Command);
            Logger.LogTrace("Header: {Header}", header);
            Logger.LogTrace("Payload Base64: {Payload}", payload.ToByteString().ToBase64());
            Logger.LogTrace("Payload: {Payload}", payload);
            Logger.LogTrace("\t{UnRegister}", "Empty");
            Logger.LogTrace("--------");
#endif
            EncodeAndSend(tcpStream, header, payload, _sessionKey);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void PingRequest(in NetworkStream? tcpStream)
        {
            var ping = new PingRequest
            {
                PingType = Im.Basic.PingRequest.Types.PingType.KPostRegister
            };

            var payload = GeneratePayload(Command.PING, ping);

            var header = GenerateHeader(payload.CalculateSize());
#if DEBUG
            Logger.LogTrace("--------");
            Logger.LogTrace("Up\t\t {HeaderSeqId}, {SeqId}, {Command}", header.SeqId, payload.SeqId,
                payload.Command);
            Logger.LogTrace("Header: {Header}", header);
            Logger.LogTrace("Payload Base64: {Payload}", payload.ToByteString().ToBase64());
            Logger.LogTrace("Payload: {Payload}", payload);
            Logger.LogTrace("\t{Ping}", ping);
            Logger.LogTrace("--------");
#endif
            EncodeAndSend(tcpStream, header, payload, _sessionKey);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private ZtLiveCsCmd GenerateCommand(in string command, in IMessage? msg = null) =>
            new()
            {
                CmdType = command,
                Ticket = Ticket,
                LiveId = LiveId,
                Payload = msg?.ToByteString() ?? ByteString.Empty
            };

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private UpstreamPayload GeneratePayload(in string command, in IMessage? msg = null) =>
            new()
            {
                Command = command,
                RetryCount = RETRY_COUNT,
                SeqId = _seqId,
                SubBiz = SUB_BIZ,
                PayloadData = msg?.ToByteString() ?? ByteString.Empty
            };

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private PacketHeader GenerateHeader(in int bodyLength,
            in PacketHeader.Types.EncryptionMode encryptionMode =
                PacketHeader.Types.EncryptionMode.KEncryptionSessionKey) =>
            new()
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