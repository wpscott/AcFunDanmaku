using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AcFunDanmuSongRequest
{
    struct Config
    {
        public enum MusicPlatform
        {
            网易云音乐,
            QQ音乐,
        }
        public int Version { get; set; }
        [JsonPropertyName("独立运行")]
        public bool Standalone { get; set; }
        [JsonPropertyName("音乐平台")]
        public MusicPlatform Platform { get; set; }
        [JsonPropertyName("主播ID")]
        public long UserId { get; set; }
        [JsonPropertyName("播放列表")]
        public string DefalutList { get; set; }
        [JsonPropertyName("点歌格式")]
        public string Format { get; set; }
        [JsonPropertyName("显示歌词")]
        public bool ShowLyrics { get; set; }

        private const int CurrentVersion = 1;
        private static readonly JsonSerializerOptions options = new JsonSerializerOptions
        {
            WriteIndented = true,
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            ReadCommentHandling = JsonCommentHandling.Skip
        };

        public static async ValueTask<Config> LoadConfig()
        {
            options.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));

            Config config;

            var configFile = new FileInfo(@".\config.json");
            if (configFile.Exists)
            {
                using var reader = configFile.OpenRead();
                config = await JsonSerializer.DeserializeAsync<Config>(reader, options);
            }
            else
            {
                config = new Config
                {
                    Version = CurrentVersion,
                    Platform = MusicPlatform.网易云音乐,
                    DefalutList = string.Empty,
                    Format = "^点歌 (.*?)$",
                    ShowLyrics = false
                };
                using var writer = configFile.OpenWrite();
                await JsonSerializer.SerializeAsync(writer, config, options);
            }

            return config;
        }

        public static async void SaveConfig(Config config)
        {
            var configFile = new FileInfo(@".\config.json");
            using var writer = configFile.OpenWrite();
            await JsonSerializer.SerializeAsync(writer, config, options);
        }
    }
}
