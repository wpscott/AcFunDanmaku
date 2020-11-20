using System;
using System.Text.Json.Serialization;

namespace AcFunDanmu.Models.Client
{
    public sealed record GiftList
    {
        public int result { get; init; }
        public GiftData data { get; init; }

        public sealed record GiftData
        {
            public Gift[] giftList { get; init; }
            public string giftListHash { get; init; }

            public sealed record Gift
            {
                public long giftId { get; init; }
                public string giftName { get; init; }
                public int giftPrice { get; init; }
                public Pic[] pngPicList { get; init; }
                public Pic[] webpPicList { get; init; }
                public record Pic
                {
                    public string cdn { get; init; }
                    public string url { get; init; }
                }
            }
        }
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
