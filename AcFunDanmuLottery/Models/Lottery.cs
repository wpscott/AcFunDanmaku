using AcFunDanmu;
using AcFunDanmu.Enums;
using Google.Protobuf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Timers;
using System.Web;

namespace AcFunDanmuLottery.Models
{
    class Lottery : INotifyPropertyChanged
    {
        public long UserId { get; set; } = -1;

        private string _currentStatus;
        public string CurrentStatus { get { return _currentStatus; } set { _currentStatus = value; OnPropertyChanged(nameof(CurrentStatus)); } }

        private bool _canConnect = true;
        public bool CanConnect { get { return _canConnect; } set { _canConnect = value; OnPropertyChanged(nameof(CanConnect)); } }

        public bool Connected { get; private set; } = false;

        public string ConnectBtnContent => Connected ? "断开" : "连接";

        private string _pattern;
        public string Pattern { get { return _pattern; } set { _pattern = value.Trim(); OnPropertyChanged(nameof(Pattern)); OnPropertyChanged(nameof(CanStart)); } }

        public bool CanStart => Connected && !string.IsNullOrEmpty(_pattern);

        public string SearchBtnContent => SearchStart ? "结束" : "开始";

        public bool SearchStart { get; private set; }

        public string SearchStatus => Comments.Count == 0 ? string.Empty : $"已找到{Comments.Count}条弹幕";

        public bool ShowAll { get; set; } = false;

        private readonly ObservableCollection<Comment> _comments = new ObservableCollection<Comment>();
        private readonly ObservableCollection<Comment> _pool = new ObservableCollection<Comment>();
        public ReadOnlyObservableCollection<Comment> Comments => new ReadOnlyObservableCollection<Comment>(ShowAll ? _comments : _pool);

        public bool Ready => !SearchStart && Comments.Count > 0 && Amount < Comments.Count;

        private int _amount = 1;
        public int Amount { get { return _amount; } set { _amount = value; OnPropertyChanged(nameof(Amount)); OnPropertyChanged(nameof(Ready)); } }

        private readonly ObservableCollection<Comment> _result = new ObservableCollection<Comment>();
        public ReadOnlyObservableCollection<Comment> Result => new ReadOnlyObservableCollection<Comment>(_result);

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public async void Connect()
        {
            if (UserId > 0)
            {
                CurrentStatus = "连接中";
                CanConnect = false;

                client = new Client();
                client.Handler = HandleSignal;

                await client.Initialize(UserId.ToString());

                var retry = 0;
                var resetTimer = new Timer(5000);
                resetTimer.Elapsed += (s, e) => retry = 0;

                CurrentStatus = "已连接";
                _comments.Clear();
                Connected = true;
                OnPropertyChanged(nameof(ConnectBtnContent));

                CanConnect = true;
                while (Connected && !await client.Start() && retry < 5)
                {
                    CurrentStatus = "断线重连中";
                    if (retry > 0) { resetTimer.Stop(); }
                    retry++;
                    resetTimer.Start();
                }
                if (retry > 0)
                {
                    CurrentStatus = "连接已断开";
                }
                else
                {
                    CurrentStatus = "直播已结束";
                }
                Connected = false;

                OnPropertyChanged(nameof(ConnectBtnContent));
            }
        }

        public async void Stop()
        {
            Connected = false;
            OnPropertyChanged(nameof(ConnectBtnContent));
            await client.Stop("Disconnect");
            StopSearch();
        }

        public void StartSearch()
        {
            _pool.Clear();
            SearchStart = true;
            ShowAll = false;

            OnPropertyChanged(nameof(SearchStart));
            OnPropertyChanged(nameof(Ready));
            OnPropertyChanged(nameof(SearchBtnContent));
            OnPropertyChanged(nameof(Comments));
            OnPropertyChanged(nameof(SearchStatus));
        }

        public void StopSearch()
        {
            SearchStart = false;

            OnPropertyChanged(nameof(SearchStart));
            OnPropertyChanged(nameof(Ready));
            OnPropertyChanged(nameof(SearchBtnContent));
        }

        public void AddComment(CommonActionSignalComment comment)
        {
            var c = new Comment { Timestamp = comment.SendTimeMs, UserId = comment.UserInfo.UserId, Nickname = comment.UserInfo.Nickname, Content = HttpUtility.HtmlDecode(comment.Content) };
#if DEBUG
            _comments.Add(c);
#endif
            if (SearchStart && comment.Content.Contains(Pattern, StringComparison.OrdinalIgnoreCase) && !_pool.Any(c => c.UserId == comment.UserInfo.UserId))
            {
                _pool.Add(c);
                OnPropertyChanged(nameof(SearchStatus));
            }
            OnPropertyChanged(nameof(Comments));
        }

