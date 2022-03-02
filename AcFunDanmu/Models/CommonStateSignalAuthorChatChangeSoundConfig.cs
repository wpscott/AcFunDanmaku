// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: CommonStateSignalAuthorChatChangeSoundConfig.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace AcFunDanmu {

  /// <summary>Holder for reflection information generated from CommonStateSignalAuthorChatChangeSoundConfig.proto</summary>
  public static partial class CommonStateSignalAuthorChatChangeSoundConfigReflection {

    #region Descriptor
    /// <summary>File descriptor for CommonStateSignalAuthorChatChangeSoundConfig.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static CommonStateSignalAuthorChatChangeSoundConfigReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "CjJDb21tb25TdGF0ZVNpZ25hbEF1dGhvckNoYXRDaGFuZ2VTb3VuZENvbmZp",
            "Zy5wcm90bxIKQWNGdW5EYW5tdSL6AQosQ29tbW9uU3RhdGVTaWduYWxBdXRo",
            "b3JDaGF0Q2hhbmdlU291bmRDb25maWcSFAoMYXV0aG9yQ2hhdElkGAEgASgJ",
            "Em0KFXNvdW5kQ29uZmlnQ2hhbmdlVHlwZRgCIAEoDjJOLkFjRnVuRGFubXUu",
            "Q29tbW9uU3RhdGVTaWduYWxBdXRob3JDaGF0Q2hhbmdlU291bmRDb25maWcu",
            "U291bmRDb25maWdDaGFuZ2VUeXBlIkUKFVNvdW5kQ29uZmlnQ2hhbmdlVHlw",
            "ZRILCgdVTktOT1dOEAASDgoKT1BFTl9TT1VORBABEg8KC0NMT1NFX1NPVU5E",
            "EAJiBnByb3RvMw=="));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { },
          new pbr::GeneratedClrTypeInfo(null, null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::AcFunDanmu.CommonStateSignalAuthorChatChangeSoundConfig), global::AcFunDanmu.CommonStateSignalAuthorChatChangeSoundConfig.Parser, new[]{ "AuthorChatId", "SoundConfigChangeType" }, null, new[]{ typeof(global::AcFunDanmu.CommonStateSignalAuthorChatChangeSoundConfig.Types.SoundConfigChangeType) }, null, null)
          }));
    }
    #endregion

  }
  #region Messages
  public sealed partial class CommonStateSignalAuthorChatChangeSoundConfig : pb::IMessage<CommonStateSignalAuthorChatChangeSoundConfig>
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      , pb::IBufferMessage
  #endif
  {
    private static readonly pb::MessageParser<CommonStateSignalAuthorChatChangeSoundConfig> _parser = new pb::MessageParser<CommonStateSignalAuthorChatChangeSoundConfig>(() => new CommonStateSignalAuthorChatChangeSoundConfig());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pb::MessageParser<CommonStateSignalAuthorChatChangeSoundConfig> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::AcFunDanmu.CommonStateSignalAuthorChatChangeSoundConfigReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public CommonStateSignalAuthorChatChangeSoundConfig() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public CommonStateSignalAuthorChatChangeSoundConfig(CommonStateSignalAuthorChatChangeSoundConfig other) : this() {
      authorChatId_ = other.authorChatId_;
      soundConfigChangeType_ = other.soundConfigChangeType_;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public CommonStateSignalAuthorChatChangeSoundConfig Clone() {
      return new CommonStateSignalAuthorChatChangeSoundConfig(this);
    }

    /// <summary>Field number for the "authorChatId" field.</summary>
    public const int AuthorChatIdFieldNumber = 1;
    private string authorChatId_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public string AuthorChatId {
      get { return authorChatId_; }
      set {
        authorChatId_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "soundConfigChangeType" field.</summary>
    public const int SoundConfigChangeTypeFieldNumber = 2;
    private global::AcFunDanmu.CommonStateSignalAuthorChatChangeSoundConfig.Types.SoundConfigChangeType soundConfigChangeType_ = global::AcFunDanmu.CommonStateSignalAuthorChatChangeSoundConfig.Types.SoundConfigChangeType.Unknown;
    /// <summary>
    /// authorChatVoiceType
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public global::AcFunDanmu.CommonStateSignalAuthorChatChangeSoundConfig.Types.SoundConfigChangeType SoundConfigChangeType {
      get { return soundConfigChangeType_; }
      set {
        soundConfigChangeType_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override bool Equals(object other) {
      return Equals(other as CommonStateSignalAuthorChatChangeSoundConfig);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool Equals(CommonStateSignalAuthorChatChangeSoundConfig other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (AuthorChatId != other.AuthorChatId) return false;
      if (SoundConfigChangeType != other.SoundConfigChangeType) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override int GetHashCode() {
      int hash = 1;
      if (AuthorChatId.Length != 0) hash ^= AuthorChatId.GetHashCode();
      if (SoundConfigChangeType != global::AcFunDanmu.CommonStateSignalAuthorChatChangeSoundConfig.Types.SoundConfigChangeType.Unknown) hash ^= SoundConfigChangeType.GetHashCode();
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
      if (AuthorChatId.Length != 0) {
        output.WriteRawTag(10);
        output.WriteString(AuthorChatId);
      }
      if (SoundConfigChangeType != global::AcFunDanmu.CommonStateSignalAuthorChatChangeSoundConfig.Types.SoundConfigChangeType.Unknown) {
        output.WriteRawTag(16);
        output.WriteEnum((int) SoundConfigChangeType);
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
      if (AuthorChatId.Length != 0) {
        output.WriteRawTag(10);
        output.WriteString(AuthorChatId);
      }
      if (SoundConfigChangeType != global::AcFunDanmu.CommonStateSignalAuthorChatChangeSoundConfig.Types.SoundConfigChangeType.Unknown) {
        output.WriteRawTag(16);
        output.WriteEnum((int) SoundConfigChangeType);
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
      if (AuthorChatId.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(AuthorChatId);
      }
      if (SoundConfigChangeType != global::AcFunDanmu.CommonStateSignalAuthorChatChangeSoundConfig.Types.SoundConfigChangeType.Unknown) {
        size += 1 + pb::CodedOutputStream.ComputeEnumSize((int) SoundConfigChangeType);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void MergeFrom(CommonStateSignalAuthorChatChangeSoundConfig other) {
      if (other == null) {
        return;
      }
      if (other.AuthorChatId.Length != 0) {
        AuthorChatId = other.AuthorChatId;
      }
      if (other.SoundConfigChangeType != global::AcFunDanmu.CommonStateSignalAuthorChatChangeSoundConfig.Types.SoundConfigChangeType.Unknown) {
        SoundConfigChangeType = other.SoundConfigChangeType;
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
            AuthorChatId = input.ReadString();
            break;
          }
          case 16: {
            SoundConfigChangeType = (global::AcFunDanmu.CommonStateSignalAuthorChatChangeSoundConfig.Types.SoundConfigChangeType) input.ReadEnum();
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
            AuthorChatId = input.ReadString();
            break;
          }
          case 16: {
            SoundConfigChangeType = (global::AcFunDanmu.CommonStateSignalAuthorChatChangeSoundConfig.Types.SoundConfigChangeType) input.ReadEnum();
            break;
          }
        }
      }
    }
    #endif

    #region Nested types
    /// <summary>Container for nested types declared in the CommonStateSignalAuthorChatChangeSoundConfig message type.</summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static partial class Types {
      public enum SoundConfigChangeType {
        [pbr::OriginalName("UNKNOWN")] Unknown = 0,
        [pbr::OriginalName("OPEN_SOUND")] OpenSound = 1,
        [pbr::OriginalName("CLOSE_SOUND")] CloseSound = 2,
      }

    }
    #endregion

  }

  #endregion

}

#endregion Designer generated code
