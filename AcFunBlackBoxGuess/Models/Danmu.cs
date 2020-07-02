using System;
using System.Collections.Generic;
using System.Text;

namespace AcFunBlackBoxGuess.Models
{
    struct Danmu
    {
        public long Timestamp { get; set; }
        public long UserId { get; set; }
        public string Nickname { get; set; }
        public string Content { get; set; }
        public bool True { get; set; }
        public bool IsBingo { get; set; }
        public bool Failed { get; set; }
        public bool Correct { get; set; }

        public string Header => $"{Nickname}({UserId})";
        public string TrueFalse => IsBingo ? string.Empty : True ? "是" : "否";
        public string Bingo => Failed ? "盲猜次数已用完" : IsBingo ? Correct ? "正确" : "错误" : string.Empty;
    }
}
