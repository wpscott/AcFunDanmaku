// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: SessionExtraSet.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace AcFunDanmu.Im.Message {

  /// <summary>Holder for reflection information generated from SessionExtraSet.proto</summary>
  public static partial class SessionExtraSetReflection {

    #region Descriptor
    /// <summary>File descriptor for SessionExtraSet.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static SessionExtraSetReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "ChVTZXNzaW9uRXh0cmFTZXQucHJvdG8SFUFjRnVuRGFubXUuSW0uTWVzc2Fn",
            "ZRoQQ2hhdFRhcmdldC5wcm90bxoRQ2hhdFNlc3Npb24ucHJvdG8iXgoWU2Vz",
            "c2lvbkV4dHJhU2V0UmVxdWVzdBI1CgpjaGF0VGFyZ2V0GAEgASgLMiEuQWNG",
            "dW5EYW5tdS5JbS5NZXNzYWdlLkNoYXRUYXJnZXQSDQoFZXh0cmEYAiABKAwi",
            "TgoXU2Vzc2lvbkV4dHJhU2V0UmVzcG9uc2USMwoHc2Vzc2lvbhgBIAEoCzIi",
            "LkFjRnVuRGFubXUuSW0uTWVzc2FnZS5DaGF0U2Vzc2lvbmIGcHJvdG8z"));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { global::AcFunDanmu.Im.Message.ChatTargetReflection.Descriptor, global::AcFunDanmu.Im.Message.ChatSessionReflection.Descriptor, },
          new pbr::GeneratedClrTypeInfo(null, null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::AcFunDanmu.Im.Message.SessionExtraSetRequest), global::AcFunDanmu.Im.Message.SessionExtraSetRequest.Parser, new[]{ "ChatTarget", "Extra" }, null, null, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::AcFunDanmu.Im.Message.SessionExtraSetResponse), global::AcFunDanmu.Im.Message.SessionExtraSetResponse.Parser, new[]{ "Session" }, null, null, null, null)
          }));
    }
    #endregion

  }
  #region Messages
  public sealed partial class SessionExtraSetRequest : pb::IMessage<SessionExtraSetRequest>
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      , pb::IBufferMessage
  #endif
  {
    private static readonly pb::MessageParser<SessionExtraSetRequest> _parser = new pb::MessageParser<SessionExtraSetRequest>(() => new SessionExtraSetRequest());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pb::MessageParser<SessionExtraSetRequest> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::AcFunDanmu.Im.Message.SessionExtraSetReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public SessionExtraSetRequest() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public SessionExtraSetRequest(SessionExtraSetRequest other) : this() {
      chatTarget_ = other.chatTarget_ != null ? other.chatTarget_.Clone() : null;
      extra_ = other.extra_;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public SessionExtraSetRequest Clone() {
      return new SessionExtraSetRequest(this);
    }

    /// <summary>Field number for the "chatTarget" field.</summary>
    public const int ChatTargetFieldNumber = 1;
    private global::AcFunDanmu.Im.Message.ChatTarget chatTarget_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public global::AcFunDanmu.Im.Message.ChatTarget ChatTarget {
      get { return chatTarget_; }
      set {
        chatTarget_ = value;
      }
    }

    /// <summary>Field number for the "extra" field.</summary>
    public const int ExtraFieldNumber = 2;
    private pb::ByteString extra_ = pb::ByteString.Empty;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public pb::ByteString Extra {
      get { return extra_; }
      set {
        extra_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override bool Equals(object other) {
      return Equals(other as SessionExtraSetRequest);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool Equals(SessionExtraSetRequest other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (!object.Equals(ChatTarget, other.ChatTarget)) return false;
      if (Extra != other.Extra) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override int GetHashCode() {
      int hash = 1;
      if (chatTarget_ != null) hash ^= ChatTarget.GetHashCode();
      if (Extra.Length != 0) hash ^= Extra.GetHashCode();
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
      if (chatTarget_ != null) {
        output.WriteRawTag(10);
        output.WriteMessage(ChatTarget);
      }
      if (Extra.Length != 0) {
        output.WriteRawTag(18);
        output.WriteBytes(Extra);
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
      if (chatTarget_ != null) {
        output.WriteRawTag(10);
        output.WriteMessage(ChatTarget);
      }
      if (Extra.Length != 0) {
        output.WriteRawTag(18);
        output.WriteBytes(Extra);
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
      if (chatTarget_ != null) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(ChatTarget);
      }
      if (Extra.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeBytesSize(Extra);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void MergeFrom(SessionExtraSetRequest other) {
      if (other == null) {
        return;
      }
      if (other.chatTarget_ != null) {
        if (chatTarget_ == null) {
          ChatTarget = new global::AcFunDanmu.Im.Message.ChatTarget();
        }
        ChatTarget.MergeFrom(other.ChatTarget);
      }
      if (other.Extra.Length != 0) {
        Extra = other.Extra;
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
            if (chatTarget_ == null) {
              ChatTarget = new global::AcFunDanmu.Im.Message.ChatTarget();
            }
            input.ReadMessage(ChatTarget);
            break;
          }
          case 18: {
            Extra = input.ReadBytes();
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
            if (chatTarget_ == null) {
              ChatTarget = new global::AcFunDanmu.Im.Message.ChatTarget();
            }
            input.ReadMessage(ChatTarget);
            break;
          }
          case 18: {
            Extra = input.ReadBytes();
            break;
          }
        }
      }
    }
    #endif

  }

  public sealed partial class SessionExtraSetResponse : pb::IMessage<SessionExtraSetResponse>
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      , pb::IBufferMessage
  #endif
  {
    private static readonly pb::MessageParser<SessionExtraSetResponse> _parser = new pb::MessageParser<SessionExtraSetResponse>(() => new SessionExtraSetResponse());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pb::MessageParser<SessionExtraSetResponse> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::AcFunDanmu.Im.Message.SessionExtraSetReflection.Descriptor.MessageTypes[1]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public SessionExtraSetResponse() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public SessionExtraSetResponse(SessionExtraSetResponse other) : this() {
      session_ = other.session_ != null ? other.session_.Clone() : null;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public SessionExtraSetResponse Clone() {
      return new SessionExtraSetResponse(this);
    }

    /// <summary>Field number for the "session" field.</summary>
    public const int SessionFieldNumber = 1;
    private global::AcFunDanmu.Im.Message.ChatSession session_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public global::AcFunDanmu.Im.Message.ChatSession Session {
      get { return session_; }
      set {
        session_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override bool Equals(object other) {
      return Equals(other as SessionExtraSetResponse);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool Equals(SessionExtraSetResponse other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (!object.Equals(Session, other.Session)) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override int GetHashCode() {
      int hash = 1;
      if (session_ != null) hash ^= Session.GetHashCode();
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
      if (session_ != null) {
        output.WriteRawTag(10);
        output.WriteMessage(Session);
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
      if (session_ != null) {
        output.WriteRawTag(10);
        output.WriteMessage(Session);
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
      if (session_ != null) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(Session);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void MergeFrom(SessionExtraSetResponse other) {
      if (other == null) {
        return;
      }
      if (other.session_ != null) {
        if (session_ == null) {
          Session = new global::AcFunDanmu.Im.Message.ChatSession();
        }
        Session.MergeFrom(other.Session);
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
            if (session_ == null) {
              Session = new global::AcFunDanmu.Im.Message.ChatSession();
            }
            input.ReadMessage(Session);
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
            if (session_ == null) {
              Session = new global::AcFunDanmu.Im.Message.ChatSession();
            }
            input.ReadMessage(Session);
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
