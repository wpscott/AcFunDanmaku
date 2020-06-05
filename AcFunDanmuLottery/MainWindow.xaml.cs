using AcFunDanmuLottery.Models;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace AcFunDanmuLottery
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

            lottery = new Lottery { CurrentStatus = "未连接", Connected = false, Amount = "1", ShowAll = false };
            DataContext = lottery;
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !NumberOnly.IsMatch(e.Text);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(lottery.UserId.Trim()))
            {
                MessageBox.Show("请输入主播ID");
                return;
            }
            else if (!long.TryParse(lottery.UserId.Trim(), out _))
            {
                MessageBox.Show("请输入正确的主播ID");
                return;
            }
            if (!lottery.Connected)
            {
                lottery.Connect();
            }
            else
            {
                lottery.Stop();
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(lottery.Pattern.Trim()))
            {
                MessageBox.Show("请输入关键词");
                return;
            }
            if (lottery.SearchStart)
            {
                lottery.StopSearch();
            }
            else
            {
                lottery.StartSearch();
                lottery.Pattern = lottery.Pattern.Trim();
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            int amount;
            if (string.IsNullOrEmpty(lottery.Amount.Trim()))
            {
                MessageBox.Show("请输入抽取数量");
                return;
            }
            else if (!int.TryParse(lottery.Amount.Trim(), out amount))
            {
                MessageBox.Show("请输入正确的抽取数量");
                return;
            }
            lottery.Roll(amount);
        }
    }
}
