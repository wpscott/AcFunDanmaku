using System.Text.Json.Serialization;

namespace AcFunDanmu.Models.Client
{
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
}
