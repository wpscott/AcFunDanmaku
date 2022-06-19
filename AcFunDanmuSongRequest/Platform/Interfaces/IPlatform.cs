using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace AcFunDanmuSongRequest.Platform.Interfaces;

internal interface IPlatform
{
    public ReadOnlyObservableCollection<ISong> Songs { get; }
    public ValueTask<ISong> AddSong(string keyword);
    public ValueTask<ISong> NextSong();
    public ValueTask<Lyrics> GetLyrics(ISong song);
}