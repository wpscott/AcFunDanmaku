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
        [JsonPropertyName("result")] public int Result { get; set; }

        [JsonPropertyName("acfun.api.visitor_st")]
        public string ServiceToken { get; set; }

        [JsonPropertyName("acSecurity")] public string SecurityKey { get; set; }
        [JsonPropertyName("userId")] public long UserId { get; set; }
        [JsonPropertyName("error_msg")] public string ErrorMsg { get; set; }
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