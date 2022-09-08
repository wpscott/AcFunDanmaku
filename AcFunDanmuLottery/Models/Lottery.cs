using AcFunDanmu;
using AcFunDanmu.Enums;
using Google.Protobuf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

        public string CurrentStatus
        {
            get => _currentStatus;
            set
            {
                _currentStatus = value;
                OnPropertyChanged(nameof(CurrentStatus));
            }
        }

        private bool _canConnect = true;

        public bool CanConnect
        {
            get => _canConnect;
            set
            {
                _canConnect = value;
                OnPropertyChanged(nameof(CanConnect));
            }
        }

        public bool Connected { get; private set; }

        public string ConnectBtnContent => Connected ? "断开" : "连接";

        private string _pattern;

        public string Pattern
        {
            get => _pattern;
            set
            {
                _pattern = value.Trim();
                OnPropertyChanged(nameof(Pattern));
                OnPropertyChanged(nameof(CanStart));
            }
        }

        public bool CanStart => Connected && !string.IsNullOrEmpty(_pattern);

        public string SearchBtnContent => SearchStart ? "结束" : "开始";

        public bool SearchStart { get; private set; }

        public string SearchStatus => Comments.Count == 0 ? string.Empty : $"已找到{Comments.Count}条弹幕";

        public bool ShowAll { get; set; }

        private readonly ObservableCollection<Comment> _comments = new();
        private readonly ObservableCollection<Comment> _pool = new();
        public ReadOnlyObservableCollection<Comment> Comments => new(ShowAll ? _comments : _pool);

        public bool Ready => !SearchStart && Comments.Count > 0 && Amount < Comments.Count;

        private int _amount = 1;

        public int Amount
        {
            get => _amount;
            set
            {
                _amount = value;
                OnPropertyChanged(nameof(Amount));
                OnPropertyChanged(nameof(Ready));
            }
        }

        private readonly ObservableCollection<Comment> _result = new();
        public ReadOnlyObservableCollection<Comment> Result => new(_result);

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void Connect()
        {
            if (UserId <= 0) return;
            CurrentStatus = "连接中";
            CanConnect = false;

            var retry = 0;
            var resetTimer = new Timer(5000);
            resetTimer.Elapsed += (s, e) => retry = 0;

            if (_client == null)
            {
                _client = new Client();
                _client.Handler += HandleSignal;
                _client.OnInitialize += () =>
                {
                    CurrentStatus = "已连接";
                    _comments.Clear();
                    Connected = true;
                    OnPropertyChanged(nameof(ConnectBtnContent));

                    CanConnect = true;
                };
                _client.OnEnd += () =>
                {
                    if (!Connected || retry >= 5)
                    {
                        CurrentStatus = retry > 0 ? "连接已断开" : "直播已结束";
                        Connected = false;

                        OnPropertyChanged(nameof(ConnectBtnContent));
                        return;
                    }

                    CurrentStatus = "断线重连中";
                    if (retry > 0)
                    {
                        resetTimer.Stop();
                    }

                    retry++;
                    resetTimer.Start();

                    _client.Start(UserId);
                };
            }
            else if (_client.IsRunning)
            {
                _client.Stop("stop");
            }

            _client.Start(UserId);
        }

        public void Stop()
        {
            Connected = false;
            OnPropertyChanged(nameof(ConnectBtnContent));
            _client.Stop("Disconnect");
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
            var c = new Comment
            {
                Timestamp = comment.SendTimeMs, UserId = comment.UserInfo.UserId, Nickname = comment.UserInfo.Nickname,
                Content = HttpUtility.HtmlDecode(comment.Content)
            };
#if DEBUG
            _comments.Add(c);
#endif
            if (comment.Content != null && SearchStart &&
                comment.Content.Contains(Pattern, StringComparison.OrdinalIgnoreCase) &&
                !_pool.Any(c => c.UserId == comment.UserInfo.UserId))
            {
                _pool.Add(c);
                OnPropertyChanged(nameof(SearchStatus));
            }

            OnPropertyChanged(nameof(Comments));
        }

        public void Roll()
        {
            _result.Clear();

            HashSet<int> indexes = new(Amount);
            while (indexes.Count < Amount)
            {
                var randInt = BitConverter.ToUInt32(RandomNumberGenerator.GetBytes(4));
                indexes.Add((int)(randInt % (uint)Comments.Count));
            }

            foreach (var index in indexes)
            {
                _result.Add(Comments[index]);
            }

            OnPropertyChanged(nameof(Result));
            using var writer = new StreamWriter(@$".\{UserId}-{DateTime.Now:yyyy-MM-dd HH_mm_ss}.txt");
            writer.Write(string.Join("\r\n\r\n",
                _result.Select(comment => $"{comment.Nickname}({comment.UserId})\r\n{comment.Content}")));
            writer.Flush();
            writer.Close();
        }

        private Client _client;

        private void HandleSignal(Client sender, string messageType, ByteString payload)
        {
            switch (messageType)
            {
                case PushMessage.ACTION_SIGNAL:
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

                    break;
                }
                case PushMessage.STATE_SIGNAL:
                {
                    var stateSignal = ZtLiveScStateSignal.Parser.ParseFrom(payload);
                    foreach (var displayInfo in stateSignal.Item
                                 .Where(item => item.SignalType == PushMessage.StateSignal.DISPLAY_INFO)
                                 .Select(item => CommonStateSignalDisplayInfo.Parser.ParseFrom(item.Payload))
                            )
                    {
                        CurrentStatus = $"观众数: {displayInfo.WatchingCount} | 点赞数: {displayInfo.LikeCount}";
                    }

                    break;
                }
            }
        }
    }
}