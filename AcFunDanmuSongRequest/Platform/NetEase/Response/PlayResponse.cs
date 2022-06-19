using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AcFunDanmuSongRequest.Platform.NetEase.Response;

internal readonly record struct PlayResponse(int Code, Source[] Data)
{
    public static readonly JsonSerializerOptions Options = new()
        { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

    static PlayResponse()
    {
        Options.Converters.Add(new TimeSpanConverter());
    }

    private class TimeSpanConverter : JsonConverter<TimeSpan>
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

internal readonly record struct Source(string Url);