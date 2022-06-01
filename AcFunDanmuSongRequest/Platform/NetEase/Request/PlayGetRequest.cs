using AcFunDanmuSongRequest.Platform.Interfaces;

namespace AcFunDanmuSongRequest.Platform.NetEase.Request
{
    internal readonly record struct PlayGetRequest(string Keyword, long Id, int BitRate) : IGetRequest
    {
        public string Host => $"https://music.163.com/api/song/enhance/player/url?csrf_token=&ids=[{Id}]&br={BitRate}";
    }
}