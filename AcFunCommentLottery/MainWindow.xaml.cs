using AcFunCommentControl.Models;
using AcFunCommentLottery.Models;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace AcFunCommentLottery
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static readonly Regex NumberOnly = new Regex(@"^[\d]+$");

        private readonly Lottery lottery;

        public MainWindow()
        {
            InitializeComponent();

            lottery = new Lottery { };
            DataContext = lottery;

            EmotIconModel.Fetch();
        }

        private void CheckInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !NumberOnly.IsMatch(e.Text);
        }

        private void Fetch(object sender, RoutedEventArgs e)
        {
            lottery.Fetch();
        }

        private void Filter(object sender, RoutedEventArgs e)
        {
            if (lottery.Filtered)
            {
                lottery.Rest();
            }
            else
            {
                lottery.Filter();
            }
        }

        private void Roll(object sender, RoutedEventArgs e)
        {
            lottery.Roll();
        }

        private void ShowUser(object sender, MouseButtonEventArgs e)
        {
            var comment = (Comment)(sender as ListBox).SelectedItem;
            Process.Start(new ProcessStartInfo { FileName = $"https://www.acfun.cn/u/{comment.userId}", UseShellExecute = true });
        }
    }
}
