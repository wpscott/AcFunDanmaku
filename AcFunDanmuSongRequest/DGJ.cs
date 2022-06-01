using AcFunDanmu;
using AcFunDanmu.Enums;
using AcFunDanmuSongRequest.Platform;
using AcFunDanmuSongRequest.Platform.Interfaces;
using Google.Protobuf;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Timers;
using Google.Protobuf.WellKnownTypes;

namespace AcFunDanmuSongRequest
{
    public delegate void DGJConnectEvent();

    public delegate void DGJExitEvent();

    public delegate void DGJAddSongEvent(ISong song);

    public static class DGJ
    {
        static DGJ()
        {
            IsRunning = false;
        }

        private static Regex _pattern;
        private static Config _config;
        private static IPlatform _platform;

        public static bool IsRunning { get; private set; }
        public static DGJConnectEvent OnConnect { get; set; }
        public static DGJExitEvent OnExit { get; set; }
        public static DGJAddSongEvent OnAddSong { get; set; }

        public static async Task<bool> Initialize()
        {
            _config = await Config.LoadConfig();
            _pattern = new Regex(_config.Format, RegexOptions.IgnoreCase | RegexOptions.Compiled);
            _platform = BasePlatform.CreatePlatform(_config);
            if (_config.Standalone && _config.UserId > 0)
            {
                Connect();
            }
#if DEBUG
            IsRunning = true;
#endif
            return _platform == null;
        }

        private static async void Connect()
        {
            var client = new Client
            {
                Handler = HandleSignal
            };

            await client.Initialize(_config.UserId.ToString());

            var retry = 0;
            var resetTimer = new Timer(10000);
            resetTimer.Elapsed += (s, e) => retry = 0;

            IsRunning = true;
            OnConnect?.Invoke();
            while (!await client.Start() && retry < 3)
            {
                if (retry > 0)
                {
                    resetTimer.Stop();
                }

                retry++;
                resetTimer.Start();
            }

            IsRunning = false;
            OnExit?.Invoke();
        }

        public static async ValueTask AddSong(string keyword)
        {
            var song = await _platform.AddSong(keyword);
            if (song != null)
            {
                OnAddSong?.Invoke(song);
            }
        }

        public static ISong Peek()
        {
            return _platform.Peek();
        }

        public static async ValueTask<ISong> NextSong()
        {
            return await _platform.NextSong();
        }

        public static bool ShowLyrics => _config.ShowLyrics;
        public static async ValueTask<Lyrics> GetLyrics(ISong song) => await _platform.GetLyrics(song);

        private static async void HandleSignal(Client sender, string messageType, ByteString payload)
        {
            if (messageType != PushMessage.ACTION_SIGNAL) return;
            var actionSignal = ZtLiveScActionSignal.Parser.ParseFrom(payload);
            foreach (var match in actionSignal.Item
                         .Where(item => item.SignalType == PushMessage.ActionSignal.COMMENT)
                         .Select(item =>
                             item.Payload.Select(CommonActionSignalComment.Parser.ParseFrom)
                                 .Select(comment => _pattern.Match(comment.Content))
                                 .Where(match => match.Success)
                         ).SelectMany(match => match)
                    )
            {
                await AddSong(match.Groups[0].Value);
            }
        }
    }
}