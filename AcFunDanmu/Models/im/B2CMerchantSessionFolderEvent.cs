// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: B2CMerchantSessionFolderEvent.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace AcFunDanmu.Im.Cloud.SessionFolder {

  /// <summary>Holder for reflection information generated from B2CMerchantSessionFolderEvent.proto</summary>
  public static partial class B2CMerchantSessionFolderEventReflection {

    #region Descriptor
    /// <summary>File descriptor for B2CMerchantSessionFolderEvent.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static B2CMerchantSessionFolderEventReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "CiNCMkNNZXJjaGFudFNlc3Npb25Gb2xkZXJFdmVudC5wcm90bxIhQWNGdW5E",
            "YW5tdS5JbS5DbG91ZC5TZXNzaW9uRm9sZGVyGhZTZXNzaW9uUmVmZXJlbmNl",
            "LnByb3RvGgpVc2VyLnByb3RvIokCCh1CMkNNZXJjaGFudFNlc3Npb25Gb2xk",
            "ZXJFdmVudBInCgR1c2VyGAEgASgLMhkuQWNGdW5EYW5tdS5JbS5CYXNpYy5V",
            "c2VyEhAKCHN1YkJpeklkGAIgASgFEkcKCnNlc3Npb25SZWYYAyABKAsyMy5B",
            "Y0Z1bkRhbm11LkltLkNsb3VkLlNlc3Npb25Gb2xkZXIuU2Vzc2lvblJlZmVy",
            "ZW5jZRIXCg9zZXNzaW9uRm9sZGVySWQYBCABKAkSDwoHZGVsZXRlZBgFIAEo",
            "CBISCgp1cGRhdGVUaW1lGAYgASgDEhIKCmNyZWF0ZVRpbWUYByABKAMSEgoK",
            "cmV0cnlDb3VudBgIIAEoBWIGcHJvdG8z"));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { global::AcFunDanmu.Im.Cloud.SessionFolder.SessionReferenceReflection.Descriptor, global::AcFunDanmu.Im.Basic.UserReflection.Descriptor, },
          new pbr::GeneratedClrTypeInfo(null, null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::AcFunDanmu.Im.Cloud.SessionFolder.B2CMerchantSessionFolderEvent), global::AcFunDanmu.Im.Cloud.SessionFolder.B2CMerchantSessionFolderEvent.Parser, new[]{ "User", "SubBizId", "SessionRef", "SessionFolderId", "Deleted", "UpdateTime", "CreateTime", "RetryCount" }, null, null, null, null)
          }));
    }
    #endregion

  }
  #region Messages
  public sealed partial class B2CMerchantSessionFolderEvent : pb::IMessage<B2CMerchantSessionFolderEvent>
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      , pb::IBufferMessage
  #endif
  {
    private static readonly pb::MessageParser<B2CMerchantSessionFolderEvent> _parser = new pb::MessageParser<B2CMerchantSessionFolderEvent>(() => new B2CMerchantSessionFolderEvent());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pb::MessageParser<B2CMerchantSessionFolderEvent> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::AcFunDanmu.Im.Cloud.SessionFolder.B2CMerchantSessionFolderEventReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public B2CMerchantSessionFolderEvent() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public B2CMerchantSessionFolderEvent(B2CMerchantSessionFolderEvent other) : this() {
      user_ = other.user_ != null ? other.user_.Clone() : null;
      subBizId_ = other.subBizId_;
      sessionRef_ = other.sessionRef_ != null ? other.sessionRef_.Clone() : null;
      sessionFolderId_ = other.sessionFolderId_;
      deleted_ = other.deleted_;
      updateTime_ = other.updateTime_;
      createTime_ = other.createTime_;
      retryCount_ = other.retryCount_;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public B2CMerchantSessionFolderEvent Clone() {
      return new B2CMerchantSessionFolderEvent(this);
    }

    /// <summary>Field number for the "user" field.</summary>
    public const int UserFieldNumber = 1;
    private global::AcFunDanmu.Im.Basic.User user_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public global::AcFunDanmu.Im.Basic.User User {
      get { return user_; }
      set {
        user_ = value;
      }
    }

    /// <summary>Field number for the "subBizId" field.</summary>
    public const int SubBizIdFieldNumber = 2;
    private int subBizId_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public int SubBizId {
      get { return subBizId_; }
      set {
        subBizId_ = value;
      }
    }

    /// <summary>Field number for the "sessionRef" field.</summary>
    public const int SessionRefFieldNumber = 3;
    private global::AcFunDanmu.Im.Cloud.SessionFolder.SessionReference sessionRef_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public global::AcFunDanmu.Im.Cloud.SessionFolder.SessionReference SessionRef {
      get { return sessionRef_; }
      set {
        sessionRef_ = value;
      }
    }

    /// <summary>Field number for the "sessionFolderId" field.</summary>
    public const int SessionFolderIdFieldNumber = 4;
    private string sessionFolderId_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public string SessionFolderId {
      get { return sessionFolderId_; }
      set {
        sessionFolderId_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "deleted" field.</summary>
    public const int DeletedFieldNumber = 5;
    private bool deleted_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool Deleted {
      get { return deleted_; }
      set {
        deleted_ = value;
      }
    }

    /// <summary>Field number for the "updateTime" field.</summary>
    public const int UpdateTimeFieldNumber = 6;
    private long updateTime_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public long UpdateTime {
      get { return updateTime_; }
      set {
        updateTime_ = value;
      }
    }

    /// <summary>Field number for the "createTime" field.</summary>
    public const int CreateTimeFieldNumber = 7;
    private long createTime_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public long CreateTime {
      get { return createTime_; }
      set {
        createTime_ = value;
      }
    }

    /// <summary>Field number for the "retryCount" field.</summary>
    public const int RetryCountFieldNumber = 8;
    private int retryCount_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public int RetryCount {
      get { return retryCount_; }
      set {
        retryCount_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override bool Equals(object other) {
      return Equals(other as B2CMerchantSessionFolderEvent);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool Equals(B2CMerchantSessionFolderEvent other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (!object.Equals(User, other.User)) return false;
      if (SubBizId != other.SubBizId) return false;
      if (!object.Equals(SessionRef, other.SessionRef)) return false;
      if (SessionFolderId != other.SessionFolderId) return false;
      if (Deleted != other.Deleted) return false;
      if (UpdateTime != other.UpdateTime) return false;
      if (CreateTime != other.CreateTime) return false;
      if (RetryCount != other.RetryCount) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override int GetHashCode() {
      int hash = 1;
      if (user_ != null) hash ^= User.GetHashCode();
      if (SubBizId != 0) hash ^= SubBizId.GetHashCode();
      if (sessionRef_ != null) hash ^= SessionRef.GetHashCode();
      if (SessionFolderId.Length != 0) hash ^= SessionFolderId.GetHashCode();
      if (Deleted != false) hash ^= Deleted.GetHashCode();
      if (UpdateTime != 0L) hash ^= UpdateTime.GetHashCode();
      if (CreateTime != 0L) hash ^= CreateTime.GetHashCode();
      if (RetryCount != 0) hash ^= RetryCount.GetHashCode();
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
      if (user_ != null) {
        output.WriteRawTag(10);
        output.WriteMessage(User);
      }
      if (SubBizId != 0) {
        output.WriteRawTag(16);
        output.WriteInt32(SubBizId);
      }
      if (sessionRef_ != null) {
        output.WriteRawTag(26);
        output.WriteMessage(SessionRef);
      }
      if (SessionFolderId.Length != 0) {
        output.WriteRawTag(34);
        output.WriteString(SessionFolderId);
      }
      if (Deleted != false) {
        output.WriteRawTag(40);
        output.WriteBool(Deleted);
      }
      if (UpdateTime != 0L) {
        output.WriteRawTag(48);
        output.WriteInt64(UpdateTime);
      }
      if (CreateTime != 0L) {
        output.WriteRawTag(56);
        output.WriteInt64(CreateTime);
      }
      if (RetryCount != 0) {
        output.WriteRawTag(64);
        output.WriteInt32(RetryCount);
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
      if (user_ != null) {
        output.WriteRawTag(10);
        output.WriteMessage(User);
      }
      if (SubBizId != 0) {
        output.WriteRawTag(16);
        output.WriteInt32(SubBizId);
      }
      if (sessionRef_ != null) {
        output.WriteRawTag(26);
        output.WriteMessage(SessionRef);
      }
      if (SessionFolderId.Length != 0) {
        output.WriteRawTag(34);
        output.WriteString(SessionFolderId);
      }
      if (Deleted != false) {
        output.WriteRawTag(40);
        output.WriteBool(Deleted);
      }
      if (UpdateTime != 0L) {
        output.WriteRawTag(48);
        output.WriteInt64(UpdateTime);
      }
      if (CreateTime != 0L) {
        output.WriteRawTag(56);
        output.WriteInt64(CreateTime);
      }
      if (RetryCount != 0) {
        output.WriteRawTag(64);
        output.WriteInt32(RetryCount);
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
      if (user_ != null) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(User);
      }
      if (SubBizId != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(SubBizId);
      }
      if (sessionRef_ != null) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(SessionRef);
      }
      if (SessionFolderId.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(SessionFolderId);
      }
      if (Deleted != false) {
        size += 1 + 1;
      }
      if (UpdateTime != 0L) {
        size += 1 + pb::CodedOutputStream.ComputeInt64Size(UpdateTime);
      }
      if (CreateTime != 0L) {
        size += 1 + pb::CodedOutputStream.ComputeInt64Size(CreateTime);
      }
      if (RetryCount != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(RetryCount);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void MergeFrom(B2CMerchantSessionFolderEvent other) {
      if (other == null) {
        return;
      }
      if (other.user_ != null) {
        if (user_ == null) {
          User = new global::AcFunDanmu.Im.Basic.User();
        }
        User.MergeFrom(other.User);
      }
      if (other.SubBizId != 0) {
        SubBizId = other.SubBizId;
      }
      if (other.sessionRef_ != null) {
        if (sessionRef_ == null) {
          SessionRef = new global::AcFunDanmu.Im.Cloud.SessionFolder.SessionReference();
        }
        SessionRef.MergeFrom(other.SessionRef);
      }
      if (other.SessionFolderId.Length != 0) {
        SessionFolderId = other.SessionFolderId;
      }
      if (other.Deleted != false) {
        Deleted = other.Deleted;
      }
      if (other.UpdateTime != 0L) {
        UpdateTime = other.UpdateTime;
      }
      if (other.CreateTime != 0L) {
        CreateTime = other.CreateTime;
      }
      if (other.RetryCount != 0) {
        RetryCount = other.RetryCount;
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
            if (user_ == null) {
              User = new global::AcFunDanmu.Im.Basic.User();
            }
            input.ReadMessage(User);
            break;
          }
          case 16: {
            SubBizId = input.ReadInt32();
            break;
          }
          case 26: {
            if (sessionRef_ == null) {
              SessionRef = new global::AcFunDanmu.Im.Cloud.SessionFolder.SessionReference();
            }
            input.ReadMessage(SessionRef);
            break;
          }
          case 34: {
            SessionFolderId = input.ReadString();
            break;
          }
          case 40: {
            Deleted = input.ReadBool();
            break;
          }
          case 48: {
            UpdateTime = input.ReadInt64();
            break;
          }
          case 56: {
            CreateTime = input.ReadInt64();
            break;
          }
          case 64: {
            RetryCount = input.ReadInt32();
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
            if (user_ == null) {
              User = new global::AcFunDanmu.Im.Basic.User();
            }
            input.ReadMessage(User);
            break;
          }
          case 16: {
            SubBizId = input.ReadInt32();
            break;
          }
          case 26: {
            if (sessionRef_ == null) {
              SessionRef = new global::AcFunDanmu.Im.Cloud.SessionFolder.SessionReference();
            }
            input.ReadMessage(SessionRef);
            break;
          }
          case 34: {
            SessionFolderId = input.ReadString();
            break;
          }
          case 40: {
            Deleted = input.ReadBool();
            break;
          }
          case 48: {
            UpdateTime = input.ReadInt64();
            break;
          }
          case 56: {
            CreateTime = input.ReadInt64();
            break;
          }
          case 64: {
            RetryCount = input.ReadInt32();
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