#pragma warning disable CS8618
namespace AcFunDanmu.Models.Client;

using System.Text.Json.Serialization;

public sealed record WatchingList
{
    [JsonPropertyName("result")] public long Result { get; set; }

    [JsonPropertyName("data")] public WatchingData? Data { get; set; }

    [JsonPropertyName("host")] public string Host { get; set; }
}

public sealed record WatchingData
{
    [JsonPropertyName("list")] public WatchingUser[] List { get; set; }
}

public sealed record WatchingUser
{
    [JsonPropertyName("userId")] public long UserId { get; set; }

    [JsonPropertyName("nickname")] public string Nickname { get; set; }

    [JsonPropertyName("avatar")] public Avatar[] Avatar { get; set; }

    [JsonPropertyName("anonymousUser")] public bool AnonymousUser { get; set; }

    [JsonPropertyName("displaySendAmount")]
    public string DisplaySendAmount { get; set; }

    [JsonPropertyName("customWatchingListData")]
    public string CustomWatchingListData { get; set; } // JSON

    [JsonPropertyName("managerType")] public long ManagerType { get; set; }
}

public sealed record Avatar
{
    [JsonPropertyName("cdn")] public string Cdn { get; set; }

    [JsonPropertyName("url")] public Uri Url { get; set; }

    [JsonPropertyName("urlPattern")] public Uri UrlPattern { get; set; }

    [JsonPropertyName("freeTraffic")] public bool FreeTraffic { get; set; }
}
#pragma warning restore CS8618