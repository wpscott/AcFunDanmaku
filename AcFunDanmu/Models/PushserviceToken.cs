// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: PushServiceToken.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace AcFunDanmu {

  /// <summary>Holder for reflection information generated from PushServiceToken.proto</summary>
  public static partial class PushServiceTokenReflection {

    #region Descriptor
    /// <summary>File descriptor for PushServiceToken.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static PushServiceTokenReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "ChZQdXNoU2VydmljZVRva2VuLnByb3RvEgpBY0Z1bkRhbm11IrcCChBQdXNo",
            "U2VydmljZVRva2VuEjcKCHB1c2hUeXBlGAEgASgOMiUuQWNGdW5EYW5tdS5Q",
            "dXNoU2VydmljZVRva2VuLlB1c2hUeXBlEg0KBXRva2VuGAIgASgMEhUKDWlz",
            "UGFzc1Rocm91Z2gYAyABKAgiwwEKCFB1c2hUeXBlEhQKEGtQdXNoVHlwZUlu",
            "dmFsaWQQABIRCg1rUHVzaFR5cGVBUE5TEAESEwoPa1B1c2hUeXBlWG1QdXNo",
            "EAISEwoPa1B1c2hUeXBlSmdQdXNoEAMSEwoPa1B1c2hUeXBlR3RQVXNoEAQS",
            "EwoPa1B1c2hUeXBlT3BQdXNoEAUSEwoPa1B1c2hUWXBlVnZQdXNoEAYSEwoP",
            "a1B1c2hUeXBlSHdQdXNoEAcSEAoMa1B1c2hUWXBlRmNtEAhiBnByb3RvMw=="));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { },
          new pbr::GeneratedClrTypeInfo(null, null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::AcFunDanmu.PushServiceToken), global::AcFunDanmu.PushServiceToken.Parser, new[]{ "PushType", "Token", "IsPassThrough" }, null, new[]{ typeof(global::AcFunDanmu.PushServiceToken.Types.PushType) }, null, null)
          }));
    }
    #endregion

  }
  #region Messages
  public sealed partial class PushServiceToken : pb::IMessage<PushServiceToken>
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      , pb::IBufferMessage
  #endif
  {
    private static readonly pb::MessageParser<PushServiceToken> _parser = new pb::MessageParser<PushServiceToken>(() => new PushServiceToken());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pb::MessageParser<PushServiceToken> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::AcFunDanmu.PushServiceTokenReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public PushServiceToken() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public PushServiceToken(PushServiceToken other) : this() {
      pushType_ = other.pushType_;
      token_ = other.token_;
      isPassThrough_ = other.isPassThrough_;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public PushServiceToken Clone() {
      return new PushServiceToken(this);
    }

    /// <summary>Field number for the "pushType" field.</summary>
    public const int PushTypeFieldNumber = 1;
    private global::AcFunDanmu.PushServiceToken.Types.PushType pushType_ = global::AcFunDanmu.PushServiceToken.Types.PushType.KPushTypeInvalid;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public global::AcFunDanmu.PushServiceToken.Types.PushType PushType {
      get { return pushType_; }
      set {
        pushType_ = value;
      }
    }

    /// <summary>Field number for the "token" field.</summary>
    public const int TokenFieldNumber = 2;
    private pb::ByteString token_ = pb::ByteString.Empty;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public pb::ByteString Token {
      get { return token_; }
      set {
        token_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "isPassThrough" field.</summary>
    public const int IsPassThroughFieldNumber = 3;
    private bool isPassThrough_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool IsPassThrough {
      get { return isPassThrough_; }
      set {
        isPassThrough_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override bool Equals(object other) {
      return Equals(other as PushServiceToken);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool Equals(PushServiceToken other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (PushType != other.PushType) return false;
      if (Token != other.Token) return false;
      if (IsPassThrough != other.IsPassThrough) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override int GetHashCode() {
      int hash = 1;
      if (PushType != global::AcFunDanmu.PushServiceToken.Types.PushType.KPushTypeInvalid) hash ^= PushType.GetHashCode();
      if (Token.Length != 0) hash ^= Token.GetHashCode();
      if (IsPassThrough != false) hash ^= IsPassThrough.GetHashCode();
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
      if (PushType != global::AcFunDanmu.PushServiceToken.Types.PushType.KPushTypeInvalid) {
        output.WriteRawTag(8);
        output.WriteEnum((int) PushType);
      }
      if (Token.Length != 0) {
        output.WriteRawTag(18);
        output.WriteBytes(Token);
      }
      if (IsPassThrough != false) {
        output.WriteRawTag(24);
        output.WriteBool(IsPassThrough);
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
      if (PushType != global::AcFunDanmu.PushServiceToken.Types.PushType.KPushTypeInvalid) {
        output.WriteRawTag(8);
        output.WriteEnum((int) PushType);
      }
      if (Token.Length != 0) {
        output.WriteRawTag(18);
        output.WriteBytes(Token);
      }
      if (IsPassThrough != false) {
        output.WriteRawTag(24);
        output.WriteBool(IsPassThrough);
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
      if (PushType != global::AcFunDanmu.PushServiceToken.Types.PushType.KPushTypeInvalid) {
        size += 1 + pb::CodedOutputStream.ComputeEnumSize((int) PushType);
      }
      if (Token.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeBytesSize(Token);
      }
      if (IsPassThrough != false) {
        size += 1 + 1;
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void MergeFrom(PushServiceToken other) {
      if (other == null) {
        return;
      }
      if (other.PushType != global::AcFunDanmu.PushServiceToken.Types.PushType.KPushTypeInvalid) {
        PushType = other.PushType;
      }
      if (other.Token.Length != 0) {
        Token = other.Token;
      }
      if (other.IsPassThrough != false) {
        IsPassThrough = other.IsPassThrough;
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
            PushType = (global::AcFunDanmu.PushServiceToken.Types.PushType) input.ReadEnum();
            break;
          }
          case 18: {
            Token = input.ReadBytes();
            break;
          }
          case 24: {
            IsPassThrough = input.ReadBool();
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
            PushType = (global::AcFunDanmu.PushServiceToken.Types.PushType) input.ReadEnum();
            break;
          }
          case 18: {
            Token = input.ReadBytes();
            break;
          }
          case 24: {
            IsPassThrough = input.ReadBool();
            break;
          }
        }
      }
    }
    #endif

    #region Nested types
    /// <summary>Container for nested types declared in the PushServiceToken message type.</summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static partial class Types {
      public enum PushType {
        [pbr::OriginalName("kPushTypeInvalid")] KPushTypeInvalid = 0,
        [pbr::OriginalName("kPushTypeAPNS")] KPushTypeApns = 1,
        [pbr::OriginalName("kPushTypeXmPush")] KPushTypeXmPush = 2,
        [pbr::OriginalName("kPushTypeJgPush")] KPushTypeJgPush = 3,
        [pbr::OriginalName("kPushTypeGtPUsh")] KPushTypeGtPush = 4,
        [pbr::OriginalName("kPushTypeOpPush")] KPushTypeOpPush = 5,
        [pbr::OriginalName("kPushTYpeVvPush")] KPushTypeVvPush = 6,
        [pbr::OriginalName("kPushTypeHwPush")] KPushTypeHwPush = 7,
        [pbr::OriginalName("kPushTYpeFcm")] KPushTypeFcm = 8,
      }

    }
    #endregion

  }

  #endregion

}

#endregion Designer generated code
