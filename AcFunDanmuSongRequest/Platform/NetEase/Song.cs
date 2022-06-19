using System;
using AcFunDanmuSongRequest.Platform.Interfaces;

namespace AcFunDanmuSongRequest.Platform.NetEase;

internal struct Song : ISong
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Artist { get; set; }
    public string Album { get; set; }
    public TimeSpan Duration { get; set; }
    public string Source { get; set; }
}