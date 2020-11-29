// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: CommonStateSignalAuthorChatCall.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace AcFunDanmu {

  /// <summary>Holder for reflection information generated from CommonStateSignalAuthorChatCall.proto</summary>
  public static partial class CommonStateSignalAuthorChatCallReflection {

    #region Descriptor
    /// <summary>File descriptor for CommonStateSignalAuthorChatCall.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static CommonStateSignalAuthorChatCallReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "CiVDb21tb25TdGF0ZVNpZ25hbEF1dGhvckNoYXRDYWxsLnByb3RvEgpBY0Z1",
            "bkRhbm11GhpBdXRob3JDaGF0UGxheWVySW5mby5wcm90byKLAQofQ29tbW9u",
            "U3RhdGVTaWduYWxBdXRob3JDaGF0Q2FsbBIUCgxhdXRob3JDaGF0SWQYASAB",
            "KAkSOQoPaW52aXRlclVzZXJJbmZvGAIgASgLMiAuQWNGdW5EYW5tdS5BdXRo",
            "b3JDaGF0UGxheWVySW5mbxIXCg9jYWxsVGltZXN0YW1wTXMYAyABKANiBnBy",
            "b3RvMw=="));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { global::AcFunDanmu.AuthorChatPlayerInfoReflection.Descriptor, },
          new pbr::GeneratedClrTypeInfo(null, null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::AcFunDanmu.CommonStateSignalAuthorChatCall), global::AcFunDanmu.CommonStateSignalAuthorChatCall.Parser, new[]{ "AuthorChatId", "InviterUserInfo", "CallTimestampMs" }, null, null, null, null)
          }));
    }
    #endregion

  }
  #region Messages
  public sealed partial class CommonStateSignalAuthorChatCall : pb::IMessage<CommonStateSignalAuthorChatCall>
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      , pb::IBufferMessage
  #endif
  {
    private static readonly pb::MessageParser<CommonStateSignalAuthorChatCall> _parser = new pb::MessageParser<CommonStateSignalAuthorChatCall>(() => new CommonStateSignalAuthorChatCall());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<CommonStateSignalAuthorChatCall> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::AcFunDanmu.CommonStateSignalAuthorChatCallReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public CommonStateSignalAuthorChatCall() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public CommonStateSignalAuthorChatCall(CommonStateSignalAuthorChatCall other) : this() {
      authorChatId_ = other.authorChatId_;
      inviterUserInfo_ = other.inviterUserInfo_ != null ? other.inviterUserInfo_.Clone() : null;
      callTimestampMs_ = other.callTimestampMs_;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public CommonStateSignalAuthorChatCall Clone() {
      return new CommonStateSignalAuthorChatCall(this);
    }

    /// <summary>Field number for the "authorChatId" field.</summary>
    public const int AuthorChatIdFieldNumber = 1;
    private string authorChatId_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string AuthorChatId {
      get { return authorChatId_; }
      set {
        authorChatId_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "inviterUserInfo" field.</summary>
    public const int InviterUserInfoFieldNumber = 2;
    private global::AcFunDanmu.AuthorChatPlayerInfo inviterUserInfo_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public global::AcFunDanmu.AuthorChatPlayerInfo InviterUserInfo {
      get { return inviterUserInfo_; }
      set {
        inviterUserInfo_ = value;
      }
    }

    /// <summary>Field number for the "callTimestampMs" field.</summary>
    public const int CallTimestampMsFieldNumber = 3;
    private long callTimestampMs_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public long CallTimestampMs {
      get { return callTimestampMs_; }
      set {
        callTimestampMs_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as CommonStateSignalAuthorChatCall);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(CommonStateSignalAuthorChatCall other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (AuthorChatId != other.AuthorChatId) return false;
      if (!object.Equals(InviterUserInfo, other.InviterUserInfo)) return false;
      if (CallTimestampMs != other.CallTimestampMs) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (AuthorChatId.Length != 0) hash ^= AuthorChatId.GetHashCode();
      if (inviterUserInfo_ != null) hash ^= InviterUserInfo.GetHashCode();
      if (CallTimestampMs != 0L) hash ^= CallTimestampMs.GetHashCode();
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
      if (AuthorChatId.Length != 0) {
        output.WriteRawTag(10);
        output.WriteString(AuthorChatId);
      }
      if (inviterUserInfo_ != null) {
        output.WriteRawTag(18);
        output.WriteMessage(InviterUserInfo);
      }
      if (CallTimestampMs != 0L) {
        output.WriteRawTag(24);
        output.WriteInt64(CallTimestampMs);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    void pb::IBufferMessage.InternalWriteTo(ref pb::WriteContext output) {
      if (AuthorChatId.Length != 0) {
        output.WriteRawTag(10);
        output.WriteString(AuthorChatId);
      }
      if (inviterUserInfo_ != null) {
        output.WriteRawTag(18);
        output.WriteMessage(InviterUserInfo);
      }
      if (CallTimestampMs != 0L) {
        output.WriteRawTag(24);
        output.WriteInt64(CallTimestampMs);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(ref output);
      }
    }
    #endif

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (AuthorChatId.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(AuthorChatId);
      }
      if (inviterUserInfo_ != null) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(InviterUserInfo);
      }
      if (CallTimestampMs != 0L) {
        size += 1 + pb::CodedOutputStream.ComputeInt64Size(CallTimestampMs);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(CommonStateSignalAuthorChatCall other) {
      if (other == null) {
        return;
      }
      if (other.AuthorChatId.Length != 0) {
        AuthorChatId = other.AuthorChatId;
      }
      if (other.inviterUserInfo_ != null) {
        if (inviterUserInfo_ == null) {
          InviterUserInfo = new global::AcFunDanmu.AuthorChatPlayerInfo();
        }
        InviterUserInfo.MergeFrom(other.InviterUserInfo);
      }
      if (other.CallTimestampMs != 0L) {
        CallTimestampMs = other.CallTimestampMs;
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
            AuthorChatId = input.ReadString();
            break;
          }
          case 18: {
            if (inviterUserInfo_ == null) {
              InviterUserInfo = new global::AcFunDanmu.AuthorChatPlayerInfo();
            }
            input.ReadMessage(InviterUserInfo);
            break;
          }
          case 24: {
            CallTimestampMs = input.ReadInt64();
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
            AuthorChatId = input.ReadString();
            break;
          }
          case 18: {
            if (inviterUserInfo_ == null) {
              InviterUserInfo = new global::AcFunDanmu.AuthorChatPlayerInfo();
            }
            input.ReadMessage(InviterUserInfo);
            break;
          }
          case 24: {
            CallTimestampMs = input.ReadInt64();
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
