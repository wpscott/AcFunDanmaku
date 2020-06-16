using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AcFunVideoDanmuLottery.Models
{
    class Lottery : INotifyPropertyChanged
    {
        private string _danmuStatus;
        public string DanmuStatus { get { return _danmuStatus; } set { _danmuStatus = value; OnPropertyChanged(nameof(DanmuStatus)); } }
        public long ACId { get; set; } = -1;

        private bool _canFetch = true;
        public bool CanFetch { get { return _canFetch; } set { _canFetch = value; OnPropertyChanged(nameof(CanFetch)); } }

        private string _pattern;
        public string Pattern { get { return _pattern; } set { _pattern = value.Trim(); OnPropertyChanged(nameof(Pattern)); OnPropertyChanged(nameof(CanFilter)); } }
        public bool CanFilter => !string.IsNullOrEmpty(_pattern);

        private bool _filtered = false;
        public bool Filtered { get { return _filtered; } set { _filtered = value; OnPropertyChanged(nameof(FilterBtnContent)); } }
        public string FilterBtnContent => _filtered ? "还原" : "筛选";

        public string FilterResult => _filtered ? $"已找到{Danmus.Count}条弹幕" : string.Empty;

        private readonly ObservableCollection<Danmu> _danmus = new ObservableCollection<Danmu>();
        private ObservableCollection<Danmu> _filteredDanmu;
        public ReadOnlyObservableCollection<Danmu> Danmus => new ReadOnlyObservableCollection<Danmu>(_filtered ? _filteredDanmu : _danmus);

        public bool Ready => Danmus.Count > 0 && Amount < Danmus.Count;

        private int _amount = 1;
        public int Amount { get { return _amount; } set { _amount = value; OnPropertyChanged(nameof(Amount)); OnPropertyChanged(nameof(Ready)); } }

        private readonly ObservableCollection<Danmu> _result = new ObservableCollection<Danmu>();
        public ReadOnlyObservableCollection<Danmu> Result => new ReadOnlyObservableCollection<Danmu>(_result);


        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public async void Fetch()
        {
            Filtered = false;
            Pattern = string.Empty;
            DanmuStatus = "获取中，请稍等";
            CanFetch = false;

            var danmus = await FetchDanmu();

            _danmus.Clear();
            foreach (var danmu in danmus)
            {
                _danmus.Add(danmu);
            }
            CanFetch = true;
            DanmuStatus = $"已获取{_danmus.Count}条弹幕";

            OnPropertyChanged(nameof(Danmus));
            OnPropertyChanged(nameof(FilterResult));
            OnPropertyChanged(nameof(Ready));
        }

        private static string GetRandomString()
        {
            var rnd = new Random();
            var chars = new byte[4];
            rnd.NextBytes(chars);
            return $"{rnd.Next(100000000, 999999999)}{BitConverter.ToString(chars).Replace("-", string.Empty).Substring(1)}";
        }

        private static readonly Regex VideoIdReg = new Regex("currentVideoId\":(\\d+)", RegexOptions.Compiled);
        private async Task<Danmu[]> FetchDanmu()
        {
            var container = new CookieContainer();

            container.Add(new Cookie { Domain = ".acfun.cn", Path = "/", Name = "_did", Value = $"web_{GetRandomString()}" });

            using var client = new HttpClient(new HttpClientHandler { UseCookies = true, AutomaticDecompression = DecompressionMethods.All, CookieContainer = container });
            client.DefaultRequestHeaders.AcceptEncoding.ParseAdd("gzip, deflate, br");

            using var videoResp = await client.GetAsync($"https://www.acfun.cn/v/ac{ACId}");
            var html = await videoResp.Content.ReadAsStringAsync();
            var match = VideoIdReg.Match(html, html.IndexOf("currentVideoId"), 32);
            var vid = match.Groups[1].Value;

            using var form = new FormUrlEncodedContent(new Dictionary<string, string> { { "videoId", vid }, { "lastFetchTime", "0" } });
            using var resp = await client.PostAsync("https://www.acfun.cn/rest/pc-direct/new-danmaku/poll", form);

            var text = await resp.Content.ReadAsStringAsync();
            var result = await JsonSerializer.DeserializeAsync<DanmuResponse>(await resp.Content.ReadAsStreamAsync());

            return result.added;
        }

        public void Filter()
        {
            Filtered = true;

            _filteredDanmu = new ObservableCollection<Danmu>(_danmus.Where(comment => comment.body.Contains(_pattern, StringComparison.OrdinalIgnoreCase)));

            OnPropertyChanged(nameof(Danmus));
            OnPropertyChanged(nameof(FilterResult));
            OnPropertyChanged(nameof(Ready));
        }

        public void Rest()
        {
            Filtered = false;
            Pattern = string.Empty;

            _filteredDanmu.Clear();

            OnPropertyChanged(nameof(Danmus));
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
                indexes.Add((int)(randInt % (uint)Danmus.Count));

                //indexes.Add(rnd.Next(Comments.Count));
            }
            foreach (var index in indexes)
            {
                _result.Add(Danmus[index]);
            }
            OnPropertyChanged(nameof(Result));

            using var writer = new StreamWriter(@$".\{ACId}-{DateTime.Now:yyyy-MM-dd HH_mm_ss}.txt");
            writer.Write(string.Join("\r\n\r\n", _result.Select(comment => $"{comment.Header}\r\n{comment.Content}")));
            writer.Flush();
            writer.Close();
        }
    }
}
