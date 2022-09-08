using AcFunBlackBoxGuess.Models;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace AcFunBlackBoxGuess
{
    public partial class MainWindow : Window
    {
        private readonly Game _game;

        public MainWindow()
        {
            InitializeComponent();
            _game = new Game { };

            DataContext = _game;
        }

        private void Connect(object sender, RoutedEventArgs e)
        {
            if (!_game.Connected)
            {
                _game.Connect();
            }
            else
            {
                _game.Stop();
            }
        }

        private void Start(object sender, RoutedEventArgs e)
        {
            if (_game.GameStart)
            {
                _game.Stop();
            }
            else
            {
                _game.Start();
            }
        }

        private void Yes(object sender, RoutedEventArgs e)
        {
            var danmu = (Danmu)(sender as Button).DataContext;
            _game.SetYes(danmu);
        }

        private void No(object sender, RoutedEventArgs e)
        {
            var danmu = (Danmu)(sender as Button).DataContext;
            _game.SetNo(danmu);
        }

        private void Bingo(object sender, RoutedEventArgs e)
        {
            var danmu = (Danmu)(sender as Button).DataContext;
            _game.Bingo(danmu);
        }

        private void Remove(object sender, RoutedEventArgs e)
        {
            var danmu = (Danmu)(sender as Button).DataContext;
            _game.Remove(danmu);
        }

        private static readonly Regex NumberOnly = new(@"^[\d]+$", RegexOptions.Compiled);

        private void CheckInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !NumberOnly.IsMatch(e.Text);
        }

        private void ShowUser(object sender, MouseButtonEventArgs e)
        {
            var comment = (Danmu)(sender as ListBox).SelectedItem;
            Process.Start(new ProcessStartInfo
                { FileName = $"https://www.acfun.cn/u/{comment.UserId}", UseShellExecute = true });
        }
    }
}