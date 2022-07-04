#if NET5_0_OR_GREATER
using System.Text.Json.Serialization;

#elif NETSTANDARD2_0_OR_GREATER
using Newtonsoft.Json;
#endif

namespace AcFunDanmu.Models.Client
{
#if NET5_0_OR_GREATER
    public sealed record SafetyId
    {
        [JsonPropertyName("code")] public int Code { get; init; }

        [JsonPropertyName("msg")] public string Msg { get; init; }

        [JsonPropertyName("safety_id")] public string Id { get; init; }
    }
#elif NETSTANDARD2_0_OR_GREATER
    public sealed class SafetyId
    {
        [JsonProperty("code")] public int Code { get; set; }

        [JsonProperty("msg")] public string Msg { get; set; }

        [JsonProperty("safety_id")] public string Id { get; set; }
    }
#endif
}