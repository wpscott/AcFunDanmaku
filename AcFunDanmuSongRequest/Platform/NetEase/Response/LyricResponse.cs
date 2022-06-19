using System.Text.Json;

namespace AcFunDanmuSongRequest.Platform.NetEase.Response;

internal readonly record struct LyricResponse(int Code, LyricResponse.LyricDetail Lrc, bool Qft, bool Sfy, bool Sgx,
    LyricResponse.LyricDetail Tlyric)
{
    public static readonly JsonSerializerOptions Options = new()
        { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

    public readonly record struct LyricDetail(string Lyric, int Version);
}