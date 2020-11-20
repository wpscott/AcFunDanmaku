using System.Text.Json.Serialization;

namespace AcFunDanmu.Models.Client
{
    public sealed record MidgroundToken
    {
        public int result { get; init; }
        [JsonPropertyName("acfun.midground.api_st")]
        public string service_token { get; init; }
        public string ssecurity { get; init; }
        public long userId { get; init; }
    }
}
