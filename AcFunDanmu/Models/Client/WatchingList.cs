using System;

namespace AcFunDanmu.Models.Client
{
    public class WatchingList
    {
        public int result { get; set; }
        public WatchingData data { get; set; }

        public class WatchingData
        {
            public User[] list { get; set; }

            public class User
            {
                public long userId { get; set; }
                public string nickname { get; set; }
                public string displaySendAmount { get; set; }
                public string customWatchingListData { get; set; }
                public bool anonymousUser { get; set; }

                public Avatar[] avatar { get; set; }

                public class Avatar
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
