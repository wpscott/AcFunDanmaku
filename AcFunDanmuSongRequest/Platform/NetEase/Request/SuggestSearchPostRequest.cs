using AcFunDanmuSongRequest.Platform.Interfaces;
using System.Net.Http;

namespace AcFunDanmuSongRequest.Platform.NetEase.Request
{
    struct SuggestSearchPostRequest : IPostRequest
    {
        public string Host => "https://music.163.com/weapi/search/suggest/web?csrf_token=";
        public bool IsJson => true;

        public string Keyword { get; set; }
        public int Offset { get; set; }
        public int Limit { get; set; }
        public int Type => 1;
        public string CsrfToken => string.Empty;

        public override string ToString()
        {
            return $"{{\"s\":\"{Keyword}\",\"offset\":{Offset},\"limit\":{Limit},\"type\":{Type},\"csrf_token\":\"{CsrfToken}\"}}";
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
