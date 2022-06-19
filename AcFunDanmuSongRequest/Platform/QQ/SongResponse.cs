using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AcFunDanmuSongRequest.Platform.QQ;

internal struct SongResponse
{
    public static readonly JsonSerializerOptions Options = new() { PropertyNameCaseInsensitive = true };

    static SongResponse()
    {
        Options.Converters.Add(new DateTimeOffsetConverter());
        Options.Converters.Add(new TimeSpanConverver());
    }

    public int Code { get; set; }
    public DateTimeOffset TS { get; set; }
    [JsonPropertyName("start_ts")] public DateTimeOffset StartTs { get; set; }
    [JsonPropertyName("req_0")] public Req Data { get; set; }

    public struct Req
    {
        public int Code { get; set; }
        public SongData Data { get; set; }

        public struct SongData
        {
            public TimeSpan Expiration { get; set; }
            [JsonPropertyName("login_key")] public string LoginKey { get; set; }
            public string Msg { get; set; }
            public int RetCode { get; set; }
            public string ServerCheck { get; set; }
            public string TestFile2G { get; set; }
            public string TestFileWifi { get; set; }
            public string Uin { get; set; }
            [JsonPropertyName("verify_type")] public int VerifyType { get; set; }
            public MidInfo[] MidUrlInfo { get; set; }
            public string[] SIP { get; set; }
            public string[] ThirdIp { get; set; }

            public struct MidInfo
            {
                [JsonPropertyName("common_downfromtag")]
                public int CommonDownFromTag { get; set; }

                public string ErrType { get; set; }
                public string FileName { get; set; }
                public string FlowFromTag { get; set; }
                public string FlowUrl { get; set; }
                public int HisBuy { get; set; }
                public int HisDown { get; set; }
                public int IsBuy { get; set; }
                public int IsOnly { get; set; }
                public int OneCan { get; set; }
                public string Opi30sUrl { get; set; }
                public string Opi48kUrl { get; set; }
                public string Opi96kUrl { get; set; }
                public string Opi128kUrl { get; set; }
                public string Opi192kOggUrl { get; set; }
                public string Opi192kUrl { get; set; }
                public string OpiFlacUrl { get; set; }
                public int P2PFromTag { get; set; }
                public int Pdl { get; set; }
                public int PNeed { get; set; }
                public int PNeebBuy { get; set; }
                public int PRemain { get; set; }
                public string PUrl { get; set; }
                public int QMdlFromTag { get; set; }
                public int Result { get; set; }
                public string SongMid { get; set; }
                public string Tips { get; set; }
                public int UIAlert { get; set; }
                [JsonPropertyName("vip_downfromtag")] public int VIPDownFromTag { get; set; }
                public string VKey { get; set; }
                public string WifiFromTag { get; set; }
                public string WifiUrl { get; set; }
            }
        }
    }

    private class DateTimeOffsetConverter : JsonConverter<DateTimeOffset>
    {
        public override DateTimeOffset Read(ref Utf8JsonReader reader, Type typeToConvert,
            JsonSerializerOptions options)
        {
            return DateTimeOffset.FromUnixTimeMilliseconds(reader.GetInt64());
        }

        public override void Write(Utf8JsonWriter writer, DateTimeOffset value, JsonSerializerOptions options)
        {
            writer.WriteNumberValue(value.ToUnixTimeMilliseconds());
        }
    }

    private class TimeSpanConverver : JsonConverter<TimeSpan>
    {
        public override TimeSpan Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return TimeSpan.FromSeconds(reader.GetInt32());
        }

        public override void Write(Utf8JsonWriter writer, TimeSpan value, JsonSerializerOptions options)
        {
            writer.WriteNumberValue(value.TotalSeconds);
        }
    }
}