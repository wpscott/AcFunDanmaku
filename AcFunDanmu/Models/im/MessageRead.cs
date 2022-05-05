// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: MessageRead.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace AcFunDanmu.Im.Message {

  /// <summary>Holder for reflection information generated from MessageRead.proto</summary>
  public static partial class MessageReadReflection {

    #region Descriptor
    /// <summary>File descriptor for MessageRead.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static MessageReadReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "ChFNZXNzYWdlUmVhZC5wcm90bxIVQWNGdW5EYW5tdS5JbS5NZXNzYWdlGgpV",
            "c2VyLnByb3RvGhRDaGF0VGFyZ2V0VHlwZS5wcm90byK2AQoSTWVzc2FnZVJl",
            "YWRSZXF1ZXN0EikKBnRhcmdldBgBIAEoCzIZLkFjRnVuRGFubXUuSW0uQmFz",
            "aWMuVXNlchIPCgdyZWFkU2VxGAIgASgDEhAKCHRhcmdldElkGAMgASgDEhMK",
            "C3N0clRhcmdldElkGAQgASgJEj0KDmNoYXRUYXJnZXRUeXBlGAUgASgOMiUu",
            "QWNGdW5EYW5tdS5JbS5NZXNzYWdlLkNoYXRUYXJnZXRUeXBlIhUKE01lc3Nh",
            "Z2VSZWFkUmVzcG9uc2UiiAEKD01lc3NhZ2VSZWFkUHVzaBIQCgh0YXJnZXRJ",
            "ZBgBIAEoAxIPCgdyZWFkU2VxGAIgASgDEj0KDmNoYXRUYXJnZXRUeXBlGAMg",
            "ASgOMiUuQWNGdW5EYW5tdS5JbS5NZXNzYWdlLkNoYXRUYXJnZXRUeXBlEhMK",
            "C3N0clRhcmdldElkGAQgASgJYgZwcm90bzM="));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { global::AcFunDanmu.Im.Basic.UserReflection.Descriptor, global::AcFunDanmu.Im.Message.ChatTargetTypeReflection.Descriptor, },
          new pbr::GeneratedClrTypeInfo(null, null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::AcFunDanmu.Im.Message.MessageReadRequest), global::AcFunDanmu.Im.Message.MessageReadRequest.Parser, new[]{ "Target", "ReadSeq", "TargetId", "StrTargetId", "ChatTargetType" }, null, null, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::AcFunDanmu.Im.Message.MessageReadResponse), global::AcFunDanmu.Im.Message.MessageReadResponse.Parser, null, null, null, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::AcFunDanmu.Im.Message.MessageReadPush), global::AcFunDanmu.Im.Message.MessageReadPush.Parser, new[]{ "TargetId", "ReadSeq", "ChatTargetType", "StrTargetId" }, null, null, null, null)
          }));
    }
    #endregion

  }
  #region Messages
  public sealed partial class MessageReadRequest : pb::IMessage<MessageReadRequest>
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      , pb::IBufferMessage
  #endif
  {
    private static readonly pb::MessageParser<MessageReadRequest> _parser = new pb::MessageParser<MessageReadRequest>(() => new MessageReadRequest());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pb::MessageParser<MessageReadRequest> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::AcFunDanmu.Im.Message.MessageReadReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public MessageReadRequest() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public MessageReadRequest(MessageReadRequest other) : this() {
      target_ = other.target_ != null ? other.target_.Clone() : null;
      readSeq_ = other.readSeq_;
      targetId_ = other.targetId_;
      strTargetId_ = other.strTargetId_;
      chatTargetType_ = other.chatTargetType_;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public MessageReadRequest Clone() {
      return new MessageReadRequest(this);
    }

    /// <summary>Field number for the "target" field.</summary>
    public const int TargetFieldNumber = 1;
    private global::AcFunDanmu.Im.Basic.User target_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public global::AcFunDanmu.Im.Basic.User Target {
      get { return target_; }
      set {
        target_ = value;
      }
    }

    /// <summary>Field number for the "readSeq" field.</summary>
    public const int ReadSeqFieldNumber = 2;
    private long readSeq_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public long ReadSeq {
      get { return readSeq_; }
      set {
        readSeq_ = value;
      }
    }

    /// <summary>Field number for the "targetId" field.</summary>
    public const int TargetIdFieldNumber = 3;
    private long targetId_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public long TargetId {
      get { return targetId_; }
      set {
        targetId_ = value;
      }
    }

    /// <summary>Field number for the "strTargetId" field.</summary>
    public const int StrTargetIdFieldNumber = 4;
    private string strTargetId_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public string StrTargetId {
      get { return strTargetId_; }
      set {
        strTargetId_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "chatTargetType" field.</summary>
    public const int ChatTargetTypeFieldNumber = 5;
    private global::AcFunDanmu.Im.Message.ChatTargetType chatTargetType_ = global::AcFunDanmu.Im.Message.ChatTargetType.CttUser;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public global::AcFunDanmu.Im.Message.ChatTargetType ChatTargetType {
      get { return chatTargetType_; }
      set {
        chatTargetType_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override bool Equals(object other) {
      return Equals(other as MessageReadRequest);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool Equals(MessageReadRequest other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (!object.Equals(Target, other.Target)) return false;
      if (ReadSeq != other.ReadSeq) return false;
      if (TargetId != other.TargetId) return false;
      if (StrTargetId != other.StrTargetId) return false;
      if (ChatTargetType != other.ChatTargetType) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override int GetHashCode() {
      int hash = 1;
      if (target_ != null) hash ^= Target.GetHashCode();
      if (ReadSeq != 0L) hash ^= ReadSeq.GetHashCode();
      if (TargetId != 0L) hash ^= TargetId.GetHashCode();
      if (StrTargetId.Length != 0) hash ^= StrTargetId.GetHashCode();
      if (ChatTargetType != global::AcFunDanmu.Im.Message.ChatTargetType.CttUser) hash ^= ChatTargetType.GetHashCode();
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
      if (target_ != null) {
        output.WriteRawTag(10);
        output.WriteMessage(Target);
      }
      if (ReadSeq != 0L) {
        output.WriteRawTag(16);
        output.WriteInt64(ReadSeq);
      }
      if (TargetId != 0L) {
        output.WriteRawTag(24);
        output.WriteInt64(TargetId);
      }
      if (StrTargetId.Length != 0) {
        output.WriteRawTag(34);
        output.WriteString(StrTargetId);
      }
      if (ChatTargetType != global::AcFunDanmu.Im.Message.ChatTargetType.CttUser) {
        output.WriteRawTag(40);
        output.WriteEnum((int) ChatTargetType);
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
      if (target_ != null) {
        output.WriteRawTag(10);
        output.WriteMessage(Target);
      }
      if (ReadSeq != 0L) {
        output.WriteRawTag(16);
        output.WriteInt64(ReadSeq);
      }
      if (TargetId != 0L) {
        output.WriteRawTag(24);
        output.WriteInt64(TargetId);
      }
      if (StrTargetId.Length != 0) {
        output.WriteRawTag(34);
        output.WriteString(StrTargetId);
      }
      if (ChatTargetType != global::AcFunDanmu.Im.Message.ChatTargetType.CttUser) {
        output.WriteRawTag(40);
        output.WriteEnum((int) ChatTargetType);
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
      if (target_ != null) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(Target);
      }
      if (ReadSeq != 0L) {
        size += 1 + pb::CodedOutputStream.ComputeInt64Size(ReadSeq);
      }
      if (TargetId != 0L) {
        size += 1 + pb::CodedOutputStream.ComputeInt64Size(TargetId);
      }
      if (StrTargetId.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(StrTargetId);
      }
      if (ChatTargetType != global::AcFunDanmu.Im.Message.ChatTargetType.CttUser) {
        size += 1 + pb::CodedOutputStream.ComputeEnumSize((int) ChatTargetType);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void MergeFrom(MessageReadRequest other) {
      if (other == null) {
        return;
      }
      if (other.target_ != null) {
        if (target_ == null) {
          Target = new global::AcFunDanmu.Im.Basic.User();
        }
        Target.MergeFrom(other.Target);
      }
      if (other.ReadSeq != 0L) {
        ReadSeq = other.ReadSeq;
      }
      if (other.TargetId != 0L) {
        TargetId = other.TargetId;
      }
      if (other.StrTargetId.Length != 0) {
        StrTargetId = other.StrTargetId;
      }
      if (other.ChatTargetType != global::AcFunDanmu.Im.Message.ChatTargetType.CttUser) {
        ChatTargetType = other.ChatTargetType;
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
            if (target_ == null) {
              Target = new global::AcFunDanmu.Im.Basic.User();
            }
            input.ReadMessage(Target);
            break;
          }
          case 16: {
            ReadSeq = input.ReadInt64();
            break;
          }
          case 24: {
            TargetId = input.ReadInt64();
            break;
          }
          case 34: {
            StrTargetId = input.ReadString();
            break;
          }
          case 40: {
            ChatTargetType = (global::AcFunDanmu.Im.Message.ChatTargetType) input.ReadEnum();
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
            if (target_ == null) {
              Target = new global::AcFunDanmu.Im.Basic.User();
            }
            input.ReadMessage(Target);
            break;
          }
          case 16: {
            ReadSeq = input.ReadInt64();
            break;
          }
          case 24: {
            TargetId = input.ReadInt64();
            break;
          }
          case 34: {
            StrTargetId = input.ReadString();
            break;
          }
          case 40: {
            ChatTargetType = (global::AcFunDanmu.Im.Message.ChatTargetType) input.ReadEnum();
            break;
          }
        }
      }
    }
    #endif

  }

  public sealed partial class MessageReadResponse : pb::IMessage<MessageReadResponse>
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      , pb::IBufferMessage
  #endif
  {
    private static readonly pb::MessageParser<MessageReadResponse> _parser = new pb::MessageParser<MessageReadResponse>(() => new MessageReadResponse());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pb::MessageParser<MessageReadResponse> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::AcFunDanmu.Im.Message.MessageReadReflection.Descriptor.MessageTypes[1]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public MessageReadResponse() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public MessageReadResponse(MessageReadResponse other) : this() {
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public MessageReadResponse Clone() {
      return new MessageReadResponse(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override bool Equals(object other) {
      return Equals(other as MessageReadResponse);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool Equals(MessageReadResponse other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override int GetHashCode() {
      int hash = 1;
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
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    void pb::IBufferMessage.InternalWriteTo(ref pb::WriteContext output) {
      if (_unknownFields != null) {
        _unknownFields.WriteTo(ref output);
      }
    }
    #endif

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public int CalculateSize() {
      int size = 0;
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void MergeFrom(MessageReadResponse other) {
      if (other == null) {
        return;
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
        }
      }
    }
    #endif

  }

  public sealed partial class MessageReadPush : pb::IMessage<MessageReadPush>
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      , pb::IBufferMessage
  #endif
  {
    private static readonly pb::MessageParser<MessageReadPush> _parser = new pb::MessageParser<MessageReadPush>(() => new MessageReadPush());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pb::MessageParser<MessageReadPush> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::AcFunDanmu.Im.Message.MessageReadReflection.Descriptor.MessageTypes[2]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public MessageReadPush() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public MessageReadPush(MessageReadPush other) : this() {
      targetId_ = other.targetId_;
      readSeq_ = other.readSeq_;
      chatTargetType_ = other.chatTargetType_;
      strTargetId_ = other.strTargetId_;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public MessageReadPush Clone() {
      return new MessageReadPush(this);
    }

    /// <summary>Field number for the "targetId" field.</summary>
    public const int TargetIdFieldNumber = 1;
    private long targetId_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public long TargetId {
      get { return targetId_; }
      set {
        targetId_ = value;
      }
    }

    /// <summary>Field number for the "readSeq" field.</summary>
    public const int ReadSeqFieldNumber = 2;
    private long readSeq_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public long ReadSeq {
      get { return readSeq_; }
      set {
        readSeq_ = value;
      }
    }

    /// <summary>Field number for the "chatTargetType" field.</summary>
    public const int ChatTargetTypeFieldNumber = 3;
    private global::AcFunDanmu.Im.Message.ChatTargetType chatTargetType_ = global::AcFunDanmu.Im.Message.ChatTargetType.CttUser;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public global::AcFunDanmu.Im.Message.ChatTargetType ChatTargetType {
      get { return chatTargetType_; }
      set {
        chatTargetType_ = value;
      }
    }

    /// <summary>Field number for the "strTargetId" field.</summary>
    public const int StrTargetIdFieldNumber = 4;
    private string strTargetId_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public string StrTargetId {
      get { return strTargetId_; }
      set {
        strTargetId_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override bool Equals(object other) {
      return Equals(other as MessageReadPush);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool Equals(MessageReadPush other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (TargetId != other.TargetId) return false;
      if (ReadSeq != other.ReadSeq) return false;
      if (ChatTargetType != other.ChatTargetType) return false;
      if (StrTargetId != other.StrTargetId) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override int GetHashCode() {
      int hash = 1;
      if (TargetId != 0L) hash ^= TargetId.GetHashCode();
      if (ReadSeq != 0L) hash ^= ReadSeq.GetHashCode();
      if (ChatTargetType != global::AcFunDanmu.Im.Message.ChatTargetType.CttUser) hash ^= ChatTargetType.GetHashCode();
      if (StrTargetId.Length != 0) hash ^= StrTargetId.GetHashCode();
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
      if (TargetId != 0L) {
        output.WriteRawTag(8);
        output.WriteInt64(TargetId);
      }
      if (ReadSeq != 0L) {
        output.WriteRawTag(16);
        output.WriteInt64(ReadSeq);
      }
      if (ChatTargetType != global::AcFunDanmu.Im.Message.ChatTargetType.CttUser) {
        output.WriteRawTag(24);
        output.WriteEnum((int) ChatTargetType);
      }
      if (StrTargetId.Length != 0) {
        output.WriteRawTag(34);
        output.WriteString(StrTargetId);
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
      if (TargetId != 0L) {
        output.WriteRawTag(8);
        output.WriteInt64(TargetId);
      }
      if (ReadSeq != 0L) {
        output.WriteRawTag(16);
        output.WriteInt64(ReadSeq);
      }
      if (ChatTargetType != global::AcFunDanmu.Im.Message.ChatTargetType.CttUser) {
        output.WriteRawTag(24);
        output.WriteEnum((int) ChatTargetType);
      }
      if (StrTargetId.Length != 0) {
        output.WriteRawTag(34);
        output.WriteString(StrTargetId);
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
      if (TargetId != 0L) {
        size += 1 + pb::CodedOutputStream.ComputeInt64Size(TargetId);
      }
      if (ReadSeq != 0L) {
        size += 1 + pb::CodedOutputStream.ComputeInt64Size(ReadSeq);
      }
      if (ChatTargetType != global::AcFunDanmu.Im.Message.ChatTargetType.CttUser) {
        size += 1 + pb::CodedOutputStream.ComputeEnumSize((int) ChatTargetType);
      }
      if (StrTargetId.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(StrTargetId);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void MergeFrom(MessageReadPush other) {
      if (other == null) {
        return;
      }
      if (other.TargetId != 0L) {
        TargetId = other.TargetId;
      }
      if (other.ReadSeq != 0L) {
        ReadSeq = other.ReadSeq;
      }
      if (other.ChatTargetType != global::AcFunDanmu.Im.Message.ChatTargetType.CttUser) {
        ChatTargetType = other.ChatTargetType;
      }
      if (other.StrTargetId.Length != 0) {
        StrTargetId = other.StrTargetId;
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
            TargetId = input.ReadInt64();
            break;
          }
          case 16: {
            ReadSeq = input.ReadInt64();
            break;
          }
          case 24: {
            ChatTargetType = (global::AcFunDanmu.Im.Message.ChatTargetType) input.ReadEnum();
            break;
          }
          case 34: {
            StrTargetId = input.ReadString();
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
            TargetId = input.ReadInt64();
            break;
          }
          case 16: {
            ReadSeq = input.ReadInt64();
            break;
          }
          case 24: {
            ChatTargetType = (global::AcFunDanmu.Im.Message.ChatTargetType) input.ReadEnum();
            break;
          }
          case 34: {
            StrTargetId = input.ReadString();
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
