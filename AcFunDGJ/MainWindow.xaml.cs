using AcFunDanmuSongRequest;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using System.Windows.Shell;
using System.Windows.Threading;

namespace AcFunDGJ
{
    public partial class MainWindow : Window
    {
        private DispatcherTimer DurationTimer;
        public MainWindow()
        {
            InitializeComponent();
            WindowChrome.SetWindowChrome(this, new WindowChrome());

            Song.Text = "没有歌曲";
            Album.Text = "欢迎点歌";
            Duration.Text = "--:--/--:--";

            Loaded += MainWindow_Loaded;
        }

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            DGJ.AddSongEvent += song => { if (!IsPlaying) { IsPlaying = true; Next(null, null); } };

            await DGJ.Initialize();
            Trace.WriteLine("DGJ initialized");
#if DEBUG
            await DGJ.AddSong("喜欢你 乌拉喵");
            await DGJ.AddSong("笨蛋AC娘 AC娘本体");
            await DGJ.AddSong("巡年之礼");
            await DGJ.AddSong("爱在A站 纱朵");
            await DGJ.AddSong("为A而战 AC娘本体");
            await DGJ.AddSong("命犯桃花 FLASH1NG");
            await DGJ.AddSong("循环 尧顺宇");
#endif
        }

        private void MediaEnded(object sender, EventArgs e)
        {
            Next(null, null);
        }

        private bool IsPlaying = false;
        private void Play(object sender, RoutedEventArgs e)
        {
            if (!IsPlaying)
            {
                Player.Play();
                PlayBtn.Content = "\uE769";
            }
            else
            {
                Player.Pause();
                PlayBtn.Content = "\uE768";
            }
            IsPlaying = !IsPlaying;
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Space:
                case Key.MediaPlayPause:
                    Play(null, null);
                    break;
                case Key.Escape:
                    Player.Stop();
                    Close();
                    break;
                case Key.N:
                case Key.MediaNextTrack:
                    Next(null, null);
                    break;
                case Key.M:
                case Key.VolumeMute:
                    Mute(null, null);
                    break;
            }
        }

        private async void Next(object sender, RoutedEventArgs e)
        {
            if (DurationTimer != null)
            {
                DurationTimer.Stop();
                DurationTimer = null;
            }
            var song = await DGJ.NextSong();
            if (song == null)
            {
                IsPlaying = false;
                Player.Stop();
                Song.Text = "没有歌曲";
                Album.Text = "请点歌";
                Duration.Text = "--:--/--:--";
            }
            else
            {
                Player.Source = new Uri(song.Source);
                Song.Text = $"{song.Artist} - {song.Name}";
                Album.Text = $"专辑：{song.Album}";
                if (DurationTimer != null)
                {
                    DurationTimer.Stop();
                    DurationTimer = null;
                }
                DurationTimer = new DispatcherTimer();
                DurationTimer.Interval = TimeSpan.FromSeconds(1);
                DurationTimer.Tick += (s, e) =>
                {
                    Duration.Text = $"{Player.Position:mm\\:ss}/{song.Duration:mm\\:ss}";
                };
                DurationTimer.Start();
                Player.Play();
            }
        }

        private void Mute(object sender, RoutedEventArgs e)
        {
            if (Player.IsMuted)
            {
                MuteBtn.Content = "\uE767";
            }
            else
            {
                MuteBtn.Content = "\uE74F";
            }
            Player.IsMuted = !Player.IsMuted;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                DragMove();
            }
        }
    }
}
