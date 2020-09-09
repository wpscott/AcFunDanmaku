using AcFunDanmu;
using AcFunDanmu.Enums;
using AcFunDanmuSongRequest.Platform;
using AcFunDanmuSongRequest.Platform.Interfaces;
using Google.Protobuf;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Timers;

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

        private static Regex Pattern;
        private static Config Config;
        private static IPlatform platform = null;

        public static bool IsRunning { get; private set; }
        public static DGJConnectEvent OnConnect { get; set; }
        public static DGJExitEvent OnExit { get; set; }
        public static DGJAddSongEvent OnAddSong { get; set; }

        public static async Task<bool> Initialize()
        {
            Config = await Config.LoadConfig();
            Pattern = new Regex(Config.Format, RegexOptions.IgnoreCase | RegexOptions.Compiled);
            platform = BasePlatform.CreatePlatform(Config);
            if (Config.Standalone && Config.UserId > 0)
            {
                Connect();
            }
#if DEBUG
            IsRunning = true;
#endif
            return platform == null;
        }

        private static async void Connect()
        {
            var client = new Client();
            client.Handler = HandleSignal;

            await client.Initialize(Config.UserId.ToString());

            var retry = 0;
            var resetTimer = new Timer(10000);
            resetTimer.Elapsed += (s, e) => retry = 0;

            IsRunning = true;
            OnConnect?.Invoke();
            while (!await client.Start() && retry < 3)
            {
                if (retry > 0) { resetTimer.Stop(); }
                retry++;
                resetTimer.Start();
            }
            IsRunning = false;
            OnExit?.Invoke();
        }

        public static async Task AddSong(string keyword)
        {
            var song = await platform.AddSong(keyword);
            if (song != null)
            {
                OnAddSong?.Invoke(song);
            }
        }

        public static ISong Peek()
        {
            return platform.Peek();
        }

        public static async Task<ISong> NextSong()
        {
            return await platform.NextSong();
        }

        public static async void HandleSignal(string messagetType, ByteString payload)
        {
            if (messagetType == PushMessage.ACTION_SIGNAL)
            {
                var actionSignal = ZtLiveScActionSignal.Parser.ParseFrom(payload);
                foreach (var match in actionSignal.Item
                    .Where(item => item.SignalType == PushMessage.ActionSignal.COMMENT)
                    .Select(item =>
                        item.Payload.Select(CommonActionSignalComment.Parser.ParseFrom)
                        .Select(comment => Pattern.Match(comment.Content))
                        .Where(match => match.Success)
                    ).SelectMany(match => match)
                    )
                {
                    await AddSong(match.Groups[0].Value);
                }
            }
        }
    }
}
