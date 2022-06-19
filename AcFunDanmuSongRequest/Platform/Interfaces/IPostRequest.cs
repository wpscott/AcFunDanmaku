using System.Net.Http;
using System.Text;

namespace AcFunDanmuSongRequest.Platform.Interfaces;

internal interface IPostRequest : IGetRequest
{
    public static readonly Encoding Encoding = Encoding.UTF8;
    public bool IsJson { get; }

    public HttpContent ToJson();
    public HttpContent ToForm();
}