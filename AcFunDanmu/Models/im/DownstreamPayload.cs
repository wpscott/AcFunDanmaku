// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: DownstreamPayload.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021, 8981
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace AcFunDanmu.Im.Basic {

  /// <summary>Holder for reflection information generated from DownstreamPayload.proto</summary>
  public static partial class DownstreamPayloadReflection {

    #region Descriptor
    /// <summary>File descriptor for DownstreamPayload.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static DownstreamPayloadReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "ChdEb3duc3RyZWFtUGF5bG9hZC5wcm90bxITQWNGdW5EYW5tdS5JbS5CYXNp",
            "YyKlAQoRRG93bnN0cmVhbVBheWxvYWQSDwoHY29tbWFuZBgBIAEoCRINCgVz",
            "ZXFJZBgCIAEoAxIRCgllcnJvckNvZGUYAyABKAUSEwoLcGF5bG9hZERhdGEY",
            "BCABKAwSEAoIZXJyb3JNc2cYBSABKAkSEQoJZXJyb3JEYXRhGAYgASgMEg4K",
            "BnN1YkJpehgHIAEoCRITCgtrbGlua1B1c2hJZBgIIAEoA2IGcHJvdG8z"));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { },
          new pbr::GeneratedClrTypeInfo(null, null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::AcFunDanmu.Im.Basic.DownstreamPayload), global::AcFunDanmu.Im.Basic.DownstreamPayload.Parser, new[]{ "Command", "SeqId", "ErrorCode", "PayloadData", "ErrorMsg", "ErrorData", "SubBiz", "KlinkPushId" }, null, null, null, null)
          }));
    }
    #endregion

  }
  #region Messages
  [global::System.Diagnostics.DebuggerDisplayAttribute("{ToString(),nq}")]
  public sealed partial class DownstreamPayload : pb::IMessage<DownstreamPayload>
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      , pb::IBufferMessage
  #endif
  {
    private static readonly pb::MessageParser<DownstreamPayload> _parser = new pb::MessageParser<DownstreamPayload>(() => new DownstreamPayload());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pb::MessageParser<DownstreamPayload> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::AcFunDanmu.Im.Basic.DownstreamPayloadReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public DownstreamPayload() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public DownstreamPayload(DownstreamPayload other) : this() {
      command_ = other.command_;
      seqId_ = other.seqId_;
      errorCode_ = other.errorCode_;
      payloadData_ = other.payloadData_;
      errorMsg_ = other.errorMsg_;
      errorData_ = other.errorData_;
      subBiz_ = other.subBiz_;
      klinkPushId_ = other.klinkPushId_;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public DownstreamPayload Clone() {
      return new DownstreamPayload(this);
    }

    /// <summary>Field number for the "command" field.</summary>
    public const int CommandFieldNumber = 1;
    private string command_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public string Command {
      get { return command_; }
      set {
        command_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "seqId" field.</summary>
    public const int SeqIdFieldNumber = 2;
    private long seqId_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public long SeqId {
      get { return seqId_; }
      set {
        seqId_ = value;
      }
    }

    /// <summary>Field number for the "errorCode" field.</summary>
    public const int ErrorCodeFieldNumber = 3;
    private int errorCode_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public int ErrorCode {
      get { return errorCode_; }
      set {
        errorCode_ = value;
      }
    }

    /// <summary>Field number for the "payloadData" field.</summary>
    public const int PayloadDataFieldNumber = 4;
    private pb::ByteString payloadData_ = pb::ByteString.Empty;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public pb::ByteString PayloadData {
      get { return payloadData_; }
      set {
        payloadData_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "errorMsg" field.</summary>
    public const int ErrorMsgFieldNumber = 5;
    private string errorMsg_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public string ErrorMsg {
      get { return errorMsg_; }
      set {
        errorMsg_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "errorData" field.</summary>
    public const int ErrorDataFieldNumber = 6;
    private pb::ByteString errorData_ = pb::ByteString.Empty;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public pb::ByteString ErrorData {
      get { return errorData_; }
      set {
        errorData_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "subBiz" field.</summary>
    public const int SubBizFieldNumber = 7;
    private string subBiz_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public string SubBiz {
      get { return subBiz_; }
      set {
        subBiz_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "klinkPushId" field.</summary>
    public const int KlinkPushIdFieldNumber = 8;
    private long klinkPushId_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public long KlinkPushId {
      get { return klinkPushId_; }
      set {
        klinkPushId_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override bool Equals(object other) {
      return Equals(other as DownstreamPayload);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool Equals(DownstreamPayload other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (Command != other.Command) return false;
      if (SeqId != other.SeqId) return false;
      if (ErrorCode != other.ErrorCode) return false;
      if (PayloadData != other.PayloadData) return false;
      if (ErrorMsg != other.ErrorMsg) return false;
      if (ErrorData != other.ErrorData) return false;
      if (SubBiz != other.SubBiz) return false;
      if (KlinkPushId != other.KlinkPushId) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override int GetHashCode() {
      int hash = 1;
      if (Command.Length != 0) hash ^= Command.GetHashCode();
      if (SeqId != 0L) hash ^= SeqId.GetHashCode();
      if (ErrorCode != 0) hash ^= ErrorCode.GetHashCode();
      if (PayloadData.Length != 0) hash ^= PayloadData.GetHashCode();
      if (ErrorMsg.Length != 0) hash ^= ErrorMsg.GetHashCode();
      if (ErrorData.Length != 0) hash ^= ErrorData.GetHashCode();
      if (SubBiz.Length != 0) hash ^= SubBiz.GetHashCode();
      if (KlinkPushId != 0L) hash ^= KlinkPushId.GetHashCode();
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
      if (Command.Length != 0) {
        output.WriteRawTag(10);
        output.WriteString(Command);
      }
      if (SeqId != 0L) {
        output.WriteRawTag(16);
        output.WriteInt64(SeqId);
      }
      if (ErrorCode != 0) {
        output.WriteRawTag(24);
        output.WriteInt32(ErrorCode);
      }
      if (PayloadData.Length != 0) {
        output.WriteRawTag(34);
        output.WriteBytes(PayloadData);
      }
      if (ErrorMsg.Length != 0) {
        output.WriteRawTag(42);
        output.WriteString(ErrorMsg);
      }
      if (ErrorData.Length != 0) {
        output.WriteRawTag(50);
        output.WriteBytes(ErrorData);
      }
      if (SubBiz.Length != 0) {
        output.WriteRawTag(58);
        output.WriteString(SubBiz);
      }
      if (KlinkPushId != 0L) {
        output.WriteRawTag(64);
        output.WriteInt64(KlinkPushId);
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
      if (Command.Length != 0) {
        output.WriteRawTag(10);
        output.WriteString(Command);
      }
      if (SeqId != 0L) {
        output.WriteRawTag(16);
        output.WriteInt64(SeqId);
      }
      if (ErrorCode != 0) {
        output.WriteRawTag(24);
        output.WriteInt32(ErrorCode);
      }
      if (PayloadData.Length != 0) {
        output.WriteRawTag(34);
        output.WriteBytes(PayloadData);
      }
      if (ErrorMsg.Length != 0) {
        output.WriteRawTag(42);
        output.WriteString(ErrorMsg);
      }
      if (ErrorData.Length != 0) {
        output.WriteRawTag(50);
        output.WriteBytes(ErrorData);
      }
      if (SubBiz.Length != 0) {
        output.WriteRawTag(58);
        output.WriteString(SubBiz);
      }
      if (KlinkPushId != 0L) {
        output.WriteRawTag(64);
        output.WriteInt64(KlinkPushId);
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
      if (Command.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(Command);
      }
      if (SeqId != 0L) {
        size += 1 + pb::CodedOutputStream.ComputeInt64Size(SeqId);
      }
      if (ErrorCode != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(ErrorCode);
      }
      if (PayloadData.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeBytesSize(PayloadData);
      }
      if (ErrorMsg.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(ErrorMsg);
      }
      if (ErrorData.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeBytesSize(ErrorData);
      }
      if (SubBiz.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(SubBiz);
      }
      if (KlinkPushId != 0L) {
        size += 1 + pb::CodedOutputStream.ComputeInt64Size(KlinkPushId);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void MergeFrom(DownstreamPayload other) {
      if (other == null) {
        return;
      }
      if (other.Command.Length != 0) {
        Command = other.Command;
      }
      if (other.SeqId != 0L) {
        SeqId = other.SeqId;
      }
      if (other.ErrorCode != 0) {
        ErrorCode = other.ErrorCode;
      }
      if (other.PayloadData.Length != 0) {
        PayloadData = other.PayloadData;
      }
      if (other.ErrorMsg.Length != 0) {
        ErrorMsg = other.ErrorMsg;
      }
      if (other.ErrorData.Length != 0) {
        ErrorData = other.ErrorData;
      }
      if (other.SubBiz.Length != 0) {
        SubBiz = other.SubBiz;
      }
      if (other.KlinkPushId != 0L) {
        KlinkPushId = other.KlinkPushId;
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
            Command = input.ReadString();
            break;
          }
          case 16: {
            SeqId = input.ReadInt64();
            break;
          }
          case 24: {
            ErrorCode = input.ReadInt32();
            break;
          }
          case 34: {
            PayloadData = input.ReadBytes();
            break;
          }
          case 42: {
            ErrorMsg = input.ReadString();
            break;
          }
          case 50: {
            ErrorData = input.ReadBytes();
            break;
          }
          case 58: {
            SubBiz = input.ReadString();
            break;
          }
          case 64: {
            KlinkPushId = input.ReadInt64();
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
            Command = input.ReadString();
            break;
          }
          case 16: {
            SeqId = input.ReadInt64();
            break;
          }
          case 24: {
            ErrorCode = input.ReadInt32();
            break;
          }
          case 34: {
            PayloadData = input.ReadBytes();
            break;
          }
          case 42: {
            ErrorMsg = input.ReadString();
            break;
          }
          case 50: {
            ErrorData = input.ReadBytes();
            break;
          }
          case 58: {
            SubBiz = input.ReadString();
            break;
          }
          case 64: {
            KlinkPushId = input.ReadInt64();
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
