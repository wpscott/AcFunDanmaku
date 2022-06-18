// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: SessionAggregationRequest.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace AcFunDanmu.Im.Message {

  /// <summary>Holder for reflection information generated from SessionAggregationRequest.proto</summary>
  public static partial class SessionAggregationRequestReflection {

    #region Descriptor
    /// <summary>File descriptor for SessionAggregationRequest.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static SessionAggregationRequestReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "Ch9TZXNzaW9uQWdncmVnYXRpb25SZXF1ZXN0LnByb3RvEhVBY0Z1bkRhbm11",
            "LkltLk1lc3NhZ2UaEENoYXRUYXJnZXQucHJvdG8aHFNlc3Npb25BZ2dyZWdh",
            "dGlvblR5cGUucHJvdG8irgEKGVNlc3Npb25BZ2dyZWdhdGlvblJlcXVlc3QS",
            "RgoPYWdncmVnYXRpb25UeXBlGAEgASgOMi0uQWNGdW5EYW5tdS5JbS5NZXNz",
            "YWdlLlNlc3Npb25BZ2dyZWdhdGlvblR5cGUSNQoKY2hhdFRhcmdldBgCIAMo",
            "CzIhLkFjRnVuRGFubXUuSW0uTWVzc2FnZS5DaGF0VGFyZ2V0EhIKCmNhdGVn",
            "b3J5SWQYAyABKAViBnByb3RvMw=="));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { global::AcFunDanmu.Im.Message.ChatTargetReflection.Descriptor, global::AcFunDanmu.Im.Message.SessionAggregationTypeReflection.Descriptor, },
          new pbr::GeneratedClrTypeInfo(null, null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::AcFunDanmu.Im.Message.SessionAggregationRequest), global::AcFunDanmu.Im.Message.SessionAggregationRequest.Parser, new[]{ "AggregationType", "ChatTarget", "CategoryId" }, null, null, null, null)
          }));
    }
    #endregion

  }
  #region Messages
  public sealed partial class SessionAggregationRequest : pb::IMessage<SessionAggregationRequest>
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      , pb::IBufferMessage
  #endif
  {
    private static readonly pb::MessageParser<SessionAggregationRequest> _parser = new pb::MessageParser<SessionAggregationRequest>(() => new SessionAggregationRequest());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pb::MessageParser<SessionAggregationRequest> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::AcFunDanmu.Im.Message.SessionAggregationRequestReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public SessionAggregationRequest() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public SessionAggregationRequest(SessionAggregationRequest other) : this() {
      aggregationType_ = other.aggregationType_;
      chatTarget_ = other.chatTarget_.Clone();
      categoryId_ = other.categoryId_;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public SessionAggregationRequest Clone() {
      return new SessionAggregationRequest(this);
    }

    /// <summary>Field number for the "aggregationType" field.</summary>
    public const int AggregationTypeFieldNumber = 1;
    private global::AcFunDanmu.Im.Message.SessionAggregationType aggregationType_ = global::AcFunDanmu.Im.Message.SessionAggregationType.UnknownAggregationType;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public global::AcFunDanmu.Im.Message.SessionAggregationType AggregationType {
      get { return aggregationType_; }
      set {
        aggregationType_ = value;
      }
    }

    /// <summary>Field number for the "chatTarget" field.</summary>
    public const int ChatTargetFieldNumber = 2;
    private static readonly pb::FieldCodec<global::AcFunDanmu.Im.Message.ChatTarget> _repeated_chatTarget_codec
        = pb::FieldCodec.ForMessage(18, global::AcFunDanmu.Im.Message.ChatTarget.Parser);
    private readonly pbc::RepeatedField<global::AcFunDanmu.Im.Message.ChatTarget> chatTarget_ = new pbc::RepeatedField<global::AcFunDanmu.Im.Message.ChatTarget>();
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public pbc::RepeatedField<global::AcFunDanmu.Im.Message.ChatTarget> ChatTarget {
      get { return chatTarget_; }
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

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override bool Equals(object other) {
      return Equals(other as SessionAggregationRequest);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool Equals(SessionAggregationRequest other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (AggregationType != other.AggregationType) return false;
      if(!chatTarget_.Equals(other.chatTarget_)) return false;
      if (CategoryId != other.CategoryId) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override int GetHashCode() {
      int hash = 1;
      if (AggregationType != global::AcFunDanmu.Im.Message.SessionAggregationType.UnknownAggregationType) hash ^= AggregationType.GetHashCode();
      hash ^= chatTarget_.GetHashCode();
      if (CategoryId != 0) hash ^= CategoryId.GetHashCode();
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
      if (AggregationType != global::AcFunDanmu.Im.Message.SessionAggregationType.UnknownAggregationType) {
        output.WriteRawTag(8);
        output.WriteEnum((int) AggregationType);
      }
      chatTarget_.WriteTo(output, _repeated_chatTarget_codec);
      if (CategoryId != 0) {
        output.WriteRawTag(24);
        output.WriteInt32(CategoryId);
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
      if (AggregationType != global::AcFunDanmu.Im.Message.SessionAggregationType.UnknownAggregationType) {
        output.WriteRawTag(8);
        output.WriteEnum((int) AggregationType);
      }
      chatTarget_.WriteTo(ref output, _repeated_chatTarget_codec);
      if (CategoryId != 0) {
        output.WriteRawTag(24);
        output.WriteInt32(CategoryId);
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
      if (AggregationType != global::AcFunDanmu.Im.Message.SessionAggregationType.UnknownAggregationType) {
        size += 1 + pb::CodedOutputStream.ComputeEnumSize((int) AggregationType);
      }
      size += chatTarget_.CalculateSize(_repeated_chatTarget_codec);
      if (CategoryId != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(CategoryId);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void MergeFrom(SessionAggregationRequest other) {
      if (other == null) {
        return;
      }
      if (other.AggregationType != global::AcFunDanmu.Im.Message.SessionAggregationType.UnknownAggregationType) {
        AggregationType = other.AggregationType;
      }
      chatTarget_.Add(other.chatTarget_);
      if (other.CategoryId != 0) {
        CategoryId = other.CategoryId;
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
            AggregationType = (global::AcFunDanmu.Im.Message.SessionAggregationType) input.ReadEnum();
            break;
          }
          case 18: {
            chatTarget_.AddEntriesFrom(input, _repeated_chatTarget_codec);
            break;
          }
          case 24: {
            CategoryId = input.ReadInt32();
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
            AggregationType = (global::AcFunDanmu.Im.Message.SessionAggregationType) input.ReadEnum();
            break;
          }
          case 18: {
            chatTarget_.AddEntriesFrom(ref input, _repeated_chatTarget_codec);
            break;
          }
          case 24: {
            CategoryId = input.ReadInt32();
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