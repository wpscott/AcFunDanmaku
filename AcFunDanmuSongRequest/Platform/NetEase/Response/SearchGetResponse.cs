using System.Text.Json.Serialization;

namespace AcFunDanmuSongRequest.Platform.NetEase.Response
{
    struct SearchGetResponse
    {
        public Song[] Songs { get; set; }
    }
}
