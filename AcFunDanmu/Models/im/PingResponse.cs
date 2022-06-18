// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: PingResponse.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace AcFunDanmu.Im.Basic {

  /// <summary>Holder for reflection information generated from PingResponse.proto</summary>
  public static partial class PingResponseReflection {

    #region Descriptor
    /// <summary>File descriptor for PingResponse.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static PingResponseReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "ChJQaW5nUmVzcG9uc2UucHJvdG8SE0FjRnVuRGFubXUuSW0uQmFzaWMidwoM",
            "UGluZ1Jlc3BvbnNlEhcKD3NlcnZlclRpbWVzdGFtcBgBIAEoDxIQCghjbGll",
            "bnRJcBgCIAEoBxISCgpyZWRpcmVjdElwGAMgASgHEhQKDHJlZGlyZWN0UG9y",
            "dBgEIAEoDRISCgpjbGllbnRJcFY2GAUgASgMYgZwcm90bzM="));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { },
          new pbr::GeneratedClrTypeInfo(null, null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::AcFunDanmu.Im.Basic.PingResponse), global::AcFunDanmu.Im.Basic.PingResponse.Parser, new[]{ "ServerTimestamp", "ClientIp", "RedirectIp", "RedirectPort", "ClientIpV6" }, null, null, null, null)
          }));
    }
    #endregion

  }
  #region Messages
  public sealed partial class PingResponse : pb::IMessage<PingResponse>
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      , pb::IBufferMessage
  #endif
  {
    private static readonly pb::MessageParser<PingResponse> _parser = new pb::MessageParser<PingResponse>(() => new PingResponse());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pb::MessageParser<PingResponse> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::AcFunDanmu.Im.Basic.PingResponseReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public PingResponse() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public PingResponse(PingResponse other) : this() {
      serverTimestamp_ = other.serverTimestamp_;
      clientIp_ = other.clientIp_;
      redirectIp_ = other.redirectIp_;
      redirectPort_ = other.redirectPort_;
      clientIpV6_ = other.clientIpV6_;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public PingResponse Clone() {
      return new PingResponse(this);
    }

    /// <summary>Field number for the "serverTimestamp" field.</summary>
    public const int ServerTimestampFieldNumber = 1;
    private int serverTimestamp_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public int ServerTimestamp {
      get { return serverTimestamp_; }
      set {
        serverTimestamp_ = value;
      }
    }

    /// <summary>Field number for the "clientIp" field.</summary>
    public const int ClientIpFieldNumber = 2;
    private uint clientIp_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public uint ClientIp {
      get { return clientIp_; }
      set {
        clientIp_ = value;
      }
    }

    /// <summary>Field number for the "redirectIp" field.</summary>
    public const int RedirectIpFieldNumber = 3;
    private uint redirectIp_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public uint RedirectIp {
      get { return redirectIp_; }
      set {
        redirectIp_ = value;
      }
    }

    /// <summary>Field number for the "redirectPort" field.</summary>
    public const int RedirectPortFieldNumber = 4;
    private uint redirectPort_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public uint RedirectPort {
      get { return redirectPort_; }
      set {
        redirectPort_ = value;
      }
    }

    /// <summary>Field number for the "clientIpV6" field.</summary>
    public const int ClientIpV6FieldNumber = 5;
    private pb::ByteString clientIpV6_ = pb::ByteString.Empty;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public pb::ByteString ClientIpV6 {
      get { return clientIpV6_; }
      set {
        clientIpV6_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override bool Equals(object other) {
      return Equals(other as PingResponse);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool Equals(PingResponse other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (ServerTimestamp != other.ServerTimestamp) return false;
      if (ClientIp != other.ClientIp) return false;
      if (RedirectIp != other.RedirectIp) return false;
      if (RedirectPort != other.RedirectPort) return false;
      if (ClientIpV6 != other.ClientIpV6) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override int GetHashCode() {
      int hash = 1;
      if (ServerTimestamp != 0) hash ^= ServerTimestamp.GetHashCode();
      if (ClientIp != 0) hash ^= ClientIp.GetHashCode();
      if (RedirectIp != 0) hash ^= RedirectIp.GetHashCode();
      if (RedirectPort != 0) hash ^= RedirectPort.GetHashCode();
      if (ClientIpV6.Length != 0) hash ^= ClientIpV6.GetHashCode();
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
      if (ServerTimestamp != 0) {
        output.WriteRawTag(13);
        output.WriteSFixed32(ServerTimestamp);
      }
      if (ClientIp != 0) {
        output.WriteRawTag(21);
        output.WriteFixed32(ClientIp);
      }
      if (RedirectIp != 0) {
        output.WriteRawTag(29);
        output.WriteFixed32(RedirectIp);
      }
      if (RedirectPort != 0) {
        output.WriteRawTag(32);
        output.WriteUInt32(RedirectPort);
      }
      if (ClientIpV6.Length != 0) {
        output.WriteRawTag(42);
        output.WriteBytes(ClientIpV6);
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
      if (ServerTimestamp != 0) {
        output.WriteRawTag(13);
        output.WriteSFixed32(ServerTimestamp);
      }
      if (ClientIp != 0) {
        output.WriteRawTag(21);
        output.WriteFixed32(ClientIp);
      }
      if (RedirectIp != 0) {
        output.WriteRawTag(29);
        output.WriteFixed32(RedirectIp);
      }
      if (RedirectPort != 0) {
        output.WriteRawTag(32);
        output.WriteUInt32(RedirectPort);
      }
      if (ClientIpV6.Length != 0) {
        output.WriteRawTag(42);
        output.WriteBytes(ClientIpV6);
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
      if (ServerTimestamp != 0) {
        size += 1 + 4;
      }
      if (ClientIp != 0) {
        size += 1 + 4;
      }
      if (RedirectIp != 0) {
        size += 1 + 4;
      }
      if (RedirectPort != 0) {
        size += 1 + pb::CodedOutputStream.ComputeUInt32Size(RedirectPort);
      }
      if (ClientIpV6.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeBytesSize(ClientIpV6);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void MergeFrom(PingResponse other) {
      if (other == null) {
        return;
      }
      if (other.ServerTimestamp != 0) {
        ServerTimestamp = other.ServerTimestamp;
      }
      if (other.ClientIp != 0) {
        ClientIp = other.ClientIp;
      }
      if (other.RedirectIp != 0) {
        RedirectIp = other.RedirectIp;
      }
      if (other.RedirectPort != 0) {
        RedirectPort = other.RedirectPort;
      }
      if (other.ClientIpV6.Length != 0) {
        ClientIpV6 = other.ClientIpV6;
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
          case 13: {
            ServerTimestamp = input.ReadSFixed32();
            break;
          }
          case 21: {
            ClientIp = input.ReadFixed32();
            break;
          }
          case 29: {
            RedirectIp = input.ReadFixed32();
            break;
          }
          case 32: {
            RedirectPort = input.ReadUInt32();
            break;
          }
          case 42: {
            ClientIpV6 = input.ReadBytes();
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
          case 13: {
            ServerTimestamp = input.ReadSFixed32();
            break;
          }
          case 21: {
            ClientIp = input.ReadFixed32();
            break;
          }
          case 29: {
            RedirectIp = input.ReadFixed32();
            break;
          }
          case 32: {
            RedirectPort = input.ReadUInt32();
            break;
          }
          case 42: {
            ClientIpV6 = input.ReadBytes();
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