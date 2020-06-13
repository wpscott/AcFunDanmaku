using System;

namespace AcFunDanmu.Models.Client
{
    public struct WatchingList
    {
        public int result { get; set; }
        public WatchingData data { get; set; }

        public struct WatchingData
        {
            public User[] list { get; set; }

            public struct User
            {
                public long userId { get; set; }
                public string nickname { get; set; }
                public string displaySendAmount { get; set; }
                public string customWatchingListData { get; set; }
                public bool anonymousUser { get; set; }

                public Avatar[] avatar { get; set; }

                public struct Avatar
                {
                    public string cdn { get; set; }
                    public bool freeTraffic { get; set; }
                    public Uri url { get; set; }
                    public Uri urlPattern { get; set; }
                }
            }
        }
    }
}
