using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AcFunDanmuSongRequest.Platform.NetEase.Response
{
    struct SearchResult
    {
        public static readonly JsonSerializerOptions Options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
        static SearchResult()
        {
            Options.Converters.Add(new DateTimeOffsetConverter());
            Options.Converters.Add(new TimeSpanConverver());
        }
        public Song[] Songs { get; set; }

        class DateTimeOffsetConverter : JsonConverter<DateTimeOffset>
        {
            public override DateTimeOffset Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                return DateTimeOffset.FromUnixTimeMilliseconds(reader.GetInt64());
            }
            public override void Write(Utf8JsonWriter writer, DateTimeOffset value, JsonSerializerOptions options)
            {
                writer.WriteNumberValue(value.ToUnixTimeMilliseconds());
            }
        }

        class TimeSpanConverver : JsonConverter<TimeSpan>
        {
            public override TimeSpan Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                return TimeSpan.FromMilliseconds(reader.GetInt64());
            }
            public override void Write(Utf8JsonWriter writer, TimeSpan value, JsonSerializerOptions options)
            {
                writer.WriteNumberValue(value.TotalMilliseconds);
            }
        }
    }

    struct Album
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long PicId { get; set; }
        public Artist Artist { get; set; }
    }

    struct Artist
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long Img1v1 { get; set; }
        public string Img1v1Url { get; set; }
        public long PicId { get; set; }
        public string PicUrl { get; set; }
    }

    struct Song
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public Artist[] Artists { get; set; }
        public Album Album { get; set; }
        public TimeSpan Duration { get; set; }
    }
}
