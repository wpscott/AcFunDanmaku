// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: ZtCommonInfo.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace AcFunDanmu {

  /// <summary>Holder for reflection information generated from ZtCommonInfo.proto</summary>
  public static partial class ZtCommonInfoReflection {

    #region Descriptor
    /// <summary>File descriptor for ZtCommonInfo.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static ZtCommonInfoReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "ChJadENvbW1vbkluZm8ucHJvdG8SCkFjRnVuRGFubXUi9AEKDFp0Q29tbW9u",
            "SW5mbxILCgNrcG4YASABKAkSCwoDa3BmGAIgASgJEg4KBnN1YkJpehgDIAEo",
            "CRILCgN1aWQYBCABKAMSCwoDZGlkGAUgASgJEhAKCGNsaWVudElwGAYgASgD",
            "Eg4KBmFwcFZlchgHIAEoCRILCgN2ZXIYCCABKAkSCwoDbGF0GAkgASgJEgsK",
            "A2xvbhgKIAEoCRILCgNtb2QYCyABKAkSCwoDbmV0GAwgASgJEgsKA3N5cxgN",
            "IAEoCRIJCgFjGA4gASgJEhAKCGxhbmd1YWdlGA8gASgJEhMKC2NvdW50cnlD",
            "b2RlGBAgASgJYgZwcm90bzM="));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { },
          new pbr::GeneratedClrTypeInfo(null, null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::AcFunDanmu.ZtCommonInfo), global::AcFunDanmu.ZtCommonInfo.Parser, new[]{ "Kpn", "Kpf", "SubBiz", "Uid", "Did", "ClientIp", "AppVer", "Ver", "Lat", "Lon", "Mod", "Net", "Sys", "C", "Language", "CountryCode" }, null, null, null, null)
          }));
    }
    #endregion

  }
  #region Messages
  public sealed partial class ZtCommonInfo : pb::IMessage<ZtCommonInfo>
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      , pb::IBufferMessage
  #endif
  {
    private static readonly pb::MessageParser<ZtCommonInfo> _parser = new pb::MessageParser<ZtCommonInfo>(() => new ZtCommonInfo());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pb::MessageParser<ZtCommonInfo> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::AcFunDanmu.ZtCommonInfoReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public ZtCommonInfo() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public ZtCommonInfo(ZtCommonInfo other) : this() {
      kpn_ = other.kpn_;
      kpf_ = other.kpf_;
      subBiz_ = other.subBiz_;
      uid_ = other.uid_;
      did_ = other.did_;
      clientIp_ = other.clientIp_;
      appVer_ = other.appVer_;
      ver_ = other.ver_;
      lat_ = other.lat_;
      lon_ = other.lon_;
      mod_ = other.mod_;
      net_ = other.net_;
      sys_ = other.sys_;
      c_ = other.c_;
      language_ = other.language_;
      countryCode_ = other.countryCode_;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public ZtCommonInfo Clone() {
      return new ZtCommonInfo(this);
    }

    /// <summary>Field number for the "kpn" field.</summary>
    public const int KpnFieldNumber = 1;
    private string kpn_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public string Kpn {
      get { return kpn_; }
      set {
        kpn_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "kpf" field.</summary>
    public const int KpfFieldNumber = 2;
    private string kpf_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public string Kpf {
      get { return kpf_; }
      set {
        kpf_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "subBiz" field.</summary>
    public const int SubBizFieldNumber = 3;
    private string subBiz_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public string SubBiz {
      get { return subBiz_; }
      set {
        subBiz_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "uid" field.</summary>
    public const int UidFieldNumber = 4;
    private long uid_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public long Uid {
      get { return uid_; }
      set {
        uid_ = value;
      }
    }

    /// <summary>Field number for the "did" field.</summary>
    public const int DidFieldNumber = 5;
    private string did_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public string Did {
      get { return did_; }
      set {
        did_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "clientIp" field.</summary>
    public const int ClientIpFieldNumber = 6;
    private long clientIp_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public long ClientIp {
      get { return clientIp_; }
      set {
        clientIp_ = value;
      }
    }

    /// <summary>Field number for the "appVer" field.</summary>
    public const int AppVerFieldNumber = 7;
    private string appVer_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public string AppVer {
      get { return appVer_; }
      set {
        appVer_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "ver" field.</summary>
    public const int VerFieldNumber = 8;
    private string ver_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public string Ver {
      get { return ver_; }
      set {
        ver_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "lat" field.</summary>
    public const int LatFieldNumber = 9;
    private string lat_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public string Lat {
      get { return lat_; }
      set {
        lat_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "lon" field.</summary>
    public const int LonFieldNumber = 10;
    private string lon_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public string Lon {
      get { return lon_; }
      set {
        lon_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "mod" field.</summary>
    public const int ModFieldNumber = 11;
    private string mod_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public string Mod {
      get { return mod_; }
      set {
        mod_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "net" field.</summary>
    public const int NetFieldNumber = 12;
    private string net_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public string Net {
      get { return net_; }
      set {
        net_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "sys" field.</summary>
    public const int SysFieldNumber = 13;
    private string sys_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public string Sys {
      get { return sys_; }
      set {
        sys_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "c" field.</summary>
    public const int CFieldNumber = 14;
    private string c_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public string C {
      get { return c_; }
      set {
        c_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "language" field.</summary>
    public const int LanguageFieldNumber = 15;
    private string language_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public string Language {
      get { return language_; }
      set {
        language_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "countryCode" field.</summary>
    public const int CountryCodeFieldNumber = 16;
    private string countryCode_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public string CountryCode {
      get { return countryCode_; }
      set {
        countryCode_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override bool Equals(object other) {
      return Equals(other as ZtCommonInfo);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool Equals(ZtCommonInfo other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (Kpn != other.Kpn) return false;
      if (Kpf != other.Kpf) return false;
      if (SubBiz != other.SubBiz) return false;
      if (Uid != other.Uid) return false;
      if (Did != other.Did) return false;
      if (ClientIp != other.ClientIp) return false;
      if (AppVer != other.AppVer) return false;
      if (Ver != other.Ver) return false;
      if (Lat != other.Lat) return false;
      if (Lon != other.Lon) return false;
      if (Mod != other.Mod) return false;
      if (Net != other.Net) return false;
      if (Sys != other.Sys) return false;
      if (C != other.C) return false;
      if (Language != other.Language) return false;
      if (CountryCode != other.CountryCode) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override int GetHashCode() {
      int hash = 1;
      if (Kpn.Length != 0) hash ^= Kpn.GetHashCode();
      if (Kpf.Length != 0) hash ^= Kpf.GetHashCode();
      if (SubBiz.Length != 0) hash ^= SubBiz.GetHashCode();
      if (Uid != 0L) hash ^= Uid.GetHashCode();
      if (Did.Length != 0) hash ^= Did.GetHashCode();
      if (ClientIp != 0L) hash ^= ClientIp.GetHashCode();
      if (AppVer.Length != 0) hash ^= AppVer.GetHashCode();
      if (Ver.Length != 0) hash ^= Ver.GetHashCode();
      if (Lat.Length != 0) hash ^= Lat.GetHashCode();
      if (Lon.Length != 0) hash ^= Lon.GetHashCode();
      if (Mod.Length != 0) hash ^= Mod.GetHashCode();
      if (Net.Length != 0) hash ^= Net.GetHashCode();
      if (Sys.Length != 0) hash ^= Sys.GetHashCode();
      if (C.Length != 0) hash ^= C.GetHashCode();
      if (Language.Length != 0) hash ^= Language.GetHashCode();
      if (CountryCode.Length != 0) hash ^= CountryCode.GetHashCode();
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
      if (Kpn.Length != 0) {
        output.WriteRawTag(10);
        output.WriteString(Kpn);
      }
      if (Kpf.Length != 0) {
        output.WriteRawTag(18);
        output.WriteString(Kpf);
      }
      if (SubBiz.Length != 0) {
        output.WriteRawTag(26);
        output.WriteString(SubBiz);
      }
      if (Uid != 0L) {
        output.WriteRawTag(32);
        output.WriteInt64(Uid);
      }
      if (Did.Length != 0) {
        output.WriteRawTag(42);
        output.WriteString(Did);
      }
      if (ClientIp != 0L) {
        output.WriteRawTag(48);
        output.WriteInt64(ClientIp);
      }
      if (AppVer.Length != 0) {
        output.WriteRawTag(58);
        output.WriteString(AppVer);
      }
      if (Ver.Length != 0) {
        output.WriteRawTag(66);
        output.WriteString(Ver);
      }
      if (Lat.Length != 0) {
        output.WriteRawTag(74);
        output.WriteString(Lat);
      }
      if (Lon.Length != 0) {
        output.WriteRawTag(82);
        output.WriteString(Lon);
      }
      if (Mod.Length != 0) {
        output.WriteRawTag(90);
        output.WriteString(Mod);
      }
      if (Net.Length != 0) {
        output.WriteRawTag(98);
        output.WriteString(Net);
      }
      if (Sys.Length != 0) {
        output.WriteRawTag(106);
        output.WriteString(Sys);
      }
      if (C.Length != 0) {
        output.WriteRawTag(114);
        output.WriteString(C);
      }
      if (Language.Length != 0) {
        output.WriteRawTag(122);
        output.WriteString(Language);
      }
      if (CountryCode.Length != 0) {
        output.WriteRawTag(130, 1);
        output.WriteString(CountryCode);
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
      if (Kpn.Length != 0) {
        output.WriteRawTag(10);
        output.WriteString(Kpn);
      }
      if (Kpf.Length != 0) {
        output.WriteRawTag(18);
        output.WriteString(Kpf);
      }
      if (SubBiz.Length != 0) {
        output.WriteRawTag(26);
        output.WriteString(SubBiz);
      }
      if (Uid != 0L) {
        output.WriteRawTag(32);
        output.WriteInt64(Uid);
      }
      if (Did.Length != 0) {
        output.WriteRawTag(42);
        output.WriteString(Did);
      }
      if (ClientIp != 0L) {
        output.WriteRawTag(48);
        output.WriteInt64(ClientIp);
      }
      if (AppVer.Length != 0) {
        output.WriteRawTag(58);
        output.WriteString(AppVer);
      }
      if (Ver.Length != 0) {
        output.WriteRawTag(66);
        output.WriteString(Ver);
      }
      if (Lat.Length != 0) {
        output.WriteRawTag(74);
        output.WriteString(Lat);
      }
      if (Lon.Length != 0) {
        output.WriteRawTag(82);
        output.WriteString(Lon);
      }
      if (Mod.Length != 0) {
        output.WriteRawTag(90);
        output.WriteString(Mod);
      }
      if (Net.Length != 0) {
        output.WriteRawTag(98);
        output.WriteString(Net);
      }
      if (Sys.Length != 0) {
        output.WriteRawTag(106);
        output.WriteString(Sys);
      }
      if (C.Length != 0) {
        output.WriteRawTag(114);
        output.WriteString(C);
      }
      if (Language.Length != 0) {
        output.WriteRawTag(122);
        output.WriteString(Language);
      }
      if (CountryCode.Length != 0) {
        output.WriteRawTag(130, 1);
        output.WriteString(CountryCode);
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
      if (Kpn.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(Kpn);
      }
      if (Kpf.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(Kpf);
      }
      if (SubBiz.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(SubBiz);
      }
      if (Uid != 0L) {
        size += 1 + pb::CodedOutputStream.ComputeInt64Size(Uid);
      }
      if (Did.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(Did);
      }
      if (ClientIp != 0L) {
        size += 1 + pb::CodedOutputStream.ComputeInt64Size(ClientIp);
      }
      if (AppVer.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(AppVer);
      }
      if (Ver.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(Ver);
      }
      if (Lat.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(Lat);
      }
      if (Lon.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(Lon);
      }
      if (Mod.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(Mod);
      }
      if (Net.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(Net);
      }
      if (Sys.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(Sys);
      }
      if (C.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(C);
      }
      if (Language.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(Language);
      }
      if (CountryCode.Length != 0) {
        size += 2 + pb::CodedOutputStream.ComputeStringSize(CountryCode);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void MergeFrom(ZtCommonInfo other) {
      if (other == null) {
        return;
      }
      if (other.Kpn.Length != 0) {
        Kpn = other.Kpn;
      }
      if (other.Kpf.Length != 0) {
        Kpf = other.Kpf;
      }
      if (other.SubBiz.Length != 0) {
        SubBiz = other.SubBiz;
      }
      if (other.Uid != 0L) {
        Uid = other.Uid;
      }
      if (other.Did.Length != 0) {
        Did = other.Did;
      }
      if (other.ClientIp != 0L) {
        ClientIp = other.ClientIp;
      }
      if (other.AppVer.Length != 0) {
        AppVer = other.AppVer;
      }
      if (other.Ver.Length != 0) {
        Ver = other.Ver;
      }
      if (other.Lat.Length != 0) {
        Lat = other.Lat;
      }
      if (other.Lon.Length != 0) {
        Lon = other.Lon;
      }
      if (other.Mod.Length != 0) {
        Mod = other.Mod;
      }
      if (other.Net.Length != 0) {
        Net = other.Net;
      }
      if (other.Sys.Length != 0) {
        Sys = other.Sys;
      }
      if (other.C.Length != 0) {
        C = other.C;
      }
      if (other.Language.Length != 0) {
        Language = other.Language;
      }
      if (other.CountryCode.Length != 0) {
        CountryCode = other.CountryCode;
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
            Kpn = input.ReadString();
            break;
          }
          case 18: {
            Kpf = input.ReadString();
            break;
          }
          case 26: {
            SubBiz = input.ReadString();
            break;
          }
          case 32: {
            Uid = input.ReadInt64();
            break;
          }
          case 42: {
            Did = input.ReadString();
            break;
          }
          case 48: {
            ClientIp = input.ReadInt64();
            break;
          }
          case 58: {
            AppVer = input.ReadString();
            break;
          }
          case 66: {
            Ver = input.ReadString();
            break;
          }
          case 74: {
            Lat = input.ReadString();
            break;
          }
          case 82: {
            Lon = input.ReadString();
            break;
          }
          case 90: {
            Mod = input.ReadString();
            break;
          }
          case 98: {
            Net = input.ReadString();
            break;
          }
          case 106: {
            Sys = input.ReadString();
            break;
          }
          case 114: {
            C = input.ReadString();
            break;
          }
          case 122: {
            Language = input.ReadString();
            break;
          }
          case 130: {
            CountryCode = input.ReadString();
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
            Kpn = input.ReadString();
            break;
          }
          case 18: {
            Kpf = input.ReadString();
            break;
          }
          case 26: {
            SubBiz = input.ReadString();
            break;
          }
          case 32: {
            Uid = input.ReadInt64();
            break;
          }
          case 42: {
            Did = input.ReadString();
            break;
          }
          case 48: {
            ClientIp = input.ReadInt64();
            break;
          }
          case 58: {
            AppVer = input.ReadString();
            break;
          }
          case 66: {
            Ver = input.ReadString();
            break;
          }
          case 74: {
            Lat = input.ReadString();
            break;
          }
          case 82: {
            Lon = input.ReadString();
            break;
          }
          case 90: {
            Mod = input.ReadString();
            break;
          }
          case 98: {
            Net = input.ReadString();
            break;
          }
          case 106: {
            Sys = input.ReadString();
            break;
          }
          case 114: {
            C = input.ReadString();
            break;
          }
          case 122: {
            Language = input.ReadString();
            break;
          }
          case 130: {
            CountryCode = input.ReadString();
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
