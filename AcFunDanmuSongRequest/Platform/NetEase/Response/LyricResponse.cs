using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace AcFunDanmuSongRequest.Platform.NetEase.Response
{
    struct LyricResponse
    {
        public static readonly JsonSerializerOptions Options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
        public int Code { get; set; }
        public LyricDetail Lrc { get; set; }
        public bool Qfy { get; set; }
        public bool Sfy { get; set; }
        public bool Sgx { get; set; }
        public LyricDetail Tlyric { get; set; }

        public struct LyricDetail
        {
            public string Lyric { get; set; }
            public int Version { get; set; }
        }
    }
}
