using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using Google.Protobuf;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace AcFunOBS;

internal static class Program
{
    private const string StreamCategory =
        "https://api-new.acfunchina.com/rest/pc-client/live/type/list?kpf=WINDOWS_PC&appver=1.9.0.200";

    private const string KUAISHOU_ZT = "https://api.kuaishouzt.com";
    private const string AUTHOR_AUTH = "/rest/zt/live/authorAuth";
    private const string LIVE_CONFIG = "/rest/zt/live/web/obs/config";
    private const string LIVE_STATUS = "/rest/zt/live/web/obs/status";
    private const string START_PUSH = "/rest/zt/live/startPush";
    private const string STOP_PUSH = "/rest/zt/live/stopPush";

    private const string APP_ACFUN = "https://id.app.acfun.cn";
    private const string TOKEN = "/rest/app/token/get";

    private const int HMAC_SHA256_SIZE = 32;

    private const int NONCE_SIZE = sizeof(long);

    private static readonly SortedList<string, string> QueryDict = new()
    {
        { "appver", "1.9.0.200" },
        { "sys", "PC_10" },
        { "kpn", "ACFUN_APP.LIVE_MATE" },
        { "kpf", "WINDOWS_PC" },
        { "subBiz", "mainApp" }
    };

    private static readonly CookieContainer Container = new();

    private static Config _config;

    private static readonly AcFunApi Api = new();

    private static readonly char[] BlockElements =
        { ' ', '▗', '▖', '▄', '▝', '▐', '▞', '▟', '▘', '▚', '▌', '▙', '▀', '▜', '▛', '█' };

    private static string Query => string.Join('&', QueryDict.Select(query => $"{query.Key}={query.Value}"));

    private static async Task Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;

        _config = await Config.Load(args.Length > 0 ? args[0] : null);
        if (_config == null)
        {
            Console.WriteLine("Load config failed");
            return;
        }

        if (_config.User.Expires <= DateTime.UtcNow)
        {
            await Login();
        }
        else
        {
            Container.Add(new Cookie("acPasstoken", _config.User.Passtoken, "/", User.CookieAcFunDomain));
            Container.Add(new Cookie("auth_key", $"{_config.User.Id}", "/", User.CookieAcFunDomain));
            Container.Add(new Cookie("userId", $"{_config.User.Id}", "/", User.CookieKuaishouDomain));
        }

        var token = await GetToken();
        while (token.Result != 0)
        {
            Console.WriteLine(token.ErrorMsg);
            Console.WriteLine("请按任意键开始扫码登录");
            Console.ReadKey();
            await Login();
            token = await GetToken();
        }

        Container.Add(new Cookie("acfun.midground.api_st", token.SToken, "/", User.CookieKuaishouDomain));

        var auth = await PostAuthorAuth(token);
        if (auth.Result != 1)
        {
            Console.WriteLine(auth.ErrorMsg);
            return;
        }

        var status = await PostStreamStatus(token);
        if (status.Result != 1) Console.WriteLine(status.ErrorMsg);

        var config = await PostStreamConfig(token);
        if (config.Result != 1)
        {
            Console.WriteLine(config.ErrorMsg);
            return;
        }

        _config.StreamAddress = config.StreamAddress;
        _config.StreamKey = config.StreamKey;
        _config.Save();

        Console.WriteLine(config);
        Console.WriteLine("请按任意键开始直播");
        Console.ReadKey();

        var push = await PostStartPush(token, _config);
        if (push.Result != 1)
        {
            Console.WriteLine(push.ErrorMsg);
            return;
        }

        _config.LiveId = push.Data.LiveId;
        _config.Save();

        Console.WriteLine($"直播已开始，直播ID为：{push.Data.LiveId}");
        Console.WriteLine("请按Ctrl+C结束直播");
        var evt = new ManualResetEvent(false);
        Console.CancelKeyPress += (s, e) =>
        {
            e.Cancel = true;
            evt.Set();
        };
        evt.WaitOne();

        if (token.Result == 0 && push.Result == 1)
        {
            await PostStopPush(token, push.Data.LiveId);

            _config.LiveId = string.Empty;
        }

