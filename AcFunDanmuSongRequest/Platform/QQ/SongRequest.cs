using AcFunDanmuSongRequest.Platform.Interfaces;
using System.Text.Json;
using System.Web;

namespace AcFunDanmuSongRequest.Platform.QQ
{
    struct SongRequest : IGetRequest
    {
        public string Host => $"https://u.y.qq.com/cgi-bin/musicu.fcg?data={JsonSerializer.Serialize(data)}";

        public SongRequest(int uin, int guid, string songmid)
        {
            data = new MusicuData
            {
                req_0 = new MusicuData.Request
                {
                    param = new MusicuData.Request.RequestParams
                    {
                        uin = $"{uin}",
                        guid = $"{guid}",
                        songmid = new string[] { songmid }
                    },
                    //comm = new MusicuData.Request.CommonParams
                    //{
                    //    uin = uin,
                    //}
                }
            };
        }
        MusicuData data { get; set; }

        struct MusicuData
        {
            public Request req_0 { get; set; }

            public struct Request
            {
                public string module => "vkey.GetVkeyServer";
                public string method => "CgiGetVkey";
                public RequestParams param { get; set; }
                //public CommonParams comm { get; set; }
                public struct RequestParams
                {
                    public string guid { get; set; }
                    public string[] songmid { get; set; }
                    public int[] songtype => new int[] { 0 };
                    public string uin { get; set; }
                    public int platform => 20;
                }
                public struct CommonParams
                {
                    public int uin { get; set; }
                    public string format => "json";
                    public int ct => 20;
                    public int cv => 0;
                }
            }
        }
    }
}
