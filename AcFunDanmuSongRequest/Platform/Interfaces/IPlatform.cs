using System.Threading.Tasks;

namespace AcFunDanmuSongRequest.Platform.Interfaces
{
    interface IPlatform
    {
        public Task<ISong> AddSong(string keyword);
        public ISong Peek();
        public Task<ISong> NextSong();
    }
}
