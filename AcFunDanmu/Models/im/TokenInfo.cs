// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: TokenInfo.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace AcFunDanmu.Im.Basic {

  /// <summary>Holder for reflection information generated from TokenInfo.proto</summary>
  public static partial class TokenInfoReflection {

    #region Descriptor
    /// <summary>File descriptor for TokenInfo.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static TokenInfoReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "Cg9Ub2tlbkluZm8ucHJvdG8SE0FjRnVuRGFubXUuSW0uQmFzaWMihQEKCVRv",
            "a2VuSW5mbxI7Cgl0b2tlblR5cGUYASABKA4yKC5BY0Z1bkRhbm11LkltLkJh",
            "c2ljLlRva2VuSW5mby5Ub2tlblR5cGUSDQoFdG9rZW4YAiABKAwiLAoJVG9r",
            "ZW5UeXBlEgwKCGtJbnZhbGlkEAASEQoNa1NlcnZpY2VUb2tlbhABYgZwcm90",
            "bzM="));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { },
          new pbr::GeneratedClrTypeInfo(null, null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::AcFunDanmu.Im.Basic.TokenInfo), global::AcFunDanmu.Im.Basic.TokenInfo.Parser, new[]{ "TokenType", "Token" }, null, new[]{ typeof(global::AcFunDanmu.Im.Basic.TokenInfo.Types.TokenType) }, null, null)
          }));
    }
    #endregion

  }
  #region Messages
  public sealed partial class TokenInfo : pb::IMessage<TokenInfo>
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      , pb::IBufferMessage
  #endif
  {
    private static readonly pb::MessageParser<TokenInfo> _parser = new pb::MessageParser<TokenInfo>(() => new TokenInfo());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pb::MessageParser<TokenInfo> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::AcFunDanmu.Im.Basic.TokenInfoReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public TokenInfo() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public TokenInfo(TokenInfo other) : this() {
      tokenType_ = other.tokenType_;
      token_ = other.token_;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public TokenInfo Clone() {
      return new TokenInfo(this);
    }

    /// <summary>Field number for the "tokenType" field.</summary>
    public const int TokenTypeFieldNumber = 1;
    private global::AcFunDanmu.Im.Basic.TokenInfo.Types.TokenType tokenType_ = global::AcFunDanmu.Im.Basic.TokenInfo.Types.TokenType.KInvalid;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public global::AcFunDanmu.Im.Basic.TokenInfo.Types.TokenType TokenType {
      get { return tokenType_; }
      set {
        tokenType_ = value;
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

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override bool Equals(object other) {
      return Equals(other as TokenInfo);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool Equals(TokenInfo other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (TokenType != other.TokenType) return false;
      if (Token != other.Token) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override int GetHashCode() {
      int hash = 1;
      if (TokenType != global::AcFunDanmu.Im.Basic.TokenInfo.Types.TokenType.KInvalid) hash ^= TokenType.GetHashCode();
      if (Token.Length != 0) hash ^= Token.GetHashCode();
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
      if (TokenType != global::AcFunDanmu.Im.Basic.TokenInfo.Types.TokenType.KInvalid) {
        output.WriteRawTag(8);
        output.WriteEnum((int) TokenType);
      }
      if (Token.Length != 0) {
        output.WriteRawTag(18);
        output.WriteBytes(Token);
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
      if (TokenType != global::AcFunDanmu.Im.Basic.TokenInfo.Types.TokenType.KInvalid) {
        output.WriteRawTag(8);
        output.WriteEnum((int) TokenType);
      }
      if (Token.Length != 0) {
        output.WriteRawTag(18);
        output.WriteBytes(Token);
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
      if (TokenType != global::AcFunDanmu.Im.Basic.TokenInfo.Types.TokenType.KInvalid) {
        size += 1 + pb::CodedOutputStream.ComputeEnumSize((int) TokenType);
      }
      if (Token.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeBytesSize(Token);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void MergeFrom(TokenInfo other) {
      if (other == null) {
        return;
      }
      if (other.TokenType != global::AcFunDanmu.Im.Basic.TokenInfo.Types.TokenType.KInvalid) {
        TokenType = other.TokenType;
      }
      if (other.Token.Length != 0) {
        Token = other.Token;
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
            TokenType = (global::AcFunDanmu.Im.Basic.TokenInfo.Types.TokenType) input.ReadEnum();
            break;
          }
          case 18: {
            Token = input.ReadBytes();
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
            TokenType = (global::AcFunDanmu.Im.Basic.TokenInfo.Types.TokenType) input.ReadEnum();
            break;
          }
          case 18: {
            Token = input.ReadBytes();
            break;
          }
        }
      }
    }
    #endif

    #region Nested types
    /// <summary>Container for nested types declared in the TokenInfo message type.</summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static partial class Types {
      public enum TokenType {
        [pbr::OriginalName("kInvalid")] KInvalid = 0,
        [pbr::OriginalName("kServiceToken")] KServiceToken = 1,
      }

    }
    #endregion

  }

  #endregion

}

#endregion Designer generated code
