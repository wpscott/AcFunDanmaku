using System;
using System.Text.Json.Serialization;

namespace AcFunDanmuSongRequest.Platform.NetEase.Response
{
    struct CloudSearchResponse
    {
        public Song[] Songs { get; set; }
        public struct Song
        {
            public Album Al { get; set; }
            public Artist[] Ar { get; set; }
            public long Id { get; set; }
            public string Name { get; set; }
            public TimeSpan Dt { get; set; }
            public struct Album
            {
                public long Id { get; set; }
                public string Name { get; set; }
                public long Pic { get; set; }
                public string PicUrl { get; set; }
            }
            public struct Artist
            {
                public long Id { get; set; }
                public string Name { get; set; }
            }

        }
    }
}
