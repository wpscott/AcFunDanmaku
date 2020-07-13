using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace AcFunDMJ.Vanilla
{
    struct Config
    {
        public int Port { get; set; }
        public bool ShowEnter { get; set; }
        public bool ShowLike { get; set; }
        public bool ShowFollow { get; set; }
        public long[] GiftList { get; set; }

        private static readonly JsonSerializerOptions options = new JsonSerializerOptions
        {
            WriteIndented = true,
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };

        private const string FileName = @".\config.json";

        public static async ValueTask<Config> LoadConfig(string path = FileName)
        {
            Config config;

            var inFile = new FileInfo(path);
            if (inFile.Exists)
            {
                using var reader = inFile.OpenRead();
                config = await JsonSerializer.DeserializeAsync<Config>(reader, options);
            }
            else
            {
                config = new Config
                {
                    Port = 5000,
                    ShowEnter = true,
                    ShowLike = true,
                    ShowFollow = true,
                    GiftList = Array.Empty<long>()
                };
                await SaveConfig(config);
            }

            return config;
        }

        public static async ValueTask SaveConfig(Config config, string path = FileName)
        {
            var outFile = new FileInfo(path);
            using var writer = outFile.OpenWrite();
            await JsonSerializer.SerializeAsync(writer, config, options);
        }
    }
}
