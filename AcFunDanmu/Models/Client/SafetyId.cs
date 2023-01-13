#pragma warning disable CS8618
namespace AcFunDanmu.Models.Client
{
#if NET6_0_OR_GREATER
    using System.Text.Json.Serialization;

    public sealed record SafetyId
    {
        [JsonPropertyName("code")] public int Code { get; set; }

        [JsonPropertyName("msg")] public string Msg { get; set; }

        [JsonPropertyName("safety_id")] public string Id { get; set; }
    }
#elif NETSTANDARD2_0_OR_GREATER
    using Newtonsoft.Json;

    public sealed class SafetyId
    {
        [JsonProperty("code")] public int Code { get; set; }

        [JsonProperty("msg")] public string Msg { get; set; }

        [JsonProperty("safety_id")] public string Id { get; set; }
    }
#endif
}
#pragma warning restore CS8618