using System.Collections.ObjectModel;
using AcFunDanmuSongRequest;
using AcFunDanmuSongRequest.Platform.Interfaces;

namespace AcFunDGJ.ViewModels;

internal class ListViewModel
{
    public ReadOnlyObservableCollection<ISong> List => DGJ.Songs;
}