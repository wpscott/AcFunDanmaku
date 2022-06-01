using AcFunDanmuSongRequest.Platform.Interfaces;
using System.Net.Http;

namespace AcFunDanmuSongRequest.Platform.NetEase.Request
{
    internal readonly record struct SearchPostRequest(string Keyword, int Offset, int Limit) : IPostRequest
    {
        public string Host => $"https://music.163.com/weapi/search/get/web?csrf_token=";
        public bool IsJson => false;
        private static int Type => 1;
        private static string CsrfToken => string.Empty;

        public override string ToString()
        {
            return
                $"{{\"s\":\"{Keyword}\",\"offset\":{Offset},\"limit\":{Limit},\"type\":{Type},\"csrf_token\":\"{CsrfToken}\"}}";
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