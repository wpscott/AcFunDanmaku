// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: GroupFindType.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace AcFunDanmu.Im.Cloud.Message {

  /// <summary>Holder for reflection information generated from GroupFindType.proto</summary>
  public static partial class GroupFindTypeReflection {

    #region Descriptor
    /// <summary>File descriptor for GroupFindType.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static GroupFindTypeReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "ChNHcm91cEZpbmRUeXBlLnByb3RvEhtBY0Z1bkRhbm11LkltLkNsb3VkLk1l",
            "c3NhZ2UqpgEKDUdyb3VwRmluZFR5cGUSDgoKQllfVU5LTk9XThAAEgkKBUJZ",
            "X0lEEAESCwoHQllfTkFNRRACEg4KCkJZX1FSX0NPREUQAxILCgdCWV9DQVJE",
            "EAQSEQoNQllfSU5WSVRBVElPThAFEhEKDUJZX1NIQVJFX0xJTksQBhIOCgpC",
            "WV9QUk9GSUxFEAcSDQoJQllfU0VBUkNIEAgSCwoHQllfTkVXUxAJYgZwcm90",
            "bzM="));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { },
          new pbr::GeneratedClrTypeInfo(new[] {typeof(global::AcFunDanmu.Im.Cloud.Message.GroupFindType), }, null, null));
    }
    #endregion

  }
  #region Enums
  public enum GroupFindType {
    [pbr::OriginalName("BY_UNKNOWN")] ByUnknown = 0,
    [pbr::OriginalName("BY_ID")] ById = 1,
    [pbr::OriginalName("BY_NAME")] ByName = 2,
    [pbr::OriginalName("BY_QR_CODE")] ByQrCode = 3,
    [pbr::OriginalName("BY_CARD")] ByCard = 4,
    [pbr::OriginalName("BY_INVITATION")] ByInvitation = 5,
    [pbr::OriginalName("BY_SHARE_LINK")] ByShareLink = 6,
    [pbr::OriginalName("BY_PROFILE")] ByProfile = 7,
    [pbr::OriginalName("BY_SEARCH")] BySearch = 8,
    [pbr::OriginalName("BY_NEWS")] ByNews = 9,
  }

  #endregion

}

#endregion Designer generated code