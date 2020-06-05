using AcFunDanmu;
using AcFunDanmu.Enums;
using AcFunDanmuLottery.Models;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Timers;
using System.Web;
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

        private Client client;
        public MainWindow()
        {
            InitializeComponent();

            lottery = new Lottery { CurrentStatus = "未连接", Connected = false, Amount = "1" };
            DataContext = lottery;
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !NumberOnly.IsMatch(e.Text);
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
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
                await Task.Run(() => Connect(lottery.UserId.Trim()));
            }
            else
            {
                lottery.Connected = false;
                await client.Stop("User Exit");
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

        private async void Connect(string userId)
        {
            lottery.CurrentStatus = "连接中";

            client = new Client(userId);
            client.Handler = HandleSignal;

            var retry = 0;
            var resetTimer = new Timer(5000);
            resetTimer.Elapsed += (s, e) => retry = 0;

            lottery.CurrentStatus = "已连接";
            lottery.Connected = true;

            while (lottery.Connected && !await client.Start() && retry < 5)
            {
                lottery.CurrentStatus = "断线重连中";
                if (retry > 0) { resetTimer.Stop(); }
                retry++;
                resetTimer.Start();
            }
            if (retry > 0)
            {
                lottery.CurrentStatus = "连接已断开";
            }
            else
            {
                lottery.CurrentStatus = "直播已结束";
            }
            lottery.Connected = false;
        }

        private void HandleSignal(string messagetType, byte[] payload)
        {
            switch (messagetType)
            {
                // Includes comment, gift, enter room, like, follower
                case PushMessage.ACTION_SIGNAL:
                    var actionSignal = ZtLiveScActionSignal.Parser.ParseFrom(payload);

                    foreach (var item in actionSignal.Item)
                    {
                        switch (item.SingalType)
                        {
                            case PushMessage.ActionSignal.COMMENT:
                                foreach (var pl in item.Payload)
                                {
                                    var comment = CommonActionSignalComment.Parser.ParseFrom(pl);
                                    comment.Content = HttpUtility.HtmlDecode(comment.Content);
                                    Trace.WriteLine($"{comment.SendTimeMs} - {comment.UserInfo.Nickname}({comment.UserInfo.UserId}): {comment.Content}");
                                    Dispatcher.Invoke(() =>
                                    {
                                        lottery.AddComment(comment);
                                        CommentList.Dispatcher.Invoke(() => CommentList.SelectedIndex = lottery.Comments.Count - 1);
                                    });
                                }
                                break;
                            case PushMessage.ActionSignal.LIKE:
                                foreach (var pl in item.Payload)
                                {
                                    var like = CommonActionSignalLike.Parser.ParseFrom(pl);
                                    Trace.WriteLine($"{like.SendTimeMs} - {like.UserInfo.Nickname}({like.UserInfo.UserId}) liked");
                                }
                                break;
                            case PushMessage.ActionSignal.ENTER_ROOM:
                                foreach (var pl in item.Payload)
                                {
                                    var enter = CommonActionSignalUserEnterRoom.Parser.ParseFrom(pl);
                                    Trace.WriteLine($"{enter.SendTimeMs} - {enter.UserInfo.Nickname}({enter.UserInfo.UserId}) entered");
                                }
                                break;
                            case PushMessage.ActionSignal.FOLLOW:
                                foreach (var pl in item.Payload)
                                {
                                    var follower = CommonActionSignalUserFollowAuthor.Parser.ParseFrom(pl);
                                    Trace.WriteLine($"{follower.SendTimeMs} - {follower.UserInfo.Nickname}({follower.UserInfo.UserId}) followed");
                                }
                                break;
                            case PushMessage.ActionSignal.KICKED_OUT:
                            case PushMessage.ActionSignal.VIOLATION_ALERT:
                                break;
                            case PushMessage.ActionSignal.THROW_BANANA:
                                foreach (var pl in item.Payload)
                                {
                                    var acer = AcfunActionSignalThrowBanana.Parser.ParseFrom(pl);
                                    Trace.WriteLine($"{acer.SendTimeMs} - {acer.Visitor.Name}({acer.Visitor.UserId})  throwed {acer.Count} banana(s)");
                                }
                                break;
                            case PushMessage.ActionSignal.GIFT:
                                foreach (var pl in item.Payload)
                                {
                                    /*
                                     * Item Id
                                     * 1 - 香蕉
                                     * 2 - 吃瓜
                                     * 3 - 
                                     * 4 - 牛啤
                                     * 5 - 手柄
                                     * 6 - 魔法棒
                                     * 7 - 好人卡
                                     * 8 - 星蕉雨
                                     * 9 - 告白
                                     * 10 - 666
                                     * 11 - 菜鸡
                                     * 12 - 打Call
                                     * 13 - 立FLAG
                                     * 14 - 窜天猴
                                     * 15 - AC机娘
                                     * 16 - 猴岛
                                     * 17 - 快乐水
                                     * 18 - 
                                     * 19 - 
                                     * 20 - 
                                     * 21 - 生日快乐
                                     * 22 - 六一快乐
                                     */
                                    var gift = CommonActionSignalGift.Parser.ParseFrom(pl);
                                    var giftName = Client.Gifts[gift.ItemId];
                                    Trace.WriteLine($"{gift.SendTimeMs} - {gift.User.Name}({gift.User.UserId}) sent gift {giftName} × {gift.Count}, Combo: {gift.Combo}, value: {gift.Value}");
#if DEBUG
                                    if (string.IsNullOrEmpty(giftName))
                                    {
                                        Trace.WriteLine($"ItemId: {gift.ItemId}, Value: {gift.Value}");
                                    }
#endif
                                }
                                break;
                            default:
                                foreach (var p in item.Payload)
                                {
                                    var pi = Client.Parse(item.SingalType, p);
#if DEBUG
                                    Trace.WriteLine($"Unhandled action type: {item.SingalType}, content: {pi}");
#endif
                                }
                                break;
                        }
                    }
                    break;
                // Includes current banana counts, watching count, like count and top 3 users sent gifts
                case PushMessage.STATE_SIGNAL:
                    ZtLiveScStateSignal signal = ZtLiveScStateSignal.Parser.ParseFrom(payload);

                    foreach (var item in signal.Item)
                    {
                        switch (item.SingalType)
                        {
                            case PushMessage.StateSignal.ACFUN_DISPLAY_INFO:
                                var acInfo = AcfunStateSignalDisplayInfo.Parser.ParseFrom(item.Payload);
                                //Trace.WriteLine("Current banada count: {0}", acInfo.BananaCount);
                                break;
                            case PushMessage.StateSignal.DISPLAY_INFO:
                                var stateInfo = CommonStateSignalDisplayInfo.Parser.ParseFrom(item.Payload);
                                //Trace.WriteLine("{0} watching, {1} likes", stateInfo.WatchingCount, stateInfo.LikeCount);
                                lottery.CurrentStatus = $"观众数: {stateInfo.WatchingCount} | 点赞数: {stateInfo.LikeCount}";
                                break;
                            case PushMessage.StateSignal.TOP_USRES:
                                var users = CommonStateSignalTopUsers.Parser.ParseFrom(item.Payload);
                                //Trace.WriteLine("Top 3 users: {0}", string.Join(", ", users.User.Select(user => user.Detail.Name)));
                                break;
                            case PushMessage.StateSignal.RECENT_COMMENT:
                                var comments = CommonStateSignalRecentComment.Parser.ParseFrom(item.Payload);
                                foreach (var comment in comments.Comment)
                                {
                                    Trace.WriteLine($"{comment.SendTimeMs} - {comment.UserInfo.Nickname}({comment.UserInfo.UserId}): {comment.Content}");
                                    Dispatcher.Invoke(() =>
                                    {
                                        lottery.AddComment(comment);
                                        CommentList.Dispatcher.Invoke(() => CommentList.ScrollIntoView(lottery.Comments.LastOrDefault()));
                                    });
                                }
                                break;
                            default:
                                var pi = Client.Parse(item.SingalType, item.Payload);
#if DEBUG
                                Trace.WriteLine($"Unhandled state type: {item.SingalType}, content: {pi}");
#endif
                                break;
                        }
                    }
                    break;
            }
        }
    }
}
