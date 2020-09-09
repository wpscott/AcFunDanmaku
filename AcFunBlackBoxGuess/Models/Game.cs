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
using System.Text;
using System.Text.RegularExpressions;
using System.Timers;
using System.Web;

namespace AcFunBlackBoxGuess.Models
{
    class Game : INotifyPropertyChanged
    {
        private static readonly Regex Pattern = new Regex(@"^[\[【]+(.*?)[\]】]+$", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        private const int MaxGuess = 100;
        private const int MaxTry = 2;

        private long _userId = -1;
        public long UserId { get { return _userId; } set { _userId = value; CanConnect = UserId > 0; } }

        private bool _canConnect = false;
        public bool CanConnect { get { return _canConnect; } set { _canConnect = value; OnPropertyChanged(nameof(CanConnect)); } }

        public bool Connected { get; private set; } = false;
        public string ConnectBtnContent => Connected ? "断开" : "连接";


        private string _danmuStatus;
        public string DanmuStatus { get { return _danmuStatus; } set { _danmuStatus = value; OnPropertyChanged(nameof(DanmuStatus)); } }

        private string _answer;
        public string Answer { get { return _answer; } set { _answer = value.Trim(); OnPropertyChanged(nameof(Answer)); OnPropertyChanged(nameof(CanStart)); OnPropertyChanged(nameof(GuessResult)); } }

        private bool _showAnswer = true;
        public bool ShowAnswer { get { return _showAnswer; } set { _showAnswer = value; OnPropertyChanged(nameof(ShowAnswer)); } }

        public bool CanStart => Connected && !string.IsNullOrEmpty(_answer) && !GameStart;

        public bool GameStart { get; private set; } = false;
        public string StartBtnContent => GameStart ? "结束" : "开始";

        public string GuessResult => GameStart ? $"已回答{_result.Where(danmu => !danmu.Failed).Count()}/{MaxGuess}条弹幕" : string.Empty;

        private ObservableCollection<Danmu> _pendingDanmu = new ObservableCollection<Danmu>();
        public ReadOnlyObservableCollection<Danmu> PendingDanmu => new ReadOnlyObservableCollection<Danmu>(_pendingDanmu);

        private ObservableCollection<Danmu> _result = new ObservableCollection<Danmu>();
        public ReadOnlyObservableCollection<Danmu> Result => new ReadOnlyObservableCollection<Danmu>(_result);

        private Dictionary<long, byte> _bingo = new Dictionary<long, byte>();


        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private Client client;

        public async void Connect()
        {
            if (UserId > 0)
            {
                DanmuStatus = "连接中";
                CanConnect = false;

                client = new Client();
                client.Handler = HandleSignal;

                await client.Initialize(UserId.ToString());

                var retry = 0;
                var resetTimer = new Timer(5000);
                resetTimer.Elapsed += (s, e) => retry = 0;

                DanmuStatus = "已连接";
                _pendingDanmu.Clear();
                Connected = true;
                OnPropertyChanged(nameof(ConnectBtnContent));
                OnPropertyChanged(nameof(CanStart));

                CanConnect = true;
                while (Connected && !await client.Start() && retry < 5)
                {
                    DanmuStatus = "断线重连中";
                    if (retry > 0) { resetTimer.Stop(); }
                    retry++;
                    resetTimer.Start();
                }
                if (retry > 0)
                {
                    DanmuStatus = "连接已断开";
                }
                else
                {
                    DanmuStatus = "直播已结束";
                }
                Connected = false;

                OnPropertyChanged(nameof(ConnectBtnContent));
                OnPropertyChanged(nameof(CanStart));
            }
        }

        public void Start()
        {
            _pendingDanmu.Clear();
            _result.Clear();
            _bingo.Clear();

            GameStart = true;
            ShowAnswer = false;
            CanConnect = false;

            OnPropertyChanged(nameof(CanStart));
            OnPropertyChanged(nameof(StartBtnContent));
            OnPropertyChanged(nameof(ConnectBtnContent));

            //SaveHash();
        }

        public void Stop()
        {
            GameStart = false;
            ShowAnswer = true;
            CanConnect = true;

            OnPropertyChanged(nameof(CanStart));
            OnPropertyChanged(nameof(StartBtnContent));
            OnPropertyChanged(nameof(ConnectBtnContent));
        }

        public void SetYes(Danmu danmu)
        {
            if (GameStart)
            {
                _pendingDanmu.Remove(danmu);
                danmu.True = true;
                danmu.IsBingo = false;
                _result.Add(danmu);

                OnPropertyChanged(nameof(PendingDanmu));
                OnPropertyChanged(nameof(Result));
                OnPropertyChanged(nameof(GuessResult));
            }
        }

        public void SetNo(Danmu danmu)
        {
            if (GameStart)
            {
                _pendingDanmu.Remove(danmu);
                danmu.True = false;
                danmu.IsBingo = false;
                _result.Add(danmu);

                OnPropertyChanged(nameof(PendingDanmu));
                OnPropertyChanged(nameof(Result));
                OnPropertyChanged(nameof(GuessResult));
            }
        }

        public void Bingo(Danmu danmu)
        {
            if (GameStart)
            {
                _pendingDanmu.Remove(danmu);
                if (_bingo.ContainsKey(danmu.UserId))
                {
                    if (++_bingo[danmu.UserId] > MaxTry) { danmu.Failed = true; }
                }
                else
                {
                    _bingo[danmu.UserId] = 1;
                }
                danmu.Correct = danmu.Content.Contains(Answer, StringComparison.OrdinalIgnoreCase);
                danmu.IsBingo = true;
                _result.Add(danmu);

                if (_result.Count > MaxGuess || danmu.Correct)
                {
                    Stop();
                }

                OnPropertyChanged(nameof(PendingDanmu));
                OnPropertyChanged(nameof(Result));
                OnPropertyChanged(nameof(GuessResult));
            }
        }

        public void Remove(Danmu danmu)
        {
            if (GameStart)
            {
                _pendingDanmu.Remove(danmu);
                OnPropertyChanged(nameof(PendingDanmu));
            }
        }

        public async void Disconnect()
        {
            Connected = false;
            OnPropertyChanged(nameof(ConnectBtnContent));
            await client.Stop("Disconnect");
        }

        public void AddDanmu(CommonActionSignalComment comment)
        {
            var match = Pattern.Match(HttpUtility.HtmlDecode(comment.Content));
            if(GameStart && match.Success)
            {
                _pendingDanmu.Add(new Danmu { Timestamp = comment.SendTimeMs, UserId = comment.UserInfo.UserId, Nickname = comment.UserInfo.Nickname, Content = match.Groups[1].Value });
                OnPropertyChanged(nameof(PendingDanmu));
            }
        }

        private void HandleSignal(string messagetType, ByteString payload)
        {
            if (messagetType == PushMessage.ACTION_SIGNAL)
            {
                var actionSignal = ZtLiveScActionSignal.Parser.ParseFrom(payload);
                foreach (var danmu in actionSignal.Item
                    .Where(item => item.SignalType == PushMessage.ActionSignal.COMMENT)
                    .Select(item =>
                        item.Payload.Select(CommonActionSignalComment.Parser.ParseFrom)
                    ).SelectMany(danmu => danmu)
                    )
                {
                    AddDanmu(danmu);
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
                    DanmuStatus = $"观众数: {displayInfo.WatchingCount} | 点赞数: {displayInfo.LikeCount}";
                }
            }
        }

        private const string Salt = "ACACACFunACAC娘";
        private static byte[] Hash(string text)
        {
            using var hash = SHA256.Create();
            return hash.ComputeHash(hash.ComputeHash(Encoding.UTF8.GetBytes(text)));
        }

        private void SaveHash()
        {
            var now = DateTimeOffset.Now;
            var timestamp = now.ToUnixTimeMilliseconds();
            using var writer = new StreamWriter(@$".\{UserId}-{now:yyyy-MM-dd HH_mm_ss}.txt");
            var toHash = $"{timestamp}{UserId}{Answer}{Salt}";
            writer.Write($"本次游戏加密结果\r\n时间戳:\t{timestamp}\r\n主播ID:\t{UserId}\r\n答案:\t{Answer}\r\nHash:\t{BitConverter.ToString(Hash(toHash)).Replace("-",string.Empty)}");
            writer.Flush();
            writer.Close();
        }
    }
}
