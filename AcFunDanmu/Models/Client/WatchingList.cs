using System;
using System.Text.Json.Serialization;

namespace AcFunDanmu.Models.Client
{
    public sealed record WatchingList
    {
        [JsonPropertyName("result")]
        public long Result { get; init; }

        [JsonPropertyName("data")]
        public WatchingData Data { get; init; }

        [JsonPropertyName("host")]
        public string Host { get; init; }
    }

    public sealed record WatchingData
    {
        [JsonPropertyName("list")]
        public WatchingUser[] List { get; init; }
    }

    public sealed record WatchingUser
    {
        [JsonPropertyName("userId")]
        public long UserId { get; init; }

        [JsonPropertyName("nickname")]
        public string Nickname { get; init; }

        [JsonPropertyName("avatar")]
        public Avatar[] Avatar { get; init; }

        [JsonPropertyName("anonymousUser")]
        public bool AnonymousUser { get; init; }

        [JsonPropertyName("displaySendAmount")]
        public string DisplaySendAmount { get; init; }

        [JsonPropertyName("customWatchingListData")]
        public string CustomWatchingListData { get; init; } // JSON

        [JsonPropertyName("managerType")]
        public long ManagerType { get; init; }
    }

    public sealed record Avatar
    {
        [JsonPropertyName("cdn")]
        public string Cdn { get; init; }

        [JsonPropertyName("url")]
        public Uri Url { get; init; }

        [JsonPropertyName("urlPattern")]
        public Uri UrlPattern { get; init; }

        [JsonPropertyName("freeTraffic")]
        public bool FreeTraffic { get; init; }
    }
}
