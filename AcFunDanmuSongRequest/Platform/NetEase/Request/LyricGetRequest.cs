using AcFunDanmuSongRequest.Platform.Interfaces;

namespace AcFunDanmuSongRequest.Platform.NetEase.Request;

internal readonly record struct LyricGetRequest(long Id) : IGetRequest
{
    public string Host => $"http://music.163.com/api/song/lyric?csrf_token=&id={Id}&lv=0&tv=0";
}