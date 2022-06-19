using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using AcFunDanmuSongRequest;
using AcFunDanmuSongRequest.Platform.Interfaces;
using AcFunDGJ.Models;

namespace AcFunDGJ.ViewModels;

internal class DGJViewModel : INotifyPropertyChanged
{
    private readonly DispatcherTimer _timer;
    private readonly ListViewModel List;

    private readonly LyricsViewModel Lyrics;

    private string _album = "请稍等";

    private string _duration = "--:--/--:--";

    private bool _isMuted;

    private bool _isPlaying;
    private ListWindow _list;

    private ICommand _listCommand;
    private LyricWindow _lyric;

    private ICommand _lyricsCommand;

    private string _muteBtnTxt = "\uE767";

    private ICommand _muteCommand;

    private ICommand _nextCommand;

    private string _playBtnTxt = "\uE769";

    private ICommand _playCommand;

    private bool _showAlbumCaption;

    private ISong _song;
    private Uri _source;

    private string _title = "连接弹幕服务器中";

    public DGJViewModel()
    {
        Lyrics = new LyricsViewModel();
        List = new ListViewModel();

        DGJ.OnConnect += () =>
        {
            Title = "没有歌曲";
            Album = "欢迎点歌";
            Duration = "--:--/--:--";
        };
        DGJ.OnExit += () =>
        {
            if (IsPlaying) return;
            Title = "连接已断开或直播已结束";
            Album = "按ESC关闭点歌姬";
            Duration = "--:--/--:--";
        };
        DGJ.OnAddSong += song =>
        {
            if (IsPlaying) return;
            IsPlaying = true;
            Next();
        };

        const double interval = 1000.0d / 144.0d;
        _timer = new DispatcherTimer
        {
            Interval = TimeSpan.FromMilliseconds(interval)
        };
        _timer.Tick += (_, _) =>
        {
            Duration = $"{Player?.Position:mm\\:ss}/{_song?.Duration:mm\\:ss}";
            Lyrics.Tick(Player?.Position ?? TimeSpan.Zero);
        };

        Initialize();
    }

    public bool ShowAlbumCaption
    {
        get => _showAlbumCaption;
        set
        {
            _showAlbumCaption = value;
            NotifyPropertyChanged(nameof(ShowAlbumCaption));
        }
    }

    public string Title
    {
        get => _title;
        set
        {
            _title = value;
            NotifyPropertyChanged(nameof(Title));
        }
    }

    public string Album
    {
        get => _album;
        set
        {
            _album = value;
            NotifyPropertyChanged(nameof(Album));
        }
    }

    public bool IsPlaying
    {
        get => _isPlaying;
        set
        {
            _isPlaying = value;
            NotifyPropertyChanged(nameof(IsPlaying));
        }
    }

    public string PlayBtnTxt
    {
        get => _playBtnTxt;
        set
        {
            _playBtnTxt = value;
            NotifyPropertyChanged(nameof(PlayBtnTxt));
        }
    }

    public ICommand PlayCommand => _playCommand ??= new CommandHandler(Play, true);

    public string Duration
    {
        get => _duration;
        set
        {
            _duration = value;
            NotifyPropertyChanged(nameof(Duration));
        }
    }

    public MediaElement Player { get; set; }

    public Uri Source
    {
        get => _source;
        set
        {
            _source = value;
            NotifyPropertyChanged(nameof(Source));
        }
    }

    public ICommand NextCommand => _nextCommand ??= new CommandHandler(Next, true);
    public ICommand MuteCommand => _muteCommand ??= new CommandHandler(Mute, true);

    public bool IsMuted
    {
        get => _isMuted;
        set
        {
            _isMuted = value;
            MuteBtnTxt = _isMuted ? "\uE74F" : "\uE767";
            NotifyPropertyChanged(nameof(IsMuted));
        }
    }

    public string MuteBtnTxt
    {
        get => _muteBtnTxt;
        set
        {
            _muteBtnTxt = value;
            NotifyPropertyChanged(nameof(MuteBtnTxt));
        }
    }

    public ICommand LyricsCommand => _lyricsCommand ??= new CommandHandler(ShowLyrics, true);
    public ICommand ListCommand => _listCommand ??= new CommandHandler(ShowList, true);

    public ICommand CloseCommand => new CommandHandler(() =>
    {
        _timer?.Stop();
        _lyric?.Close();
        _list?.Close();
    }, true);

    public event PropertyChangedEventHandler PropertyChanged;

    private void Play()
    {
        if (!IsPlaying)
        {
            Player?.Play();
            PlayBtnTxt = "\uE769";
        }
        else
        {
            Player?.Pause();
            PlayBtnTxt = "\uE768";
        }

        IsPlaying = !IsPlaying;
    }


    private static async void Initialize()
    {
        var ready = await DGJ.Initialize();
        Trace.WriteLine("DGJ initialized");
#if DEBUG
        await DGJ.AddSong("喜欢你 乌拉喵");
        await DGJ.AddSong("笨蛋AC娘2.0 AC娘本体");
        await DGJ.AddSong("笨蛋AC娘 AC娘本体");
        await DGJ.AddSong("A级伙伴");
        await DGJ.AddSong("爱在A站 纱朵");
        await DGJ.AddSong("为A而战 AC娘本体");
        await DGJ.AddSong("命犯桃花 FLASH1NG");
        await DGJ.AddSong("循环 尧顺宇");
#endif
    }

    private async void Next()
    {
        if (DGJ.IsRunning)
        {
            _song = await DGJ.NextSong();
            _timer.Stop();
            if (_song == null)
            {
                IsPlaying = false;
                ShowAlbumCaption = false;
                Player?.Stop();
                Title = "没有歌曲";
                Album = "欢迎点歌";
                Duration = "--:--/--:--";
            }
            else
            {
                Source = new Uri(_song.Source);
                Lyrics.Lyrics = await DGJ.GetLyrics(_song);
                Title = $"{_song.Artist} - {_song.Name}";
                Album = _song.Album;
                ShowAlbumCaption = true;
                _timer.Start();
                Player?.Play();
            }
        }
        else
        {
            ShowAlbumCaption = false;
            IsPlaying = false;
            Title = "连接已断开或直播已结束";
            Album = "按ESC关闭点歌姬";
            Duration = "--:--/--:--";
        }
    }

    private void Mute()
    {
        IsMuted = !IsMuted;
    }

    private void ShowLyrics()
    {
        if (_lyric == null)
        {
            _lyric = new LyricWindow
            {
                DataContext = Lyrics
            };
            _lyric.Show();
        }

        switch (_lyric.Visibility)
        {
            case Visibility.Visible:
                _lyric.Hide();
                break;
            case Visibility.Hidden:
            case Visibility.Collapsed:
            default:
                _lyric.Show();
                break;
        }
    }

    private void ShowList()
    {
        if (_list == null)
        {
            _list = new ListWindow
            {
                DataContext = List
            };
            _list.Show();
        }

        switch (_list.Visibility)
        {
            case Visibility.Visible:
                _list.Hide();
                break;
            case Visibility.Hidden:
            case Visibility.Collapsed:
            default:
                _list.Show();
                break;
        }
    }

    private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}