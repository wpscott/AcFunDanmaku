// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: RichTextNoticeMessage.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace AcFunDanmu.Im.Message {

  /// <summary>Holder for reflection information generated from RichTextNoticeMessage.proto</summary>
  public static partial class RichTextNoticeMessageReflection {

    #region Descriptor
    /// <summary>File descriptor for RichTextNoticeMessage.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static RichTextNoticeMessageReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "ChtSaWNoVGV4dE5vdGljZU1lc3NhZ2UucHJvdG8SFUFjRnVuRGFubXUuSW0u",
            "TWVzc2FnZSLEAQoVUmljaFRleHROb3RpY2VNZXNzYWdlEkgKBWl0ZW1zGAEg",
            "AygLMjkuQWNGdW5EYW5tdS5JbS5NZXNzYWdlLlJpY2hUZXh0Tm90aWNlTWVz",
            "c2FnZS5SaWNoVGV4dEl0ZW0aYQoMUmljaFRleHRJdGVtEg0KBWNvbG9yGAEg",
            "ASgFEg0KBXN0YXJ0GAIgASgFEgsKA2xlbhgDIAEoBRITCgtjbGlja0FjdGlv",
            "bhgEIAEoCRIRCgl1bmRlcmxpbmUYBSABKAhiBnByb3RvMw=="));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { },
          new pbr::GeneratedClrTypeInfo(null, null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::AcFunDanmu.Im.Message.RichTextNoticeMessage), global::AcFunDanmu.Im.Message.RichTextNoticeMessage.Parser, new[]{ "Items" }, null, null, null, new pbr::GeneratedClrTypeInfo[] { new pbr::GeneratedClrTypeInfo(typeof(global::AcFunDanmu.Im.Message.RichTextNoticeMessage.Types.RichTextItem), global::AcFunDanmu.Im.Message.RichTextNoticeMessage.Types.RichTextItem.Parser, new[]{ "Color", "Start", "Len", "ClickAction", "Underline" }, null, null, null, null)})
          }));
    }
    #endregion

  }
  #region Messages
  public sealed partial class RichTextNoticeMessage : pb::IMessage<RichTextNoticeMessage>
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      , pb::IBufferMessage
  #endif
  {
    private static readonly pb::MessageParser<RichTextNoticeMessage> _parser = new pb::MessageParser<RichTextNoticeMessage>(() => new RichTextNoticeMessage());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pb::MessageParser<RichTextNoticeMessage> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::AcFunDanmu.Im.Message.RichTextNoticeMessageReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public RichTextNoticeMessage() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public RichTextNoticeMessage(RichTextNoticeMessage other) : this() {
      items_ = other.items_.Clone();
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public RichTextNoticeMessage Clone() {
      return new RichTextNoticeMessage(this);
    }

    /// <summary>Field number for the "items" field.</summary>
    public const int ItemsFieldNumber = 1;
    private static readonly pb::FieldCodec<global::AcFunDanmu.Im.Message.RichTextNoticeMessage.Types.RichTextItem> _repeated_items_codec
        = pb::FieldCodec.ForMessage(10, global::AcFunDanmu.Im.Message.RichTextNoticeMessage.Types.RichTextItem.Parser);
    private readonly pbc::RepeatedField<global::AcFunDanmu.Im.Message.RichTextNoticeMessage.Types.RichTextItem> items_ = new pbc::RepeatedField<global::AcFunDanmu.Im.Message.RichTextNoticeMessage.Types.RichTextItem>();
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public pbc::RepeatedField<global::AcFunDanmu.Im.Message.RichTextNoticeMessage.Types.RichTextItem> Items {
      get { return items_; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override bool Equals(object other) {
      return Equals(other as RichTextNoticeMessage);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool Equals(RichTextNoticeMessage other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if(!items_.Equals(other.items_)) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override int GetHashCode() {
      int hash = 1;
      hash ^= items_.GetHashCode();
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
      items_.WriteTo(output, _repeated_items_codec);
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    void pb::IBufferMessage.InternalWriteTo(ref pb::WriteContext output) {
      items_.WriteTo(ref output, _repeated_items_codec);
      if (_unknownFields != null) {
        _unknownFields.WriteTo(ref output);
      }
    }
    #endif

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public int CalculateSize() {
      int size = 0;
      size += items_.CalculateSize(_repeated_items_codec);
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void MergeFrom(RichTextNoticeMessage other) {
      if (other == null) {
        return;
      }
      items_.Add(other.items_);
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
            items_.AddEntriesFrom(input, _repeated_items_codec);
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
            items_.AddEntriesFrom(ref input, _repeated_items_codec);
            break;
          }
        }
      }
    }
    #endif

    #region Nested types
    /// <summary>Container for nested types declared in the RichTextNoticeMessage message type.</summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static partial class Types {
      public sealed partial class RichTextItem : pb::IMessage<RichTextItem>
      #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
          , pb::IBufferMessage
      #endif
      {
        private static readonly pb::MessageParser<RichTextItem> _parser = new pb::MessageParser<RichTextItem>(() => new RichTextItem());
        private pb::UnknownFieldSet _unknownFields;
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
        public static pb::MessageParser<RichTextItem> Parser { get { return _parser; } }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
        public static pbr::MessageDescriptor Descriptor {
          get { return global::AcFunDanmu.Im.Message.RichTextNoticeMessage.Descriptor.NestedTypes[0]; }
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
        pbr::MessageDescriptor pb::IMessage.Descriptor {
          get { return Descriptor; }
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
        public RichTextItem() {
          OnConstruction();
        }

        partial void OnConstruction();

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
        public RichTextItem(RichTextItem other) : this() {
          color_ = other.color_;
          start_ = other.start_;
          len_ = other.len_;
          clickAction_ = other.clickAction_;
          underline_ = other.underline_;
          _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
        public RichTextItem Clone() {
          return new RichTextItem(this);
        }

        /// <summary>Field number for the "color" field.</summary>
        public const int ColorFieldNumber = 1;
        private int color_;
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
        public int Color {
          get { return color_; }
          set {
            color_ = value;
          }
        }

        /// <summary>Field number for the "start" field.</summary>
        public const int StartFieldNumber = 2;
        private int start_;
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
        public int Start {
          get { return start_; }
          set {
            start_ = value;
          }
        }

        /// <summary>Field number for the "len" field.</summary>
        public const int LenFieldNumber = 3;
        private int len_;
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
        public int Len {
          get { return len_; }
          set {
            len_ = value;
          }
        }

        /// <summary>Field number for the "clickAction" field.</summary>
        public const int ClickActionFieldNumber = 4;
        private string clickAction_ = "";
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
        public string ClickAction {
          get { return clickAction_; }
          set {
            clickAction_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
          }
        }

        /// <summary>Field number for the "underline" field.</summary>
        public const int UnderlineFieldNumber = 5;
        private bool underline_;
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
        public bool Underline {
          get { return underline_; }
          set {
            underline_ = value;
          }
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
        public override bool Equals(object other) {
          return Equals(other as RichTextItem);
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
        public bool Equals(RichTextItem other) {
          if (ReferenceEquals(other, null)) {
            return false;
          }
          if (ReferenceEquals(other, this)) {
            return true;
          }
          if (Color != other.Color) return false;
          if (Start != other.Start) return false;
          if (Len != other.Len) return false;
          if (ClickAction != other.ClickAction) return false;
          if (Underline != other.Underline) return false;
          return Equals(_unknownFields, other._unknownFields);
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
        public override int GetHashCode() {
          int hash = 1;
          if (Color != 0) hash ^= Color.GetHashCode();
          if (Start != 0) hash ^= Start.GetHashCode();
          if (Len != 0) hash ^= Len.GetHashCode();
          if (ClickAction.Length != 0) hash ^= ClickAction.GetHashCode();
          if (Underline != false) hash ^= Underline.GetHashCode();
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
          if (Color != 0) {
            output.WriteRawTag(8);
            output.WriteInt32(Color);
          }
          if (Start != 0) {
            output.WriteRawTag(16);
            output.WriteInt32(Start);
          }
          if (Len != 0) {
            output.WriteRawTag(24);
            output.WriteInt32(Len);
          }
          if (ClickAction.Length != 0) {
            output.WriteRawTag(34);
            output.WriteString(ClickAction);
          }
          if (Underline != false) {
            output.WriteRawTag(40);
            output.WriteBool(Underline);
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
          if (Color != 0) {
            output.WriteRawTag(8);
            output.WriteInt32(Color);
          }
          if (Start != 0) {
            output.WriteRawTag(16);
            output.WriteInt32(Start);
          }
          if (Len != 0) {
            output.WriteRawTag(24);
            output.WriteInt32(Len);
          }
          if (ClickAction.Length != 0) {
            output.WriteRawTag(34);
            output.WriteString(ClickAction);
          }
          if (Underline != false) {
            output.WriteRawTag(40);
            output.WriteBool(Underline);
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
          if (Color != 0) {
            size += 1 + pb::CodedOutputStream.ComputeInt32Size(Color);
          }
          if (Start != 0) {
            size += 1 + pb::CodedOutputStream.ComputeInt32Size(Start);
          }
          if (Len != 0) {
            size += 1 + pb::CodedOutputStream.ComputeInt32Size(Len);
          }
          if (ClickAction.Length != 0) {
            size += 1 + pb::CodedOutputStream.ComputeStringSize(ClickAction);
          }
          if (Underline != false) {
            size += 1 + 1;
          }
          if (_unknownFields != null) {
            size += _unknownFields.CalculateSize();
          }
          return size;
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
        public void MergeFrom(RichTextItem other) {
          if (other == null) {
            return;
          }
          if (other.Color != 0) {
            Color = other.Color;
          }
          if (other.Start != 0) {
            Start = other.Start;
          }
          if (other.Len != 0) {
            Len = other.Len;
          }
          if (other.ClickAction.Length != 0) {
            ClickAction = other.ClickAction;
          }
          if (other.Underline != false) {
            Underline = other.Underline;
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
                Color = input.ReadInt32();
                break;
              }
              case 16: {
                Start = input.ReadInt32();
                break;
              }
              case 24: {
                Len = input.ReadInt32();
                break;
              }
              case 34: {
                ClickAction = input.ReadString();
                break;
              }
              case 40: {
                Underline = input.ReadBool();
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
                Color = input.ReadInt32();
                break;
              }
              case 16: {
                Start = input.ReadInt32();
                break;
              }
              case 24: {
                Len = input.ReadInt32();
                break;
              }
              case 34: {
                ClickAction = input.ReadString();
                break;
              }
              case 40: {
                Underline = input.ReadBool();
                break;
              }
            }
          }
        }
        #endif

      }

    }
    #endregion

  }

  #endregion

}

#endregion Designer generated code
