#pragma warning disable CS8618
namespace AcFunDanmu.Models.Client;

using System.Text.Json.Serialization;

public sealed record SafetyId
{
    [JsonPropertyName("code")] public int Code { get; set; }

    [JsonPropertyName("msg")] public string Msg { get; set; }

    [JsonPropertyName("safety_id")] public string Id { get; set; }
}
#pragma warning restore CS8618