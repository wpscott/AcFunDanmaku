using System.Net.Http;
using AcFunDanmuSongRequest.Platform.Interfaces;

namespace AcFunDanmuSongRequest.Platform.NetEase.Request;

internal readonly record struct CloudSearchPostRequest(string Keyword, int Offset, int Limit) : IPostRequest
{
    private static int Type => 1;
    private static string CsrfToken => string.Empty;
    public string Host => "https://music.163.com/weapi/cloudsearch/get/web?csrf_token=";
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
        return
            $"{{\"s\":\"{Keyword}\",\"offset\":{Offset},\"limit\":{Limit},\"type\":{Type},\"csrf_token\":\"{CsrfToken}\"}}";
    }
}