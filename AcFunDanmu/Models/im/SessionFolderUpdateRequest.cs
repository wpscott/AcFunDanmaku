// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: SessionFolderUpdateRequest.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace AcFunDanmu.Im.Cloud.SessionFolder {

  /// <summary>Holder for reflection information generated from SessionFolderUpdateRequest.proto</summary>
  public static partial class SessionFolderUpdateRequestReflection {

    #region Descriptor
    /// <summary>File descriptor for SessionFolderUpdateRequest.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static SessionFolderUpdateRequestReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "CiBTZXNzaW9uRm9sZGVyVXBkYXRlUmVxdWVzdC5wcm90bxIhQWNGdW5EYW5t",
            "dS5JbS5DbG91ZC5TZXNzaW9uRm9sZGVyIpECChpTZXNzaW9uRm9sZGVyVXBk",
            "YXRlUmVxdWVzdBIXCg9zZXNzaW9uRm9sZGVySWQYASABKAkSWQoGZmllbGRz",
            "GAIgAygOMkkuQWNGdW5EYW5tdS5JbS5DbG91ZC5TZXNzaW9uRm9sZGVyLlNl",
            "c3Npb25Gb2xkZXJVcGRhdGVSZXF1ZXN0LlVwZGF0ZUZpZWxkEhIKCmZvbGRl",
            "ck5hbWUYAyABKAkSDwoHaWNvblVybBgEIAEoCRINCgVleHRyYRgFIAEoDCJL",
            "CgtVcGRhdGVGaWVsZBISCg5VTl9LTk9XTl9GSUVMRBAAEg8KC0ZPTERFUl9O",
            "QU1FEAESDAoISUNPTl9VUkwQAhIJCgVFWFRSQRADYgZwcm90bzM="));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { },
          new pbr::GeneratedClrTypeInfo(null, null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::AcFunDanmu.Im.Cloud.SessionFolder.SessionFolderUpdateRequest), global::AcFunDanmu.Im.Cloud.SessionFolder.SessionFolderUpdateRequest.Parser, new[]{ "SessionFolderId", "Fields", "FolderName", "IconUrl", "Extra" }, null, new[]{ typeof(global::AcFunDanmu.Im.Cloud.SessionFolder.SessionFolderUpdateRequest.Types.UpdateField) }, null, null)
          }));
    }
    #endregion

  }
  #region Messages
  public sealed partial class SessionFolderUpdateRequest : pb::IMessage<SessionFolderUpdateRequest>
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      , pb::IBufferMessage
  #endif
  {
    private static readonly pb::MessageParser<SessionFolderUpdateRequest> _parser = new pb::MessageParser<SessionFolderUpdateRequest>(() => new SessionFolderUpdateRequest());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pb::MessageParser<SessionFolderUpdateRequest> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::AcFunDanmu.Im.Cloud.SessionFolder.SessionFolderUpdateRequestReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public SessionFolderUpdateRequest() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public SessionFolderUpdateRequest(SessionFolderUpdateRequest other) : this() {
      sessionFolderId_ = other.sessionFolderId_;
      fields_ = other.fields_.Clone();
      folderName_ = other.folderName_;
      iconUrl_ = other.iconUrl_;
      extra_ = other.extra_;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public SessionFolderUpdateRequest Clone() {
      return new SessionFolderUpdateRequest(this);
    }

    /// <summary>Field number for the "sessionFolderId" field.</summary>
    public const int SessionFolderIdFieldNumber = 1;
    private string sessionFolderId_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public string SessionFolderId {
      get { return sessionFolderId_; }
      set {
        sessionFolderId_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "fields" field.</summary>
    public const int FieldsFieldNumber = 2;
    private static readonly pb::FieldCodec<global::AcFunDanmu.Im.Cloud.SessionFolder.SessionFolderUpdateRequest.Types.UpdateField> _repeated_fields_codec
        = pb::FieldCodec.ForEnum(18, x => (int) x, x => (global::AcFunDanmu.Im.Cloud.SessionFolder.SessionFolderUpdateRequest.Types.UpdateField) x);
    private readonly pbc::RepeatedField<global::AcFunDanmu.Im.Cloud.SessionFolder.SessionFolderUpdateRequest.Types.UpdateField> fields_ = new pbc::RepeatedField<global::AcFunDanmu.Im.Cloud.SessionFolder.SessionFolderUpdateRequest.Types.UpdateField>();
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public pbc::RepeatedField<global::AcFunDanmu.Im.Cloud.SessionFolder.SessionFolderUpdateRequest.Types.UpdateField> Fields {
      get { return fields_; }
    }

    /// <summary>Field number for the "folderName" field.</summary>
    public const int FolderNameFieldNumber = 3;
    private string folderName_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public string FolderName {
      get { return folderName_; }
      set {
        folderName_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "iconUrl" field.</summary>
    public const int IconUrlFieldNumber = 4;
    private string iconUrl_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public string IconUrl {
      get { return iconUrl_; }
      set {
        iconUrl_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "extra" field.</summary>
    public const int ExtraFieldNumber = 5;
    private pb::ByteString extra_ = pb::ByteString.Empty;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public pb::ByteString Extra {
      get { return extra_; }
      set {
        extra_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override bool Equals(object other) {
      return Equals(other as SessionFolderUpdateRequest);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool Equals(SessionFolderUpdateRequest other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (SessionFolderId != other.SessionFolderId) return false;
      if(!fields_.Equals(other.fields_)) return false;
      if (FolderName != other.FolderName) return false;
      if (IconUrl != other.IconUrl) return false;
      if (Extra != other.Extra) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override int GetHashCode() {
      int hash = 1;
      if (SessionFolderId.Length != 0) hash ^= SessionFolderId.GetHashCode();
      hash ^= fields_.GetHashCode();
      if (FolderName.Length != 0) hash ^= FolderName.GetHashCode();
      if (IconUrl.Length != 0) hash ^= IconUrl.GetHashCode();
      if (Extra.Length != 0) hash ^= Extra.GetHashCode();
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
      if (SessionFolderId.Length != 0) {
        output.WriteRawTag(10);
        output.WriteString(SessionFolderId);
      }
      fields_.WriteTo(output, _repeated_fields_codec);
      if (FolderName.Length != 0) {
        output.WriteRawTag(26);
        output.WriteString(FolderName);
      }
      if (IconUrl.Length != 0) {
        output.WriteRawTag(34);
        output.WriteString(IconUrl);
      }
      if (Extra.Length != 0) {
        output.WriteRawTag(42);
        output.WriteBytes(Extra);
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
      if (SessionFolderId.Length != 0) {
        output.WriteRawTag(10);
        output.WriteString(SessionFolderId);
      }
      fields_.WriteTo(ref output, _repeated_fields_codec);
      if (FolderName.Length != 0) {
        output.WriteRawTag(26);
        output.WriteString(FolderName);
      }
      if (IconUrl.Length != 0) {
        output.WriteRawTag(34);
        output.WriteString(IconUrl);
      }
      if (Extra.Length != 0) {
        output.WriteRawTag(42);
        output.WriteBytes(Extra);
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
      if (SessionFolderId.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(SessionFolderId);
      }
      size += fields_.CalculateSize(_repeated_fields_codec);
      if (FolderName.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(FolderName);
      }
      if (IconUrl.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(IconUrl);
      }
      if (Extra.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeBytesSize(Extra);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void MergeFrom(SessionFolderUpdateRequest other) {
      if (other == null) {
        return;
      }
      if (other.SessionFolderId.Length != 0) {
        SessionFolderId = other.SessionFolderId;
      }
      fields_.Add(other.fields_);
      if (other.FolderName.Length != 0) {
        FolderName = other.FolderName;
      }
      if (other.IconUrl.Length != 0) {
        IconUrl = other.IconUrl;
      }
      if (other.Extra.Length != 0) {
        Extra = other.Extra;
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
            SessionFolderId = input.ReadString();
            break;
          }
          case 18:
          case 16: {
            fields_.AddEntriesFrom(input, _repeated_fields_codec);
            break;
          }
          case 26: {
            FolderName = input.ReadString();
            break;
          }
          case 34: {
            IconUrl = input.ReadString();
            break;
          }
          case 42: {
            Extra = input.ReadBytes();
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
            SessionFolderId = input.ReadString();
            break;
          }
          case 18:
          case 16: {
            fields_.AddEntriesFrom(ref input, _repeated_fields_codec);
            break;
          }
          case 26: {
            FolderName = input.ReadString();
            break;
          }
          case 34: {
            IconUrl = input.ReadString();
            break;
          }
          case 42: {
            Extra = input.ReadBytes();
            break;
          }
        }
      }
    }
    #endif

    #region Nested types
    /// <summary>Container for nested types declared in the SessionFolderUpdateRequest message type.</summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static partial class Types {
      public enum UpdateField {
        [pbr::OriginalName("UN_KNOWN_FIELD")] UnKnownField = 0,
        [pbr::OriginalName("FOLDER_NAME")] FolderName = 1,
        [pbr::OriginalName("ICON_URL")] IconUrl = 2,
        [pbr::OriginalName("EXTRA")] Extra = 3,
      }

    }
    #endregion

  }

  #endregion

}

#endregion Designer generated code