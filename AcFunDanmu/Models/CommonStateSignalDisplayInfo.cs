// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: CommonStateSignalDisplayInfo.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace AcFunDanmu {

  /// <summary>Holder for reflection information generated from CommonStateSignalDisplayInfo.proto</summary>
  public static partial class CommonStateSignalDisplayInfoReflection {

    #region Descriptor
    /// <summary>File descriptor for CommonStateSignalDisplayInfo.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static CommonStateSignalDisplayInfoReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "CiJDb21tb25TdGF0ZVNpZ25hbERpc3BsYXlJbmZvLnByb3RvEgpBY0Z1bkRh",
            "bm11IlsKHENvbW1vblN0YXRlU2lnbmFsRGlzcGxheUluZm8SFQoNd2F0Y2hp",
            "bmdDb3VudBgBIAEoCRIRCglsaWtlQ291bnQYAiABKAkSEQoJbGlrZURlbHRh",
            "GAMgASgFYgZwcm90bzM="));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { },
          new pbr::GeneratedClrTypeInfo(null, null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::AcFunDanmu.CommonStateSignalDisplayInfo), global::AcFunDanmu.CommonStateSignalDisplayInfo.Parser, new[]{ "WatchingCount", "LikeCount", "LikeDelta" }, null, null, null, null)
          }));
    }
    #endregion

  }
  #region Messages
  public sealed partial class CommonStateSignalDisplayInfo : pb::IMessage<CommonStateSignalDisplayInfo>
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      , pb::IBufferMessage
  #endif
  {
    private static readonly pb::MessageParser<CommonStateSignalDisplayInfo> _parser = new pb::MessageParser<CommonStateSignalDisplayInfo>(() => new CommonStateSignalDisplayInfo());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pb::MessageParser<CommonStateSignalDisplayInfo> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::AcFunDanmu.CommonStateSignalDisplayInfoReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public CommonStateSignalDisplayInfo() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public CommonStateSignalDisplayInfo(CommonStateSignalDisplayInfo other) : this() {
      watchingCount_ = other.watchingCount_;
      likeCount_ = other.likeCount_;
      likeDelta_ = other.likeDelta_;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public CommonStateSignalDisplayInfo Clone() {
      return new CommonStateSignalDisplayInfo(this);
    }

    /// <summary>Field number for the "watchingCount" field.</summary>
    public const int WatchingCountFieldNumber = 1;
    private string watchingCount_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public string WatchingCount {
      get { return watchingCount_; }
      set {
        watchingCount_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "likeCount" field.</summary>
    public const int LikeCountFieldNumber = 2;
    private string likeCount_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public string LikeCount {
      get { return likeCount_; }
      set {
        likeCount_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "likeDelta" field.</summary>
    public const int LikeDeltaFieldNumber = 3;
    private int likeDelta_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public int LikeDelta {
      get { return likeDelta_; }
      set {
        likeDelta_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override bool Equals(object other) {
      return Equals(other as CommonStateSignalDisplayInfo);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool Equals(CommonStateSignalDisplayInfo other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (WatchingCount != other.WatchingCount) return false;
      if (LikeCount != other.LikeCount) return false;
      if (LikeDelta != other.LikeDelta) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override int GetHashCode() {
      int hash = 1;
      if (WatchingCount.Length != 0) hash ^= WatchingCount.GetHashCode();
      if (LikeCount.Length != 0) hash ^= LikeCount.GetHashCode();
      if (LikeDelta != 0) hash ^= LikeDelta.GetHashCode();
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
      if (WatchingCount.Length != 0) {
        output.WriteRawTag(10);
        output.WriteString(WatchingCount);
      }
      if (LikeCount.Length != 0) {
        output.WriteRawTag(18);
        output.WriteString(LikeCount);
      }
      if (LikeDelta != 0) {
        output.WriteRawTag(24);
        output.WriteInt32(LikeDelta);
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
      if (WatchingCount.Length != 0) {
        output.WriteRawTag(10);
        output.WriteString(WatchingCount);
      }
      if (LikeCount.Length != 0) {
        output.WriteRawTag(18);
        output.WriteString(LikeCount);
      }
      if (LikeDelta != 0) {
        output.WriteRawTag(24);
        output.WriteInt32(LikeDelta);
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
      if (WatchingCount.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(WatchingCount);
      }
      if (LikeCount.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(LikeCount);
      }
      if (LikeDelta != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(LikeDelta);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void MergeFrom(CommonStateSignalDisplayInfo other) {
      if (other == null) {
        return;
      }
      if (other.WatchingCount.Length != 0) {
        WatchingCount = other.WatchingCount;
      }
      if (other.LikeCount.Length != 0) {
        LikeCount = other.LikeCount;
      }
      if (other.LikeDelta != 0) {
        LikeDelta = other.LikeDelta;
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
            WatchingCount = input.ReadString();
            break;
          }
          case 18: {
            LikeCount = input.ReadString();
            break;
          }
          case 24: {
            LikeDelta = input.ReadInt32();
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
            WatchingCount = input.ReadString();
            break;
          }
          case 18: {
            LikeCount = input.ReadString();
            break;
          }
          case 24: {
            LikeDelta = input.ReadInt32();
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
