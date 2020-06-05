using System;
using System.Collections.Generic;
using System.Text;

namespace AcFunDanmuLottery.Models
{
    struct Comment
    {
        public long Timestamp { get; set; }
        public long UserId { get; set; }
        public string Nickname { get; set; }
        public string Content { get; set; }

        public Comment(long userId, string nickname, string content, long timestamp)
        {
            Timestamp = timestamp;
            UserId = userId;
            Nickname = nickname;
            Content = content;
        }
    }
}
