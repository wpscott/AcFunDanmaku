// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: CommonStateSignalPKInvitation.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace AcFunDanmu {

  /// <summary>Holder for reflection information generated from CommonStateSignalPKInvitation.proto</summary>
  public static partial class CommonStateSignalPKInvitationReflection {

    #region Descriptor
    /// <summary>File descriptor for CommonStateSignalPKInvitation.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static CommonStateSignalPKInvitationReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "CiNDb21tb25TdGF0ZVNpZ25hbFBLSW52aXRhdGlvbi5wcm90bxIKQWNGdW5E",
            "YW5tdRoSUGtQbGF5ZXJJbmZvLnByb3RvIloKHUNvbW1vblN0YXRlU2lnbmFs",
            "UEtJbnZpdGF0aW9uEgkKAWEYASABKAkSIwoBYhgCIAEoCzIYLkFjRnVuRGFu",
            "bXUuUGtQbGF5ZXJJbmZvEgkKAWMYAyABKANiBnByb3RvMw=="));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { global::AcFunDanmu.PkPlayerInfoReflection.Descriptor, },
          new pbr::GeneratedClrTypeInfo(null, null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::AcFunDanmu.CommonStateSignalPKInvitation), global::AcFunDanmu.CommonStateSignalPKInvitation.Parser, new[]{ "A", "B", "C" }, null, null, null, null)
          }));
    }
    #endregion

  }
  #region Messages
  public sealed partial class CommonStateSignalPKInvitation : pb::IMessage<CommonStateSignalPKInvitation>
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      , pb::IBufferMessage
  #endif
  {
    private static readonly pb::MessageParser<CommonStateSignalPKInvitation> _parser = new pb::MessageParser<CommonStateSignalPKInvitation>(() => new CommonStateSignalPKInvitation());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<CommonStateSignalPKInvitation> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::AcFunDanmu.CommonStateSignalPKInvitationReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public CommonStateSignalPKInvitation() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public CommonStateSignalPKInvitation(CommonStateSignalPKInvitation other) : this() {
      a_ = other.a_;
      b_ = other.b_ != null ? other.b_.Clone() : null;
      c_ = other.c_;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public CommonStateSignalPKInvitation Clone() {
      return new CommonStateSignalPKInvitation(this);
    }

    /// <summary>Field number for the "a" field.</summary>
    public const int AFieldNumber = 1;
    private string a_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string A {
      get { return a_; }
      set {
        a_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "b" field.</summary>
    public const int BFieldNumber = 2;
    private global::AcFunDanmu.PkPlayerInfo b_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public global::AcFunDanmu.PkPlayerInfo B {
      get { return b_; }
      set {
        b_ = value;
      }
    }

    /// <summary>Field number for the "c" field.</summary>
    public const int CFieldNumber = 3;
    private long c_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public long C {
      get { return c_; }
      set {
        c_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as CommonStateSignalPKInvitation);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(CommonStateSignalPKInvitation other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (A != other.A) return false;
      if (!object.Equals(B, other.B)) return false;
      if (C != other.C) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (A.Length != 0) hash ^= A.GetHashCode();
      if (b_ != null) hash ^= B.GetHashCode();
      if (C != 0L) hash ^= C.GetHashCode();
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
      if (A.Length != 0) {
        output.WriteRawTag(10);
        output.WriteString(A);
      }
      if (b_ != null) {
        output.WriteRawTag(18);
        output.WriteMessage(B);
      }
      if (C != 0L) {
        output.WriteRawTag(24);
        output.WriteInt64(C);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    void pb::IBufferMessage.InternalWriteTo(ref pb::WriteContext output) {
      if (A.Length != 0) {
        output.WriteRawTag(10);
        output.WriteString(A);
      }
      if (b_ != null) {
        output.WriteRawTag(18);
        output.WriteMessage(B);
      }
      if (C != 0L) {
        output.WriteRawTag(24);
        output.WriteInt64(C);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(ref output);
      }
    }
    #endif

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (A.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(A);
      }
      if (b_ != null) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(B);
      }
      if (C != 0L) {
        size += 1 + pb::CodedOutputStream.ComputeInt64Size(C);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(CommonStateSignalPKInvitation other) {
      if (other == null) {
        return;
      }
      if (other.A.Length != 0) {
        A = other.A;
      }
      if (other.b_ != null) {
        if (b_ == null) {
          B = new global::AcFunDanmu.PkPlayerInfo();
        }
        B.MergeFrom(other.B);
      }
      if (other.C != 0L) {
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
            A = input.ReadString();
            break;
          }
          case 18: {
            if (b_ == null) {
              B = new global::AcFunDanmu.PkPlayerInfo();
            }
            input.ReadMessage(B);
            break;
          }
          case 24: {
            C = input.ReadInt64();
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
            A = input.ReadString();
            break;
          }
          case 18: {
            if (b_ == null) {
              B = new global::AcFunDanmu.PkPlayerInfo();
            }
            input.ReadMessage(B);
            break;
          }
          case 24: {
            C = input.ReadInt64();
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
