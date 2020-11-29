// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: CommonStateSignalCurrentRedpackList.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace AcFunDanmu {

  /// <summary>Holder for reflection information generated from CommonStateSignalCurrentRedpackList.proto</summary>
  public static partial class CommonStateSignalCurrentRedpackListReflection {

    #region Descriptor
    /// <summary>File descriptor for CommonStateSignalCurrentRedpackList.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static CommonStateSignalCurrentRedpackListReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "CilDb21tb25TdGF0ZVNpZ25hbEN1cnJlbnRSZWRwYWNrTGlzdC5wcm90bxIK",
            "QWNGdW5EYW5tdRoUWnRMaXZlVXNlckluZm8ucHJvdG8i0gMKI0NvbW1vblN0",
            "YXRlU2lnbmFsQ3VycmVudFJlZHBhY2tMaXN0EkkKCHJlZHBhY2tzGAEgAygL",
            "MjcuQWNGdW5EYW5tdS5Db21tb25TdGF0ZVNpZ25hbEN1cnJlbnRSZWRwYWNr",
            "TGlzdC5SZWRwYWNrGqQCCgdSZWRwYWNrEioKBnNlbmRlchgBIAEoCzIaLkFj",
            "RnVuRGFubXUuWnRMaXZlVXNlckluZm8SWwoNZGlzcGxheVN0YXR1cxgCIAEo",
            "DjJELkFjRnVuRGFubXUuQ29tbW9uU3RhdGVTaWduYWxDdXJyZW50UmVkcGFj",
            "a0xpc3QuUmVkcGFja0Rpc3BsYXlTdGF0dXMSFwoPZ3JhYkJlZ2luVGltZU1z",
            "GAMgASgDEhwKFGdldFRva2VuTGF0ZXN0VGltZU1zGAQgASgDEhEKCXJlZFBh",
            "Y2tJZBgFIAEoCRIWCg5yZWRwYWNrQml6VW5pdBgGIAEoCRIVCg1yZWRwYWNr",
            "QW1vdW50GAcgASgDEhcKD3NldHRsZUJlZ2luVGltZRgIIAEoAyI5ChRSZWRw",
            "YWNrRGlzcGxheVN0YXR1cxIICgRTSE9XEAASDQoJR0VUX1RPS0VOEAESCAoE",
            "R1JBQhACYgZwcm90bzM="));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { global::AcFunDanmu.ZtLiveUserInfoReflection.Descriptor, },
          new pbr::GeneratedClrTypeInfo(null, null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::AcFunDanmu.CommonStateSignalCurrentRedpackList), global::AcFunDanmu.CommonStateSignalCurrentRedpackList.Parser, new[]{ "Redpacks" }, null, new[]{ typeof(global::AcFunDanmu.CommonStateSignalCurrentRedpackList.Types.RedpackDisplayStatus) }, null, new pbr::GeneratedClrTypeInfo[] { new pbr::GeneratedClrTypeInfo(typeof(global::AcFunDanmu.CommonStateSignalCurrentRedpackList.Types.Redpack), global::AcFunDanmu.CommonStateSignalCurrentRedpackList.Types.Redpack.Parser, new[]{ "Sender", "DisplayStatus", "GrabBeginTimeMs", "GetTokenLatestTimeMs", "RedPackId", "RedpackBizUnit", "RedpackAmount", "SettleBeginTime" }, null, null, null, null)})
          }));
    }
    #endregion

  }
  #region Messages
  public sealed partial class CommonStateSignalCurrentRedpackList : pb::IMessage<CommonStateSignalCurrentRedpackList>
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      , pb::IBufferMessage
  #endif
  {
    private static readonly pb::MessageParser<CommonStateSignalCurrentRedpackList> _parser = new pb::MessageParser<CommonStateSignalCurrentRedpackList>(() => new CommonStateSignalCurrentRedpackList());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<CommonStateSignalCurrentRedpackList> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::AcFunDanmu.CommonStateSignalCurrentRedpackListReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public CommonStateSignalCurrentRedpackList() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public CommonStateSignalCurrentRedpackList(CommonStateSignalCurrentRedpackList other) : this() {
      redpacks_ = other.redpacks_.Clone();
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public CommonStateSignalCurrentRedpackList Clone() {
      return new CommonStateSignalCurrentRedpackList(this);
    }

    /// <summary>Field number for the "redpacks" field.</summary>
    public const int RedpacksFieldNumber = 1;
    private static readonly pb::FieldCodec<global::AcFunDanmu.CommonStateSignalCurrentRedpackList.Types.Redpack> _repeated_redpacks_codec
        = pb::FieldCodec.ForMessage(10, global::AcFunDanmu.CommonStateSignalCurrentRedpackList.Types.Redpack.Parser);
    private readonly pbc::RepeatedField<global::AcFunDanmu.CommonStateSignalCurrentRedpackList.Types.Redpack> redpacks_ = new pbc::RepeatedField<global::AcFunDanmu.CommonStateSignalCurrentRedpackList.Types.Redpack>();
    /// <summary>
    ///redpack
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public pbc::RepeatedField<global::AcFunDanmu.CommonStateSignalCurrentRedpackList.Types.Redpack> Redpacks {
      get { return redpacks_; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as CommonStateSignalCurrentRedpackList);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(CommonStateSignalCurrentRedpackList other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if(!redpacks_.Equals(other.redpacks_)) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      hash ^= redpacks_.GetHashCode();
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
      redpacks_.WriteTo(output, _repeated_redpacks_codec);
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    void pb::IBufferMessage.InternalWriteTo(ref pb::WriteContext output) {
      redpacks_.WriteTo(ref output, _repeated_redpacks_codec);
      if (_unknownFields != null) {
        _unknownFields.WriteTo(ref output);
      }
    }
    #endif

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      size += redpacks_.CalculateSize(_repeated_redpacks_codec);
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(CommonStateSignalCurrentRedpackList other) {
      if (other == null) {
        return;
      }
      redpacks_.Add(other.redpacks_);
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
            redpacks_.AddEntriesFrom(input, _repeated_redpacks_codec);
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
            redpacks_.AddEntriesFrom(ref input, _repeated_redpacks_codec);
            break;
          }
        }
      }
    }
    #endif

    #region Nested types
    /// <summary>Container for nested types declared in the CommonStateSignalCurrentRedpackList message type.</summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static partial class Types {
      public enum RedpackDisplayStatus {
        [pbr::OriginalName("SHOW")] Show = 0,
        [pbr::OriginalName("GET_TOKEN")] GetToken = 1,
        [pbr::OriginalName("GRAB")] Grab = 2,
      }

      public sealed partial class Redpack : pb::IMessage<Redpack>
      #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
          , pb::IBufferMessage
      #endif
      {
        private static readonly pb::MessageParser<Redpack> _parser = new pb::MessageParser<Redpack>(() => new Redpack());
        private pb::UnknownFieldSet _unknownFields;
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public static pb::MessageParser<Redpack> Parser { get { return _parser; } }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public static pbr::MessageDescriptor Descriptor {
          get { return global::AcFunDanmu.CommonStateSignalCurrentRedpackList.Descriptor.NestedTypes[0]; }
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        pbr::MessageDescriptor pb::IMessage.Descriptor {
          get { return Descriptor; }
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public Redpack() {
          OnConstruction();
        }

        partial void OnConstruction();

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public Redpack(Redpack other) : this() {
          sender_ = other.sender_ != null ? other.sender_.Clone() : null;
          displayStatus_ = other.displayStatus_;
          grabBeginTimeMs_ = other.grabBeginTimeMs_;
          getTokenLatestTimeMs_ = other.getTokenLatestTimeMs_;
          redPackId_ = other.redPackId_;
          redpackBizUnit_ = other.redpackBizUnit_;
          redpackAmount_ = other.redpackAmount_;
          settleBeginTime_ = other.settleBeginTime_;
          _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public Redpack Clone() {
          return new Redpack(this);
        }

        /// <summary>Field number for the "sender" field.</summary>
        public const int SenderFieldNumber = 1;
        private global::AcFunDanmu.ZtLiveUserInfo sender_;
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public global::AcFunDanmu.ZtLiveUserInfo Sender {
          get { return sender_; }
          set {
            sender_ = value;
          }
        }

        /// <summary>Field number for the "displayStatus" field.</summary>
        public const int DisplayStatusFieldNumber = 2;
        private global::AcFunDanmu.CommonStateSignalCurrentRedpackList.Types.RedpackDisplayStatus displayStatus_ = global::AcFunDanmu.CommonStateSignalCurrentRedpackList.Types.RedpackDisplayStatus.Show;
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public global::AcFunDanmu.CommonStateSignalCurrentRedpackList.Types.RedpackDisplayStatus DisplayStatus {
          get { return displayStatus_; }
          set {
            displayStatus_ = value;
          }
        }

        /// <summary>Field number for the "grabBeginTimeMs" field.</summary>
        public const int GrabBeginTimeMsFieldNumber = 3;
        private long grabBeginTimeMs_;
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public long GrabBeginTimeMs {
          get { return grabBeginTimeMs_; }
          set {
            grabBeginTimeMs_ = value;
          }
        }

        /// <summary>Field number for the "getTokenLatestTimeMs" field.</summary>
        public const int GetTokenLatestTimeMsFieldNumber = 4;
        private long getTokenLatestTimeMs_;
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public long GetTokenLatestTimeMs {
          get { return getTokenLatestTimeMs_; }
          set {
            getTokenLatestTimeMs_ = value;
          }
        }

        /// <summary>Field number for the "redPackId" field.</summary>
        public const int RedPackIdFieldNumber = 5;
        private string redPackId_ = "";
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public string RedPackId {
          get { return redPackId_; }
          set {
            redPackId_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
          }
        }

        /// <summary>Field number for the "redpackBizUnit" field.</summary>
        public const int RedpackBizUnitFieldNumber = 6;
        private string redpackBizUnit_ = "";
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public string RedpackBizUnit {
          get { return redpackBizUnit_; }
          set {
            redpackBizUnit_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
          }
        }

        /// <summary>Field number for the "redpackAmount" field.</summary>
        public const int RedpackAmountFieldNumber = 7;
        private long redpackAmount_;
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public long RedpackAmount {
          get { return redpackAmount_; }
          set {
            redpackAmount_ = value;
          }
        }

        /// <summary>Field number for the "settleBeginTime" field.</summary>
        public const int SettleBeginTimeFieldNumber = 8;
        private long settleBeginTime_;
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public long SettleBeginTime {
          get { return settleBeginTime_; }
          set {
            settleBeginTime_ = value;
          }
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public override bool Equals(object other) {
          return Equals(other as Redpack);
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public bool Equals(Redpack other) {
          if (ReferenceEquals(other, null)) {
            return false;
          }
          if (ReferenceEquals(other, this)) {
            return true;
          }
          if (!object.Equals(Sender, other.Sender)) return false;
          if (DisplayStatus != other.DisplayStatus) return false;
          if (GrabBeginTimeMs != other.GrabBeginTimeMs) return false;
          if (GetTokenLatestTimeMs != other.GetTokenLatestTimeMs) return false;
          if (RedPackId != other.RedPackId) return false;
          if (RedpackBizUnit != other.RedpackBizUnit) return false;
          if (RedpackAmount != other.RedpackAmount) return false;
          if (SettleBeginTime != other.SettleBeginTime) return false;
          return Equals(_unknownFields, other._unknownFields);
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public override int GetHashCode() {
          int hash = 1;
          if (sender_ != null) hash ^= Sender.GetHashCode();
          if (DisplayStatus != global::AcFunDanmu.CommonStateSignalCurrentRedpackList.Types.RedpackDisplayStatus.Show) hash ^= DisplayStatus.GetHashCode();
          if (GrabBeginTimeMs != 0L) hash ^= GrabBeginTimeMs.GetHashCode();
          if (GetTokenLatestTimeMs != 0L) hash ^= GetTokenLatestTimeMs.GetHashCode();
          if (RedPackId.Length != 0) hash ^= RedPackId.GetHashCode();
          if (RedpackBizUnit.Length != 0) hash ^= RedpackBizUnit.GetHashCode();
          if (RedpackAmount != 0L) hash ^= RedpackAmount.GetHashCode();
          if (SettleBeginTime != 0L) hash ^= SettleBeginTime.GetHashCode();
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
          if (sender_ != null) {
            output.WriteRawTag(10);
            output.WriteMessage(Sender);
          }
          if (DisplayStatus != global::AcFunDanmu.CommonStateSignalCurrentRedpackList.Types.RedpackDisplayStatus.Show) {
            output.WriteRawTag(16);
            output.WriteEnum((int) DisplayStatus);
          }
          if (GrabBeginTimeMs != 0L) {
            output.WriteRawTag(24);
            output.WriteInt64(GrabBeginTimeMs);
          }
          if (GetTokenLatestTimeMs != 0L) {
            output.WriteRawTag(32);
            output.WriteInt64(GetTokenLatestTimeMs);
          }
          if (RedPackId.Length != 0) {
            output.WriteRawTag(42);
            output.WriteString(RedPackId);
          }
          if (RedpackBizUnit.Length != 0) {
            output.WriteRawTag(50);
            output.WriteString(RedpackBizUnit);
          }
          if (RedpackAmount != 0L) {
            output.WriteRawTag(56);
            output.WriteInt64(RedpackAmount);
          }
          if (SettleBeginTime != 0L) {
            output.WriteRawTag(64);
            output.WriteInt64(SettleBeginTime);
          }
          if (_unknownFields != null) {
            _unknownFields.WriteTo(output);
          }
        #endif
        }

        #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        void pb::IBufferMessage.InternalWriteTo(ref pb::WriteContext output) {
          if (sender_ != null) {
            output.WriteRawTag(10);
            output.WriteMessage(Sender);
          }
          if (DisplayStatus != global::AcFunDanmu.CommonStateSignalCurrentRedpackList.Types.RedpackDisplayStatus.Show) {
            output.WriteRawTag(16);
            output.WriteEnum((int) DisplayStatus);
          }
          if (GrabBeginTimeMs != 0L) {
            output.WriteRawTag(24);
            output.WriteInt64(GrabBeginTimeMs);
          }
          if (GetTokenLatestTimeMs != 0L) {
            output.WriteRawTag(32);
            output.WriteInt64(GetTokenLatestTimeMs);
          }
          if (RedPackId.Length != 0) {
            output.WriteRawTag(42);
            output.WriteString(RedPackId);
          }
          if (RedpackBizUnit.Length != 0) {
            output.WriteRawTag(50);
            output.WriteString(RedpackBizUnit);
          }
          if (RedpackAmount != 0L) {
            output.WriteRawTag(56);
            output.WriteInt64(RedpackAmount);
          }
          if (SettleBeginTime != 0L) {
            output.WriteRawTag(64);
            output.WriteInt64(SettleBeginTime);
          }
          if (_unknownFields != null) {
            _unknownFields.WriteTo(ref output);
          }
        }
        #endif

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public int CalculateSize() {
          int size = 0;
          if (sender_ != null) {
            size += 1 + pb::CodedOutputStream.ComputeMessageSize(Sender);
          }
          if (DisplayStatus != global::AcFunDanmu.CommonStateSignalCurrentRedpackList.Types.RedpackDisplayStatus.Show) {
            size += 1 + pb::CodedOutputStream.ComputeEnumSize((int) DisplayStatus);
          }
          if (GrabBeginTimeMs != 0L) {
            size += 1 + pb::CodedOutputStream.ComputeInt64Size(GrabBeginTimeMs);
          }
          if (GetTokenLatestTimeMs != 0L) {
            size += 1 + pb::CodedOutputStream.ComputeInt64Size(GetTokenLatestTimeMs);
          }
          if (RedPackId.Length != 0) {
            size += 1 + pb::CodedOutputStream.ComputeStringSize(RedPackId);
          }
          if (RedpackBizUnit.Length != 0) {
            size += 1 + pb::CodedOutputStream.ComputeStringSize(RedpackBizUnit);
          }
          if (RedpackAmount != 0L) {
            size += 1 + pb::CodedOutputStream.ComputeInt64Size(RedpackAmount);
          }
          if (SettleBeginTime != 0L) {
            size += 1 + pb::CodedOutputStream.ComputeInt64Size(SettleBeginTime);
          }
          if (_unknownFields != null) {
            size += _unknownFields.CalculateSize();
          }
          return size;
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public void MergeFrom(Redpack other) {
          if (other == null) {
            return;
          }
          if (other.sender_ != null) {
            if (sender_ == null) {
              Sender = new global::AcFunDanmu.ZtLiveUserInfo();
            }
            Sender.MergeFrom(other.Sender);
          }
          if (other.DisplayStatus != global::AcFunDanmu.CommonStateSignalCurrentRedpackList.Types.RedpackDisplayStatus.Show) {
            DisplayStatus = other.DisplayStatus;
          }
          if (other.GrabBeginTimeMs != 0L) {
            GrabBeginTimeMs = other.GrabBeginTimeMs;
          }
          if (other.GetTokenLatestTimeMs != 0L) {
            GetTokenLatestTimeMs = other.GetTokenLatestTimeMs;
          }
          if (other.RedPackId.Length != 0) {
            RedPackId = other.RedPackId;
          }
          if (other.RedpackBizUnit.Length != 0) {
            RedpackBizUnit = other.RedpackBizUnit;
          }
          if (other.RedpackAmount != 0L) {
            RedpackAmount = other.RedpackAmount;
          }
          if (other.SettleBeginTime != 0L) {
            SettleBeginTime = other.SettleBeginTime;
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
                if (sender_ == null) {
                  Sender = new global::AcFunDanmu.ZtLiveUserInfo();
                }
                input.ReadMessage(Sender);
                break;
              }
              case 16: {
                DisplayStatus = (global::AcFunDanmu.CommonStateSignalCurrentRedpackList.Types.RedpackDisplayStatus) input.ReadEnum();
                break;
              }
              case 24: {
                GrabBeginTimeMs = input.ReadInt64();
                break;
              }
              case 32: {
                GetTokenLatestTimeMs = input.ReadInt64();
                break;
              }
              case 42: {
                RedPackId = input.ReadString();
                break;
              }
              case 50: {
                RedpackBizUnit = input.ReadString();
                break;
              }
              case 56: {
                RedpackAmount = input.ReadInt64();
                break;
              }
              case 64: {
                SettleBeginTime = input.ReadInt64();
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
                if (sender_ == null) {
                  Sender = new global::AcFunDanmu.ZtLiveUserInfo();
                }
                input.ReadMessage(Sender);
                break;
              }
              case 16: {
                DisplayStatus = (global::AcFunDanmu.CommonStateSignalCurrentRedpackList.Types.RedpackDisplayStatus) input.ReadEnum();
                break;
              }
              case 24: {
                GrabBeginTimeMs = input.ReadInt64();
                break;
              }
              case 32: {
                GetTokenLatestTimeMs = input.ReadInt64();
                break;
              }
              case 42: {
                RedPackId = input.ReadString();
                break;
              }
              case 50: {
                RedpackBizUnit = input.ReadString();
                break;
              }
              case 56: {
                RedpackAmount = input.ReadInt64();
                break;
              }
              case 64: {
                SettleBeginTime = input.ReadInt64();
                break;
              }
            }
          }
        }
        #endif

      }

    }
    #endregion

  }

  #endregion

}

#endregion Designer generated code
