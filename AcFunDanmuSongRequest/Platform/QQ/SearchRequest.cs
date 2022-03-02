using AcFunDanmuSongRequest.Platform.Interfaces;
using System.Web;

namespace AcFunDanmuSongRequest.Platform.QQ
{
    struct SearchRequest : IGetRequest
    {
        public string Host => $"https://c.y.qq.com/soso/fcgi-bin/client_search_cp?format=json&cr=1&p=1&n=1&w={HttpUtility.UrlEncode(Keyword)}";

        public string Keyword { get; set; }
    }
}
