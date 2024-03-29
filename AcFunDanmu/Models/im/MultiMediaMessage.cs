// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: MultiMediaMessage.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021, 8981
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace AcFunDanmu.Im.Cloud.Message {

  /// <summary>Holder for reflection information generated from MultiMediaMessage.proto</summary>
  public static partial class MultiMediaMessageReflection {

    #region Descriptor
    /// <summary>File descriptor for MultiMediaMessage.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static MultiMediaMessageReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "ChdNdWx0aU1lZGlhTWVzc2FnZS5wcm90bxIbQWNGdW5EYW5tdS5JbS5DbG91",
            "ZC5NZXNzYWdlGgtNZWRpYS5wcm90byJjChFNdWx0aU1lZGlhTWVzc2FnZRIM",
            "CgR0ZXh0GAEgASgJEjEKBW1lZGlhGAIgAygLMiIuQWNGdW5EYW5tdS5JbS5D",
            "bG91ZC5NZXNzYWdlLk1lZGlhEg0KBWV4dHJhGAMgASgJYgZwcm90bzM="));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { global::AcFunDanmu.Im.Cloud.Message.MediaReflection.Descriptor, },
          new pbr::GeneratedClrTypeInfo(null, null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::AcFunDanmu.Im.Cloud.Message.MultiMediaMessage), global::AcFunDanmu.Im.Cloud.Message.MultiMediaMessage.Parser, new[]{ "Text", "Media", "Extra" }, null, null, null, null)
          }));
    }
    #endregion

  }
  #region Messages
  [global::System.Diagnostics.DebuggerDisplayAttribute("{ToString(),nq}")]
  public sealed partial class MultiMediaMessage : pb::IMessage<MultiMediaMessage>
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      , pb::IBufferMessage
  #endif
  {
    private static readonly pb::MessageParser<MultiMediaMessage> _parser = new pb::MessageParser<MultiMediaMessage>(() => new MultiMediaMessage());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pb::MessageParser<MultiMediaMessage> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::AcFunDanmu.Im.Cloud.Message.MultiMediaMessageReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public MultiMediaMessage() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public MultiMediaMessage(MultiMediaMessage other) : this() {
      text_ = other.text_;
      media_ = other.media_.Clone();
      extra_ = other.extra_;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public MultiMediaMessage Clone() {
      return new MultiMediaMessage(this);
    }

    /// <summary>Field number for the "text" field.</summary>
    public const int TextFieldNumber = 1;
    private string text_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public string Text {
      get { return text_; }
      set {
        text_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "media" field.</summary>
    public const int MediaFieldNumber = 2;
    private static readonly pb::FieldCodec<global::AcFunDanmu.Im.Cloud.Message.Media> _repeated_media_codec
        = pb::FieldCodec.ForMessage(18, global::AcFunDanmu.Im.Cloud.Message.Media.Parser);
    private readonly pbc::RepeatedField<global::AcFunDanmu.Im.Cloud.Message.Media> media_ = new pbc::RepeatedField<global::AcFunDanmu.Im.Cloud.Message.Media>();
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public pbc::RepeatedField<global::AcFunDanmu.Im.Cloud.Message.Media> Media {
      get { return media_; }
    }

    /// <summary>Field number for the "extra" field.</summary>
    public const int ExtraFieldNumber = 3;
    private string extra_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public string Extra {
      get { return extra_; }
      set {
        extra_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override bool Equals(object other) {
      return Equals(other as MultiMediaMessage);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool Equals(MultiMediaMessage other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (Text != other.Text) return false;
      if(!media_.Equals(other.media_)) return false;
      if (Extra != other.Extra) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override int GetHashCode() {
      int hash = 1;
      if (Text.Length != 0) hash ^= Text.GetHashCode();
      hash ^= media_.GetHashCode();
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
      if (Text.Length != 0) {
        output.WriteRawTag(10);
        output.WriteString(Text);
      }
      media_.WriteTo(output, _repeated_media_codec);
      if (Extra.Length != 0) {
        output.WriteRawTag(26);
        output.WriteString(Extra);
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
      if (Text.Length != 0) {
        output.WriteRawTag(10);
        output.WriteString(Text);
      }
      media_.WriteTo(ref output, _repeated_media_codec);
      if (Extra.Length != 0) {
        output.WriteRawTag(26);
        output.WriteString(Extra);
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
      if (Text.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(Text);
      }
      size += media_.CalculateSize(_repeated_media_codec);
      if (Extra.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(Extra);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void MergeFrom(MultiMediaMessage other) {
      if (other == null) {
        return;
      }
      if (other.Text.Length != 0) {
        Text = other.Text;
      }
      media_.Add(other.media_);
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
            Text = input.ReadString();
            break;
          }
          case 18: {
            media_.AddEntriesFrom(input, _repeated_media_codec);
            break;
          }
          case 26: {
            Extra = input.ReadString();
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
            Text = input.ReadString();
            break;
          }
          case 18: {
            media_.AddEntriesFrom(ref input, _repeated_media_codec);
            break;
          }
          case 26: {
            Extra = input.ReadString();
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
