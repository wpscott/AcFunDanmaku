using AcFunDanmuSongRequest.Platform.Interfaces;
using System.Net.Http;

namespace AcFunDanmuSongRequest.Platform.NetEase.Request
{
    class LyricPostRequest : IPostRequest
    {
        public string Host => $"http://music.163.com/weapi/song/lyric?csrf_token=";
        public bool IsJson => false;

        public long Id { get; set; }

        public override string ToString()
        {
            return $"{{\"id\":\"{Id}\",\"lv\":0,\"tv\":0}}";
        }

        public HttpContent ToJson()
        {
            return new StringContent(ToString(), IPostRequest.Encoding, "application/json");
        }

        public HttpContent ToForm()
        {
            return new FormUrlEncodedContent(NetEasePlatform.NetEaseEncryptionUtil.GenerateParams(ToString()));
        }
    }
}
