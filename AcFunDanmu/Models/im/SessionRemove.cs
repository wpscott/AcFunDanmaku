// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: SessionRemove.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace AcFunDanmu.Im.Message {

  /// <summary>Holder for reflection information generated from SessionRemove.proto</summary>
  public static partial class SessionRemoveReflection {

    #region Descriptor
    /// <summary>File descriptor for SessionRemove.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static SessionRemoveReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "ChNTZXNzaW9uUmVtb3ZlLnByb3RvEhVBY0Z1bkRhbm11LkltLk1lc3NhZ2Ua",
            "FENoYXRUYXJnZXRUeXBlLnByb3RvIq0BChRTZXNzaW9uUmVtb3ZlUmVxdWVz",
            "dBIQCgh0YXJnZXRJZBgBIAEoAxI9Cg5jaGF0VGFyZ2V0VHlwZRgCIAEoDjIl",
            "LkFjRnVuRGFubXUuSW0uTWVzc2FnZS5DaGF0VGFyZ2V0VHlwZRISCgpjYXRl",
            "Z29yeUlkGAMgASgFEhMKC3N0clRhcmdldElkGAQgASgJEhsKE25vdENsZWFu",
            "QWxsTWVzc2FnZXMYBSABKAgiFwoVU2Vzc2lvblJlbW92ZVJlc3BvbnNlYgZw",
            "cm90bzM="));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { global::AcFunDanmu.Im.Message.ChatTargetTypeReflection.Descriptor, },
          new pbr::GeneratedClrTypeInfo(null, null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::AcFunDanmu.Im.Message.SessionRemoveRequest), global::AcFunDanmu.Im.Message.SessionRemoveRequest.Parser, new[]{ "TargetId", "ChatTargetType", "CategoryId", "StrTargetId", "NotCleanAllMessages" }, null, null, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::AcFunDanmu.Im.Message.SessionRemoveResponse), global::AcFunDanmu.Im.Message.SessionRemoveResponse.Parser, null, null, null, null, null)
          }));
    }
    #endregion

  }
  #region Messages
  public sealed partial class SessionRemoveRequest : pb::IMessage<SessionRemoveRequest>
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      , pb::IBufferMessage
  #endif
  {
    private static readonly pb::MessageParser<SessionRemoveRequest> _parser = new pb::MessageParser<SessionRemoveRequest>(() => new SessionRemoveRequest());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pb::MessageParser<SessionRemoveRequest> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::AcFunDanmu.Im.Message.SessionRemoveReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public SessionRemoveRequest() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public SessionRemoveRequest(SessionRemoveRequest other) : this() {
      targetId_ = other.targetId_;
      chatTargetType_ = other.chatTargetType_;
      categoryId_ = other.categoryId_;
      strTargetId_ = other.strTargetId_;
      notCleanAllMessages_ = other.notCleanAllMessages_;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public SessionRemoveRequest Clone() {
      return new SessionRemoveRequest(this);
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

    /// <summary>Field number for the "chatTargetType" field.</summary>
    public const int ChatTargetTypeFieldNumber = 2;
    private global::AcFunDanmu.Im.Message.ChatTargetType chatTargetType_ = global::AcFunDanmu.Im.Message.ChatTargetType.CttUser;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public global::AcFunDanmu.Im.Message.ChatTargetType ChatTargetType {
      get { return chatTargetType_; }
      set {
        chatTargetType_ = value;
      }
    }

    /// <summary>Field number for the "categoryId" field.</summary>
    public const int CategoryIdFieldNumber = 3;
    private int categoryId_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public int CategoryId {
      get { return categoryId_; }
      set {
        categoryId_ = value;
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

    /// <summary>Field number for the "notCleanAllMessages" field.</summary>
    public const int NotCleanAllMessagesFieldNumber = 5;
    private bool notCleanAllMessages_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool NotCleanAllMessages {
      get { return notCleanAllMessages_; }
      set {
        notCleanAllMessages_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override bool Equals(object other) {
      return Equals(other as SessionRemoveRequest);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool Equals(SessionRemoveRequest other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (TargetId != other.TargetId) return false;
      if (ChatTargetType != other.ChatTargetType) return false;
      if (CategoryId != other.CategoryId) return false;
      if (StrTargetId != other.StrTargetId) return false;
      if (NotCleanAllMessages != other.NotCleanAllMessages) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override int GetHashCode() {
      int hash = 1;
      if (TargetId != 0L) hash ^= TargetId.GetHashCode();
      if (ChatTargetType != global::AcFunDanmu.Im.Message.ChatTargetType.CttUser) hash ^= ChatTargetType.GetHashCode();
      if (CategoryId != 0) hash ^= CategoryId.GetHashCode();
      if (StrTargetId.Length != 0) hash ^= StrTargetId.GetHashCode();
      if (NotCleanAllMessages != false) hash ^= NotCleanAllMessages.GetHashCode();
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
      if (ChatTargetType != global::AcFunDanmu.Im.Message.ChatTargetType.CttUser) {
        output.WriteRawTag(16);
        output.WriteEnum((int) ChatTargetType);
      }
      if (CategoryId != 0) {
        output.WriteRawTag(24);
        output.WriteInt32(CategoryId);
      }
      if (StrTargetId.Length != 0) {
        output.WriteRawTag(34);
        output.WriteString(StrTargetId);
      }
      if (NotCleanAllMessages != false) {
        output.WriteRawTag(40);
        output.WriteBool(NotCleanAllMessages);
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
      if (ChatTargetType != global::AcFunDanmu.Im.Message.ChatTargetType.CttUser) {
        output.WriteRawTag(16);
        output.WriteEnum((int) ChatTargetType);
      }
      if (CategoryId != 0) {
        output.WriteRawTag(24);
        output.WriteInt32(CategoryId);
      }
      if (StrTargetId.Length != 0) {
        output.WriteRawTag(34);
        output.WriteString(StrTargetId);
      }
      if (NotCleanAllMessages != false) {
        output.WriteRawTag(40);
        output.WriteBool(NotCleanAllMessages);
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
      if (ChatTargetType != global::AcFunDanmu.Im.Message.ChatTargetType.CttUser) {
        size += 1 + pb::CodedOutputStream.ComputeEnumSize((int) ChatTargetType);
      }
      if (CategoryId != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(CategoryId);
      }
      if (StrTargetId.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(StrTargetId);
      }
      if (NotCleanAllMessages != false) {
        size += 1 + 1;
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void MergeFrom(SessionRemoveRequest other) {
      if (other == null) {
        return;
      }
      if (other.TargetId != 0L) {
        TargetId = other.TargetId;
      }
      if (other.ChatTargetType != global::AcFunDanmu.Im.Message.ChatTargetType.CttUser) {
        ChatTargetType = other.ChatTargetType;
      }
      if (other.CategoryId != 0) {
        CategoryId = other.CategoryId;
      }
      if (other.StrTargetId.Length != 0) {
        StrTargetId = other.StrTargetId;
      }
      if (other.NotCleanAllMessages != false) {
        NotCleanAllMessages = other.NotCleanAllMessages;
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
            ChatTargetType = (global::AcFunDanmu.Im.Message.ChatTargetType) input.ReadEnum();
            break;
          }
          case 24: {
            CategoryId = input.ReadInt32();
            break;
          }
          case 34: {
            StrTargetId = input.ReadString();
            break;
          }
          case 40: {
            NotCleanAllMessages = input.ReadBool();
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
            ChatTargetType = (global::AcFunDanmu.Im.Message.ChatTargetType) input.ReadEnum();
            break;
          }
          case 24: {
            CategoryId = input.ReadInt32();
            break;
          }
          case 34: {
            StrTargetId = input.ReadString();
            break;
          }
          case 40: {
            NotCleanAllMessages = input.ReadBool();
            break;
          }
        }
      }
    }
    #endif

  }

  public sealed partial class SessionRemoveResponse : pb::IMessage<SessionRemoveResponse>
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      , pb::IBufferMessage
  #endif
  {
    private static readonly pb::MessageParser<SessionRemoveResponse> _parser = new pb::MessageParser<SessionRemoveResponse>(() => new SessionRemoveResponse());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pb::MessageParser<SessionRemoveResponse> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::AcFunDanmu.Im.Message.SessionRemoveReflection.Descriptor.MessageTypes[1]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public SessionRemoveResponse() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public SessionRemoveResponse(SessionRemoveResponse other) : this() {
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public SessionRemoveResponse Clone() {
      return new SessionRemoveResponse(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override bool Equals(object other) {
      return Equals(other as SessionRemoveResponse);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool Equals(SessionRemoveResponse other) {
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
    public void MergeFrom(SessionRemoveResponse other) {
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

  #endregion

}

#endregion Designer generated code
