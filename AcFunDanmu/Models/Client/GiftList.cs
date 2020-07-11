using System;

namespace AcFunDanmu.Models.Client
{
    public struct GiftList
    {
        public int result { get; set; }
        public GiftData data { get; set; }

        public struct GiftData
        {
            public Gift[] giftList { get; set; }
            public string giftListHash { get; set; }

            public struct Gift
            {
                public int giftId { get; set; }
                public string giftName { get; set; }
                public int giftPrice { get; set; }
                public Pic[] pngPicList { get; set; }
                public Pic[] webpPicList { get; set; }
                public struct Pic
                {
                    public string cdn { get; set; }
                    public string url { get; set; }
                }
            }
        }
    }

    public struct GiftInfo
    {
        public string Name { get; set; }
        public Uri Pic { get; set; }
    }
}
