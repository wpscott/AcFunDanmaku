using System.Text.Json.Serialization;

namespace AcFunDanmu.Models.Client
{
    public class VisitorToken
    {
        public int result { get; set; }
        [JsonPropertyName("acfun.api.visitor_st")]
        public string service_token { get; set; }
        public string acSecurity { get; set; }
        public long userId { get; set; }
    }
}
