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
            }
        }
    }
}
