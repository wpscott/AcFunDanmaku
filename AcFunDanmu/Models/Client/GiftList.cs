using System;
#if NET5_0_OR_GREATER
using System.Text.Json.Serialization;
#elif NETSTANDARD2_0_OR_GREATER
using Newtonsoft.Json;
#endif

namespace AcFunDanmu.Models.Client
{
#if NET5_0_OR_GREATER
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

    public sealed record Pic
    {
        [JsonPropertyName("cdn")]
        public string CDN { get; init; }
        [JsonPropertyName("url")]
        public string Url { get; init; }
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
#elif NETSTANDARD2_0_OR_GREATER
    public sealed class GiftList
    {
        [JsonProperty("result")]
        public int Result { get; set; }
        [JsonProperty("data")]
        public GiftData Data { get; set; }
        [JsonProperty("host")]
        public string Host { get; set; }
    }

    public sealed class GiftData
    {
        [JsonProperty("giftList")]
        public Gift[] GiftList { get; set; }

        [JsonProperty("externalDisplayGiftId")]
        public long ExternalDisplayGiftId { get; set; }

        [JsonProperty("externalDisplayGiftTipsDelayTime")]
        public long ExternalDisplayGiftTipsDelayTime { get; set; }

        [JsonProperty("externalDisplayGift")]
        public ExternalDisplayGift ExternalDisplayGift { get; set; }
    }

    public sealed class Gift
    {
        [JsonProperty("giftId")]
        public long GiftId { get; set; }

        [JsonProperty("giftName")]
        public string GiftName { get; set; }

        [JsonProperty("arLiveName")]
        public string ArLiveName { get; set; }

        [JsonProperty("payWalletType")]
        public long PayWalletType { get; set; }

        [JsonProperty("giftPrice")]
        public int GiftPrice { get; set; }

        [JsonProperty("webpPicList")]
        public Pic[]
        WebpPicList
        { get; set; }

        [JsonProperty("pngPicList")]
        public Pic[]
        PngPicList
        { get; set; }

        [JsonProperty("smallPngPicList")]
        public Pic[]
        SmallPngPicList
        { get; set; }

        [JsonProperty("allowBatchSendSizeList")]
        public long[]
        AllowBatchSendSizeList
        { get; set; }

        [JsonProperty("canCombo")]
        public bool CanCombo { get; set; }

        [JsonProperty("canDraw")]
        public bool CanDraw { get; set; }

        [JsonProperty("magicFaceId")]
        public long MagicFaceId { get; set; }

        [JsonProperty("vupArId")]
        public long VupArId { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("redpackPrice")]
        public long RedpackPrice { get; set; }

        [JsonProperty("cornerMarkerText")]
        public string CornerMarkerText { get; set; }
    }

    public sealed class ExternalDisplayGift
    {
        [JsonProperty("cdn")]
        public string Cdn { get; set; }

        [JsonProperty("url")]
        public Uri Url { get; set; }

        [JsonProperty("urlPattern")]
        public Uri UrlPattern { get; set; }

        [JsonProperty("freeTraffic")]
        public bool FreeTraffic { get; set; }
    }

    public sealed class Pic
    {
        [JsonProperty("cdn")]
        public string CDN { get; set; }
        [JsonProperty("url")]
        public string Url { get; set; }
    }

    public sealed class GiftInfo
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("value")]
        public int Value { get; set; }
        [JsonProperty("pic")]
        public Uri Pic { get; set; }
    }
#endif
}
