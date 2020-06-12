using AcFunDanmuSongRequest.Platform.Interfaces;
using System.Web;

namespace AcFunDanmuSongRequest.Platform.NetEase.Request
{
    struct SearchGetRequest : IGetRequest
    {
        public string Host => $"https://music.163.com/api/search/get/web?csrf_token=&s={HttpUtility.UrlEncode(Keyword)}&type={Type}&offset={Offset}&limit={Limit}";

        public string Keyword { get; set; }
        public int Offset { get; set; }
        public int Limit { get; set; }
        public int Type => 1;
    }
}
