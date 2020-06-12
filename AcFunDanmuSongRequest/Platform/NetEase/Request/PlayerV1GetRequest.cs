using AcFunDanmuSongRequest.Platform.Interfaces;

namespace AcFunDanmuSongRequest.Platform.NetEase.Request
{
    struct PlayerV1GetRequest : IGetRequest
    {
        public string Host => $"https://music.163.com/api/song/enhance/player/url/v1?csrf_token=&ids=[{Id}]&level={Level}&encodeType={EncodeType}";

        public string Keyword { get; set; }
        public long Id { get; set; }
        public string Level => "standard";
        public string EncodeType => "aac";

    }
}
