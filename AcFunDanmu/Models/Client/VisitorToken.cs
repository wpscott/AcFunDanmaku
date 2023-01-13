#pragma warning disable CS8618
namespace AcFunDanmu.Models.Client
{
#if NET6_0_OR_GREATER
    using System.Text.Json.Serialization;

    public sealed record VisitorToken
    {
        [JsonPropertyName("result")] public int Result { get; set; }

        [JsonPropertyName("acfun.api.visitor_st")]
        public string ServiceToken { get; set; }

        [JsonPropertyName("acSecurity")] public string SecurityKey { get; set; }
        [JsonPropertyName("userId")] public long UserId { get; set; }
        [JsonPropertyName("error_msg")] public string ErrorMsg { get; set; }
    }
#elif NETSTANDARD2_0_OR_GREATER
    using Newtonsoft.Json;

    public sealed class VisitorToken
    {
        [JsonProperty("result")] public int Result { get; set; }

        [JsonProperty("acfun.api.visitor_st")] public string ServiceToken { get; set; }

        [JsonProperty("acSecurity")] public string SecurityKey { get; set; }

        [JsonProperty("userId")] public long UserId { get; set; }
        [JsonProperty("error_msg")] public string ErrorMsg { get; set; }
    }
#endif
}
#pragma warning restore CS8618