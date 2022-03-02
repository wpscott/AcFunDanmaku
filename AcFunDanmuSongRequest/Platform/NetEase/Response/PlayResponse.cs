using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AcFunDanmuSongRequest.Platform.NetEase.Response
{
    struct PlayResponse
    {
        public static readonly JsonSerializerOptions Options = new() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
        static PlayResponse()
        {
            Options.Converters.Add(new TimeSpanConverver());
        }

        public int Code { get; set; }
        public Source[] Data { get; set; }

        class TimeSpanConverver : JsonConverter<TimeSpan>
        {
            public override TimeSpan Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                return TimeSpan.FromSeconds(reader.GetInt64());
            }
            public override void Write(Utf8JsonWriter writer, TimeSpan value, JsonSerializerOptions options)
            {
                writer.WriteNumberValue(value.TotalMilliseconds);
            }
        }
    }

    struct Source
    {
        public string Url { get; set; }
    }
}
