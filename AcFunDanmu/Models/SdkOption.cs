// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: SdkOption.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace AcFunDanmu {

  /// <summary>Holder for reflection information generated from SdkOption.proto</summary>
  public static partial class SdkOptionReflection {

    #region Descriptor
    /// <summary>File descriptor for SdkOption.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static SdkOptionReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "Cg9TZGtPcHRpb24ucHJvdG8SCkFjRnVuRGFubXUigQEKCVNka09wdGlvbhId",
            "ChVyZXBvcnRJbnRlcnZhbFNlY29uZHMYASABKAUSFgoOcmVwb3J0U2VjdXJp",
            "dHkYAiABKAkSJAocbHo0Q29tcHJlc3Npb25UaHJlc2hvbGRCeXRlcxgDIAEo",
            "BRIXCg9uZXRDaGVja1NlcnZlcnMYBCADKAliBnByb3RvMw=="));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { },
          new pbr::GeneratedClrTypeInfo(null, null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::AcFunDanmu.SdkOption), global::AcFunDanmu.SdkOption.Parser, new[]{ "ReportIntervalSeconds", "ReportSecurity", "Lz4CompressionThresholdBytes", "NetCheckServers" }, null, null, null, null)
          }));
    }
    #endregion

  }
  #region Messages
  public sealed partial class SdkOption : pb::IMessage<SdkOption>
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      , pb::IBufferMessage
  #endif
  {
    private static readonly pb::MessageParser<SdkOption> _parser = new pb::MessageParser<SdkOption>(() => new SdkOption());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pb::MessageParser<SdkOption> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::AcFunDanmu.SdkOptionReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public SdkOption() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public SdkOption(SdkOption other) : this() {
      reportIntervalSeconds_ = other.reportIntervalSeconds_;
      reportSecurity_ = other.reportSecurity_;
      lz4CompressionThresholdBytes_ = other.lz4CompressionThresholdBytes_;
      netCheckServers_ = other.netCheckServers_.Clone();
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public SdkOption Clone() {
      return new SdkOption(this);
    }

    /// <summary>Field number for the "reportIntervalSeconds" field.</summary>
    public const int ReportIntervalSecondsFieldNumber = 1;
    private int reportIntervalSeconds_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public int ReportIntervalSeconds {
      get { return reportIntervalSeconds_; }
      set {
        reportIntervalSeconds_ = value;
      }
    }

    /// <summary>Field number for the "reportSecurity" field.</summary>
    public const int ReportSecurityFieldNumber = 2;
    private string reportSecurity_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public string ReportSecurity {
      get { return reportSecurity_; }
      set {
        reportSecurity_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "lz4CompressionThresholdBytes" field.</summary>
    public const int Lz4CompressionThresholdBytesFieldNumber = 3;
    private int lz4CompressionThresholdBytes_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public int Lz4CompressionThresholdBytes {
      get { return lz4CompressionThresholdBytes_; }
      set {
        lz4CompressionThresholdBytes_ = value;
      }
    }

    /// <summary>Field number for the "netCheckServers" field.</summary>
    public const int NetCheckServersFieldNumber = 4;
    private static readonly pb::FieldCodec<string> _repeated_netCheckServers_codec
        = pb::FieldCodec.ForString(34);
    private readonly pbc::RepeatedField<string> netCheckServers_ = new pbc::RepeatedField<string>();
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public pbc::RepeatedField<string> NetCheckServers {
      get { return netCheckServers_; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override bool Equals(object other) {
      return Equals(other as SdkOption);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool Equals(SdkOption other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (ReportIntervalSeconds != other.ReportIntervalSeconds) return false;
      if (ReportSecurity != other.ReportSecurity) return false;
      if (Lz4CompressionThresholdBytes != other.Lz4CompressionThresholdBytes) return false;
      if(!netCheckServers_.Equals(other.netCheckServers_)) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override int GetHashCode() {
      int hash = 1;
      if (ReportIntervalSeconds != 0) hash ^= ReportIntervalSeconds.GetHashCode();
      if (ReportSecurity.Length != 0) hash ^= ReportSecurity.GetHashCode();
      if (Lz4CompressionThresholdBytes != 0) hash ^= Lz4CompressionThresholdBytes.GetHashCode();
      hash ^= netCheckServers_.GetHashCode();
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
      if (ReportIntervalSeconds != 0) {
        output.WriteRawTag(8);
        output.WriteInt32(ReportIntervalSeconds);
      }
      if (ReportSecurity.Length != 0) {
        output.WriteRawTag(18);
        output.WriteString(ReportSecurity);
      }
      if (Lz4CompressionThresholdBytes != 0) {
        output.WriteRawTag(24);
        output.WriteInt32(Lz4CompressionThresholdBytes);
      }
      netCheckServers_.WriteTo(output, _repeated_netCheckServers_codec);
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    void pb::IBufferMessage.InternalWriteTo(ref pb::WriteContext output) {
      if (ReportIntervalSeconds != 0) {
        output.WriteRawTag(8);
        output.WriteInt32(ReportIntervalSeconds);
      }
      if (ReportSecurity.Length != 0) {
        output.WriteRawTag(18);
        output.WriteString(ReportSecurity);
      }
      if (Lz4CompressionThresholdBytes != 0) {
        output.WriteRawTag(24);
        output.WriteInt32(Lz4CompressionThresholdBytes);
      }
      netCheckServers_.WriteTo(ref output, _repeated_netCheckServers_codec);
      if (_unknownFields != null) {
        _unknownFields.WriteTo(ref output);
      }
    }
    #endif

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public int CalculateSize() {
      int size = 0;
      if (ReportIntervalSeconds != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(ReportIntervalSeconds);
      }
      if (ReportSecurity.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(ReportSecurity);
      }
      if (Lz4CompressionThresholdBytes != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(Lz4CompressionThresholdBytes);
      }
      size += netCheckServers_.CalculateSize(_repeated_netCheckServers_codec);
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void MergeFrom(SdkOption other) {
      if (other == null) {
        return;
      }
      if (other.ReportIntervalSeconds != 0) {
        ReportIntervalSeconds = other.ReportIntervalSeconds;
      }
      if (other.ReportSecurity.Length != 0) {
        ReportSecurity = other.ReportSecurity;
      }
      if (other.Lz4CompressionThresholdBytes != 0) {
        Lz4CompressionThresholdBytes = other.Lz4CompressionThresholdBytes;
      }
      netCheckServers_.Add(other.netCheckServers_);
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
            ReportIntervalSeconds = input.ReadInt32();
            break;
          }
          case 18: {
            ReportSecurity = input.ReadString();
            break;
          }
          case 24: {
            Lz4CompressionThresholdBytes = input.ReadInt32();
            break;
          }
          case 34: {
            netCheckServers_.AddEntriesFrom(input, _repeated_netCheckServers_codec);
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
            ReportIntervalSeconds = input.ReadInt32();
            break;
          }
          case 18: {
            ReportSecurity = input.ReadString();
            break;
          }
          case 24: {
            Lz4CompressionThresholdBytes = input.ReadInt32();
            break;
          }
          case 34: {
            netCheckServers_.AddEntriesFrom(ref input, _repeated_netCheckServers_codec);
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
