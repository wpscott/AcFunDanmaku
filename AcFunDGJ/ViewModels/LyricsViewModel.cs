using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using AcFunDanmuSongRequest.Platform;

namespace AcFunDGJ.ViewModels;

internal class LyricsViewModel : INotifyPropertyChanged
{
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

    public event PropertyChangedEventHandler PropertyChanged;

    public void Tick(TimeSpan timeSpan)
    {
        _lyrics.Tick(timeSpan);
    }

    private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}