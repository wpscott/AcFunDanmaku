// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: CsAckErrorCode.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace AcFunDanmu {

  /// <summary>Holder for reflection information generated from CsAckErrorCode.proto</summary>
  public static partial class CsAckErrorCodeReflection {

    #region Descriptor
    /// <summary>File descriptor for CsAckErrorCode.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static CsAckErrorCodeReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "ChRDc0Fja0Vycm9yQ29kZS5wcm90bxIKQWNGdW5EYW5tdSraAQoOQ3NBY2tF",
            "cnJvckNvZGUSEgoOU1VDQ0VTU19DU19BQ0sQABIPCgtMSVZFX0NMT1NFRBAB",
            "EhIKDlRJQ0tFVF9JTExFR0FMEAISEgoOQVRUQUNIX0lMTEVHQUwQAxIUChBV",
            "U0VSX05PVF9JTl9ST09NEAQSEAoMU0VSVkVSX0VSUk9SEAUSGQoVUkVRVUVT",
            "VF9QQVJBTV9JTlZBTElEEAYSIwofUk9PTV9OT1RfRVhJU1RfSU5fU1RBVEVf",
            "TUFOQUdFUhAHEhMKD05FV19MSVZFX09QRU5FRBAIYgZwcm90bzM="));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { },
          new pbr::GeneratedClrTypeInfo(new[] {typeof(global::AcFunDanmu.CsAckErrorCode), }, null, null));
    }
    #endregion

  }
  #region Enums
  public enum CsAckErrorCode {
    [pbr::OriginalName("SUCCESS_CS_ACK")] SuccessCsAck = 0,
    [pbr::OriginalName("LIVE_CLOSED")] LiveClosed = 1,
    [pbr::OriginalName("TICKET_ILLEGAL")] TicketIllegal = 2,
    [pbr::OriginalName("ATTACH_ILLEGAL")] AttachIllegal = 3,
    [pbr::OriginalName("USER_NOT_IN_ROOM")] UserNotInRoom = 4,
    [pbr::OriginalName("SERVER_ERROR")] ServerError = 5,
    [pbr::OriginalName("REQUEST_PARAM_INVALID")] RequestParamInvalid = 6,
    [pbr::OriginalName("ROOM_NOT_EXIST_IN_STATE_MANAGER")] RoomNotExistInStateManager = 7,
    [pbr::OriginalName("NEW_LIVE_OPENED")] NewLiveOpened = 8,
  }

  #endregion

}

#endregion Designer generated code