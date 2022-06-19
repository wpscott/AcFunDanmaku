using AcFunDanmuSongRequest.Platform.Interfaces;

namespace AcFunDanmuSongRequest.Platform.NetEase.Request;

internal readonly record struct PlayerV1GetRequest(string Keyword, long Id) : IGetRequest
{
    private static string Level => "standard";
    private static string EncodeType => "aac";

    public string Host =>
        $"https://music.163.com/api/song/enhance/player/url/v1?csrf_token=&ids=[{Id}]&level={Level}&encodeType={EncodeType}";
}