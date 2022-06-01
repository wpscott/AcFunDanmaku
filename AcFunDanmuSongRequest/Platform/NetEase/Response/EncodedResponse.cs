using AcFunDanmuSongRequest.Platform.Interfaces;

namespace AcFunDanmuSongRequest.Platform.NetEase.Response
{
    internal readonly record struct EncodedResponse(bool Abroad, int Code, string Result) : IEncodedResponse
    {
        public string Decode() => NetEasePlatform.NetEaseDecodeUtil.Decode(Result);
    }
}