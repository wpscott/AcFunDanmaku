#pragma warning disable CS8618
namespace AcFunDanmu.Models.Client;

using System.Text.Json.Serialization;

public sealed record MidgroundToken
{
    [JsonPropertyName("result")] public int Result { get; set; }

    [JsonPropertyName("acfun.midground.api_st")]
    public string ServiceToken { get; set; }

    [JsonPropertyName("ssecurity")] public string SecurityKey { get; set; }
    [JsonPropertyName("userId")] public long UserId { get; set; }
    [JsonPropertyName("error_msg")] public string ErrorMsg { get; set; }
}
#pragma warning restore CS8618