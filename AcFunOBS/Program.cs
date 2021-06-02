using Google.Protobuf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AcFunOBS
{
    class Program
    {
        private const string StreamCategory = "https://api-new.acfunchina.com/rest/pc-client/live/type/list?kpf=WINDOWS_PC&appver=1.4.0.145";

        private static readonly SortedList<string, string> QueryDict = new SortedList<string, string> {
            { "appver", "1.4.0.145" },
            { "sys", "PC_10" },
            { "kpn", "ACFUN_APP.LIVE_MATE" },
            { "kpf", "WINDOWS_PC" },
            { "subBiz", "mainApp" },
        };
        private static string Query => string.Join('&', QueryDict.Select(query => $"{query.Key}={query.Value}"));

        private const string KuaishouZt = "https://api.kuaishouzt.com";
        private const string AuthorAuth = "/rest/zt/live/authorAuth";
        private const string StartPush = "/rest/zt/live/startPush";

        private const string AppAcfun = "https://id.app.acfun.cn";
        private const string Token = "/rest/app/token/get";

        private const string DefaultKey = "zqVDaMgabl6SjsS1EPlhJA==";

        private static readonly CookieContainer Container = new CookieContainer();

        struct Config
        {
            public string acPasstoken { get; set; }
            public long uid { get; set; }
            public int category { get; set; }
            public int type { get; set; }
            public int bitrate { get; set; }
            public int fps { get; set; }
            public string title { get; set; }
            public string cover { get; set; }
        }

        static async Task Main(string[] args)
        {
            var file = @".\config.json";
            if (args.Length == 1)
            {
                file = args[0];
            }
            var fInfo = new FileInfo(file);
            if (!fInfo.Exists)
            {
                using var writer = new StreamWriter(fInfo.OpenWrite());
                writer.Write(JsonSerializer.Serialize(new Config(), new JsonSerializerOptions { WriteIndented = true }));
                Console.WriteLine($"Cannot find {file}, created a defalut one");
                return;
            }
            using var reader = fInfo.OpenText();
            var config = JsonSerializer.Deserialize<Config>(reader.ReadToEnd());

            Container.Add(new Cookie("acPasstoken", config.acPasstoken, "/", ".acfun.cn"));
            Container.Add(new Cookie("auth_key", $"{config.uid}", "/", ".acfun.cn"));
            Container.Add(new Cookie("userId", $"{config.uid}", "/", ".kuaishouzt.com"));

            var token = await GetToken();

            Container.Add(new Cookie("acfun.midground.api_st", token.st, "/", ".kuaishouzt.com"));

            await PostAuthorAuth(token);

            await PostStartPush(token, config.title, config.cover, new StartPushRequest { Category = config.category, Type = config.type, Bitrate = config.bitrate, Fps = config.fps, Unknown1 = 7, Unknown2 = 1, Unknown3 = 3000 });
        }

        static void Test(string url, string key, string sign)
        {
            using var hmac = new HMACSHA256(Convert.FromBase64String(key));

            Span<byte> data = FromBase64Url(sign);
            var rnd = data.Slice(0, 8);
            if (BitConverter.IsLittleEndian) { rnd.Reverse(); }
            var num = BitConverter.ToInt64(rnd);
            if (BitConverter.IsLittleEndian) { rnd.Reverse(); }

            var test = hmac.ComputeHash(Encoding.UTF8.GetBytes($"POST&{url}&{num}")); // no extra data !!
            Console.WriteLine(num);
            Console.WriteLine(BitConverter.ToString(rnd.ToArray()));
            Console.WriteLine(BitConverter.ToString(data.Slice(8).ToArray()));
            Console.WriteLine(BitConverter.ToString(test));
            Console.WriteLine(BitConverter.ToString(data.ToArray()));

            Console.WriteLine(BitConverter.ToString(FromBase64Url(Sign(url, key, num))));
        }


        static async Task<TokenResult> GetToken()
        {
            using var client = new HttpClient(new HttpClientHandler { UseCookies = true, CookieContainer = Container });
            using var form = new FormUrlEncodedContent(new[] { new KeyValuePair<string, string>("sid", "acfun.midground.api") });

            using var resp = await client.PostAsync($"{AppAcfun}{Token}?{Query}", form);
            var token = await JsonSerializer.DeserializeAsync<TokenResult>(await resp.Content.ReadAsStreamAsync());
            return token;
        }

        static async Task PostAuthorAuth(TokenResult token)
        {
            using var client = new HttpClient(new HttpClientHandler { UseCookies = true, CookieContainer = Container });

            var sign = Sign(AuthorAuth, token.ssecurity);

            using var form = new StringContent(string.Empty);
            using var resp = await client.PostAsync($"{KuaishouZt}{AuthorAuth}?{Query}&__clientSign={sign}", form);
            var content = await resp.Content.ReadAsStringAsync();
            Console.WriteLine(content);
        }

        static async Task PostStartPush(TokenResult token, string title, string cover, StartPushRequest startPushRequest)
        {
            var bizCustomData = $"{{\"typeId\":{startPushRequest.Category}}}";
            var req = Convert.ToBase64String(startPushRequest.ToByteArray());
            var sign = Sign(StartPush, token.ssecurity, new SortedList<string, string> { { "bizCustomData", bizCustomData }, { "caption", title }, { "videoPushReq", req } });

            using var client = new HttpClient(new HttpClientHandler { UseCookies = true, CookieContainer = Container });
            client.DefaultRequestHeaders.ExpectContinue = true;

            using var form = new MultipartFormDataContent();
            using var videoPushReq = new ByteArrayContent(Encoding.UTF8.GetBytes(req));
            using var caption = new ByteArrayContent(Encoding.UTF8.GetBytes(title));
            using var biz = new ByteArrayContent(Encoding.UTF8.GetBytes(bizCustomData));
            using var reader = File.OpenRead(cover);
            using var file = new StreamContent(reader);
            form.Add(videoPushReq, "videoPushReq");
            form.Add(caption, "caption");
            form.Add(biz, "bizCustomData");
            form.Add(file, "cover", "live-preview.jpg");

            using var resp = await client.PostAsync($"{KuaishouZt}{StartPush}?{Query}&__clientSign={sign}", form);
            var content = await resp.Content.ReadAsStringAsync();
            Console.WriteLine(content);
        }

        struct TokenResult
        {
            public int result { get; set; }
            [JsonPropertyName("acfun.midground.api_st")]
            public string st { get; set; }
            [JsonPropertyName("acfun.midground.api.at")]
            public string at { get; set; }
            public long userId { get; set; }
            public string ssecurity { get; set; }
        }

        static string Sign(string uri, string key, long rnd, Span<byte> bytes, SortedList<string, string> extra = null)
        {
            using var hmac = new HMACSHA256(Convert.FromBase64String(key));
            string query = extra == null ? Query : string.Join('&', QueryDict.Concat(extra).OrderBy(query => query.Key).Select(query => $"{query.Key}={query.Value}"));

            var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes($"POST&{uri}&{query}&{rnd}"));
            Span<byte> sign = stackalloc byte[bytes.Length + hash.Length];
            if (BitConverter.IsLittleEndian) { bytes.Reverse(); }

            for (var i = 0; i < bytes.Length; i++)
            {
                sign[i] = bytes[i];
            }
            for (var i = 0; i < hash.Length; i++)
            {
                sign[bytes.Length + i] = hash[i];
            }

            return ToBase64Url(sign);
        }

        static string Sign(string url, string key, long rnd, SortedList<string, string> extra = null) => Sign(url, key, rnd, BitConverter.GetBytes(rnd), extra);


        static string Sign(string url, string key = DefaultKey, SortedList<string, string> extra = null) => Sign(url, key, Random(), extra);


        static long Random()
        {
            var random = new Random();
            var now = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            Span<byte> rand = stackalloc byte[4];
            random.NextBytes(rand);
            long result = BitConverter.ToInt32(rand);
            result <<= 0x20;
            result |= now;
            return result;
        }

        static byte[] FromBase64Url(string text)
        {
            var temp = text.Replace('-', '+').Replace('_', '/');
            var rem = 4 - (temp.Length & 3);
            if (rem == 4)
            {
                return Convert.FromBase64String(temp);
            }
            else
            {
                return Convert.FromBase64String(temp.PadRight(temp.Length + rem, '='));
            }
        }

        static string ToBase64Url(ReadOnlySpan<byte> data)
        {
            return Convert.ToBase64String(data).Replace('/', '_').Replace('+', '-').Trim('=');
        }
    }
}
