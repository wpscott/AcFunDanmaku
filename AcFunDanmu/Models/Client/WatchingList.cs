#pragma warning disable CS8618
using System;

namespace AcFunDanmu.Models.Client
{
#if NET6_0_OR_GREATER
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
#elif NETSTANDARD2_0_OR_GREATER
    using Newtonsoft.Json;

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
#pragma warning restore CS8618