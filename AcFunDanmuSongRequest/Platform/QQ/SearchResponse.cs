using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AcFunDanmuSongRequest.Platform.QQ
{
    internal struct SearchResponse
    {
        public static readonly JsonSerializerOptions Options = new()
            { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

        static SearchResponse()
        {
            Options.Converters.Add(new DateTimeOffsetConverter());
            Options.Converters.Add(new TimeSpanConverver());
        }

        public int Code { get; set; }
        public int Subcode { get; set; }
        public string Message { get; set; }
        public string Notice { get; set; }
        public DateTimeOffset Time { get; set; }
        public string Tips { get; set; }
        public SearchData Data { get; set; }

        public struct SearchData
        {
            public SongList Song { get; set; }


            public struct SongList
            {
                public int Curnum { get; set; }
                public int Curpage { get; set; }
                public SongItem[] List { get; set; }
                public int Totalnum { get; set; }

                public struct SongItem
                {
                    public long Albumid { get; set; }
                    public string Albummid { get; set; }
                    public string Albumname { get; set; }
                    public int Alertid { get; set; }
                    public TimeSpan Interval { get; set; }
                    [JsonPropertyName("media_mid")] public string MediaMid { get; set; }
                    public SingerInfo[] Singer { get; set; }
                    public long Songid { get; set; }
                    public string Songmid { get; set; }
                    public string Songname { get; set; }
                    public string StrMediaMid { get; set; }

                    public struct SingerInfo
                    {
                        public long Id { get; set; }
                        public string Mid { get; set; }
                        public string Name { get; set; }
                    }
                }
            }
        }

        private class DateTimeOffsetConverter : JsonConverter<DateTimeOffset>
        {
            public override DateTimeOffset Read(ref Utf8JsonReader reader, Type typeToConvert,
                JsonSerializerOptions options)
            {
                return DateTimeOffset.FromUnixTimeSeconds(reader.GetInt64());
            }

            public override void Write(Utf8JsonWriter writer, DateTimeOffset value, JsonSerializerOptions options)
            {
                writer.WriteNumberValue(value.ToUnixTimeSeconds());
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
}