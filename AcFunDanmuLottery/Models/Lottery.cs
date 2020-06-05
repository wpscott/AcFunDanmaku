using AcFunDanmu;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace AcFunDanmuLottery.Models
{
    class Lottery : INotifyPropertyChanged
    {
        public string UserId { get; set; }

        private string _currentStatus;
        public string CurrentStatus { get { return _currentStatus; } set { _currentStatus = value; OnPropertyChanged(nameof(CurrentStatus)); } }

        private bool _connected;
        public bool Connected { get { return _connected; } set { _connected = value; OnPropertyChanged(nameof(Connected)); OnPropertyChanged(nameof(ConnectBtnContent)); } }

        public string ConnectBtnContent => _connected ? "断开" : "连接";

        public string Pattern { get; set; }

        public string SearchBtnContent => SearchStart ? "结束" : "开始";

        public bool SearchStart { get; private set; }

        public string SearchStatus => Comments.Count == 0 ? string.Empty : $"已找到{Comments.Count}条弹幕";

        public bool ShowAll { get; set; } = false;

        private readonly ObservableCollection<CommonActionSignalComment> _comments = new ObservableCollection<CommonActionSignalComment>();
        private readonly ObservableCollection<CommonActionSignalComment> _pool = new ObservableCollection<CommonActionSignalComment>();
        public ReadOnlyObservableCollection<CommonActionSignalComment> Comments => new ReadOnlyObservableCollection<CommonActionSignalComment>(ShowAll ? _comments : _pool);

        public bool Ready => !SearchStart && _comments.Count > 0;
        public string Amount { get; set; }

        private readonly ObservableCollection<CommonActionSignalComment> _result = new ObservableCollection<CommonActionSignalComment>();
        public ReadOnlyObservableCollection<CommonActionSignalComment> Result => new ReadOnlyObservableCollection<CommonActionSignalComment>(_result);


        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public event PropertyChangedEventHandler PropertyChanged;

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
            _comments.Add(comment);
            if (SearchStart && comment.Content.Contains(Pattern, StringComparison.OrdinalIgnoreCase) && !_pool.Any(c => c.UserInfo.UserId == comment.UserInfo.UserId))
            {
                _pool.Add(comment);
                OnPropertyChanged(nameof(SearchStatus));
            }
            OnPropertyChanged(nameof(Comments));
        }

        public void Roll(int amount)
        {
            _result.Clear();
            var rnd = new Random();
            HashSet<int> indexes = new HashSet<int>(amount);
            while (indexes.Count < amount)
            {
                indexes.Add(rnd.Next(_pool.Count));
            }
            foreach (var index in indexes)
            {
                _result.Add(_pool[index]);
            }
            OnPropertyChanged(nameof(Result));
        }

    }
}
