using AcFunDanmuSongRequest.Platform.Interfaces;

namespace AcFunDanmuSongRequest.Platform.NetEase.Request
{
    struct LyricGetRequest : IGetRequest
    {
        public string Host => $"http://music.163.com/api/song/lyric?csrf_token=&id={Id}&lv=0&tv=0";

        public long Id { get; set; }
    }
}
