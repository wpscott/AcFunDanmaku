using System.Net.Http;
using AcFunDanmuSongRequest.Platform.Interfaces;

namespace AcFunDanmuSongRequest.Platform.NetEase.Request;

internal readonly record struct LyricPostRequest(long Id) : IPostRequest
{
    public string Host => "http://music.163.com/weapi/song/lyric?csrf_token=";
    public bool IsJson => false;

    public HttpContent ToJson()
    {
        return new StringContent(ToString(), IPostRequest.Encoding, "application/json");
    }

    public HttpContent ToForm()
    {
        return new FormUrlEncodedContent(NetEasePlatform.NetEaseEncryptionUtil.GenerateParams(ToString()));
    }

    public override string ToString()
    {
        return $"{{\"id\":\"{Id}\",\"lv\":0,\"tv\":0}}";
    }
}