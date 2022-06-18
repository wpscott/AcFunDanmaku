// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: InviteVoiceCallRequest.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace AcFunDanmu.Im.Cloud.Voice.Call {

  /// <summary>Holder for reflection information generated from InviteVoiceCallRequest.proto</summary>
  public static partial class InviteVoiceCallRequestReflection {

    #region Descriptor
    /// <summary>File descriptor for InviteVoiceCallRequest.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static InviteVoiceCallRequestReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "ChxJbnZpdGVWb2ljZUNhbGxSZXF1ZXN0LnByb3RvEh5BY0Z1bkRhbm11Lklt",
            "LkNsb3VkLlZvaWNlLkNhbGwaClVzZXIucHJvdG8iVAoWSW52aXRlVm9pY2VD",
            "YWxsUmVxdWVzdBIOCgZyb29tSWQYASABKAkSKgoHaW52aXRlZRgCIAMoCzIZ",
            "LkFjRnVuRGFubXUuSW0uQmFzaWMuVXNlcmIGcHJvdG8z"));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { global::AcFunDanmu.Im.Basic.UserReflection.Descriptor, },
          new pbr::GeneratedClrTypeInfo(null, null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::AcFunDanmu.Im.Cloud.Voice.Call.InviteVoiceCallRequest), global::AcFunDanmu.Im.Cloud.Voice.Call.InviteVoiceCallRequest.Parser, new[]{ "RoomId", "Invitee" }, null, null, null, null)
          }));
    }
    #endregion

  }
  #region Messages
  public sealed partial class InviteVoiceCallRequest : pb::IMessage<InviteVoiceCallRequest>
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      , pb::IBufferMessage
  #endif
  {
    private static readonly pb::MessageParser<InviteVoiceCallRequest> _parser = new pb::MessageParser<InviteVoiceCallRequest>(() => new InviteVoiceCallRequest());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pb::MessageParser<InviteVoiceCallRequest> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::AcFunDanmu.Im.Cloud.Voice.Call.InviteVoiceCallRequestReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public InviteVoiceCallRequest() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public InviteVoiceCallRequest(InviteVoiceCallRequest other) : this() {
      roomId_ = other.roomId_;
      invitee_ = other.invitee_.Clone();
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public InviteVoiceCallRequest Clone() {
      return new InviteVoiceCallRequest(this);
    }

    /// <summary>Field number for the "roomId" field.</summary>
    public const int RoomIdFieldNumber = 1;
    private string roomId_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public string RoomId {
      get { return roomId_; }
      set {
        roomId_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "invitee" field.</summary>
    public const int InviteeFieldNumber = 2;
    private static readonly pb::FieldCodec<global::AcFunDanmu.Im.Basic.User> _repeated_invitee_codec
        = pb::FieldCodec.ForMessage(18, global::AcFunDanmu.Im.Basic.User.Parser);
    private readonly pbc::RepeatedField<global::AcFunDanmu.Im.Basic.User> invitee_ = new pbc::RepeatedField<global::AcFunDanmu.Im.Basic.User>();
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public pbc::RepeatedField<global::AcFunDanmu.Im.Basic.User> Invitee {
      get { return invitee_; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override bool Equals(object other) {
      return Equals(other as InviteVoiceCallRequest);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool Equals(InviteVoiceCallRequest other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (RoomId != other.RoomId) return false;
      if(!invitee_.Equals(other.invitee_)) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override int GetHashCode() {
      int hash = 1;
      if (RoomId.Length != 0) hash ^= RoomId.GetHashCode();
      hash ^= invitee_.GetHashCode();
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
      if (RoomId.Length != 0) {
        output.WriteRawTag(10);
        output.WriteString(RoomId);
      }
      invitee_.WriteTo(output, _repeated_invitee_codec);
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    void pb::IBufferMessage.InternalWriteTo(ref pb::WriteContext output) {
      if (RoomId.Length != 0) {
        output.WriteRawTag(10);
        output.WriteString(RoomId);
      }
      invitee_.WriteTo(ref output, _repeated_invitee_codec);
      if (_unknownFields != null) {
        _unknownFields.WriteTo(ref output);
      }
    }
    #endif

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public int CalculateSize() {
      int size = 0;
      if (RoomId.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(RoomId);
      }
      size += invitee_.CalculateSize(_repeated_invitee_codec);
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void MergeFrom(InviteVoiceCallRequest other) {
      if (other == null) {
        return;
      }
      if (other.RoomId.Length != 0) {
        RoomId = other.RoomId;
      }
      invitee_.Add(other.invitee_);
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
            RoomId = input.ReadString();
            break;
          }
          case 18: {
            invitee_.AddEntriesFrom(input, _repeated_invitee_codec);
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
            RoomId = input.ReadString();
            break;
          }
          case 18: {
            invitee_.AddEntriesFrom(ref input, _repeated_invitee_codec);
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