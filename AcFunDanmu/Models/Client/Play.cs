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
        public int Result { get; init; }    // 129004: LiveNotOpen, 129015 UserBanned, 380205 LiveNotPaid
        [JsonPropertyName("error_msg")]
        public string ErrorMsg { get; init; }
        [JsonPropertyName("data")]
        public PlayData Data { get; init; }
        [JsonPropertyName("host")]
        public string Host { get; init; }
    }

    public sealed record PlayData
    {
        [JsonPropertyName("liveId")]
        public string LiveId { get; init; }

        [JsonPropertyName("availableTickets")]
        public string[] AvailableTickets { get; init; }

        [JsonPropertyName("enterRoomAttach")]
        public string EnterRoomAttach { get; init; }

        [JsonPropertyName("videoPlayRes")]
        public string VideoPlayRes { get; init; }

        [JsonPropertyName("caption")]
        public string Caption { get; init; }

        [JsonPropertyName("ticketRetryCount")]
        public long TicketRetryCount { get; init; }

        [JsonPropertyName("ticketRetryIntervalMs")]
        public long TicketRetryIntervalMs { get; init; }

        [JsonPropertyName("notices")]
        public Notice[] Notices { get; init; }

        [JsonPropertyName("config")]
        public Config Config { get; init; }

        [JsonPropertyName("liveStartTime")]
        public long LiveStartTime { get; init; }

        [JsonPropertyName("panoramic")]
        public bool Panoramic { get; init; }
    }

    public sealed record Notice
    {
        [JsonPropertyName("userId")]
        public long UserId { get; init; }

        [JsonPropertyName("userName")]
        public string UserName { get; init; }

        [JsonPropertyName("userGender")]
        public string UserGender { get; init; }

        [JsonPropertyName("notice")]
        public string NoticeNotice { get; init; }
    }

    public sealed record Config
    {
        [JsonPropertyName("giftSlotSize")]
        public long GiftSlotSize { get; init; }
    }
#elif NETSTANDARD2_0_OR_GREATER
    public sealed class Play
    {
        [JsonProperty("Result")]
        public int Result { get; set; }    // 129004: LiveNotOpen, 129015 UserBanned, 380205 LiveNotPaid
        [JsonProperty("error_msg")]
        public string ErrorMsg { get; set; }
        [JsonProperty("data")]
        public PlayData Data { get; set; }
        [JsonProperty("host")]
        public string Host { get; set; }
    }

    public sealed class PlayData
    {
        [JsonProperty("liveId")]
        public string LiveId { get; set; }

        [JsonProperty("availableTickets")]
        public string[] AvailableTickets { get; set; }

        [JsonProperty("enterRoomAttach")]
        public string EnterRoomAttach { get; set; }

        [JsonProperty("videoPlayRes")]
        public string VideoPlayRes { get; set; }

        [JsonProperty("caption")]
        public string Caption { get; set; }

        [JsonProperty("ticketRetryCount")]
        public long TicketRetryCount { get; set; }

        [JsonProperty("ticketRetryIntervalMs")]
        public long TicketRetryIntervalMs { get; set; }

        [JsonProperty("notices")]
        public Notice[] Notices { get; set; }

        [JsonProperty("config")]
        public Config Config { get; set; }

        [JsonProperty("liveStartTime")]
        public long LiveStartTime { get; set; }

        [JsonProperty("panoramic")]
        public bool Panoramic { get; set; }
    }

    public sealed class Notice
    {
        [JsonProperty("userId")]
        public long UserId { get; set; }

        [JsonProperty("userName")]
        public string UserName { get; set; }

        [JsonProperty("userGender")]
        public string UserGender { get; set; }

        [JsonProperty("notice")]
        public string NoticeNotice { get; set; }
    }

    public sealed class Config
    {
        [JsonProperty("giftSlotSize")]
        public long GiftSlotSize { get; set; }
    }
#endif
}
