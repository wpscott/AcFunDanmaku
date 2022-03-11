#if NET5_0_OR_GREATER
using System.Text.Json.Serialization;
#elif NETSTANDARD2_0_OR_GREATER
using Newtonsoft.Json;
#endif

namespace AcFunDanmu.Models.Client
{
#if NET5_0_OR_GREATER
    public sealed record MidgroundToken
    {
        [JsonPropertyName("result")]
        public int Result { get; init; }
        [JsonPropertyName("acfun.midground.api_st")]
        public string ServiceToken { get; init; }
        [JsonPropertyName("ssecurity")]
        public string SecurityKey { get; init; }
        [JsonPropertyName("userId")]
        public long UserId { get; init; }
    }
#elif NETSTANDARD2_0_OR_GREATER
    public sealed class MidgroundToken
    {
        [JsonProperty("result")]
        public int Result { get; set; }
        [JsonProperty("acfun.midground.api_st")]
        public string ServiceToken { get; set; }
        [JsonProperty("ssecurity")]
        public string SecurityKey { get; set; }
        [JsonProperty("userId")]
        public long UserId { get; set; }
    }
#endif
}
