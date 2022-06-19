using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AcFunDanmuSongRequest;
using AcFunDanmuSongRequest.Platform.Interfaces;

namespace AcFunDGJ.ViewModels
{
    internal class ListViewModel
    {
        public ReadOnlyObservableCollection<ISong> List => DGJ.Songs;

        public ListViewModel()
        {
        }
    }
}