// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: KeepAlive.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace AcFunDanmu {

  /// <summary>Holder for reflection information generated from KeepAlive.proto</summary>
  public static partial class KeepAliveReflection {

    #region Descriptor
    /// <summary>File descriptor for KeepAlive.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static KeepAliveReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "Cg9LZWVwQWxpdmUucHJvdG8SCkFjRnVuRGFubXUaDlJlZ2lzdGVyLnByb3Rv",
            "GhZQdXNoU2VydmljZVRva2VuLnByb3RvGhhBY2Nlc3NQb2ludHNDb25maWcu",
            "cHJvdG8iwgIKEEtlZXBBbGl2ZVJlcXVlc3QSQgoOcHJlc2VuY2VTdGF0dXMY",
            "ASABKA4yKi5BY0Z1bkRhbm11LlJlZ2lzdGVyUmVxdWVzdC5QcmVzZW5jZVN0",
            "YXR1cxJBCg9hcHBBY3RpdmVTdGF0dXMYAiABKA4yKC5BY0Z1bkRhbm11LlJl",
            "Z2lzdGVyUmVxdWVzdC5BY3RpdmVTdGF0dXMSNgoQcHVzaFNlcnZpY2VUb2tl",
            "bhgDIAEoCzIcLkFjRnVuRGFubXUuUHVzaFNlcnZpY2VUb2tlbhI6ChRwdXNo",
            "U2VydmljZVRva2VuTGlzdBgEIAMoCzIcLkFjRnVuRGFubXUuUHVzaFNlcnZp",
            "Y2VUb2tlbhIcChRrZWVwYWxpdmVJbnRlcnZhbFNlYxgFIAEoBRIVCg1pcHY2",
            "QXZhaWxhYmxlGAYgASgIIpwDChFLZWVwQWxpdmVSZXNwb25zZRI6ChJhY2Nl",
            "c3NQb2ludHNDb25maWcYASABKAsyHi5BY0Z1bkRhbm11LkFjY2Vzc1BvaW50",
            "c0NvbmZpZxISCgpzZXJ2ZXJNc2VjGAIgASgDEj4KFmFjY2Vzc1BvaW50c0Nv",
            "bmZpZ0lwdjYYAyABKAsyHi5BY0Z1bkRhbm11LkFjY2Vzc1BvaW50c0NvbmZp",
            "ZxI+ChZhY2Nlc3NQb2ludHNDb25maWdRVWljGAYgASgLMh4uQWNGdW5EYW5t",
            "dS5BY2Nlc3NQb2ludHNDb25maWcSQgoaYWNjZXNzUG9pbnRzQ29uZmlnUXVp",
            "Y0lwdjYYByABKAsyHi5BY0Z1bkRhbm11LkFjY2Vzc1BvaW50c0NvbmZpZxIa",
            "ChJmbG93Q29zdFNhbXBsZVJhdGUYCCABKAISGQoRY29tbWFuZFNhbXBsZVJh",
            "dGUYCSABKAISPAoUYWNjZXNzUG9pbnRzQ29uZmlnV3MYCiABKAsyHi5BY0Z1",
            "bkRhbm11LkFjY2Vzc1BvaW50c0NvbmZpZ2IGcHJvdG8z"));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { global::AcFunDanmu.RegisterReflection.Descriptor, global::AcFunDanmu.PushServiceTokenReflection.Descriptor, global::AcFunDanmu.AccessPointsConfigReflection.Descriptor, },
          new pbr::GeneratedClrTypeInfo(null, null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::AcFunDanmu.KeepAliveRequest), global::AcFunDanmu.KeepAliveRequest.Parser, new[]{ "PresenceStatus", "AppActiveStatus", "PushServiceToken", "PushServiceTokenList", "KeepaliveIntervalSec", "Ipv6Available" }, null, null, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::AcFunDanmu.KeepAliveResponse), global::AcFunDanmu.KeepAliveResponse.Parser, new[]{ "AccessPointsConfig", "ServerMsec", "AccessPointsConfigIpv6", "AccessPointsConfigQUic", "AccessPointsConfigQuicIpv6", "FlowCostSampleRate", "CommandSampleRate", "AccessPointsConfigWs" }, null, null, null, null)
          }));
    }
    #endregion

  }
  #region Messages
  public sealed partial class KeepAliveRequest : pb::IMessage<KeepAliveRequest>
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      , pb::IBufferMessage
  #endif
  {
    private static readonly pb::MessageParser<KeepAliveRequest> _parser = new pb::MessageParser<KeepAliveRequest>(() => new KeepAliveRequest());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pb::MessageParser<KeepAliveRequest> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::AcFunDanmu.KeepAliveReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public KeepAliveRequest() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public KeepAliveRequest(KeepAliveRequest other) : this() {
      presenceStatus_ = other.presenceStatus_;
      appActiveStatus_ = other.appActiveStatus_;
      pushServiceToken_ = other.pushServiceToken_ != null ? other.pushServiceToken_.Clone() : null;
      pushServiceTokenList_ = other.pushServiceTokenList_.Clone();
      keepaliveIntervalSec_ = other.keepaliveIntervalSec_;
      ipv6Available_ = other.ipv6Available_;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public KeepAliveRequest Clone() {
      return new KeepAliveRequest(this);
    }

    /// <summary>Field number for the "presenceStatus" field.</summary>
    public const int PresenceStatusFieldNumber = 1;
    private global::AcFunDanmu.RegisterRequest.Types.PresenceStatus presenceStatus_ = global::AcFunDanmu.RegisterRequest.Types.PresenceStatus.KPresenceOffline;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public global::AcFunDanmu.RegisterRequest.Types.PresenceStatus PresenceStatus {
      get { return presenceStatus_; }
      set {
        presenceStatus_ = value;
      }
    }

    /// <summary>Field number for the "appActiveStatus" field.</summary>
    public const int AppActiveStatusFieldNumber = 2;
    private global::AcFunDanmu.RegisterRequest.Types.ActiveStatus appActiveStatus_ = global::AcFunDanmu.RegisterRequest.Types.ActiveStatus.KInvalid;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public global::AcFunDanmu.RegisterRequest.Types.ActiveStatus AppActiveStatus {
      get { return appActiveStatus_; }
      set {
        appActiveStatus_ = value;
      }
    }

    /// <summary>Field number for the "pushServiceToken" field.</summary>
    public const int PushServiceTokenFieldNumber = 3;
    private global::AcFunDanmu.PushServiceToken pushServiceToken_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public global::AcFunDanmu.PushServiceToken PushServiceToken {
      get { return pushServiceToken_; }
      set {
        pushServiceToken_ = value;
      }
    }

    /// <summary>Field number for the "pushServiceTokenList" field.</summary>
    public const int PushServiceTokenListFieldNumber = 4;
    private static readonly pb::FieldCodec<global::AcFunDanmu.PushServiceToken> _repeated_pushServiceTokenList_codec
        = pb::FieldCodec.ForMessage(34, global::AcFunDanmu.PushServiceToken.Parser);
    private readonly pbc::RepeatedField<global::AcFunDanmu.PushServiceToken> pushServiceTokenList_ = new pbc::RepeatedField<global::AcFunDanmu.PushServiceToken>();
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public pbc::RepeatedField<global::AcFunDanmu.PushServiceToken> PushServiceTokenList {
      get { return pushServiceTokenList_; }
    }

    /// <summary>Field number for the "keepaliveIntervalSec" field.</summary>
    public const int KeepaliveIntervalSecFieldNumber = 5;
    private int keepaliveIntervalSec_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public int KeepaliveIntervalSec {
      get { return keepaliveIntervalSec_; }
      set {
        keepaliveIntervalSec_ = value;
      }
    }

    /// <summary>Field number for the "ipv6Available" field.</summary>
    public const int Ipv6AvailableFieldNumber = 6;
    private bool ipv6Available_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool Ipv6Available {
      get { return ipv6Available_; }
      set {
        ipv6Available_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override bool Equals(object other) {
      return Equals(other as KeepAliveRequest);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool Equals(KeepAliveRequest other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (PresenceStatus != other.PresenceStatus) return false;
      if (AppActiveStatus != other.AppActiveStatus) return false;
      if (!object.Equals(PushServiceToken, other.PushServiceToken)) return false;
      if(!pushServiceTokenList_.Equals(other.pushServiceTokenList_)) return false;
      if (KeepaliveIntervalSec != other.KeepaliveIntervalSec) return false;
      if (Ipv6Available != other.Ipv6Available) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override int GetHashCode() {
      int hash = 1;
      if (PresenceStatus != global::AcFunDanmu.RegisterRequest.Types.PresenceStatus.KPresenceOffline) hash ^= PresenceStatus.GetHashCode();
      if (AppActiveStatus != global::AcFunDanmu.RegisterRequest.Types.ActiveStatus.KInvalid) hash ^= AppActiveStatus.GetHashCode();
      if (pushServiceToken_ != null) hash ^= PushServiceToken.GetHashCode();
      hash ^= pushServiceTokenList_.GetHashCode();
      if (KeepaliveIntervalSec != 0) hash ^= KeepaliveIntervalSec.GetHashCode();
      if (Ipv6Available != false) hash ^= Ipv6Available.GetHashCode();
      if (_unknownFields != null) {
        hash ^= _unknownFields.GetHashCode();
      }
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void WriteTo(pb::CodedOutputStream output) {
    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      output.WriteRawMessage(this);
    #else
      if (PresenceStatus != global::AcFunDanmu.RegisterRequest.Types.PresenceStatus.KPresenceOffline) {
        output.WriteRawTag(8);
        output.WriteEnum((int) PresenceStatus);
      }
      if (AppActiveStatus != global::AcFunDanmu.RegisterRequest.Types.ActiveStatus.KInvalid) {
        output.WriteRawTag(16);
        output.WriteEnum((int) AppActiveStatus);
      }
      if (pushServiceToken_ != null) {
        output.WriteRawTag(26);
        output.WriteMessage(PushServiceToken);
      }
      pushServiceTokenList_.WriteTo(output, _repeated_pushServiceTokenList_codec);
      if (KeepaliveIntervalSec != 0) {
        output.WriteRawTag(40);
        output.WriteInt32(KeepaliveIntervalSec);
      }
      if (Ipv6Available != false) {
        output.WriteRawTag(48);
        output.WriteBool(Ipv6Available);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    void pb::IBufferMessage.InternalWriteTo(ref pb::WriteContext output) {
      if (PresenceStatus != global::AcFunDanmu.RegisterRequest.Types.PresenceStatus.KPresenceOffline) {
        output.WriteRawTag(8);
        output.WriteEnum((int) PresenceStatus);
      }
      if (AppActiveStatus != global::AcFunDanmu.RegisterRequest.Types.ActiveStatus.KInvalid) {
        output.WriteRawTag(16);
        output.WriteEnum((int) AppActiveStatus);
      }
      if (pushServiceToken_ != null) {
        output.WriteRawTag(26);
        output.WriteMessage(PushServiceToken);
      }
      pushServiceTokenList_.WriteTo(ref output, _repeated_pushServiceTokenList_codec);
      if (KeepaliveIntervalSec != 0) {
        output.WriteRawTag(40);
        output.WriteInt32(KeepaliveIntervalSec);
      }
      if (Ipv6Available != false) {
        output.WriteRawTag(48);
        output.WriteBool(Ipv6Available);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(ref output);
      }
    }
    #endif

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public int CalculateSize() {
      int size = 0;
      if (PresenceStatus != global::AcFunDanmu.RegisterRequest.Types.PresenceStatus.KPresenceOffline) {
        size += 1 + pb::CodedOutputStream.ComputeEnumSize((int) PresenceStatus);
      }
      if (AppActiveStatus != global::AcFunDanmu.RegisterRequest.Types.ActiveStatus.KInvalid) {
        size += 1 + pb::CodedOutputStream.ComputeEnumSize((int) AppActiveStatus);
      }
      if (pushServiceToken_ != null) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(PushServiceToken);
      }
      size += pushServiceTokenList_.CalculateSize(_repeated_pushServiceTokenList_codec);
      if (KeepaliveIntervalSec != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(KeepaliveIntervalSec);
      }
      if (Ipv6Available != false) {
        size += 1 + 1;
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void MergeFrom(KeepAliveRequest other) {
      if (other == null) {
        return;
      }
      if (other.PresenceStatus != global::AcFunDanmu.RegisterRequest.Types.PresenceStatus.KPresenceOffline) {
        PresenceStatus = other.PresenceStatus;
      }
      if (other.AppActiveStatus != global::AcFunDanmu.RegisterRequest.Types.ActiveStatus.KInvalid) {
        AppActiveStatus = other.AppActiveStatus;
      }
      if (other.pushServiceToken_ != null) {
        if (pushServiceToken_ == null) {
          PushServiceToken = new global::AcFunDanmu.PushServiceToken();
        }
        PushServiceToken.MergeFrom(other.PushServiceToken);
      }
      pushServiceTokenList_.Add(other.pushServiceTokenList_);
      if (other.KeepaliveIntervalSec != 0) {
        KeepaliveIntervalSec = other.KeepaliveIntervalSec;
      }
      if (other.Ipv6Available != false) {
        Ipv6Available = other.Ipv6Available;
      }
      _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void MergeFrom(pb::CodedInputStream input) {
    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      input.ReadRawMessage(this);
    #else
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
            break;
          case 8: {
            PresenceStatus = (global::AcFunDanmu.RegisterRequest.Types.PresenceStatus) input.ReadEnum();
            break;
          }
          case 16: {
            AppActiveStatus = (global::AcFunDanmu.RegisterRequest.Types.ActiveStatus) input.ReadEnum();
            break;
          }
          case 26: {
            if (pushServiceToken_ == null) {
              PushServiceToken = new global::AcFunDanmu.PushServiceToken();
            }
            input.ReadMessage(PushServiceToken);
            break;
          }
          case 34: {
            pushServiceTokenList_.AddEntriesFrom(input, _repeated_pushServiceTokenList_codec);
            break;
          }
          case 40: {
            KeepaliveIntervalSec = input.ReadInt32();
            break;
          }
          case 48: {
            Ipv6Available = input.ReadBool();
            break;
          }
        }
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    void pb::IBufferMessage.InternalMergeFrom(ref pb::ParseContext input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, ref input);
            break;
          case 8: {
            PresenceStatus = (global::AcFunDanmu.RegisterRequest.Types.PresenceStatus) input.ReadEnum();
            break;
          }
          case 16: {
            AppActiveStatus = (global::AcFunDanmu.RegisterRequest.Types.ActiveStatus) input.ReadEnum();
            break;
          }
          case 26: {
            if (pushServiceToken_ == null) {
              PushServiceToken = new global::AcFunDanmu.PushServiceToken();
            }
            input.ReadMessage(PushServiceToken);
            break;
          }
          case 34: {
            pushServiceTokenList_.AddEntriesFrom(ref input, _repeated_pushServiceTokenList_codec);
            break;
          }
          case 40: {
            KeepaliveIntervalSec = input.ReadInt32();
            break;
          }
          case 48: {
            Ipv6Available = input.ReadBool();
            break;
          }
        }
      }
    }
    #endif

  }

  public sealed partial class KeepAliveResponse : pb::IMessage<KeepAliveResponse>
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      , pb::IBufferMessage
  #endif
  {
    private static readonly pb::MessageParser<KeepAliveResponse> _parser = new pb::MessageParser<KeepAliveResponse>(() => new KeepAliveResponse());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pb::MessageParser<KeepAliveResponse> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::AcFunDanmu.KeepAliveReflection.Descriptor.MessageTypes[1]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public KeepAliveResponse() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public KeepAliveResponse(KeepAliveResponse other) : this() {
      accessPointsConfig_ = other.accessPointsConfig_ != null ? other.accessPointsConfig_.Clone() : null;
      serverMsec_ = other.serverMsec_;
      accessPointsConfigIpv6_ = other.accessPointsConfigIpv6_ != null ? other.accessPointsConfigIpv6_.Clone() : null;
      accessPointsConfigQUic_ = other.accessPointsConfigQUic_ != null ? other.accessPointsConfigQUic_.Clone() : null;
      accessPointsConfigQuicIpv6_ = other.accessPointsConfigQuicIpv6_ != null ? other.accessPointsConfigQuicIpv6_.Clone() : null;
      flowCostSampleRate_ = other.flowCostSampleRate_;
      commandSampleRate_ = other.commandSampleRate_;
      accessPointsConfigWs_ = other.accessPointsConfigWs_ != null ? other.accessPointsConfigWs_.Clone() : null;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public KeepAliveResponse Clone() {
      return new KeepAliveResponse(this);
    }

    /// <summary>Field number for the "accessPointsConfig" field.</summary>
    public const int AccessPointsConfigFieldNumber = 1;
    private global::AcFunDanmu.AccessPointsConfig accessPointsConfig_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public global::AcFunDanmu.AccessPointsConfig AccessPointsConfig {
      get { return accessPointsConfig_; }
      set {
        accessPointsConfig_ = value;
      }
    }

    /// <summary>Field number for the "serverMsec" field.</summary>
    public const int ServerMsecFieldNumber = 2;
    private long serverMsec_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public long ServerMsec {
      get { return serverMsec_; }
      set {
        serverMsec_ = value;
      }
    }

    /// <summary>Field number for the "accessPointsConfigIpv6" field.</summary>
    public const int AccessPointsConfigIpv6FieldNumber = 3;
    private global::AcFunDanmu.AccessPointsConfig accessPointsConfigIpv6_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public global::AcFunDanmu.AccessPointsConfig AccessPointsConfigIpv6 {
      get { return accessPointsConfigIpv6_; }
      set {
        accessPointsConfigIpv6_ = value;
      }
    }

    /// <summary>Field number for the "accessPointsConfigQUic" field.</summary>
    public const int AccessPointsConfigQUicFieldNumber = 6;
    private global::AcFunDanmu.AccessPointsConfig accessPointsConfigQUic_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public global::AcFunDanmu.AccessPointsConfig AccessPointsConfigQUic {
      get { return accessPointsConfigQUic_; }
      set {
        accessPointsConfigQUic_ = value;
      }
    }

    /// <summary>Field number for the "accessPointsConfigQuicIpv6" field.</summary>
    public const int AccessPointsConfigQuicIpv6FieldNumber = 7;
    private global::AcFunDanmu.AccessPointsConfig accessPointsConfigQuicIpv6_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public global::AcFunDanmu.AccessPointsConfig AccessPointsConfigQuicIpv6 {
      get { return accessPointsConfigQuicIpv6_; }
      set {
        accessPointsConfigQuicIpv6_ = value;
      }
    }

    /// <summary>Field number for the "flowCostSampleRate" field.</summary>
    public const int FlowCostSampleRateFieldNumber = 8;
    private float flowCostSampleRate_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public float FlowCostSampleRate {
      get { return flowCostSampleRate_; }
      set {
        flowCostSampleRate_ = value;
      }
    }

    /// <summary>Field number for the "commandSampleRate" field.</summary>
    public const int CommandSampleRateFieldNumber = 9;
    private float commandSampleRate_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public float CommandSampleRate {
      get { return commandSampleRate_; }
      set {
        commandSampleRate_ = value;
      }
    }

    /// <summary>Field number for the "accessPointsConfigWs" field.</summary>
    public const int AccessPointsConfigWsFieldNumber = 10;
    private global::AcFunDanmu.AccessPointsConfig accessPointsConfigWs_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public global::AcFunDanmu.AccessPointsConfig AccessPointsConfigWs {
      get { return accessPointsConfigWs_; }
      set {
        accessPointsConfigWs_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override bool Equals(object other) {
      return Equals(other as KeepAliveResponse);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool Equals(KeepAliveResponse other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (!object.Equals(AccessPointsConfig, other.AccessPointsConfig)) return false;
      if (ServerMsec != other.ServerMsec) return false;
      if (!object.Equals(AccessPointsConfigIpv6, other.AccessPointsConfigIpv6)) return false;
      if (!object.Equals(AccessPointsConfigQUic, other.AccessPointsConfigQUic)) return false;
      if (!object.Equals(AccessPointsConfigQuicIpv6, other.AccessPointsConfigQuicIpv6)) return false;
      if (!pbc::ProtobufEqualityComparers.BitwiseSingleEqualityComparer.Equals(FlowCostSampleRate, other.FlowCostSampleRate)) return false;
      if (!pbc::ProtobufEqualityComparers.BitwiseSingleEqualityComparer.Equals(CommandSampleRate, other.CommandSampleRate)) return false;
      if (!object.Equals(AccessPointsConfigWs, other.AccessPointsConfigWs)) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override int GetHashCode() {
      int hash = 1;
      if (accessPointsConfig_ != null) hash ^= AccessPointsConfig.GetHashCode();
      if (ServerMsec != 0L) hash ^= ServerMsec.GetHashCode();
      if (accessPointsConfigIpv6_ != null) hash ^= AccessPointsConfigIpv6.GetHashCode();
      if (accessPointsConfigQUic_ != null) hash ^= AccessPointsConfigQUic.GetHashCode();
      if (accessPointsConfigQuicIpv6_ != null) hash ^= AccessPointsConfigQuicIpv6.GetHashCode();
      if (FlowCostSampleRate != 0F) hash ^= pbc::ProtobufEqualityComparers.BitwiseSingleEqualityComparer.GetHashCode(FlowCostSampleRate);
      if (CommandSampleRate != 0F) hash ^= pbc::ProtobufEqualityComparers.BitwiseSingleEqualityComparer.GetHashCode(CommandSampleRate);
      if (accessPointsConfigWs_ != null) hash ^= AccessPointsConfigWs.GetHashCode();
      if (_unknownFields != null) {
        hash ^= _unknownFields.GetHashCode();
      }
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void WriteTo(pb::CodedOutputStream output) {
    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      output.WriteRawMessage(this);
    #else
      if (accessPointsConfig_ != null) {
        output.WriteRawTag(10);
        output.WriteMessage(AccessPointsConfig);
      }
      if (ServerMsec != 0L) {
        output.WriteRawTag(16);
        output.WriteInt64(ServerMsec);
      }
      if (accessPointsConfigIpv6_ != null) {
        output.WriteRawTag(26);
        output.WriteMessage(AccessPointsConfigIpv6);
      }
      if (accessPointsConfigQUic_ != null) {
        output.WriteRawTag(50);
        output.WriteMessage(AccessPointsConfigQUic);
      }
      if (accessPointsConfigQuicIpv6_ != null) {
        output.WriteRawTag(58);
        output.WriteMessage(AccessPointsConfigQuicIpv6);
      }
      if (FlowCostSampleRate != 0F) {
        output.WriteRawTag(69);
        output.WriteFloat(FlowCostSampleRate);
      }
      if (CommandSampleRate != 0F) {
        output.WriteRawTag(77);
        output.WriteFloat(CommandSampleRate);
      }
      if (accessPointsConfigWs_ != null) {
        output.WriteRawTag(82);
        output.WriteMessage(AccessPointsConfigWs);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    void pb::IBufferMessage.InternalWriteTo(ref pb::WriteContext output) {
      if (accessPointsConfig_ != null) {
        output.WriteRawTag(10);
        output.WriteMessage(AccessPointsConfig);
      }
      if (ServerMsec != 0L) {
        output.WriteRawTag(16);
        output.WriteInt64(ServerMsec);
      }
      if (accessPointsConfigIpv6_ != null) {
        output.WriteRawTag(26);
        output.WriteMessage(AccessPointsConfigIpv6);
      }
      if (accessPointsConfigQUic_ != null) {
        output.WriteRawTag(50);
        output.WriteMessage(AccessPointsConfigQUic);
      }
      if (accessPointsConfigQuicIpv6_ != null) {
        output.WriteRawTag(58);
        output.WriteMessage(AccessPointsConfigQuicIpv6);
      }
      if (FlowCostSampleRate != 0F) {
        output.WriteRawTag(69);
        output.WriteFloat(FlowCostSampleRate);
      }
      if (CommandSampleRate != 0F) {
        output.WriteRawTag(77);
        output.WriteFloat(CommandSampleRate);
      }
      if (accessPointsConfigWs_ != null) {
        output.WriteRawTag(82);
        output.WriteMessage(AccessPointsConfigWs);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(ref output);
      }
    }
    #endif

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public int CalculateSize() {
      int size = 0;
      if (accessPointsConfig_ != null) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(AccessPointsConfig);
      }
      if (ServerMsec != 0L) {
        size += 1 + pb::CodedOutputStream.ComputeInt64Size(ServerMsec);
      }
      if (accessPointsConfigIpv6_ != null) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(AccessPointsConfigIpv6);
      }
      if (accessPointsConfigQUic_ != null) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(AccessPointsConfigQUic);
      }
      if (accessPointsConfigQuicIpv6_ != null) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(AccessPointsConfigQuicIpv6);
      }
      if (FlowCostSampleRate != 0F) {
        size += 1 + 4;
      }
      if (CommandSampleRate != 0F) {
        size += 1 + 4;
      }
      if (accessPointsConfigWs_ != null) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(AccessPointsConfigWs);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void MergeFrom(KeepAliveResponse other) {
      if (other == null) {
        return;
      }
      if (other.accessPointsConfig_ != null) {
        if (accessPointsConfig_ == null) {
          AccessPointsConfig = new global::AcFunDanmu.AccessPointsConfig();
        }
        AccessPointsConfig.MergeFrom(other.AccessPointsConfig);
      }
      if (other.ServerMsec != 0L) {
        ServerMsec = other.ServerMsec;
      }
      if (other.accessPointsConfigIpv6_ != null) {
        if (accessPointsConfigIpv6_ == null) {
          AccessPointsConfigIpv6 = new global::AcFunDanmu.AccessPointsConfig();
        }
        AccessPointsConfigIpv6.MergeFrom(other.AccessPointsConfigIpv6);
      }
      if (other.accessPointsConfigQUic_ != null) {
        if (accessPointsConfigQUic_ == null) {
          AccessPointsConfigQUic = new global::AcFunDanmu.AccessPointsConfig();
        }
        AccessPointsConfigQUic.MergeFrom(other.AccessPointsConfigQUic);
      }
      if (other.accessPointsConfigQuicIpv6_ != null) {
        if (accessPointsConfigQuicIpv6_ == null) {
          AccessPointsConfigQuicIpv6 = new global::AcFunDanmu.AccessPointsConfig();
        }
        AccessPointsConfigQuicIpv6.MergeFrom(other.AccessPointsConfigQuicIpv6);
      }
      if (other.FlowCostSampleRate != 0F) {
        FlowCostSampleRate = other.FlowCostSampleRate;
      }
      if (other.CommandSampleRate != 0F) {
        CommandSampleRate = other.CommandSampleRate;
      }
      if (other.accessPointsConfigWs_ != null) {
        if (accessPointsConfigWs_ == null) {
          AccessPointsConfigWs = new global::AcFunDanmu.AccessPointsConfig();
        }
        AccessPointsConfigWs.MergeFrom(other.AccessPointsConfigWs);
      }
      _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void MergeFrom(pb::CodedInputStream input) {
    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      input.ReadRawMessage(this);
    #else
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
            break;
          case 10: {
            if (accessPointsConfig_ == null) {
              AccessPointsConfig = new global::AcFunDanmu.AccessPointsConfig();
            }
            input.ReadMessage(AccessPointsConfig);
            break;
          }
          case 16: {
            ServerMsec = input.ReadInt64();
            break;
          }
          case 26: {
            if (accessPointsConfigIpv6_ == null) {
              AccessPointsConfigIpv6 = new global::AcFunDanmu.AccessPointsConfig();
            }
            input.ReadMessage(AccessPointsConfigIpv6);
            break;
          }
          case 50: {
            if (accessPointsConfigQUic_ == null) {
              AccessPointsConfigQUic = new global::AcFunDanmu.AccessPointsConfig();
            }
            input.ReadMessage(AccessPointsConfigQUic);
            break;
          }
          case 58: {
            if (accessPointsConfigQuicIpv6_ == null) {
              AccessPointsConfigQuicIpv6 = new global::AcFunDanmu.AccessPointsConfig();
            }
            input.ReadMessage(AccessPointsConfigQuicIpv6);
            break;
          }
          case 69: {
            FlowCostSampleRate = input.ReadFloat();
            break;
          }
          case 77: {
            CommandSampleRate = input.ReadFloat();
            break;
          }
          case 82: {
            if (accessPointsConfigWs_ == null) {
              AccessPointsConfigWs = new global::AcFunDanmu.AccessPointsConfig();
            }
            input.ReadMessage(AccessPointsConfigWs);
            break;
          }
        }
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    void pb::IBufferMessage.InternalMergeFrom(ref pb::ParseContext input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, ref input);
            break;
          case 10: {
            if (accessPointsConfig_ == null) {
              AccessPointsConfig = new global::AcFunDanmu.AccessPointsConfig();
            }
            input.ReadMessage(AccessPointsConfig);
            break;
          }
          case 16: {
            ServerMsec = input.ReadInt64();
            break;
          }
          case 26: {
            if (accessPointsConfigIpv6_ == null) {
              AccessPointsConfigIpv6 = new global::AcFunDanmu.AccessPointsConfig();
            }
            input.ReadMessage(AccessPointsConfigIpv6);
            break;
          }
          case 50: {
            if (accessPointsConfigQUic_ == null) {
              AccessPointsConfigQUic = new global::AcFunDanmu.AccessPointsConfig();
            }
            input.ReadMessage(AccessPointsConfigQUic);
            break;
          }
          case 58: {
            if (accessPointsConfigQuicIpv6_ == null) {
              AccessPointsConfigQuicIpv6 = new global::AcFunDanmu.AccessPointsConfig();
            }
            input.ReadMessage(AccessPointsConfigQuicIpv6);
            break;
          }
          case 69: {
            FlowCostSampleRate = input.ReadFloat();
            break;
          }
          case 77: {
            CommandSampleRate = input.ReadFloat();
            break;
          }
          case 82: {
            if (accessPointsConfigWs_ == null) {
              AccessPointsConfigWs = new global::AcFunDanmu.AccessPointsConfig();
            }
            input.ReadMessage(AccessPointsConfigWs);
            break;
          }
        }
      }
    }
    #endif

  }

  #endregion

}

#endregion Designer generated code
