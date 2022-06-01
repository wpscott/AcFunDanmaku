using System.Threading.Tasks;

namespace AcFunDanmuSongRequest.Platform.Interfaces
{
    internal interface IPlatform
    {
        public ValueTask<ISong> AddSong(string keyword);
        public ISong Peek();
        public ValueTask<ISong> NextSong();
        public ValueTask<Lyrics> GetLyrics(ISong song);
    }
}