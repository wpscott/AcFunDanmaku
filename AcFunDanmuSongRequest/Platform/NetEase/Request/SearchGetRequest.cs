using System.Web;
using AcFunDanmuSongRequest.Platform.Interfaces;

namespace AcFunDanmuSongRequest.Platform.NetEase.Request;

internal readonly record struct SearchGetRequest(string Keyword, int Offset, int Limit) : IGetRequest
{
    private static int Type => 1;

    public string Host =>
        $"https://music.163.com/api/search/get/web?csrf_token=&s={HttpUtility.UrlEncode(Keyword)}&type={Type}&offset={Offset}&limit={Limit}";
}