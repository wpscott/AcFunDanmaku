using System.Text.Json.Serialization;

namespace AcFunDanmu.Models.Client
{
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
}
