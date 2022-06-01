using AcFunDanmuSongRequest.Platform.Interfaces;
using System.Web;

namespace AcFunDanmuSongRequest.Platform.NetEase.Request
{
    internal readonly record struct CloudSearchGetRequest(string Keyword, int Offset, int Limit) : IGetRequest
    {
        public string Host =>
            $"https://music.163.com/api/cloudsearch/get/web?csrf_token=&s={HttpUtility.UrlEncode(Keyword)}&type={Type}&limit={Limit}&offset={Offset}";

        private static int Type => 1;
    }
}