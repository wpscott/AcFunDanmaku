using AcFunCommentControl.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Security.Cryptography;

namespace AcFunMomentLottery.Models
{
    class Lottery : INotifyPropertyChanged
    {
        private string _commentStatus;
        public string CommentStatus { get { return _commentStatus; } set { _commentStatus = value; OnPropertyChanged(nameof(CommentStatus)); } }
        public long MomentId { get; set; } = -1;

        private bool _canFetch = true;
        public bool CanFetch { get { return _canFetch; } set { _canFetch = value; OnPropertyChanged(nameof(CanFetch)); } }

        private string _pattern;
        public string Pattern { get { return _pattern; } set { _pattern = value.Trim(); OnPropertyChanged(nameof(Pattern)); OnPropertyChanged(nameof(CanFilter)); } }
        public bool CanFilter => !string.IsNullOrEmpty(_pattern);

        private bool _filtered = false;
        public bool Filtered { get { return _filtered; } set { _filtered = value; OnPropertyChanged(nameof(FilterBtnContent)); } }
        public string FilterBtnContent => _filtered ? "还原" : "筛选";

        public string FilterResult => _filtered ? $"已找到{Comments.Count}条评论" : string.Empty;

        private readonly ObservableCollection<Comment> _comments = new ObservableCollection<Comment>();
        private ObservableCollection<Comment> _filteredComment;
        public ReadOnlyObservableCollection<Comment> Comments => new ReadOnlyObservableCollection<Comment>(_filtered ? _filteredComment : _comments);

        public bool Ready => Comments.Count > 0 && Amount < Comments.Count;

        private int _amount = 1;
        public int Amount { get { return _amount; } set { _amount = value; OnPropertyChanged(nameof(Amount)); OnPropertyChanged(nameof(Ready)); } }

        private readonly ObservableCollection<Comment> _result = new ObservableCollection<Comment>();
        public ReadOnlyObservableCollection<Comment> Result => new ReadOnlyObservableCollection<Comment>(_result);


        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public async void Fetch()
        {
            Filtered = false;
            Pattern = string.Empty;
            CommentStatus = "获取中，请稍等";
            CanFetch = false;

            var comments = await CommentModel.FetchMoment(MomentId, total => CommentStatus = $"获取中，已获取{total}条评论");

            _comments.Clear();
            foreach (var comment in comments)
            {
                _comments.Add(comment);
            }
            CanFetch = true;
            CommentStatus = $"已获取{_comments.Count}条评论";

            OnPropertyChanged(nameof(Comments));
            OnPropertyChanged(nameof(FilterResult));
            OnPropertyChanged(nameof(Ready));
        }

        public void Filter()
        {
            Filtered = true;

            _filteredComment = new ObservableCollection<Comment>(_comments.Where(comment => comment.content.Contains(_pattern, StringComparison.OrdinalIgnoreCase)));

            OnPropertyChanged(nameof(Comments));
            OnPropertyChanged(nameof(FilterResult));
            OnPropertyChanged(nameof(Ready));
        }

        public void Rest()
        {
            Filtered = false;
            Pattern = string.Empty;

            _filteredComment.Clear();

            OnPropertyChanged(nameof(Comments));
            OnPropertyChanged(nameof(FilterResult));
            OnPropertyChanged(nameof(Ready));
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

            using var writer = new StreamWriter(@$".\{MomentId}-{DateTime.Now:yyyy-MM-dd HH_mm_ss}.txt");
            writer.Write(string.Join("\r\n\r\n", _result.Select(comment => $"{comment.Header}\r\n{comment.content}")));
            writer.Flush();
            writer.Close();
        }

    }
}
