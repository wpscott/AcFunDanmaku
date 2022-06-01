using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AcFunDanmuSongRequest
{
    internal struct Config
    {
        public enum MusicPlatform
        {
            网易云音乐,
            QQ音乐,
        }

        public int Version { get; set; }
        [JsonPropertyName("独立运行")] public bool Standalone { get; set; }
        [JsonPropertyName("音乐平台")] public MusicPlatform Platform { get; set; }
        [JsonPropertyName("主播ID")] public long UserId { get; set; }
        [JsonPropertyName("播放列表")] public string DefaultList { get; set; }
        [JsonPropertyName("点歌格式")] public string Format { get; set; }
        [JsonPropertyName("显示歌词")] public bool ShowLyrics { get; set; }

        private const int CURRENT_VERSION = 1;

        private static readonly JsonSerializerOptions Options = new()
        {
            WriteIndented = true,
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            ReadCommentHandling = JsonCommentHandling.Skip
        };

        public static async ValueTask<Config> LoadConfig()
        {
            Options.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));

            Config config;

            var configFile = new FileInfo(@".\config.json");
            if (configFile.Exists)
            {
                await using var reader = configFile.OpenRead();
                config = await JsonSerializer.DeserializeAsync<Config>(reader, Options);
            }
            else
            {
                config = new Config
                {
                    Version = CURRENT_VERSION,
                    Platform = MusicPlatform.网易云音乐,
                    DefaultList = string.Empty,
                    Format = "^点歌 (.*?)$",
                    ShowLyrics = false
                };
                await using var writer = configFile.OpenWrite();
                await JsonSerializer.SerializeAsync(writer, config, Options);
            }

            return config;
        }

        public static async void SaveConfig(Config config)
        {
            var configFile = new FileInfo(@".\config.json");
            await using var writer = configFile.OpenWrite();
            await JsonSerializer.SerializeAsync(writer, config, Options);
        }
    }
}