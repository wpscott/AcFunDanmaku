using System;

namespace AcFunDanmu.Models.Client
{
    public sealed record WatchingList
    {
        public int result { get; init; }
        public WatchingData data { get; init; }

        public sealed record WatchingData
        {
            public User[] list { get; init; }

            public sealed record User
            {
                public long userId { get; init; }
                public string nickname { get; init; }
                public string displaySendAmount { get; init; }
                public string customWatchingListData { get; init; }
                public bool anonymousUser { get; init; }

                public Avatar[] avatar { get; init; }

                public sealed record Avatar
                {
                    public string cdn { get; init; }
                    public bool freeTraffic { get; init; }
                    public Uri url { get; init; }
                    public Uri urlPattern { get; init; }
                }
            }
        }
    }
}
