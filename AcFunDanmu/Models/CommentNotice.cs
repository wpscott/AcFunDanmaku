// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: CommentNotice.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace AcFunDanmu {

  /// <summary>Holder for reflection information generated from CommentNotice.proto</summary>
  public static partial class CommentNoticeReflection {

    #region Descriptor
    /// <summary>File descriptor for CommentNotice.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static CommentNoticeReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "ChNDb21tZW50Tm90aWNlLnByb3RvEgpBY0Z1bkRhbm11GgxCdXR0b24ucHJv",
            "dG8aEENsaWNrRXZlbnQucHJvdG8aEkltYWdlQ2RuTm9kZS5wcm90byL+AQoN",
            "Q29tbWVudE5vdGljZRIJCgFhGAEgASgJEgkKAWIYAiABKAwSCQoBYxgDIAEo",
            "CRIjCgFkGAQgAygLMhguQWNGdW5EYW5tdS5JbWFnZUNkbk5vZGUSIwoBZRgF",
            "IAMoCzIYLkFjRnVuRGFubXUuSW1hZ2VDZG5Ob2RlEgkKAWYYBiABKAkSCQoB",
            "ZxgHIAEoCRIdCgFoGAggASgLMhIuQWNGdW5EYW5tdS5CdXR0b24SIQoBaRgJ",
            "IAEoCzIWLkFjRnVuRGFubXUuQ2xpY2tFdmVudBIJCgFqGAogASgFEgkKAWsY",
            "CyABKAMSCQoBbBgMIAEoAxIJCgFtGA0gASgFYgZwcm90bzM="));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { global::AcFunDanmu.ButtonReflection.Descriptor, global::AcFunDanmu.ClickEventReflection.Descriptor, global::AcFunDanmu.ImageCdnNodeReflection.Descriptor, },
          new pbr::GeneratedClrTypeInfo(null, null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::AcFunDanmu.CommentNotice), global::AcFunDanmu.CommentNotice.Parser, new[]{ "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M" }, null, null, null, null)
          }));
    }
    #endregion

  }
  #region Messages
  public sealed partial class CommentNotice : pb::IMessage<CommentNotice>
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      , pb::IBufferMessage
  #endif
  {
    private static readonly pb::MessageParser<CommentNotice> _parser = new pb::MessageParser<CommentNotice>(() => new CommentNotice());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pb::MessageParser<CommentNotice> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::AcFunDanmu.CommentNoticeReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public CommentNotice() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public CommentNotice(CommentNotice other) : this() {
      a_ = other.a_;
      b_ = other.b_;
      c_ = other.c_;
      d_ = other.d_.Clone();
      e_ = other.e_.Clone();
      f_ = other.f_;
      g_ = other.g_;
      h_ = other.h_ != null ? other.h_.Clone() : null;
      i_ = other.i_ != null ? other.i_.Clone() : null;
      j_ = other.j_;
      k_ = other.k_;
      l_ = other.l_;
      m_ = other.m_;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public CommentNotice Clone() {
      return new CommentNotice(this);
    }

    /// <summary>Field number for the "a" field.</summary>
    public const int AFieldNumber = 1;
    private string a_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public string A {
      get { return a_; }
      set {
        a_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "b" field.</summary>
    public const int BFieldNumber = 2;
    private pb::ByteString b_ = pb::ByteString.Empty;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public pb::ByteString B {
      get { return b_; }
      set {
        b_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "c" field.</summary>
    public const int CFieldNumber = 3;
    private string c_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public string C {
      get { return c_; }
      set {
        c_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "d" field.</summary>
    public const int DFieldNumber = 4;
    private static readonly pb::FieldCodec<global::AcFunDanmu.ImageCdnNode> _repeated_d_codec
        = pb::FieldCodec.ForMessage(34, global::AcFunDanmu.ImageCdnNode.Parser);
    private readonly pbc::RepeatedField<global::AcFunDanmu.ImageCdnNode> d_ = new pbc::RepeatedField<global::AcFunDanmu.ImageCdnNode>();
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public pbc::RepeatedField<global::AcFunDanmu.ImageCdnNode> D {
      get { return d_; }
    }

    /// <summary>Field number for the "e" field.</summary>
    public const int EFieldNumber = 5;
    private static readonly pb::FieldCodec<global::AcFunDanmu.ImageCdnNode> _repeated_e_codec
        = pb::FieldCodec.ForMessage(42, global::AcFunDanmu.ImageCdnNode.Parser);
    private readonly pbc::RepeatedField<global::AcFunDanmu.ImageCdnNode> e_ = new pbc::RepeatedField<global::AcFunDanmu.ImageCdnNode>();
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public pbc::RepeatedField<global::AcFunDanmu.ImageCdnNode> E {
      get { return e_; }
    }

    /// <summary>Field number for the "f" field.</summary>
    public const int FFieldNumber = 6;
    private string f_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public string F {
      get { return f_; }
      set {
        f_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "g" field.</summary>
    public const int GFieldNumber = 7;
    private string g_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public string G {
      get { return g_; }
      set {
        g_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "h" field.</summary>
    public const int HFieldNumber = 8;
    private global::AcFunDanmu.Button h_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public global::AcFunDanmu.Button H {
      get { return h_; }
      set {
        h_ = value;
      }
    }

    /// <summary>Field number for the "i" field.</summary>
    public const int IFieldNumber = 9;
    private global::AcFunDanmu.ClickEvent i_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public global::AcFunDanmu.ClickEvent I {
      get { return i_; }
      set {
        i_ = value;
      }
    }

    /// <summary>Field number for the "j" field.</summary>
    public const int JFieldNumber = 10;
    private int j_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public int J {
      get { return j_; }
      set {
        j_ = value;
      }
    }

    /// <summary>Field number for the "k" field.</summary>
    public const int KFieldNumber = 11;
    private long k_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public long K {
      get { return k_; }
      set {
        k_ = value;
      }
    }

    /// <summary>Field number for the "l" field.</summary>
    public const int LFieldNumber = 12;
    private long l_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public long L {
      get { return l_; }
      set {
        l_ = value;
      }
    }

    /// <summary>Field number for the "m" field.</summary>
    public const int MFieldNumber = 13;
    private int m_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public int M {
      get { return m_; }
      set {
        m_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override bool Equals(object other) {
      return Equals(other as CommentNotice);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool Equals(CommentNotice other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (A != other.A) return false;
      if (B != other.B) return false;
      if (C != other.C) return false;
      if(!d_.Equals(other.d_)) return false;
      if(!e_.Equals(other.e_)) return false;
      if (F != other.F) return false;
      if (G != other.G) return false;
      if (!object.Equals(H, other.H)) return false;
      if (!object.Equals(I, other.I)) return false;
      if (J != other.J) return false;
      if (K != other.K) return false;
      if (L != other.L) return false;
      if (M != other.M) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override int GetHashCode() {
      int hash = 1;
      if (A.Length != 0) hash ^= A.GetHashCode();
      if (B.Length != 0) hash ^= B.GetHashCode();
      if (C.Length != 0) hash ^= C.GetHashCode();
      hash ^= d_.GetHashCode();
      hash ^= e_.GetHashCode();
      if (F.Length != 0) hash ^= F.GetHashCode();
      if (G.Length != 0) hash ^= G.GetHashCode();
      if (h_ != null) hash ^= H.GetHashCode();
      if (i_ != null) hash ^= I.GetHashCode();
      if (J != 0) hash ^= J.GetHashCode();
      if (K != 0L) hash ^= K.GetHashCode();
      if (L != 0L) hash ^= L.GetHashCode();
      if (M != 0) hash ^= M.GetHashCode();
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
      if (A.Length != 0) {
        output.WriteRawTag(10);
        output.WriteString(A);
      }
      if (B.Length != 0) {
        output.WriteRawTag(18);
        output.WriteBytes(B);
      }
      if (C.Length != 0) {
        output.WriteRawTag(26);
        output.WriteString(C);
      }
      d_.WriteTo(output, _repeated_d_codec);
      e_.WriteTo(output, _repeated_e_codec);
      if (F.Length != 0) {
        output.WriteRawTag(50);
        output.WriteString(F);
      }
      if (G.Length != 0) {
        output.WriteRawTag(58);
        output.WriteString(G);
      }
      if (h_ != null) {
        output.WriteRawTag(66);
        output.WriteMessage(H);
      }
      if (i_ != null) {
        output.WriteRawTag(74);
        output.WriteMessage(I);
      }
      if (J != 0) {
        output.WriteRawTag(80);
        output.WriteInt32(J);
      }
      if (K != 0L) {
        output.WriteRawTag(88);
        output.WriteInt64(K);
      }
      if (L != 0L) {
        output.WriteRawTag(96);
        output.WriteInt64(L);
      }
      if (M != 0) {
        output.WriteRawTag(104);
        output.WriteInt32(M);
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
      if (A.Length != 0) {
        output.WriteRawTag(10);
        output.WriteString(A);
      }
      if (B.Length != 0) {
        output.WriteRawTag(18);
        output.WriteBytes(B);
      }
      if (C.Length != 0) {
        output.WriteRawTag(26);
        output.WriteString(C);
      }
      d_.WriteTo(ref output, _repeated_d_codec);
      e_.WriteTo(ref output, _repeated_e_codec);
      if (F.Length != 0) {
        output.WriteRawTag(50);
        output.WriteString(F);
      }
      if (G.Length != 0) {
        output.WriteRawTag(58);
        output.WriteString(G);
      }
      if (h_ != null) {
        output.WriteRawTag(66);
        output.WriteMessage(H);
      }
      if (i_ != null) {
        output.WriteRawTag(74);
        output.WriteMessage(I);
      }
      if (J != 0) {
        output.WriteRawTag(80);
        output.WriteInt32(J);
      }
      if (K != 0L) {
        output.WriteRawTag(88);
        output.WriteInt64(K);
      }
      if (L != 0L) {
        output.WriteRawTag(96);
        output.WriteInt64(L);
      }
      if (M != 0) {
        output.WriteRawTag(104);
        output.WriteInt32(M);
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
      if (A.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(A);
      }
      if (B.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeBytesSize(B);
      }
      if (C.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(C);
      }
      size += d_.CalculateSize(_repeated_d_codec);
      size += e_.CalculateSize(_repeated_e_codec);
      if (F.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(F);
      }
      if (G.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(G);
      }
      if (h_ != null) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(H);
      }
      if (i_ != null) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(I);
      }
      if (J != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(J);
      }
      if (K != 0L) {
        size += 1 + pb::CodedOutputStream.ComputeInt64Size(K);
      }
      if (L != 0L) {
        size += 1 + pb::CodedOutputStream.ComputeInt64Size(L);
      }
      if (M != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(M);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void MergeFrom(CommentNotice other) {
      if (other == null) {
        return;
      }
      if (other.A.Length != 0) {
        A = other.A;
      }
      if (other.B.Length != 0) {
        B = other.B;
      }
      if (other.C.Length != 0) {
        C = other.C;
      }
      d_.Add(other.d_);
      e_.Add(other.e_);
      if (other.F.Length != 0) {
        F = other.F;
      }
      if (other.G.Length != 0) {
        G = other.G;
      }
      if (other.h_ != null) {
        if (h_ == null) {
          H = new global::AcFunDanmu.Button();
        }
        H.MergeFrom(other.H);
      }
      if (other.i_ != null) {
        if (i_ == null) {
          I = new global::AcFunDanmu.ClickEvent();
        }
        I.MergeFrom(other.I);
      }
      if (other.J != 0) {
        J = other.J;
      }
      if (other.K != 0L) {
        K = other.K;
      }
      if (other.L != 0L) {
        L = other.L;
      }
      if (other.M != 0) {
        M = other.M;
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
            A = input.ReadString();
            break;
          }
          case 18: {
            B = input.ReadBytes();
            break;
          }
          case 26: {
            C = input.ReadString();
            break;
          }
          case 34: {
            d_.AddEntriesFrom(input, _repeated_d_codec);
            break;
          }
          case 42: {
            e_.AddEntriesFrom(input, _repeated_e_codec);
            break;
          }
          case 50: {
            F = input.ReadString();
            break;
          }
          case 58: {
            G = input.ReadString();
            break;
          }
          case 66: {
            if (h_ == null) {
              H = new global::AcFunDanmu.Button();
            }
            input.ReadMessage(H);
            break;
          }
          case 74: {
            if (i_ == null) {
              I = new global::AcFunDanmu.ClickEvent();
            }
            input.ReadMessage(I);
            break;
          }
          case 80: {
            J = input.ReadInt32();
            break;
          }
          case 88: {
            K = input.ReadInt64();
            break;
          }
          case 96: {
            L = input.ReadInt64();
            break;
          }
          case 104: {
            M = input.ReadInt32();
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
            A = input.ReadString();
            break;
          }
          case 18: {
            B = input.ReadBytes();
            break;
          }
          case 26: {
            C = input.ReadString();
            break;
          }
          case 34: {
            d_.AddEntriesFrom(ref input, _repeated_d_codec);
            break;
          }
          case 42: {
            e_.AddEntriesFrom(ref input, _repeated_e_codec);
            break;
          }
          case 50: {
            F = input.ReadString();
            break;
          }
          case 58: {
            G = input.ReadString();
            break;
          }
          case 66: {
            if (h_ == null) {
              H = new global::AcFunDanmu.Button();
            }
            input.ReadMessage(H);
            break;
          }
          case 74: {
            if (i_ == null) {
              I = new global::AcFunDanmu.ClickEvent();
            }
            input.ReadMessage(I);
            break;
          }
          case 80: {
            J = input.ReadInt32();
            break;
          }
          case 88: {
            K = input.ReadInt64();
            break;
          }
          case 96: {
            L = input.ReadInt64();
            break;
          }
          case 104: {
            M = input.ReadInt32();
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
