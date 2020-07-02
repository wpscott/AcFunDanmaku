using AcFunBlackBoxGuess.Models;
using System;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace AcFunBlackBoxGuess
{
    public partial class MainWindow : Window
    {
        private readonly Game game;

        public MainWindow()
        {
            InitializeComponent();
            game = new Game { };

            DataContext = game;
        }

        private void Connect(object sender, RoutedEventArgs e)
        {
            if (!game.Connected)
            {
                game.Connect();
            }
            else
            {
                game.Stop();
            }
        }

        private void Start(object sender, RoutedEventArgs e)
        {
            if (game.GameStart)
            {
                game.Stop();
            }
            else
            {
                game.Start();
            }
        }

        private void Yes(object sender, RoutedEventArgs e)
        {
            var danmu = (Danmu)(sender as Button).DataContext;
            game.SetYes(danmu);
        }

        private void No(object sender, RoutedEventArgs e)
        {
            var danmu = (Danmu)(sender as Button).DataContext;
            game.SetNo(danmu);
        }

        private void Bingo(object sender, RoutedEventArgs e)
        {
            var danmu = (Danmu)(sender as Button).DataContext;
            game.Bingo(danmu);
        }

        private void Remove(object sender, RoutedEventArgs e)
        {
            var danmu = (Danmu)(sender as Button).DataContext;
            game.Remove(danmu);
        }

        private static readonly Regex NumberOnly = new Regex(@"^[\d]+$", RegexOptions.Compiled);
        private void CheckInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !NumberOnly.IsMatch(e.Text);
        }

        private void ShowUser(object sender, MouseButtonEventArgs e)
        {
            var comment = (Danmu)(sender as ListBox).SelectedItem;
            Process.Start(new ProcessStartInfo { FileName = $"https://www.acfun.cn/u/{comment.UserId}", UseShellExecute = true });
        }
    }
}
