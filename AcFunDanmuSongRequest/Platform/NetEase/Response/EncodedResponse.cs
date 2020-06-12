using AcFunDanmuSongRequest.Platform.Interfaces;

namespace AcFunDanmuSongRequest.Platform.NetEase.Response
{
    struct EncodedResponse : IEncodedResponse
    {
        public bool Abroad { get; set; }
        public int Code { get; set; }
        public string Result { get; set; }

        public string Decode() => NetEasePlatform.NetEaseDecodeUtil.Decode(Result);
    }
}