        _config.Save();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void Test(string url, string key, string sign)
    {
        using var hmac = new HMACSHA256(Convert.FromBase64String(key));

        Span<byte> data = FromBase64Url(sign);
        var rnd = data[..8];
        if (BitConverter.IsLittleEndian) rnd.Reverse();

        var num = BitConverter.ToInt64(rnd);
        if (BitConverter.IsLittleEndian) rnd.Reverse();

        var test = hmac.ComputeHash(Encoding.UTF8.GetBytes($"POST&{url}&{num}")); // no extra data !!
        Console.WriteLine(num);
        Console.WriteLine(BitConverter.ToString(rnd.ToArray()));
        Console.WriteLine(BitConverter.ToString(data[8..].ToArray()));
        Console.WriteLine(BitConverter.ToString(test));
        Console.WriteLine(BitConverter.ToString(data.ToArray()));

        Console.WriteLine(BitConverter.ToString(FromBase64Url(Sign(url, key, num))));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static async Task Login()
    {
        _config.User = await QrLogin();

        _config.Save();

        Container.Add(new Cookie("acPasstoken", _config.User.Passtoken, "/", User.CookieAcFunDomain));
        Container.Add(new Cookie("auth_key", $"{_config.User.Id}", "/", User.CookieAcFunDomain));
        Container.Add(new Cookie("userId", $"{_config.User.Id}", "/", User.CookieKuaishouDomain));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static async Task<Token> GetToken()
    {
        using var client = new HttpClient(new HttpClientHandler { UseCookies = true, CookieContainer = Container });
        using var form = new FormUrlEncodedContent(new[]
            { new KeyValuePair<string, string>("sid", "acfun.midground.api") });

        using var resp = await client.PostAsync($"{APP_ACFUN}{TOKEN}?{Query}", form);
#if DEBUG
        Console.WriteLine(await resp.Content.ReadAsStringAsync());
# endif
        if (!resp.IsSuccessStatusCode) return default;

        var token = await JsonSerializer.DeserializeAsync<Token>(await resp.Content.ReadAsStreamAsync());
        return token;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static async Task<Auth> PostAuthorAuth(Token token)
    {
        using var client = new HttpClient(new HttpClientHandler { UseCookies = true, CookieContainer = Container });

        var sign = Sign(AUTHOR_AUTH, token.Ssecurity);

        using var resp = await client.PostAsync($"{KUAISHOU_ZT}{AUTHOR_AUTH}?{Query}&__clientSign={sign}", null);
#if DEBUG
        Console.WriteLine(await resp.Content.ReadAsStringAsync());
# endif
        if (!resp.IsSuccessStatusCode) return default;

        var content = await JsonSerializer.DeserializeAsync<Auth>(await resp.Content.ReadAsStreamAsync());
        return content;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static async Task<StreamConfig> PostStreamConfig(Token token)
    {
        using var client = new HttpClient();
        client.DefaultRequestHeaders.Referrer = new Uri("https://member.acfun.cn/");

        using var form = new FormUrlEncodedContent(new Dictionary<string, string>
        {
            { "kpf", "PC_WEB" },
            { "kpn", "ACFUN_APP" },
            { "subBiz", "mainApp" },
            { "userId", $"{token.UserId}" },
            { Token.ST, token.SToken }
        });

        using var resp = await client.PostAsync($"{KUAISHOU_ZT}{LIVE_CONFIG}", form);
#if DEBUG
        Console.WriteLine(await resp.Content.ReadAsStringAsync());
# endif
        if (!resp.IsSuccessStatusCode) return default;

        var content = await JsonSerializer.DeserializeAsync<StreamConfig>(await resp.Content.ReadAsStreamAsync());
        return content;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static async Task<StreamStatus> PostStreamStatus(Token token)
    {
        using var client = new HttpClient();
        client.DefaultRequestHeaders.Referrer = new Uri("https://member.acfun.cn/");

        using var form = new FormUrlEncodedContent(new Dictionary<string, string>
        {
            { "kpf", "PC_WEB" },
            { "kpn", "ACFUN_APP" },
            { "subBiz", "mainApp" },
            { "userId", $"{token.UserId}" },
            { Token.ST, token.SToken }
        });

        using var resp = await client.PostAsync($"{KUAISHOU_ZT}{LIVE_STATUS}", form);
#if DEBUG
        Console.WriteLine(await resp.Content.ReadAsStringAsync());
# endif
        if (!resp.IsSuccessStatusCode) return default;

        var content = await JsonSerializer.DeserializeAsync<StreamStatus>(await resp.Content.ReadAsStreamAsync());
        return content;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static async Task<StartPush> PostStartPush(Token token, Config config)
    {
        var bizCustomData = $"{{\"typeId\":{config.Category}}}";
        var req = Convert.ToBase64String(config.Request.ToByteArray());
        var sign = Sign(START_PUSH, token.Ssecurity,
            new Dictionary<string, string>
                { { "bizCustomData", bizCustomData }, { "caption", config.Title }, { "videoPushReq", req } });

        using var client = new HttpClient(new HttpClientHandler { UseCookies = true, CookieContainer = Container });

        using var form = new MultipartFormDataContent();
        using var videoPushReq = new ByteArrayContent(Encoding.UTF8.GetBytes(req));
        using var caption = new ByteArrayContent(Encoding.UTF8.GetBytes(config.Title));
        using var biz = new ByteArrayContent(Encoding.UTF8.GetBytes(bizCustomData));
        await using var reader = config.LoadCover();
        using var file = new StreamContent(reader);
        form.Add(videoPushReq, "videoPushReq");
        form.Add(caption, "caption");
        form.Add(biz, "bizCustomData");
        form.Add(file, "cover", "live-preview.jpg");

        using var resp = await client.PostAsync($"{KUAISHOU_ZT}{START_PUSH}?{Query}&__clientSign={sign}", form);
#if DEBUG
        Console.WriteLine(await resp.Content.ReadAsStringAsync());
# endif
        if (!resp.IsSuccessStatusCode) return default;

        var content = await JsonSerializer.DeserializeAsync<StartPush>(await resp.Content.ReadAsStreamAsync());
        return content;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static async Task<StopPush> PostStopPush(Token token, string liveId)
    {
        var sign = Sign(STOP_PUSH, token.Ssecurity, new Dictionary<string, string> { { "liveId", liveId } });

        using var client = new HttpClient(new HttpClientHandler { UseCookies = true, CookieContainer = Container });

        using var form = new MultipartFormDataContent();
        using var formContent = new ByteArrayContent(Encoding.UTF8.GetBytes(liveId));
        form.Add(formContent, "liveId");

        using var resp = await client.PostAsync($"{KUAISHOU_ZT}{STOP_PUSH}?{Query}&__clientSign={sign}", form);
#if DEBUG
        Console.WriteLine(await resp.Content.ReadAsStringAsync());
# endif
        if (!resp.IsSuccessStatusCode) return default;

        var content = await JsonSerializer.DeserializeAsync<StopPush>(await resp.Content.ReadAsStreamAsync());
        return content;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static unsafe string Sign(in string uri, in string key, in Nonce nonce,
        in IEnumerable<KeyValuePair<string, string>> extra = null)
    {
        using var hmac = new HMACSHA256(Convert.FromBase64String(key));
        var query =
            extra == null
                ? Query
                : string.Join('&',
                    QueryDict.Concat(extra).OrderBy(query => query.Key)
                        .Select(query => $"{query.Key}={query.Value}"));

        var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes($"POST&{uri}&{query}&{nonce.Result}"));

        Span<byte> sign = stackalloc byte[NONCE_SIZE + HMAC_SHA256_SIZE];

        fixed (byte* pData = nonce.Data)
        {
            if (BitConverter.IsLittleEndian)
            {
                sign[0] = *(pData + 7);
                sign[1] = *(pData + 6);
                sign[2] = *(pData + 5);
                sign[3] = *(pData + 4);
                sign[4] = *(pData + 3);
                sign[5] = *(pData + 2);
                sign[6] = *(pData + 1);
                sign[7] = *(pData + 0);
            }
            else
            {
                sign[0] = *(pData + 0);
                sign[1] = *(pData + 1);
                sign[2] = *(pData + 2);
                sign[3] = *(pData + 3);
                sign[4] = *(pData + 4);
                sign[5] = *(pData + 5);
                sign[6] = *(pData + 6);
                sign[7] = *(pData + 7);
            }
        }

        for (var i = 0; i < HMAC_SHA256_SIZE; i += 8)
        {
            sign[NONCE_SIZE + i + 0] = hash[i + 0];
            sign[NONCE_SIZE + i + 1] = hash[i + 1];
            sign[NONCE_SIZE + i + 2] = hash[i + 2];
            sign[NONCE_SIZE + i + 3] = hash[i + 3];
            sign[NONCE_SIZE + i + 4] = hash[i + 4];
            sign[NONCE_SIZE + i + 5] = hash[i + 5];
            sign[NONCE_SIZE + i + 6] = hash[i + 6];
            sign[NONCE_SIZE + i + 7] = hash[i + 7];
        }

        return ToBase64Url(sign);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static unsafe string Sign(in string url, in string key, in long nonce,
        in IEnumerable<KeyValuePair<string, string>> extra = null)
    {
        fixed (long* ptr = &nonce)
        {
            return Sign(url, key, Unsafe.AsRef<Nonce>(ptr), extra);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string Sign(in string url, in string key, in IEnumerable<KeyValuePair<string, string>> extra = null)
    {
        return Sign(url, key, Random(), extra);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static unsafe Nonce Random()
    {
        var now = DateTimeOffset.UtcNow.ToUnixTimeSeconds() / 60;

        var bytes = RandomNumberGenerator.GetBytes(4);
        fixed (byte* pByte = bytes)
        {
            var nonce = new Nonce
            {
                Result = now
            };
            nonce.Random[0] = *(pByte + 0);
            nonce.Random[1] = *(pByte + 1);
            nonce.Random[2] = *(pByte + 2);
            nonce.Random[3] = *(pByte + 3);
            return nonce;
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static byte[] FromBase64Url(in string text)
    {
        var temp = text.Replace('-', '+').Replace('_', '/');
        var rem = 4 - (temp.Length & 3);
        return Convert.FromBase64String(rem == 4 ? temp : temp.PadRight(temp.Length + rem, '='));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string ToBase64Url(in ReadOnlySpan<byte> data)
    {
        return Convert.ToBase64String(data).Replace('/', '_').Replace('+', '-').Trim('=');
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static async Task<User> QrLogin()
    {
        while (true)
        {
            string next = string.Empty, token = string.Empty, signature = string.Empty;
            Console.WriteLine("获取二维码中");
            {
                var (result, startResult) = await Api.StartScan();
                if (result)
                {
                    var data = Convert.FromBase64String(startResult.ImageData);
                    using var ms = new MemoryStream(data.Length);
                    await ms.WriteAsync(data);
                    ms.Seek(0, SeekOrigin.Begin);
                    using var bitmap = await Image.LoadAsync<L8>(Configuration.Default.Clone(), ms);

                    Console.Clear();
                    bitmap.ProcessPixelRows(accessor =>
                    {
                        for (var y = 8; y < accessor.Height - 8; y += 4)
                        {
                            ReadOnlySpan<L8> row0 = accessor.GetRowSpan(y);
                            ReadOnlySpan<L8> row1 = accessor.GetRowSpan(y + 2);

                            for (var x = 8; x < row0.Length - 9; x += 2)
                            {
                                var idx = (row0[x].PackedValue & 0b1000) | (row0[x + 1].PackedValue & 0b0100) |
                                          (row1[x].PackedValue & 0b0010) | (row1[x + 1].PackedValue & 0b0001);
                                if (y + 2 >= accessor.Height - 8) idx &= 0b1100;

                                if (x + 1 >= row0.Length - 9) idx &= 0b1010;

                                Console.Write(BlockElements[idx]);
                            }

                            Console.WriteLine();
                        }

                        Console.Write("请使用AcFun App扫码登录");
                    });

                    next = startResult.Next;
                    token = startResult.QrLoginToken;
                    signature = startResult.QrLoginSignature;
                }
                else
                {
                    continue;
                }
            }
            {
                try
                {
                    var (result, scanResult) = await Api.AcceptScan(next, token, signature);

                    if (result)
                    {
                        Console.Write("\r已扫码，请在AcFun App上确认登录");
                        next = scanResult.Next;
                        signature = scanResult.QrLoginSignature;
                    }
                    else
                    {
                        Console.Write("\r网络错误，重新获取二维码");
                        continue;
                    }
                }
                catch (TaskCanceledException)
                {
                    Console.Write("\r登录超时，重新获取二维码");
                    continue;
                }
            }
            {
                var (result, user) = await Api.ConfirmScan(next, token, signature);
                if (result)
                {
                    Console.Clear();
                    Console.Write($"\r成功登录，欢迎回来 {user.Username}");
                    return user;
                }

                Console.Write("\r登录失败，重新获取二维码");
            }
        }
    }

    private enum QualityType
    {
        UNKNOWN,
        STANDARD,
        HIGH,
        SUPER,
        CUSTOM,
        AUTO_ADAPT,
        BLUE_RAY,
        ORIGIN,
        SMOOTH = 101,
        WQHD_2K = 200,
        UHD_4K = 300
    }

    private class Config
    {
        public const string DefaultName = "config.json";
        private static readonly JsonSerializerOptions Options = new() { WriteIndented = true };

        [JsonIgnore] private string Directory { get; set; }
        [JsonIgnore] private string FileName { get; set; }

        [JsonPropertyName("user")] public User User { get; set; }
        [JsonPropertyName("category")] public int Category { get; set; }
        [JsonPropertyName("type")] public QualityType Type { get; set; }
        [JsonPropertyName("bitrate")] public int BitRate { get; set; }
        [JsonPropertyName("fps")] public int Fps { get; set; }
        [JsonPropertyName("title")] public string Title { get; set; } = string.Empty;
        [JsonPropertyName("cover")] public string Cover { get; set; }
        [JsonPropertyName("liveId")] public string LiveId { get; set; } = string.Empty;
        [JsonPropertyName("streamAddress")] public string StreamAddress { get; set; } = string.Empty;
        [JsonPropertyName("streamKey")] public string StreamKey { get; set; } = string.Empty;

        [JsonIgnore]
        public StartPushRequest Request => new()
        {
            Category = Category,
            Type = (int)Type,
            Bitrate = BitRate,
            Fps = Fps,
            Unknown1 = 7,
            Unknown2 = 1,
            Unknown3 = 3000
        };

        public static async Task<Config> Load(string path = null)
        {
            Config config;

            if (string.IsNullOrEmpty(path)) path = DefaultName;

            try
            {
                if (File.Exists(Path.Combine(path, DefaultName)))
                {
                    path = Path.Combine(path, DefaultName);
                    await using var stream = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.Read);
                    config = await JsonSerializer.DeserializeAsync<Config>(stream);
                }
                else if (File.Exists(path))
                {
                    await using var stream = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.Read);
                    config = await JsonSerializer.DeserializeAsync<Config>(stream);
                }
                else
                {
                    Console.WriteLine($"File not found: {path}");
                    config = new Config
                    {
                        Cover = "cover.jpg",
                        Type = QualityType.BLUE_RAY,
                        BitRate = 8000,
                        Fps = 60
                    };
                }
            }
            catch (JsonException)
            {
                return null;
            }

            if (config == null) return null;

            var info = new FileInfo(path);
            config.Directory = info.DirectoryName;
            config.FileName = info.FullName;

            return config;
        }

        public async void Save()
        {
            await using var stream = File.Open(FileName, FileMode.Create, FileAccess.Write, FileShare.Read);
            await JsonSerializer.SerializeAsync(stream, this, Options);
        }

        public Stream LoadCover()
        {
            return File.Exists(Path.Combine(Directory, Cover))
                ? File.Open(Path.Combine(Directory, Cover), FileMode.Open, FileAccess.Read, FileShare.Read)
                : File.Exists(Cover)
                    ? File.Open(Cover, FileMode.Open, FileAccess.Read, FileShare.Read)
                    : null;
        }
    }

    private readonly struct Auth
    {
        [JsonPropertyName("result")] public int Result { get; init; } // 1
        [JsonPropertyName("data")] public AuthData Data { get; init; }
        [JsonPropertyName("host")] public string Host { get; init; }
        [JsonPropertyName("error_msg")] public string ErrorMsg { get; init; }

        public override string ToString()
        {
            return $"{{ \"data\": {Data} }}";
        }
    }

    private readonly struct AuthData
    {
        [JsonPropertyName("authStatus")] public string AuthStatus { get; init; } // "AVAILABLE"
        [JsonPropertyName("desc")] public string Desc { get; init; } // "打开"

        public override string ToString()
        {
            return $"{{ \"authStatus\": {AuthStatus}, \"desc\": {Desc} }}";
        }
    }

    private readonly struct StreamConfig
    {
        [JsonPropertyName("result")] public int Result { get; init; }
        [JsonPropertyName("data")] public ConfigData Data { get; init; }
        [JsonPropertyName("host")] public string Host { get; init; }
        [JsonPropertyName("error_msg")] public string ErrorMsg { get; init; }

        public string StreamAddress => Data.StreamPushAddress[0][..Data.StreamPushAddress[0].LastIndexOf('/')];
        public string StreamKey => Data.StreamPushAddress[0][(Data.StreamPushAddress[0].LastIndexOf('/') + 1)..];

        public override string ToString()
        {
            return $"RTMP Server: {StreamAddress}\r\nStream Key: {StreamKey}";
        }
    }

    private readonly struct StreamStatus
    {
        [JsonPropertyName("result")] public int Result { get; init; }
        [JsonPropertyName("host")] public string Host { get; init; }
        [JsonPropertyName("error_msg")] public string ErrorMsg { get; init; }
    }

    private readonly struct ConfigData
    {
        [JsonPropertyName("intervalMillis")] public int IntervalMillis { get; init; }
        [JsonPropertyName("panoramic")] public bool Panoramic { get; init; }
        [JsonPropertyName("streamName")] public string StreamName { get; init; }

        [JsonPropertyName("streamPullAddress")]
        public string StreamPullAddress { get; init; }

        [JsonPropertyName("streamPushAddress")]
        public string[] StreamPushAddress { get; init; }
    }

    private readonly struct StartPush
    {
        [JsonPropertyName("result")] public int Result { get; init; } // 1
        [JsonPropertyName("data")] public StartPushData Data { get; init; }
        [JsonPropertyName("host")] public string Host { get; init; }
        [JsonPropertyName("error_msg")] public string ErrorMsg { get; init; }
    }

    private readonly struct StartPushData
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

        [JsonPropertyName("_config")] public PushConfig Config { get; init; }

        public override string ToString()
        {
            return $"{{ \"liveId\": {LiveId}, \"videoPushRes\": {VideoPushRes} }}";
        }
    }

    private readonly struct PushNotice
    {
        [JsonPropertyName("userId")] public long UserId { get; init; }
        [JsonPropertyName("userName")] public string UserName { get; init; }
        [JsonPropertyName("userGender")] public string UserGender { get; init; }
        [JsonPropertyName("notice")] public string Notice { get; init; }
    }

    private readonly struct PushConfig
    {
        [JsonPropertyName("giftSlotSize")] public short GiftSlotSize { get; init; } // 2
    }

    private readonly struct StopPush
    {
        [JsonPropertyName("result")] public int Result { get; init; }
        [JsonPropertyName("data")] public StopPushData Data { get; init; }
        [JsonPropertyName("host")] public string Host { get; init; }
    }

    private readonly struct StopPushData
    {
        [JsonPropertyName("durationMs")] public long DurationMs { get; init; }
        [JsonPropertyName("endReason")] public string EndReason { get; init; }
    }

    private readonly struct Token
    {
        public const string ST = "acfun.midground.api_st";
        public const string AT = "acfun.midground.api.at";

        [JsonPropertyName("result")] public int Result { get; init; }

        [JsonPropertyName(ST)] public string SToken { get; init; }

        [JsonPropertyName(AT)] public string AToken { get; init; }

        [JsonPropertyName("userId")] public long UserId { get; init; }
        [JsonPropertyName("ssecurity")] public string Ssecurity { get; init; }

        [JsonPropertyName("error_msg")] public string ErrorMsg { get; init; }


        public override string ToString()
        {
            return $"{{ \"st\": {SToken}, \"ssecurity\": {Ssecurity} }}";
        }
    }

    [StructLayout(LayoutKind.Explicit, Pack = NONCE_SIZE, Size = NONCE_SIZE)]
    private unsafe struct Nonce
    {
        [FieldOffset(0)] public long Result;
        [FieldOffset(4)] public fixed byte Random[NONCE_SIZE >> 1];
        [FieldOffset(0)] public fixed byte Data[NONCE_SIZE];
    }
}