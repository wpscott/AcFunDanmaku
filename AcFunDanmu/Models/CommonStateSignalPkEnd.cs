// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: CommonStateSignalPkEnd.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace AcFunDanmu {

  /// <summary>Holder for reflection information generated from CommonStateSignalPkEnd.proto</summary>
  public static partial class CommonStateSignalPkEndReflection {

    #region Descriptor
    /// <summary>File descriptor for CommonStateSignalPkEnd.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static CommonStateSignalPkEndReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "ChxDb21tb25TdGF0ZVNpZ25hbFBrRW5kLnByb3RvEgpBY0Z1bkRhbm11IsQB",
            "ChZDb21tb25TdGF0ZVNpZ25hbFBrRW5kEgkKAWEYASABKAkSNQoBYhgCIAEo",
            "DjIqLkFjRnVuRGFubXUuQ29tbW9uU3RhdGVTaWduYWxQa0VuZC51bmtub3du",
            "EgkKAWMYAyABKAkiXQoHdW5rbm93bhIFCgFkEAASBQoBZRABEgUKAWYQAhIF",
            "CgFnEAMSBQoBaBAEEgUKAWkQBRIFCgFqEAYSBQoBaxAHEgUKAWwQCBIFCgFt",
            "EAkSBQoBbhAKEgUKAW8QC2IGcHJvdG8z"));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { },
          new pbr::GeneratedClrTypeInfo(null, null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::AcFunDanmu.CommonStateSignalPkEnd), global::AcFunDanmu.CommonStateSignalPkEnd.Parser, new[]{ "A", "B", "C" }, null, new[]{ typeof(global::AcFunDanmu.CommonStateSignalPkEnd.Types.unknown) }, null, null)
          }));
    }
    #endregion

  }
  #region Messages
  public sealed partial class CommonStateSignalPkEnd : pb::IMessage<CommonStateSignalPkEnd>
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      , pb::IBufferMessage
  #endif
  {
    private static readonly pb::MessageParser<CommonStateSignalPkEnd> _parser = new pb::MessageParser<CommonStateSignalPkEnd>(() => new CommonStateSignalPkEnd());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<CommonStateSignalPkEnd> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::AcFunDanmu.CommonStateSignalPkEndReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public CommonStateSignalPkEnd() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public CommonStateSignalPkEnd(CommonStateSignalPkEnd other) : this() {
      a_ = other.a_;
      b_ = other.b_;
      c_ = other.c_;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public CommonStateSignalPkEnd Clone() {
      return new CommonStateSignalPkEnd(this);
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
    private global::AcFunDanmu.CommonStateSignalPkEnd.Types.unknown b_ = global::AcFunDanmu.CommonStateSignalPkEnd.Types.unknown.D;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public global::AcFunDanmu.CommonStateSignalPkEnd.Types.unknown B {
      get { return b_; }
      set {
        b_ = value;
      }
    }

    /// <summary>Field number for the "c" field.</summary>
    public const int CFieldNumber = 3;
    private string c_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string C {
      get { return c_; }
      set {
        c_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as CommonStateSignalPkEnd);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(CommonStateSignalPkEnd other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (A != other.A) return false;
      if (B != other.B) return false;
      if (C != other.C) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (A.Length != 0) hash ^= A.GetHashCode();
      if (B != global::AcFunDanmu.CommonStateSignalPkEnd.Types.unknown.D) hash ^= B.GetHashCode();
      if (C.Length != 0) hash ^= C.GetHashCode();
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
      if (B != global::AcFunDanmu.CommonStateSignalPkEnd.Types.unknown.D) {
        output.WriteRawTag(16);
        output.WriteEnum((int) B);
      }
      if (C.Length != 0) {
        output.WriteRawTag(26);
        output.WriteString(C);
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
      if (B != global::AcFunDanmu.CommonStateSignalPkEnd.Types.unknown.D) {
        output.WriteRawTag(16);
        output.WriteEnum((int) B);
      }
      if (C.Length != 0) {
        output.WriteRawTag(26);
        output.WriteString(C);
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
      if (B != global::AcFunDanmu.CommonStateSignalPkEnd.Types.unknown.D) {
        size += 1 + pb::CodedOutputStream.ComputeEnumSize((int) B);
      }
      if (C.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(C);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(CommonStateSignalPkEnd other) {
      if (other == null) {
        return;
      }
      if (other.A.Length != 0) {
        A = other.A;
      }
      if (other.B != global::AcFunDanmu.CommonStateSignalPkEnd.Types.unknown.D) {
        B = other.B;
      }
      if (other.C.Length != 0) {
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
          case 16: {
            B = (global::AcFunDanmu.CommonStateSignalPkEnd.Types.unknown) input.ReadEnum();
            break;
          }
          case 26: {
            C = input.ReadString();
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
          case 16: {
            B = (global::AcFunDanmu.CommonStateSignalPkEnd.Types.unknown) input.ReadEnum();
            break;
          }
          case 26: {
            C = input.ReadString();
            break;
          }
        }
      }
    }
    #endif

    #region Nested types
    /// <summary>Container for nested types declared in the CommonStateSignalPkEnd message type.</summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static partial class Types {
      public enum unknown {
        [pbr::OriginalName("d")] D = 0,
        [pbr::OriginalName("e")] E = 1,
        [pbr::OriginalName("f")] F = 2,
        [pbr::OriginalName("g")] G = 3,
        [pbr::OriginalName("h")] H = 4,
        [pbr::OriginalName("i")] I = 5,
        [pbr::OriginalName("j")] J = 6,
        [pbr::OriginalName("k")] K = 7,
        [pbr::OriginalName("l")] L = 8,
        [pbr::OriginalName("m")] M = 9,
        [pbr::OriginalName("n")] N = 10,
        [pbr::OriginalName("o")] O = 11,
      }

    }
    #endregion

  }

  #endregion

}

#endregion Designer generated code
