using System;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using SixLabors.ImageSharp;

namespace AcFunOBS;

internal class AcFunApi
{
    private const string SCAN_ADDRESS = "https://scan.acfun.cn/rest/pc-direct/qr/";
    private const string START_SCAN_URI = $"{SCAN_ADDRESS}start?type=WEB_LOGIN&_={{0}}";

    private const string ACCEPT_SCAN_URI =
        $"{SCAN_ADDRESS}{{0}}?qrLoginToken={{1}}&qrLoginSignature={{2}}&_=&_={{3}}";

    private const string CONFIRM_SCAN_URI =
        $"{SCAN_ADDRESS}{{0}}?qrLoginToken={{1}}&qrLoginSignature={{2}}&_=&_={{3}}";

    private readonly HttpClient _client;
    private readonly CookieContainer _container;


    public AcFunApi()
    {
        _container = new CookieContainer();
        _client = new HttpClient(new HttpClientHandler { UseCookies = true, CookieContainer = _container });
        _client.DefaultRequestHeaders.UserAgent.ParseAdd("kuaishou 1.9.0.200");
    }

    ~AcFunApi()
    {
        _client.Dispose();
    }

    public void Initialize(in User user)
    {
        _container.Add(new Cookie(User.CookieId, $"{user.Id}", "/", User.CookieAcFunDomain)
            { Expires = user.Expires });
        _container.Add(new Cookie(User.CookiePasstoken, user.Passtoken, "/", User.CookieAcFunDomain)
            { Expires = user.Expires });

        _container.Add(new Cookie(User.CookieUserId, $"{user.Id}", "/", User.CookieKuaishouDomain)
            { Expires = user.Expires });
    }

    public async Task<IImage> FetchImage(string url)
    {
        using var response = await _client.GetAsync(url);
        if (!response.IsSuccessStatusCode) return null;
        var bitmap = await Image.LoadAsync(await response.Content.ReadAsStreamAsync());
        return bitmap;
    }

    public async Task<(bool, StartResult)> StartScan()
    {
        using var start =
            await _client.GetAsync(
                string.Format(START_SCAN_URI, DateTimeOffset.UtcNow.ToUnixTimeSeconds()));

#if DEBUG
        Console.WriteLine(await start.Content.ReadAsStringAsync());
#endif

        if (start.IsSuccessStatusCode)
            return (true, await JsonSerializer.DeserializeAsync<StartResult>(
                await start.Content.ReadAsStreamAsync()));

        return (false, default);
    }

    public async Task<(bool, ScanResult)> AcceptScan(string next, string token, string signature)
    {
        using var scan =
            await _client.GetAsync(
                string.Format(ACCEPT_SCAN_URI, next, token, signature,
                    DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()));

#if DEBUG
        Console.WriteLine(await scan.Content.ReadAsStringAsync());
#endif

        if (scan.IsSuccessStatusCode)
            return (true, await JsonSerializer.DeserializeAsync<ScanResult>(
                await scan.Content.ReadAsStreamAsync()));

        return (false, default);
    }

    public async Task<(bool, User)> ConfirmScan(string next, string token, string signature)
    {
        using var accept = await _client.GetAsync(
            string.Format(CONFIRM_SCAN_URI, next, token, signature,
                DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()));

#if DEBUG
        Console.WriteLine(await accept.Content.ReadAsStringAsync());
#endif

        if (!accept.IsSuccessStatusCode) return (false, default);

        var result = await JsonSerializer.DeserializeAsync<AcceptResult>(
            await accept.Content.ReadAsStreamAsync());

        if (result.Result != 0) return (false, default);

        var collection = _container.GetCookies(User.CookieUri);

        return (true, new User
        {
            Id = result.UserId,
            Username = result.Username,
            Avatar = result.Avatar,
            Passtoken = result.Passtoken,
            Expires = TimeZoneInfo.ConvertTimeToUtc(collection[User.CookiePasstoken]!.Expires)
        });
    }
}

public readonly record struct StartResult
{
    [JsonPropertyName("expireTime")] public long ExpireTime { get; init; }
    [JsonPropertyName("imageData")] public string ImageData { get; init; }
    [JsonPropertyName("next")] public string Next { get; init; }
    [JsonPropertyName("qrLoginSignature")] public string QrLoginSignature { get; init; }
    [JsonPropertyName("qrLoginToken")] public string QrLoginToken { get; init; }
    [JsonPropertyName("result")] public int Result { get; init; }
    [JsonPropertyName("error_msg")] public string ErrorMsg { get; init; }
}

public readonly record struct ScanResult
{
    [JsonPropertyName("next")] public string Next { get; init; }
    [JsonPropertyName("qrLoginSignature")] public string QrLoginSignature { get; init; }
    [JsonPropertyName("status")] private string Status { get; init; }
    [JsonPropertyName("result")] public int Result { get; init; }
    [JsonPropertyName("error_msg")] public string ErrorMsg { get; init; }
}

public readonly record struct AcceptResult
{
    [JsonPropertyName("next")] public string Next { get; init; }
    [JsonPropertyName("qrLoginSignature")] public string QrLoginSignature { get; init; }
    [JsonPropertyName("status")] private string Status { get; init; }
    [JsonPropertyName("result")] public int Result { get; init; }
    [JsonPropertyName("userId")] public long UserId { get; init; }
    [JsonPropertyName("ac_username")] public string Username { get; init; }
    [JsonPropertyName("ac_userimg")] public string Avatar { get; init; }
    [JsonPropertyName("acPasstoken")] public string Passtoken { get; init; }
    [JsonPropertyName("error_msg")] public string ErrorMsg { get; init; }
}

public readonly record struct User(long Id, string Username, string Avatar, string Passtoken, DateTime Expires)
{
    public const string CookieKuaishouDomain = ".kuaishouzt.com";
    public const string CookieUserId = "userId";

    public const string CookieAcFunDomain = ".acfun.cn";
    public const string CookieId = "auth_key";
    public const string CookieUsername = "ac_username";
    public const string CookieAvatar = "ac_userimg";
    public const string CookiePasstoken = "acPasstoken";
    public static readonly Uri CookieUri = new($"http://{CookieAcFunDomain[1..]}");
}