using System;

namespace AcFunDanmuSongRequest.Platform.Interfaces
{
    public interface ISong
    {
        public string Name { get; set; }
        public string Artist { get; set; }
        public string Album { get; set; }
        public TimeSpan Duration { get; set; }
        public string Source { get; set; }
    }
}
