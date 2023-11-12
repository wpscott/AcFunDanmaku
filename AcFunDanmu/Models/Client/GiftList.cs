#pragma warning disable CS8618
using System;

namespace AcFunDanmu.Models.Client;

using System.Text.Json.Serialization;

public sealed record GiftList
{
    [JsonPropertyName("result")] public int Result { get; set; }

    [JsonPropertyName("data")] public GiftData? Data { get; set; }

    [JsonPropertyName("host")] public string Host { get; set; }
}

public sealed record GiftData
{
    [JsonPropertyName("giftList")] public Gift[] GiftList { get; set; }

    [JsonPropertyName("externalDisplayGiftId")]
    public long ExternalDisplayGiftId { get; set; }

    [JsonPropertyName("externalDisplayGiftTipsDelayTime")]
    public long ExternalDisplayGiftTipsDelayTime { get; set; }

    [JsonPropertyName("externalDisplayGift")]
    public ExternalDisplayGift ExternalDisplayGift { get; set; }
}

public sealed record Gift
{
    [JsonPropertyName("giftId")] public long GiftId { get; set; }

    [JsonPropertyName("giftName")] public string GiftName { get; set; }

    [JsonPropertyName("arLiveName")] public string ArLiveName { get; set; }

    [JsonPropertyName("payWalletType")] public long PayWalletType { get; set; }

    [JsonPropertyName("giftPrice")] public int GiftPrice { get; set; }

    [JsonPropertyName("webpPicList")] public Pic[] WebpPicList { get; set; }

    [JsonPropertyName("pngPicList")] public Pic[] PngPicList { get; set; }

    [JsonPropertyName("smallPngPicList")] public Pic[] SmallPngPicList { get; set; }

    [JsonPropertyName("allowBatchSendSizeList")]
    public long[] AllowBatchSendSizeList { get; set; }

    [JsonPropertyName("canCombo")] public bool CanCombo { get; set; }

    [JsonPropertyName("canDraw")] public bool CanDraw { get; set; }

    [JsonPropertyName("magicFaceId")] public long MagicFaceId { get; set; }

    [JsonPropertyName("vupArId")] public long VupArId { get; set; }

    [JsonPropertyName("description")] public string Description { get; set; }

    [JsonPropertyName("redpackPrice")] public long RedpackPrice { get; set; }

    [JsonPropertyName("cornerMarkerText")] public string CornerMarkerText { get; set; }
}

public sealed record ExternalDisplayGift
{
    [JsonPropertyName("cdn")] public string Cdn { get; set; }

    [JsonPropertyName("url")] public Uri Url { get; set; }

    [JsonPropertyName("urlPattern")] public Uri UrlPattern { get; set; }

    [JsonPropertyName("freeTraffic")] public bool FreeTraffic { get; set; }
}

public sealed record Pic
{
    [JsonPropertyName("cdn")] public string CDN { get; set; }

    [JsonPropertyName("url")] public string Url { get; set; }
}

public sealed record GiftInfo
{
    [JsonPropertyName("name")] public string Name { get; set; }

    [JsonPropertyName("value")] public int Value { get; set; }

    [JsonPropertyName("pic")] public Uri Pic { get; set; }
}
#pragma warning restore CS8618