// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: MessageStatusSettingListResponse.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace AcFunDanmu.Im.Message {

  /// <summary>Holder for reflection information generated from MessageStatusSettingListResponse.proto</summary>
  public static partial class MessageStatusSettingListResponseReflection {

    #region Descriptor
    /// <summary>File descriptor for MessageStatusSettingListResponse.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static MessageStatusSettingListResponseReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "CiZNZXNzYWdlU3RhdHVzU2V0dGluZ0xpc3RSZXNwb25zZS5wcm90bxIVQWNG",
            "dW5EYW5tdS5JbS5NZXNzYWdlGhFDaGF0U2Vzc2lvbi5wcm90byJ8CiBNZXNz",
            "YWdlU3RhdHVzU2V0dGluZ0xpc3RSZXNwb25zZRIzCgdzZXNzaW9uGAEgAygL",
            "MiIuQWNGdW5EYW5tdS5JbS5NZXNzYWdlLkNoYXRTZXNzaW9uEhIKCm5leHRP",
            "ZmZzZXQYAiABKAkSDwoHaGFzTW9yZRgDIAEoCGIGcHJvdG8z"));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { global::AcFunDanmu.Im.Message.ChatSessionReflection.Descriptor, },
          new pbr::GeneratedClrTypeInfo(null, null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::AcFunDanmu.Im.Message.MessageStatusSettingListResponse), global::AcFunDanmu.Im.Message.MessageStatusSettingListResponse.Parser, new[]{ "Session", "NextOffset", "HasMore" }, null, null, null, null)
          }));
    }
    #endregion

  }
  #region Messages
  public sealed partial class MessageStatusSettingListResponse : pb::IMessage<MessageStatusSettingListResponse>
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      , pb::IBufferMessage
  #endif
  {
    private static readonly pb::MessageParser<MessageStatusSettingListResponse> _parser = new pb::MessageParser<MessageStatusSettingListResponse>(() => new MessageStatusSettingListResponse());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pb::MessageParser<MessageStatusSettingListResponse> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::AcFunDanmu.Im.Message.MessageStatusSettingListResponseReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public MessageStatusSettingListResponse() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public MessageStatusSettingListResponse(MessageStatusSettingListResponse other) : this() {
      session_ = other.session_.Clone();
      nextOffset_ = other.nextOffset_;
      hasMore_ = other.hasMore_;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public MessageStatusSettingListResponse Clone() {
      return new MessageStatusSettingListResponse(this);
    }

    /// <summary>Field number for the "session" field.</summary>
    public const int SessionFieldNumber = 1;
    private static readonly pb::FieldCodec<global::AcFunDanmu.Im.Message.ChatSession> _repeated_session_codec
        = pb::FieldCodec.ForMessage(10, global::AcFunDanmu.Im.Message.ChatSession.Parser);
    private readonly pbc::RepeatedField<global::AcFunDanmu.Im.Message.ChatSession> session_ = new pbc::RepeatedField<global::AcFunDanmu.Im.Message.ChatSession>();
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public pbc::RepeatedField<global::AcFunDanmu.Im.Message.ChatSession> Session {
      get { return session_; }
    }

    /// <summary>Field number for the "nextOffset" field.</summary>
    public const int NextOffsetFieldNumber = 2;
    private string nextOffset_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public string NextOffset {
      get { return nextOffset_; }
      set {
        nextOffset_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "hasMore" field.</summary>
    public const int HasMoreFieldNumber = 3;
    private bool hasMore_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool HasMore {
      get { return hasMore_; }
      set {
        hasMore_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override bool Equals(object other) {
      return Equals(other as MessageStatusSettingListResponse);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool Equals(MessageStatusSettingListResponse other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if(!session_.Equals(other.session_)) return false;
      if (NextOffset != other.NextOffset) return false;
      if (HasMore != other.HasMore) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override int GetHashCode() {
      int hash = 1;
      hash ^= session_.GetHashCode();
      if (NextOffset.Length != 0) hash ^= NextOffset.GetHashCode();
      if (HasMore != false) hash ^= HasMore.GetHashCode();
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
      session_.WriteTo(output, _repeated_session_codec);
      if (NextOffset.Length != 0) {
        output.WriteRawTag(18);
        output.WriteString(NextOffset);
      }
      if (HasMore != false) {
        output.WriteRawTag(24);
        output.WriteBool(HasMore);
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
      session_.WriteTo(ref output, _repeated_session_codec);
      if (NextOffset.Length != 0) {
        output.WriteRawTag(18);
        output.WriteString(NextOffset);
      }
      if (HasMore != false) {
        output.WriteRawTag(24);
        output.WriteBool(HasMore);
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
      size += session_.CalculateSize(_repeated_session_codec);
      if (NextOffset.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(NextOffset);
      }
      if (HasMore != false) {
        size += 1 + 1;
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void MergeFrom(MessageStatusSettingListResponse other) {
      if (other == null) {
        return;
      }
      session_.Add(other.session_);
      if (other.NextOffset.Length != 0) {
        NextOffset = other.NextOffset;
      }
      if (other.HasMore != false) {
        HasMore = other.HasMore;
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
            session_.AddEntriesFrom(input, _repeated_session_codec);
            break;
          }
          case 18: {
            NextOffset = input.ReadString();
            break;
          }
          case 24: {
            HasMore = input.ReadBool();
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
            session_.AddEntriesFrom(ref input, _repeated_session_codec);
            break;
          }
          case 18: {
            NextOffset = input.ReadString();
            break;
          }
          case 24: {
            HasMore = input.ReadBool();
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