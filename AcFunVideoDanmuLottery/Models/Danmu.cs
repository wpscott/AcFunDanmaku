using System;
using System.Collections.Generic;
using System.Text;

namespace AcFunVideoDanmuLottery.Models
{
    struct Danmu : IEquatable<Danmu>
    {
        public long userId { get; set; }
        public long danmakuId { get; set; }
        public string body { get; set; }

        public string Header => $"{danmakuId} - {userId}";
        public string Content => body;

        bool IEquatable<Danmu>.Equals(Danmu other)
        {
            return other.danmakuId == danmakuId;
        }
    }

    struct DanmuResponse
    {
        public int totalCount { get; set; }
        public Danmu[] added { get; set; }
        public Danmu[] deleted { get; set; }
    }
}
