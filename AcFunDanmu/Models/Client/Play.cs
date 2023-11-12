#pragma warning disable CS8618
namespace AcFunDanmu.Models.Client;

using System.Text.Json.Serialization;

public sealed record Play
{
    [JsonPropertyName("Result")]
    public int Result { get; set; } // 129004: LiveNotOpen, 129015 UserBanned, 380205 LiveNotPaid

    [JsonPropertyName("error_msg")] public string ErrorMsg { get; set; }

    [JsonPropertyName("data")] public PlayData? Data { get; set; }

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
#pragma warning restore CS8618