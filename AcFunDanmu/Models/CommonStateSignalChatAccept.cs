// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: CommonStateSignalChatAccept.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace AcFunDanmu {

  /// <summary>Holder for reflection information generated from CommonStateSignalChatAccept.proto</summary>
  public static partial class CommonStateSignalChatAcceptReflection {

    #region Descriptor
    /// <summary>File descriptor for CommonStateSignalChatAccept.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static CommonStateSignalChatAcceptReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "CiFDb21tb25TdGF0ZVNpZ25hbENoYXRBY2NlcHQucHJvdG8SCkFjRnVuRGFu",
            "bXUaE0NoYXRNZWRpYVR5cGUucHJvdG8icwobQ29tbW9uU3RhdGVTaWduYWxD",
            "aGF0QWNjZXB0Eg4KBmNoYXRJZBgBIAEoCRIsCgltZWRpYVR5cGUYAiABKA4y",
            "GS5BY0Z1bkRhbm11LkNoYXRNZWRpYVR5cGUSFgoOYXJ5YVNpZ25hbEluZm8Y",
            "AyABKAliBnByb3RvMw=="));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { global::AcFunDanmu.ChatMediaTypeReflection.Descriptor, },
          new pbr::GeneratedClrTypeInfo(null, null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::AcFunDanmu.CommonStateSignalChatAccept), global::AcFunDanmu.CommonStateSignalChatAccept.Parser, new[]{ "ChatId", "MediaType", "AryaSignalInfo" }, null, null, null, null)
          }));
    }
    #endregion

  }
  #region Messages
  public sealed partial class CommonStateSignalChatAccept : pb::IMessage<CommonStateSignalChatAccept>
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      , pb::IBufferMessage
  #endif
  {
    private static readonly pb::MessageParser<CommonStateSignalChatAccept> _parser = new pb::MessageParser<CommonStateSignalChatAccept>(() => new CommonStateSignalChatAccept());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<CommonStateSignalChatAccept> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::AcFunDanmu.CommonStateSignalChatAcceptReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public CommonStateSignalChatAccept() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public CommonStateSignalChatAccept(CommonStateSignalChatAccept other) : this() {
      chatId_ = other.chatId_;
      mediaType_ = other.mediaType_;
      aryaSignalInfo_ = other.aryaSignalInfo_;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public CommonStateSignalChatAccept Clone() {
      return new CommonStateSignalChatAccept(this);
    }

    /// <summary>Field number for the "chatId" field.</summary>
    public const int ChatIdFieldNumber = 1;
    private string chatId_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string ChatId {
      get { return chatId_; }
      set {
        chatId_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "mediaType" field.</summary>
    public const int MediaTypeFieldNumber = 2;
    private global::AcFunDanmu.ChatMediaType mediaType_ = global::AcFunDanmu.ChatMediaType.Unknown;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public global::AcFunDanmu.ChatMediaType MediaType {
      get { return mediaType_; }
      set {
        mediaType_ = value;
      }
    }

    /// <summary>Field number for the "aryaSignalInfo" field.</summary>
    public const int AryaSignalInfoFieldNumber = 3;
    private string aryaSignalInfo_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string AryaSignalInfo {
      get { return aryaSignalInfo_; }
      set {
        aryaSignalInfo_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as CommonStateSignalChatAccept);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(CommonStateSignalChatAccept other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (ChatId != other.ChatId) return false;
      if (MediaType != other.MediaType) return false;
      if (AryaSignalInfo != other.AryaSignalInfo) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (ChatId.Length != 0) hash ^= ChatId.GetHashCode();
      if (MediaType != global::AcFunDanmu.ChatMediaType.Unknown) hash ^= MediaType.GetHashCode();
      if (AryaSignalInfo.Length != 0) hash ^= AryaSignalInfo.GetHashCode();
      if (_unknownFields != null) {
        hash ^= _unknownFields.GetHashCode();
      }
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      output.WriteRawMessage(this);
    #else
      if (ChatId.Length != 0) {
        output.WriteRawTag(10);
        output.WriteString(ChatId);
      }
      if (MediaType != global::AcFunDanmu.ChatMediaType.Unknown) {
        output.WriteRawTag(16);
        output.WriteEnum((int) MediaType);
      }
      if (AryaSignalInfo.Length != 0) {
        output.WriteRawTag(26);
        output.WriteString(AryaSignalInfo);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    void pb::IBufferMessage.InternalWriteTo(ref pb::WriteContext output) {
      if (ChatId.Length != 0) {
        output.WriteRawTag(10);
        output.WriteString(ChatId);
      }
      if (MediaType != global::AcFunDanmu.ChatMediaType.Unknown) {
        output.WriteRawTag(16);
        output.WriteEnum((int) MediaType);
      }
      if (AryaSignalInfo.Length != 0) {
        output.WriteRawTag(26);
        output.WriteString(AryaSignalInfo);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(ref output);
      }
    }
    #endif

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (ChatId.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(ChatId);
      }
      if (MediaType != global::AcFunDanmu.ChatMediaType.Unknown) {
        size += 1 + pb::CodedOutputStream.ComputeEnumSize((int) MediaType);
      }
      if (AryaSignalInfo.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(AryaSignalInfo);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(CommonStateSignalChatAccept other) {
      if (other == null) {
        return;
      }
      if (other.ChatId.Length != 0) {
        ChatId = other.ChatId;
      }
      if (other.MediaType != global::AcFunDanmu.ChatMediaType.Unknown) {
        MediaType = other.MediaType;
      }
      if (other.AryaSignalInfo.Length != 0) {
        AryaSignalInfo = other.AryaSignalInfo;
      }
      _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
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
            ChatId = input.ReadString();
            break;
          }
          case 16: {
            MediaType = (global::AcFunDanmu.ChatMediaType) input.ReadEnum();
            break;
          }
          case 26: {
            AryaSignalInfo = input.ReadString();
            break;
          }
        }
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    void pb::IBufferMessage.InternalMergeFrom(ref pb::ParseContext input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, ref input);
            break;
          case 10: {
            ChatId = input.ReadString();
            break;
          }
          case 16: {
            MediaType = (global::AcFunDanmu.ChatMediaType) input.ReadEnum();
            break;
          }
          case 26: {
            AryaSignalInfo = input.ReadString();
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
