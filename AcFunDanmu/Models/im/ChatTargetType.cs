// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: ChatTargetType.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace AcFunDanmu.Im.Message {

  /// <summary>Holder for reflection information generated from ChatTargetType.proto</summary>
  public static partial class ChatTargetTypeReflection {

    #region Descriptor
    /// <summary>File descriptor for ChatTargetType.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static ChatTargetTypeReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "ChRDaGF0VGFyZ2V0VHlwZS5wcm90bxIVQWNGdW5EYW5tdS5JbS5NZXNzYWdl",
            "KqcBCg5DaGF0VGFyZ2V0VHlwZRIMCghDVFRfVVNFUhAAEhEKDUNUVF9DSEFU",
            "X1JPT00QAhIZChVDVFRfTVVMVElfUExBWUVSX1JPT00QAxINCglDVFRfR1JP",
            "VVAQBBIPCgtDVFRfQ0hBTk5FTBAFEhEKDUNUVF9BR0dSRUdBVEUQBhILCgdD",
            "VFRfQjJDEAcSGQoVQ1RUX1NVQl9CSVpfQUdHUkVHQVRFEAhiBnByb3RvMw=="));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { },
          new pbr::GeneratedClrTypeInfo(new[] {typeof(global::AcFunDanmu.Im.Message.ChatTargetType), }, null, null));
    }
    #endregion

  }
  #region Enums
  public enum ChatTargetType {
    [pbr::OriginalName("CTT_USER")] CttUser = 0,
    [pbr::OriginalName("CTT_CHAT_ROOM")] CttChatRoom = 2,
    [pbr::OriginalName("CTT_MULTI_PLAYER_ROOM")] CttMultiPlayerRoom = 3,
    [pbr::OriginalName("CTT_GROUP")] CttGroup = 4,
    [pbr::OriginalName("CTT_CHANNEL")] CttChannel = 5,
    [pbr::OriginalName("CTT_AGGREGATE")] CttAggregate = 6,
    [pbr::OriginalName("CTT_B2C")] CttB2C = 7,
    [pbr::OriginalName("CTT_SUB_BIZ_AGGREGATE")] CttSubBizAggregate = 8,
  }

  #endregion

}

#endregion Designer generated code
