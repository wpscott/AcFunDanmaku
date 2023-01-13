#pragma warning disable CS8618
namespace AcFunDanmu.Models.Client
{
#if NET6_0_OR_GREATER
using System.Text.Json.Serialization;

public sealed class SignIn
{
    [JsonPropertyName("result")] public int Result { get; set; }
    [JsonPropertyName("img")] public string Img { get; set; }
    [JsonPropertyName("userId")] public long UserId { get; set; }
    [JsonPropertyName("username")] public string Username { get; set; }
}
#elif NETSTANDARD2_0_OR_GREATER
    using Newtonsoft.Json;

    public sealed class SignIn
    {
        [JsonProperty("result")] public int Result { get; set; }
        [JsonProperty("img")] public string Img { get; set; }
        [JsonProperty("userId")] public long UserId { get; set; }
        [JsonProperty("username")] public string Username { get; set; }
    }
#endif
}
#pragma warning restore CS8618