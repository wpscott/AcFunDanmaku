using System;
#if NET5_0_OR_GREATER
using System.Text.Json.Serialization;

#elif NETSTANDARD2_0_OR_GREATER
using Newtonsoft.Json;
#endif

namespace AcFunDanmu.Models.Client
{
#if NET5_0_OR_GREATER
    public sealed record WatchingList
    {
        [JsonPropertyName("result")] public long Result { get; init; }

        [JsonPropertyName("data")] public WatchingData Data { get; init; }

        [JsonPropertyName("host")] public string Host { get; init; }
    }

    public sealed record WatchingData
    {
        [JsonPropertyName("list")] public WatchingUser[] List { get; init; }
    }

    public sealed record WatchingUser
    {
        [JsonPropertyName("userId")] public long UserId { get; init; }

        [JsonPropertyName("nickname")] public string Nickname { get; init; }

        [JsonPropertyName("avatar")] public Avatar[] Avatar { get; init; }

        [JsonPropertyName("anonymousUser")] public bool AnonymousUser { get; init; }

        [JsonPropertyName("displaySendAmount")]
        public string DisplaySendAmount { get; init; }

        [JsonPropertyName("customWatchingListData")]
        public string CustomWatchingListData { get; init; } // JSON

        [JsonPropertyName("managerType")] public long ManagerType { get; init; }
    }

    public sealed record Avatar
    {
        [JsonPropertyName("cdn")] public string Cdn { get; init; }

        [JsonPropertyName("url")] public Uri Url { get; init; }

        [JsonPropertyName("urlPattern")] public Uri UrlPattern { get; init; }

        [JsonPropertyName("freeTraffic")] public bool FreeTraffic { get; init; }
    }
#elif NETSTANDARD2_0_OR_GREATER
    public sealed class WatchingList
    {
        [JsonProperty("result")] public long Result { get; set; }

        [JsonProperty("data")] public WatchingData Data { get; set; }

        [JsonProperty("host")] public string Host { get; set; }
    }

    public sealed class WatchingData
    {
        [JsonProperty("list")] public WatchingUser[] List { get; set; }
    }

    public sealed class WatchingUser
    {
        [JsonProperty("userId")] public long UserId { get; set; }

        [JsonProperty("nickname")] public string Nickname { get; set; }

        [JsonProperty("avatar")] public Avatar[] Avatar { get; set; }

        [JsonProperty("anonymousUser")] public bool AnonymousUser { get; set; }

        [JsonProperty("displaySendAmount")] public string DisplaySendAmount { get; set; }

        [JsonProperty("customWatchingListData")]
        public string CustomWatchingListData { get; set; } // JSON

        [JsonProperty("managerType")] public long ManagerType { get; set; }
    }

    public sealed class Avatar
    {
        [JsonProperty("cdn")] public string Cdn { get; set; }

        [JsonProperty("url")] public Uri Url { get; set; }

        [JsonProperty("urlPattern")] public Uri UrlPattern { get; set; }

        [JsonProperty("freeTraffic")] public bool FreeTraffic { get; set; }
    }
#endif
}