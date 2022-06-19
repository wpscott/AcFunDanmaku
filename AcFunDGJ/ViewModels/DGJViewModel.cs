using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using AcFunDanmuSongRequest;
using AcFunDanmuSongRequest.Platform.Interfaces;
using AcFunDGJ.Models;

namespace AcFunDGJ.ViewModels
{
    internal class DGJViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private bool _showAlbumCaption;

        public bool ShowAlbumCaption
        {
            get => _showAlbumCaption;
            set
            {
                _showAlbumCaption = value;
                NotifyPropertyChanged(nameof(ShowAlbumCaption));
            }
        }

        private string _title = "连接弹幕服务器中";

        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                NotifyPropertyChanged(nameof(Title));
            }
        }

        private string _album = "请稍等";

        public string Album
        {
            get => _album;
            set
            {
                _album = value;
                NotifyPropertyChanged(nameof(Album));
            }
        }

        private bool _isPlaying;

        public bool IsPlaying
        {
            get => _isPlaying;
            set
            {
                _isPlaying = value;
                NotifyPropertyChanged(nameof(IsPlaying));
            }
        }

        private string _playBtnTxt = "\uE769";

        public string PlayBtnTxt
        {
            get => _playBtnTxt;
            set
            {
                _playBtnTxt = value;
                NotifyPropertyChanged(nameof(PlayBtnTxt));
            }
        }

        private ICommand _playCommand;
        public ICommand PlayCommand => _playCommand ??= new CommandHandler(Play, true);

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

        private readonly DispatcherTimer _timer;

        private string _duration = "--:--/--:--";

        public string Duration
        {
            get => _duration;
            set
            {
                _duration = value;
                NotifyPropertyChanged(nameof(Duration));
            }
        }

        private readonly LyricsViewModel Lyrics;
        private readonly ListViewModel List;

        public DGJViewModel()
        {
            Lyrics = new();
            List = new();

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

        public MediaElement Player { get; set; }
        private Uri _source;

        public Uri Source
        {
            get => _source;
            set
            {
                _source = value;
                NotifyPropertyChanged(nameof(Source));
            }
        }

        private ISong _song;


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

        private ICommand _nextCommand;

        public ICommand NextCommand => _nextCommand ??= new CommandHandler(Next, true);

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
                    Source = new(_song.Source);
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

        private ICommand _muteCommand;
        public ICommand MuteCommand => _muteCommand ??= new CommandHandler(Mute, true);

        private bool _isMuted;

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

        private string _muteBtnTxt = "\uE767";

        public string MuteBtnTxt
        {
            get => _muteBtnTxt;
            set
            {
                _muteBtnTxt = value;
                NotifyPropertyChanged(nameof(MuteBtnTxt));
            }
        }

        private void Mute()
        {
            IsMuted = !IsMuted;
        }

        private ICommand _lyricsCommand;
        public ICommand LyricsCommand => _lyricsCommand ??= new CommandHandler(ShowLyrics, true);
        private LyricWindow _lyric;

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

        private ICommand _listCommand;
        public ICommand ListCommand => _listCommand ??= new CommandHandler(ShowList, true);
        private ListWindow _list;

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

        public ICommand CloseCommand => new CommandHandler(() =>
        {
            _timer?.Stop();
            _lyric?.Close();
        }, true);

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}