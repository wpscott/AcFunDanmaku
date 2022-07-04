#if NET5_0_OR_GREATER
using System.Text.Json.Serialization;

#elif NETSTANDARD2_0_OR_GREATER
using Newtonsoft.Json;
#endif

namespace AcFunDanmu.Models.Client
{
#if NET5_0_OR_GREATER
    public sealed record VisitorToken
    {
        [JsonPropertyName("result")] public int Result { get; init; }

        [JsonPropertyName("acfun.api.visitor_st")]
        public string ServiceToken { get; init; }

        [JsonPropertyName("acSecurity")] public string SecurityKey { get; init; }
        [JsonPropertyName("userId")] public long UserId { get; init; }
        [JsonPropertyName("error_msg")] public string ErrorMsg { get; init; }
    }
#elif NETSTANDARD2_0_OR_GREATER
    public sealed class VisitorToken
    {
        [JsonProperty("result")] public int Result { get; set; }

        [JsonProperty("acfun.api.visitor_st")] public string ServiceToken { get; set; }

        [JsonProperty("acSecurity")] public string SecurityKey { get; set; }

        [JsonProperty("userId")] public long UserId { get; set; }
    }
#endif
}