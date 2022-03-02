using System;
using System.Text.Json.Serialization;

namespace AcFunDanmu.Models.Client
{
    public sealed record GiftList
    {
        [JsonPropertyName("result")]
        public int Result { get; init; }
        [JsonPropertyName("data")]
        public GiftData Data { get; init; }
        [JsonPropertyName("host")]
        public string Host { get; init; }
    }

    public sealed record GiftData
    {
        [JsonPropertyName("giftList")]
        public Gift[] GiftList { get; init; }

        [JsonPropertyName("externalDisplayGiftId")]
        public long ExternalDisplayGiftId { get; init; }

        [JsonPropertyName("externalDisplayGiftTipsDelayTime")]
        public long ExternalDisplayGiftTipsDelayTime { get; init; }

        [JsonPropertyName("externalDisplayGift")]
        public ExternalDisplayGift ExternalDisplayGift { get; init; }
    }

    public sealed record Gift
    {
        [JsonPropertyName("giftId")]
        public long GiftId { get; init; }

        [JsonPropertyName("giftName")]
        public string GiftName { get; init; }

        [JsonPropertyName("arLiveName")]
        public string ArLiveName { get; init; }

        [JsonPropertyName("payWalletType")]
        public long PayWalletType { get; init; }

        [JsonPropertyName("giftPrice")]
        public int GiftPrice { get; init; }

        [JsonPropertyName("webpPicList")]
        public Pic[] WebpPicList { get; init; }

        [JsonPropertyName("pngPicList")]
        public Pic[] PngPicList { get; init; }

        [JsonPropertyName("smallPngPicList")]
        public Pic[] SmallPngPicList { get; init; }

        [JsonPropertyName("allowBatchSendSizeList")]
        public long[] AllowBatchSendSizeList { get; init; }

        [JsonPropertyName("canCombo")]
        public bool CanCombo { get; init; }

        [JsonPropertyName("canDraw")]
        public bool CanDraw { get; init; }

        [JsonPropertyName("magicFaceId")]
        public long MagicFaceId { get; init; }

        [JsonPropertyName("vupArId")]
        public long VupArId { get; init; }

        [JsonPropertyName("description")]
        public string Description { get; init; }

        [JsonPropertyName("redpackPrice")]
        public long RedpackPrice { get; init; }

        [JsonPropertyName("cornerMarkerText")]
        public string CornerMarkerText { get; init; }
    }

    public sealed record ExternalDisplayGift
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

    public record Pic
    {
        public string cdn { get; init; }
        public string url { get; init; }
    }

    public sealed record GiftInfo
    {
        [JsonPropertyName("name")]
        public string Name { get; init; }
        [JsonPropertyName("value")]
        public int Value { get; init; }
        [JsonPropertyName("pic")]
        public Uri Pic { get; init; }
    }
}
