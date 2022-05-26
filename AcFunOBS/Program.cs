﻿using Google.Protobuf;
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
    internal static class Program
    {
        private const string StreamCategory =
            "https://api-new.acfunchina.com/rest/pc-client/live/type/list?kpf=WINDOWS_PC&appver=1.4.0.145";

        private static readonly SortedList<string, string> QueryDict = new()
        {
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

        private static readonly CookieContainer Container = new();

        private struct Config
        {
            [JsonPropertyName("acPasstoken")] public string AcPasstoken { get; set; }
            [JsonPropertyName("uid")] public long Uid { get; set; }
            [JsonPropertyName("category")] public int Category { get; set; }
            [JsonPropertyName("type")] public int Type { get; set; }
            [JsonPropertyName("bitrate")] public int Bitrate { get; set; }
            [JsonPropertyName("fps")] public int Fps { get; set; }
            [JsonPropertyName("title")] public string Title { get; set; }
            [JsonPropertyName("cover")] public string Cover { get; set; }
        }

        private static async Task Main(string[] args)
        {
            var file = @".\config.json";
            if (args.Length == 1)
            {
                file = args[0];
            }

            var fInfo = new FileInfo(file);
            if (!fInfo.Exists)
            {
                await using var writer = new StreamWriter(fInfo.OpenWrite());
                await writer.WriteAsync(JsonSerializer.Serialize(new Config(),
                    new JsonSerializerOptions { WriteIndented = true }));
                Console.WriteLine($"Cannot find {file}, created a default one");
                return;
            }

            using var reader = fInfo.OpenText();
            var config = JsonSerializer.Deserialize<Config>(await reader.ReadToEndAsync());

            Container.Add(new Cookie("acPasstoken", config.AcPasstoken, "/", ".acfun.cn"));
            Container.Add(new Cookie("auth_key", $"{config.Uid}", "/", ".acfun.cn"));
            Container.Add(new Cookie("userId", $"{config.Uid}", "/", ".kuaishouzt.com"));

            var token = await GetToken();

            Container.Add(new Cookie("acfun.midground.api_st", token.ST, "/", ".kuaishouzt.com"));

            await PostAuthorAuth(token);

            await PostStartPush(token, config.Title, config.Cover,
                new StartPushRequest
                {
                    Category = config.Category, Type = config.Type, Bitrate = config.Bitrate, Fps = config.Fps,
                    Unknown1 = 7, Unknown2 = 1, Unknown3 = 3000
                });
        }

        private static void Test(string url, string key, string sign)
        {
            using var hmac = new HMACSHA256(Convert.FromBase64String(key));

            Span<byte> data = FromBase64Url(sign);
            var rnd = data[..8];
            if (BitConverter.IsLittleEndian)
            {
                rnd.Reverse();
            }

            var num = BitConverter.ToInt64(rnd);
            if (BitConverter.IsLittleEndian)
            {
                rnd.Reverse();
            }

            var test = hmac.ComputeHash(Encoding.UTF8.GetBytes($"POST&{url}&{num}")); // no extra data !!
            Console.WriteLine(num);
            Console.WriteLine(BitConverter.ToString(rnd.ToArray()));
            Console.WriteLine(BitConverter.ToString(data[8..].ToArray()));
            Console.WriteLine(BitConverter.ToString(test));
            Console.WriteLine(BitConverter.ToString(data.ToArray()));

            Console.WriteLine(BitConverter.ToString(FromBase64Url(Sign(url, key, num))));
        }


        private static async Task<TokenResult> GetToken()
        {
            using var client = new HttpClient(new HttpClientHandler { UseCookies = true, CookieContainer = Container });
            using var form = new FormUrlEncodedContent(new[]
                { new KeyValuePair<string, string>("sid", "acfun.midground.api") });

            using var resp = await client.PostAsync($"{AppAcfun}{Token}?{Query}", form);
            var token = await JsonSerializer.DeserializeAsync<TokenResult>(await resp.Content.ReadAsStreamAsync());
            return token;
        }

        private struct Auth
        {
            [JsonPropertyName("result")] public int Result { get; init; } // 1
            [JsonPropertyName("data")] public AuthData Data { get; init; }
            [JsonPropertyName("host")] public string Host { get; init; }
        }

        private struct AuthData
        {
            [JsonPropertyName("authStatus")] public string AuthStatus { get; init; } // "AVAILABLE"
            [JsonPropertyName("desc")] public string Desc { get; init; } // "打开"
        }

        private static async Task PostAuthorAuth(TokenResult token)
        {
            using var client = new HttpClient(new HttpClientHandler { UseCookies = true, CookieContainer = Container });

            var sign = Sign(AuthorAuth, token.Ssecurity);

            using var form = new StringContent(string.Empty);
            using var resp = await client.PostAsync($"{KuaishouZt}{AuthorAuth}?{Query}&__clientSign={sign}", form);
            var content = await JsonSerializer.DeserializeAsync<Auth>(await resp.Content.ReadAsStreamAsync());
            Console.WriteLine(content);
        }

        private struct Push
        {
            [JsonPropertyName("result")] public int Result { get; init; } // 1
            [JsonPropertyName("data")] public PushData Data { get; init; }
            [JsonPropertyName("host")] public string Host { get; init; }
        }

        private struct PushData
        {
            [JsonPropertyName("videoPushRes")]
            public string VideoPushRes { get; init; } // ?.proto {number, pushAddr, streamName}

            [JsonPropertyName("liveId")] public string LiveId { get; init; }
            [JsonPropertyName("enterRoomAttach")] public string EnterRoomAttach { get; init; }
            [JsonPropertyName("availableTickets")] public string[] AvailableTickets { get; init; }
            [JsonPropertyName("notices")] public PushNotice[] Notices { get; init; }
            [JsonPropertyName("ticketRetryCount")] public short TicketRetryCount { get; init; } // 2

            [JsonPropertyName("ticketRetryIntervalMs")]
            public int TicketRetryIntervalMs { get; init; } // 2000

            [JsonPropertyName("config")] public PushConfig Config { get; init; }
        }

        private struct PushNotice
        {
            [JsonPropertyName("userId")] public long UserId { get; init; }
            [JsonPropertyName("userName")] public string UserName { get; init; }
            [JsonPropertyName("userGender")] public string UserGender { get; init; }
            [JsonPropertyName("notice")] public string Notice { get; init; }
        }

        private struct PushConfig
        {
            [JsonPropertyName("giftSlotSize")] public short GiftSlotSize { get; init; } // 2
        }

        private static async Task PostStartPush(TokenResult token, string title, string cover,
            StartPushRequest startPushRequest)
        {
            var bizCustomData = $"{{\"typeId\":{startPushRequest.Category}}}";
            var req = Convert.ToBase64String(startPushRequest.ToByteArray());
            var sign = Sign(StartPush, token.Ssecurity,
                new SortedList<string, string>
                    { { "bizCustomData", bizCustomData }, { "caption", title }, { "videoPushReq", req } });

            using var client = new HttpClient(new HttpClientHandler { UseCookies = true, CookieContainer = Container });
            client.DefaultRequestHeaders.ExpectContinue = true;

            using var form = new MultipartFormDataContent();
            using var videoPushReq = new ByteArrayContent(Encoding.UTF8.GetBytes(req));
            using var caption = new ByteArrayContent(Encoding.UTF8.GetBytes(title));
            using var biz = new ByteArrayContent(Encoding.UTF8.GetBytes(bizCustomData));
            await using var reader = File.OpenRead(cover);
            using var file = new StreamContent(reader);
            form.Add(videoPushReq, "videoPushReq");
            form.Add(caption, "caption");
            form.Add(biz, "bizCustomData");
            form.Add(file, "cover", "live-preview.jpg");

            using var resp = await client.PostAsync($"{KuaishouZt}{StartPush}?{Query}&__clientSign={sign}", form);
            var content = await JsonSerializer.DeserializeAsync<Push>(await resp.Content.ReadAsStreamAsync());
            Console.WriteLine(content);
        }

        private struct TokenResult
        {
            [JsonPropertyName("result")] public int Result { get; set; }

            [JsonPropertyName("acfun.midground.api_st")]
            public string ST { get; set; }

            [JsonPropertyName("acfun.midground.api.at")]
            public string AT { get; set; }

            [JsonPropertyName("userId")] public long UserId { get; set; }
            [JsonPropertyName("ssecurity")] public string Ssecurity { get; set; }
        }

        private static string Sign(string uri, string key, long rnd, Span<byte> bytes,
            SortedList<string, string> extra = null)
        {
            using var hmac = new HMACSHA256(Convert.FromBase64String(key));
            var query = extra == null
                ? Query
                : string.Join('&',
                    QueryDict.Concat(extra).OrderBy(query => query.Key).Select(query => $"{query.Key}={query.Value}"));

            var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes($"POST&{uri}&{query}&{rnd}"));
            Span<byte> sign = stackalloc byte[bytes.Length + hash.Length];
            if (BitConverter.IsLittleEndian)
            {
                bytes.Reverse();
            }

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

        private static string Sign(string url, string key, long rnd, SortedList<string, string> extra = null) =>
            Sign(url, key, rnd, BitConverter.GetBytes(rnd), extra);


        private static string Sign(string url, string key = DefaultKey, SortedList<string, string> extra = null) =>
            Sign(url, key, Random(), extra);


        private static long Random()
        {
            long result = BitConverter.ToInt32(RandomNumberGenerator.GetBytes(4));
            result <<= 0x20;
            result |= DateTimeOffset.UtcNow.ToUnixTimeSeconds() / 60;
            return result;
        }

        private static byte[] FromBase64Url(string text)
        {
            var temp = text.Replace('-', '+').Replace('_', '/');
            var rem = 4 - (temp.Length & 3);
            return Convert.FromBase64String(rem == 4 ? temp : temp.PadRight(temp.Length + rem, '='));
        }

        private static string ToBase64Url(ReadOnlySpan<byte> data) =>
            Convert.ToBase64String(data).Replace('/', '_').Replace('+', '-').Trim('=');
    }
}