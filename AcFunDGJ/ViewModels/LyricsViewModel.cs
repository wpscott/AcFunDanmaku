using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using AcFunDanmuSongRequest.Platform;
using AcFunDGJ.Models;

namespace AcFunDGJ.ViewModels
{
    internal class LyricsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private Lyrics _lyrics;

        public Lyrics Lyrics
        {
            get => _lyrics;
            set
            {
                _lyrics = value;
                NotifyPropertyChanged(nameof(Lyrics));
            }
        }

        public void Tick(TimeSpan timeSpan)
        {
            _lyrics.Tick(timeSpan);
        }

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}