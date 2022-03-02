using AcFunDanmuSongRequest.Platform.Interfaces;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AcFunDanmuSongRequest.Platform.QQ
{
    struct SongRequest : IGetRequest
    {
        public string Host => $"https://u.y.qq.com/cgi-bin/musicu.fcg?data={JsonSerializer.Serialize(Data)}";

        public SongRequest(int uin, int guid, string songmid)
        {
            Data = new MusicuData
            {
                Req0 = new MusicuData.Request
                {
                    Param = new MusicuData.Request.RequestParams
                    {
                        Uin = $"{uin}",
                        Guid = $"{guid}",
                        Songmid = new string[] { songmid }
                    },
                    //comm = new MusicuData.Request.CommonParams
                    //{
                    //    uin = uin,
                    //}
                }
            };
        }

        [JsonPropertyName("data")]
        MusicuData Data { get; set; }

        struct MusicuData
        {
            [JsonPropertyName("req_0")]
            public Request Req0 { get; set; }

            public struct Request
            {
                [JsonPropertyName("module")]
                public static string Module => "vkey.GetVkeyServer";
                [JsonPropertyName("method")]
                public static string Method => "CgiGetVkey";
                [JsonPropertyName("param")]
                public RequestParams Param { get; set; }
                //public CommonParams comm { get; set; }
                public struct RequestParams
                {
                    [JsonPropertyName("guid")]
                    public string Guid { get; set; }
                    [JsonPropertyName("songmid")]
                    public string[] Songmid { get; set; }
                    [JsonPropertyName("songtype")]
                    public static int[] Songtype => new int[] { 0 };
                    [JsonPropertyName("uin")]
                    public string Uin { get; set; }
                    [JsonPropertyName("platform")]
                    public static int Platform => 20;
                }
                public struct CommonParams
                {
                    [JsonPropertyName("uin")]
                    public int Uin { get; set; }
                    [JsonPropertyName("format")]
                    public static string Format => "json";
                    [JsonPropertyName("ct")]
                    public static int Ct => 20;
                    [JsonPropertyName("cv")]
                    public static int Cv => 0;
                }
            }
        }
    }
}
