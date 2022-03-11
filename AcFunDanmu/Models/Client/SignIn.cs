#if NET5_0_OR_GREATER
using System.Text.Json.Serialization;
#elif NETSTANDARD2_0_OR_GREATER
using Newtonsoft.Json;
#endif

namespace AcFunDanmu.Models.Client
{
#if NET5_0_OR_GREATER
    public sealed class SignIn
    {
        [JsonPropertyName("result")]
        public int Result { get; init; }
        [JsonPropertyName("img")]
        public string Img { get; init; }
        [JsonPropertyName("userId")]
        public long UserId { get; init; }
        [JsonPropertyName("username")]
        public string Username { get; init; }
    }
#elif NETSTANDARD2_0_OR_GREATER
    public sealed class SignIn
    {
        [JsonProperty("result")]
        public int Result { get; set; }
        [JsonProperty("img")]
        public string Img { get; set; }
        [JsonProperty("userId")]
        public long UserId { get; set; }
        [JsonProperty("username")]
        public string Username { get; set; }
    }
#endif
}
