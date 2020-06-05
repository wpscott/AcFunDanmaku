namespace AcFunDanmu.Models.Client
{
    public class GiftList
    {
        public int result { get; set; }
        public GiftData data { get; set; }

        public class GiftData
        {
            public Gift[] giftList { get; set; }
            public string giftListHash { get; set; }

            public class Gift
            {
                public int giftId { get; set; }
                public string giftName { get; set; }
                public int giftPrice { get; set; }
            }
        }
    }
}