        public void Roll()
        {
            _result.Clear();

            using var provider = new RNGCryptoServiceProvider();
            //var rnd = new Random();
            HashSet<int> indexes = new HashSet<int>(Amount);
            while (indexes.Count < Amount)
            {
                var bytes = new byte[4];
                provider.GetBytes(bytes);
                var randInt = BitConverter.ToUInt32(bytes);
                indexes.Add((int)(randInt % (uint)Comments.Count));

                //indexes.Add(rnd.Next(Comments.Count));
            }
            foreach (var index in indexes)
            {
                _result.Add(Comments[index]);
            }
            OnPropertyChanged(nameof(Result));
            using var writer = new StreamWriter(@$".\{UserId}-{DateTime.Now:yyyy-MM-dd HH_mm_ss}.txt");
            writer.Write(string.Join("\r\n\r\n", _result.Select(comment => $"{comment.Nickname}({comment.UserId})\r\n{comment.Content}")));
            writer.Flush();
            writer.Close();
        }

        private Client client;

        private void HandleSignal(string messagetType, ByteString payload)
        {
            if (messagetType == PushMessage.ACTION_SIGNAL)
            {
                var actionSignal = ZtLiveScActionSignal.Parser.ParseFrom(payload);
                foreach (var comment in actionSignal.Item
                    .Where(item => item.SignalType == PushMessage.ActionSignal.COMMENT)
                    .Select(item =>
                        item.Payload.Select(CommonActionSignalComment.Parser.ParseFrom)
                    ).SelectMany(comment => comment)
                    )
                {
                    AddComment(comment);
                }
            }
            else if (messagetType == PushMessage.STATE_SIGNAL)
            {
                var stateSignal = ZtLiveScStateSignal.Parser.ParseFrom(payload);
                foreach (var displayInfo in stateSignal.Item
                    .Where(item => item.SignalType == PushMessage.StateSignal.DISPLAY_INFO)
                    .Select(item => CommonStateSignalDisplayInfo.Parser.ParseFrom(item.Payload))
                    )
                {
                    CurrentStatus = $"观众数: {displayInfo.WatchingCount} | 点赞数: {displayInfo.LikeCount}";
                }
            }
            //            switch (messagetType)
            //            {
            //                // Includes comment, gift, enter room, like, follower
            //                case PushMessage.ACTION_SIGNAL:
            //                    var actionSignal = ZtLiveScActionSignal.Parser.ParseFrom(payload);

            //                    foreach (var item in actionSignal.Item)
            //                    {
            //                        switch (item.SignalType)
            //                        {
            //                            case PushMessage.ActionSignal.COMMENT:
            //                                foreach (var pl in item.Payload)
            //                                {
            //                                    var comment = CommonActionSignalComment.Parser.ParseFrom(pl);
            //                                    Trace.WriteLine($"{comment.SendTimeMs} - {comment.UserInfo.Nickname}({comment.UserInfo.UserId}): {comment.Content}");
            //                                    AddComment(comment);
            //                                }
            //                                break;
            //                            case PushMessage.ActionSignal.LIKE:
            //                                foreach (var pl in item.Payload)
            //                                {
            //                                    var like = CommonActionSignalLike.Parser.ParseFrom(pl);
            //                                    Trace.WriteLine($"{like.SendTimeMs} - {like.UserInfo.Nickname}({like.UserInfo.UserId}) liked");
            //                                }
            //                                break;
            //                            case PushMessage.ActionSignal.ENTER_ROOM:
            //                                foreach (var pl in item.Payload)
            //                                {
            //                                    var enter = CommonActionSignalUserEnterRoom.Parser.ParseFrom(pl);
            //                                    Trace.WriteLine($"{enter.SendTimeMs} - {enter.UserInfo.Nickname}({enter.UserInfo.UserId}) entered");
            //                                }
            //                                break;
            //                            case PushMessage.ActionSignal.FOLLOW:
            //                                foreach (var pl in item.Payload)
            //                                {
            //                                    var follower = CommonActionSignalUserFollowAuthor.Parser.ParseFrom(pl);
            //                                    Trace.WriteLine($"{follower.SendTimeMs} - {follower.UserInfo.Nickname}({follower.UserInfo.UserId}) followed");
            //                                }
            //                                break;
            //                            case PushMessage.ActionSignal.KICKED_OUT:
            //                            case PushMessage.ActionSignal.VIOLATION_ALERT:
            //                                break;
            //                            case PushMessage.ActionSignal.THROW_BANANA:
            //                                foreach (var pl in item.Payload)
            //                                {
            //                                    var acer = AcfunActionSignalThrowBanana.Parser.ParseFrom(pl);
            //                                    Trace.WriteLine($"{acer.SendTimeMs} - {acer.Visitor.Name}({acer.Visitor.UserId})  throwed {acer.Count} banana(s)");
            //                                }
            //                                break;
            //                            case PushMessage.ActionSignal.GIFT:
            //                                foreach (var pl in item.Payload)
            //                                {
            //                                    /*
            //                                     * Item Id
            //                                     * 1 - 香蕉
            //                                     * 2 - 吃瓜
            //                                     * 3 - 
            //                                     * 4 - 牛啤
            //                                     * 5 - 手柄
            //                                     * 6 - 魔法棒
            //                                     * 7 - 好人卡
            //                                     * 8 - 星蕉雨
            //                                     * 9 - 告白
            //                                     * 10 - 666
            //                                     * 11 - 菜鸡
            //                                     * 12 - 打Call
            //                                     * 13 - 立FLAG
            //                                     * 14 - 窜天猴
            //                                     * 15 - AC机娘
            //                                     * 16 - 猴岛
            //                                     * 17 - 快乐水
            //                                     * 18 - 
            //                                     * 19 - 
            //                                     * 20 - 
            //                                     * 21 - 生日快乐
            //                                     * 22 - 六一快乐
            //                                     */
            //                                    var gift = CommonActionSignalGift.Parser.ParseFrom(pl);
            //                                    var giftName = Client.Gifts[gift.ItemId];
            //                                    Trace.WriteLine($"{gift.SendTimeMs} - {gift.User.Name}({gift.User.UserId}) sent gift {giftName} × {gift.Count}, Combo: {gift.Combo}, value: {gift.Value}");
            //#if DEBUG
            //                                    if (string.IsNullOrEmpty(giftName))
            //                                    {
            //                                        Trace.WriteLine($"ItemId: {gift.ItemId}, Value: {gift.Value}");
            //                                    }
            //#endif
            //                                }
            //                                break;
            //                            default:
            //                                foreach (var p in item.Payload)
            //                                {
            //                                    var pi = Client.Parse(item.SignalType, p);
            //#if DEBUG
            //                                    Trace.WriteLine($"Unhandled action type: {item.SignalType}, content: {pi}");
            //#endif
            //                                }
            //                                break;
            //                        }
            //                    }
            //                    break;
            //                // Includes current banana counts, watching count, like count and top 3 users sent gifts
            //                case PushMessage.STATE_SIGNAL:
            //                    ZtLiveScStateSignal signal = ZtLiveScStateSignal.Parser.ParseFrom(payload);

            //                    foreach (var item in signal.Item)
            //                    {
            //                        switch (item.SignalType)
            //                        {
            //                            case PushMessage.StateSignal.ACFUN_DISPLAY_INFO:
            //                                var acInfo = AcfunStateSignalDisplayInfo.Parser.ParseFrom(item.Payload);
            //                                //Trace.WriteLine("Current banada count: {0}", acInfo.BananaCount);
            //                                break;
            //                            case PushMessage.StateSignal.DISPLAY_INFO:
            //                                var stateInfo = CommonStateSignalDisplayInfo.Parser.ParseFrom(item.Payload);
            //                                //Trace.WriteLine("{0} watching, {1} likes", stateInfo.WatchingCount, stateInfo.LikeCount);
            //                                CurrentStatus = $"观众数: {stateInfo.WatchingCount} | 点赞数: {stateInfo.LikeCount}";
            //                                break;
            //                            case PushMessage.StateSignal.TOP_USRES:
            //                                var users = CommonStateSignalTopUsers.Parser.ParseFrom(item.Payload);
            //                                //Trace.WriteLine("Top 3 users: {0}", string.Join(", ", users.User.Select(user => user.Detail.Name)));
            //                                break;
            //                            case PushMessage.StateSignal.RECENT_COMMENT:
            //                                var comments = CommonStateSignalRecentComment.Parser.ParseFrom(item.Payload);
            //                                foreach (var comment in comments.Comment)
            //                                {
            //                                    Trace.WriteLine($"{comment.SendTimeMs} - {comment.UserInfo.Nickname}({comment.UserInfo.UserId}): {comment.Content}");
            //                                    AddComment(comment);
            //                                }
            //                                break;
            //                            default:
            //                                var pi = Client.Parse(item.SignalType, item.Payload);
            //#if DEBUG
            //                                Trace.WriteLine($"Unhandled state type: {item.SignalType}, content: {pi}");
            //#endif
            //                                break;
            //                        }
            //                    }
            //                    break;
            //            }
        }
    }
}
