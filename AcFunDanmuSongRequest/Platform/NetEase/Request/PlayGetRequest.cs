using AcFunDanmuSongRequest.Platform.Interfaces;

namespace AcFunDanmuSongRequest.Platform.NetEase.Request
{
    struct  PlayGetRequest : IGetRequest
    {
        public string Host => $"https://music.163.com/api/song/enhance/player/url?csrf_token=&ids=[{Id}]&br={BitRate}";

        public string Keyword { get; set; }
        public long Id { get; set; }
        public int BitRate { get; set; }

    }
}
