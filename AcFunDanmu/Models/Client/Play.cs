#if NET5_0_OR_GREATER
using System.Text.Json.Serialization;

#elif NETSTANDARD2_0_OR_GREATER
using Newtonsoft.Json;
#endif

namespace AcFunDanmu.Models.Client
{
#if NET5_0_OR_GREATER
    public sealed record Play
    {
        [JsonPropertyName("Result")]
        public int Result { get; set; } // 129004: LiveNotOpen, 129015 UserBanned, 380205 LiveNotPaid

        [JsonPropertyName("error_msg")] public string ErrorMsg { get; set; }

        [JsonPropertyName("data")] public PlayData Data { get; set; }

        [JsonPropertyName("host")] public string Host { get; set; }
    }

    public sealed record PlayData
    {
        [JsonPropertyName("liveId")] public string LiveId { get; set; }

        [JsonPropertyName("availableTickets")] public string[] AvailableTickets { get; set; }

        [JsonPropertyName("enterRoomAttach")] public string EnterRoomAttach { get; set; }

        [JsonPropertyName("videoPlayRes")] public string VideoPlayRes { get; set; }

        [JsonPropertyName("caption")] public string Caption { get; set; }

        [JsonPropertyName("ticketRetryCount")] public long TicketRetryCount { get; set; }

        [JsonPropertyName("ticketRetryIntervalMs")]
        public long TicketRetryIntervalMs { get; set; }

        [JsonPropertyName("notices")] public Notice[] Notices { get; set; }

        [JsonPropertyName("config")] public Config Config { get; set; }

        [JsonPropertyName("liveStartTime")] public long LiveStartTime { get; set; }

        [JsonPropertyName("panoramic")] public bool Panoramic { get; set; }
    }

    public sealed record Notice
    {
        [JsonPropertyName("userId")] public long UserId { get; set; }

        [JsonPropertyName("userName")] public string UserName { get; set; }

        [JsonPropertyName("userGender")] public string UserGender { get; set; }

        [JsonPropertyName("notice")] public string NoticeNotice { get; set; }
    }

    public sealed record Config
    {
        [JsonPropertyName("giftSlotSize")] public long GiftSlotSize { get; set; }
    }
#elif NETSTANDARD2_0_OR_GREATER
    public sealed class Play
    {
        [JsonProperty("Result")]
        public int Result { get; set; } // 129004: LiveNotOpen, 129015 UserBanned, 380205 LiveNotPaid

        [JsonProperty("error_msg")] public string ErrorMsg { get; set; }

        [JsonProperty("data")] public PlayData Data { get; set; }

        [JsonProperty("host")] public string Host { get; set; }
    }

    public sealed class PlayData
    {
        [JsonProperty("liveId")] public string LiveId { get; set; }

        [JsonProperty("availableTickets")] public string[] AvailableTickets { get; set; }

        [JsonProperty("enterRoomAttach")] public string EnterRoomAttach { get; set; }

        [JsonProperty("videoPlayRes")] public string VideoPlayRes { get; set; }

        [JsonProperty("caption")] public string Caption { get; set; }

        [JsonProperty("ticketRetryCount")] public long TicketRetryCount { get; set; }

        [JsonProperty("ticketRetryIntervalMs")]
        public long TicketRetryIntervalMs { get; set; }

        [JsonProperty("notices")] public Notice[] Notices { get; set; }

        [JsonProperty("config")] public Config Config { get; set; }

        [JsonProperty("liveStartTime")] public long LiveStartTime { get; set; }

        [JsonProperty("panoramic")] public bool Panoramic { get; set; }
    }

    public sealed class Notice
    {
        [JsonProperty("userId")] public long UserId { get; set; }

        [JsonProperty("userName")] public string UserName { get; set; }

        [JsonProperty("userGender")] public string UserGender { get; set; }

        [JsonProperty("notice")] public string NoticeNotice { get; set; }
    }

    public sealed class Config
    {
        [JsonProperty("giftSlotSize")] public long GiftSlotSize { get; set; }
    }
#endif
}