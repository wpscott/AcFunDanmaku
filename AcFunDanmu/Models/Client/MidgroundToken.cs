#pragma warning disable CS8618
namespace AcFunDanmu.Models.Client
{
#if NET6_0_OR_GREATER
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
#elif NETSTANDARD2_0_OR_GREATER
    using Newtonsoft.Json;

    public sealed class MidgroundToken
    {
        [JsonProperty("result")] public int Result { get; set; }

        [JsonProperty("acfun.midground.api_st")]
        public string ServiceToken { get; set; }

        [JsonProperty("ssecurity")] public string SecurityKey { get; set; }
        [JsonProperty("userId")] public long UserId { get; set; }
        [JsonProperty("error_msg")] public string ErrorMsg { get; set; }
    }
#endif
}
#pragma warning restore CS8618