// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: PkPlayerInfo.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace AcFunDanmu {

  /// <summary>Holder for reflection information generated from PkPlayerInfo.proto</summary>
  public static partial class PkPlayerInfoReflection {

    #region Descriptor
    /// <summary>File descriptor for PkPlayerInfo.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static PkPlayerInfoReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "ChJQa1BsYXllckluZm8ucHJvdG8SCkFjRnVuRGFubXUaFFp0TGl2ZVVzZXJJ",
            "bmZvLnByb3RvIksKDFBrUGxheWVySW5mbxIlCgFhGAEgASgLMhouQWNGdW5E",
            "YW5tdS5adExpdmVVc2VySW5mbxIJCgFiGAIgASgJEgkKAWMYAyABKAhiBnBy",
            "b3RvMw=="));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { global::AcFunDanmu.ZtLiveUserInfoReflection.Descriptor, },
          new pbr::GeneratedClrTypeInfo(null, null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::AcFunDanmu.PkPlayerInfo), global::AcFunDanmu.PkPlayerInfo.Parser, new[]{ "A", "B", "C" }, null, null, null, null)
          }));
    }
    #endregion

  }
  #region Messages
  public sealed partial class PkPlayerInfo : pb::IMessage<PkPlayerInfo>
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      , pb::IBufferMessage
  #endif
  {
    private static readonly pb::MessageParser<PkPlayerInfo> _parser = new pb::MessageParser<PkPlayerInfo>(() => new PkPlayerInfo());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<PkPlayerInfo> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::AcFunDanmu.PkPlayerInfoReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public PkPlayerInfo() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public PkPlayerInfo(PkPlayerInfo other) : this() {
      a_ = other.a_ != null ? other.a_.Clone() : null;
      b_ = other.b_;
      c_ = other.c_;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public PkPlayerInfo Clone() {
      return new PkPlayerInfo(this);
    }

    /// <summary>Field number for the "a" field.</summary>
    public const int AFieldNumber = 1;
    private global::AcFunDanmu.ZtLiveUserInfo a_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public global::AcFunDanmu.ZtLiveUserInfo A {
      get { return a_; }
      set {
        a_ = value;
      }
    }

    /// <summary>Field number for the "b" field.</summary>
    public const int BFieldNumber = 2;
    private string b_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string B {
      get { return b_; }
      set {
        b_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "c" field.</summary>
    public const int CFieldNumber = 3;
    private bool c_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool C {
      get { return c_; }
      set {
        c_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as PkPlayerInfo);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(PkPlayerInfo other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (!object.Equals(A, other.A)) return false;
      if (B != other.B) return false;
      if (C != other.C) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (a_ != null) hash ^= A.GetHashCode();
      if (B.Length != 0) hash ^= B.GetHashCode();
      if (C != false) hash ^= C.GetHashCode();
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
      if (a_ != null) {
        output.WriteRawTag(10);
        output.WriteMessage(A);
      }
      if (B.Length != 0) {
        output.WriteRawTag(18);
        output.WriteString(B);
      }
      if (C != false) {
        output.WriteRawTag(24);
        output.WriteBool(C);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    void pb::IBufferMessage.InternalWriteTo(ref pb::WriteContext output) {
      if (a_ != null) {
        output.WriteRawTag(10);
        output.WriteMessage(A);
      }
      if (B.Length != 0) {
        output.WriteRawTag(18);
        output.WriteString(B);
      }
      if (C != false) {
        output.WriteRawTag(24);
        output.WriteBool(C);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(ref output);
      }
    }
    #endif

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (a_ != null) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(A);
      }
      if (B.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(B);
      }
      if (C != false) {
        size += 1 + 1;
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(PkPlayerInfo other) {
      if (other == null) {
        return;
      }
      if (other.a_ != null) {
        if (a_ == null) {
          A = new global::AcFunDanmu.ZtLiveUserInfo();
        }
        A.MergeFrom(other.A);
      }
      if (other.B.Length != 0) {
        B = other.B;
      }
      if (other.C != false) {
        C = other.C;
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
            if (a_ == null) {
              A = new global::AcFunDanmu.ZtLiveUserInfo();
            }
            input.ReadMessage(A);
            break;
          }
          case 18: {
            B = input.ReadString();
            break;
          }
          case 24: {
            C = input.ReadBool();
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
            if (a_ == null) {
              A = new global::AcFunDanmu.ZtLiveUserInfo();
            }
            input.ReadMessage(A);
            break;
          }
          case 18: {
            B = input.ReadString();
            break;
          }
          case 24: {
            C = input.ReadBool();
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
