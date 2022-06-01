using AcFunDanmuSongRequest.Platform.Interfaces;
using AcFunDanmuSongRequest.Platform.NetEase;
using AcFunDanmuSongRequest.Platform.QQ;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace AcFunDanmuSongRequest.Platform
{
    internal abstract class BasePlatform : IPlatform
    {
        private Config Config { get; }
        protected Queue<ISong> Songs { get; }

        protected BasePlatform(Config config)
        {
            Config = config;
            Songs = new Queue<ISong>();
        }

        public abstract ValueTask<ISong> AddSong(string keyword);
        public ISong Peek() => Songs.Peek();
        public abstract ValueTask<ISong> NextSong();
        public abstract ValueTask<Lyrics> GetLyrics(ISong song);

        public static IPlatform CreatePlatform(Config config)
        {
            return config.Platform switch
            {
                Config.MusicPlatform.网易云音乐 => new NetEasePlatform(config),
                Config.MusicPlatform.QQ音乐 => new QQPlatform(config),
                _ => null
            };
        }

        private static HttpClient CreateClient()
        {
            var client = new HttpClient(new HttpClientHandler { AutomaticDecompression = DecompressionMethods.All });
            client.DefaultRequestHeaders.AcceptEncoding.ParseAdd("gzip, deflate, br");
            return client;
        }

        protected static async ValueTask<T> GetAsync<T>(IGetRequest request, JsonSerializerOptions options)
        {
            using var client = CreateClient();

            using var resp = await client.GetAsync(request.Host);
            var json = await JsonSerializer.DeserializeAsync<T>(await resp.Content.ReadAsStreamAsync(), options);
            return json;
        }

        protected static async ValueTask<TResult> GetAsync<TResult, TEncodedResponse>(IGetRequest request,
            JsonSerializerOptions options) where TEncodedResponse : IEncodedResponse
        {
            using var client = CreateClient();

            using var resp = await client.GetAsync(request.Host);
            var content = await resp.Content.ReadAsStringAsync();
            var json = await JsonSerializer.DeserializeAsync<TEncodedResponse>(await resp.Content.ReadAsStreamAsync(),
                IEncodedResponse.Options);
            if (json == null) return default;
            var result = JsonSerializer.Deserialize<TResult>(json.Decode(), options);
            return result;
        }

        protected static async ValueTask<T> PostAsync<T>(IPostRequest request, JsonSerializerOptions options)
        {
            using var client = CreateClient();

            using var content = request.IsJson ? request.ToJson() : request.ToForm();

            using var resp = await client.PostAsync(request.Host, content);
            await using var stream = await resp.Content.ReadAsStreamAsync();
            if (stream.Length <= 0) return default;
            var json = await JsonSerializer.DeserializeAsync<T>(stream, options);
            return json;
        }

        protected static async ValueTask<TResult> PostAsync<TResult, TEncodeResponse>(IPostRequest request,
            JsonSerializerOptions options) where TEncodeResponse : IEncodedResponse
        {
            using var client = CreateClient();

            using var content = request.IsJson ? request.ToJson() : request.ToForm();

            using var resp = await client.PostAsync(request.Host, content);
            await using var stream = await resp.Content.ReadAsStreamAsync();
            if (stream.Length <= 0) return default;
            var json = await JsonSerializer.DeserializeAsync<TEncodeResponse>(
                await resp.Content.ReadAsStreamAsync());
            if (json == null) return default;
            var result = JsonSerializer.Deserialize<TResult>(json.Decode(), options);
            return result;
        }
    }
}