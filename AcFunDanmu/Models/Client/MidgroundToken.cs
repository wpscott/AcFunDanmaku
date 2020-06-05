using System.Text.Json.Serialization;

namespace AcFunDanmu.Models.Client
{
    public class MidgroundToken
    {
        public int result { get; set; }
        [JsonPropertyName("acfun.midground.api_st")]
        public string service_token { get; set; }
        public string ssecurity { get; set; }
        public long userId { get; set; }
    }
}
