using System.Text.Json.Serialization;

namespace AcFunDanmu.Models.Client
{
    [JsonSerializable(typeof(SignIn))]
    [JsonSerializable(typeof(SafetyId))]
    [JsonSerializable(typeof(MidgroundToken))]
    [JsonSerializable(typeof(VisitorToken))]
    [JsonSerializable(typeof(Play))]
    [JsonSerializable(typeof(GiftList))]
    [JsonSerializable(typeof(WatchingList))]
    internal partial class ClientModelsContext : JsonSerializerContext
    {
    }
}