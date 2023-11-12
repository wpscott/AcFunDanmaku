#pragma warning disable CS8618
namespace AcFunDanmu.Models.Client;

using System.Text.Json.Serialization;

public sealed class SignIn
{
    [JsonPropertyName("result")] public int Result { get; set; }
    [JsonPropertyName("img")] public string Img { get; set; }
    [JsonPropertyName("userId")] public long UserId { get; set; }
    [JsonPropertyName("username")] public string Username { get; set; }
}
#pragma warning restore CS8618