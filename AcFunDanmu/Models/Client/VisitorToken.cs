using System.Text.Json.Serialization;

namespace AcFunDanmu.Models.Client
{
    public sealed record VisitorToken
    {
        [JsonPropertyName("result")]
        public int Result { get; init; }
        [JsonPropertyName("acfun.api.visitor_st")]
        public string ServiceToken { get; init; }
        [JsonPropertyName("acSecurity")]
        public string SecurityKey { get; init; }
        [JsonPropertyName("userId")]
        public long UserId { get; init; }
    }
}
