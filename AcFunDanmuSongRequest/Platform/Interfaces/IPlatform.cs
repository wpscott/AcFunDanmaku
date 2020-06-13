using System.Threading.Tasks;

namespace AcFunDanmuSongRequest.Platform.Interfaces
{
    interface IPlatform
    {
        public ValueTask<ISong> AddSong(string keyword);
        public ISong Peek();
        public ValueTask<ISong> NextSong();
    }
}
