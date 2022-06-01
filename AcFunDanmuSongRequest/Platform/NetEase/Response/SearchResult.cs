using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AcFunDanmuSongRequest.Platform.NetEase.Response
{
    internal readonly record struct SearchResult(Song[] Songs)
    {
        public static readonly JsonSerializerOptions Options = new()
            { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

        static SearchResult()
        {
            Options.Converters.Add(new DateTimeOffsetConverter());
            Options.Converters.Add(new TimeSpanConverter());
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

        private class TimeSpanConverter : JsonConverter<TimeSpan>
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

    internal readonly record struct Album(long Id, string Name, long PicId, Artist Artist);

    internal readonly record struct Artist(long Id, string Name, long Img1v1, string Img1v1Url, long PicId,
        string PicUrl);

    internal readonly record struct Song(long Id, string Name, Artist[] Artists, Album Album, TimeSpan Duration);
}