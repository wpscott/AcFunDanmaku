using System;

namespace AcFunDanmuSongRequest.Platform.Interfaces;

public interface ISong
{
    public string Name { get; }
    public string Artist { get; }
    public string Album { get; }
    public TimeSpan Duration { get; }
    public string Source { get; }
}