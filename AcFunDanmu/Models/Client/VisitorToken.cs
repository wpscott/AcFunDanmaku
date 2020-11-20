using System.Text.Json.Serialization;

namespace AcFunDanmu.Models.Client
{
    public sealed record VisitorToken
    {
        public int result { get; init; }
        [JsonPropertyName("acfun.api.visitor_st")]
        public string service_token { get; init; }
        public string acSecurity { get; init; }
        public long userId { get; init; }
    }
}
