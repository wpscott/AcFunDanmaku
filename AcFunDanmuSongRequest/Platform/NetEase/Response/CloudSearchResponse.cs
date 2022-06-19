using System;

namespace AcFunDanmuSongRequest.Platform.NetEase.Response;

internal readonly record struct CloudSearchResponse
{
    public Song[] Songs { get; init; }

    public readonly record struct Song(Song.Album Al, Song.Artist[] Ar, long Id, string Name, TimeSpan Dt)
    {
        public readonly record struct Album(long Id, string Name, long Pic, string PicUrl);

        public readonly record struct Artist(long Id, string Name);
    }
}