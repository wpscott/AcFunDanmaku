﻿using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using AcFunDanmu;
using AcFunDanmu.Enums;
using AcFunDanmu.Models.Client;
using Google.Protobuf;

namespace AcFunDMJ.Vanilla;

internal static class Program
{
    private static readonly Encoding Encoding = Encoding.UTF8;

    private static readonly JsonSerializerOptions Options = new()
        { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

    private static WebSocket _ws;
    private static Client _danmaku;

    private static Config _config;

    private static async Task Main(string[] args)
    {
        _config = await Config.LoadConfig();
        var address = $"http://localhost:{_config.Port}/";
        using var server = new HttpListener();
        server.Prefixes.Add(address);
        server.Start();
        Console.WriteLine($"弹幕姬已启动，地址为{address}");
        while (true)
        {
            var ctx = await server.GetContextAsync();
            var path = ctx.Request.Url!.LocalPath[1..];

            switch (path)
            {
                case { } uid when long.TryParse(path, out _):
                    if (ctx.Request.IsWebSocketRequest)
                    {
                        var wsCtx = await ctx.AcceptWebSocketAsync(null);
                        Start(wsCtx.WebSocket, uid);
                    }
                    else
                    {
                        StaticFile(ctx.Response, "index.html", "text/html; charset=utf-8");
                    }

                    break;
                case { } when path.EndsWith(".htm"):
                case { } when path.EndsWith(".html"):
                    StaticFile(ctx.Response, path, "text/html; charset=utf-8");
                    break;
                case { } when path.EndsWith(".css"):
                    StaticFile(ctx.Response, path, "text/css; charset=utf-8");
                    break;
                case { } when path.EndsWith(".js"):
                    StaticFile(ctx.Response, path, "application/javascript; charset=utf-8");
                    break;
                case { } when path.EndsWith(".json"):
                    StaticFile(ctx.Response, path, "application/json; charset=utf-8");
                    break;
                case { } when path.EndsWith(".ttf"):
                    StaticFile(ctx.Response, path, "font/ttf");
                    break;
                case { } when path.EndsWith(".woff"):
                    StaticFile(ctx.Response, path, "font/woff");
                    break;
                case { } when path.EndsWith(".jpg"):
                case { } when path.EndsWith(".jpeg"):
                    StaticFile(ctx.Response, path, "image/jpeg");
                    break;
                case { } when path.EndsWith(".png"):
                    StaticFile(ctx.Response, path, "image/png");
                    break;
                case { } when path.EndsWith(".gif"):
                    StaticFile(ctx.Response, path, "image/gif");
                    break;
                default:
                    StaticFile(ctx.Response, Path.Combine(path, "index.html"), "text/html; charset=utf-8");
                    break;
            }
        }
    }

    private static async void StaticFile(HttpListenerResponse response, string path, string contentType)
    {
        if (File.Exists(path))
        {
            await using var stream = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

            response.StatusCode = 200;
            response.ContentType = contentType;
            response.ContentEncoding = Encoding;
            await stream.CopyToAsync(response.OutputStream);
        }
        else
        {
            response.StatusCode = 404;
        }

        response.Close();
    }

    private static async void Start(WebSocket websocket, string uid)
    {
        switch (_danmaku)
        {
            case null:
                _danmaku = new Client();
                _danmaku.Handler += HandleSignal;
                _danmaku.OnInitialize += () => { SendMessage(MessageType.Text, "正在初始化弹幕姬"); };
                _danmaku.OnStart += () => { SendMessage(MessageType.Text, "正在启动弹幕姬"); };
                _danmaku.OnEnd += () => { SendMessage(MessageType.Text, "直播已结束或连接已断开"); };
                break;
            case { IsRunning: true }:
                _danmaku.Stop("Disconnect");
                break;
        }

        if (_ws != null)
            try
            {
                await _ws.CloseAsync(WebSocketCloseStatus.Empty, string.Empty, default);
            }
            catch (WebSocketException)
            {
            }
            finally
            {
                _ws.Dispose();
                _ws = null;
            }

        _ws = websocket;
        SendMessage(MessageType.Text, $"正在连接到直播间：{uid}");
        _danmaku.Start(uid);
    }

    private static async void SendMessage(MessageType type, object obj)
    {
        try
        {
            if (_ws is { State: WebSocketState.Open })
            {
                var text = JsonSerializer.Serialize(new { Type = type, Obj = obj }, Options);
                await _ws.SendAsync(Encoding.GetBytes(text), WebSocketMessageType.Text, true, default);
            }
            else
            {
                _danmaku.Stop("Disconnect");
            }
        }
        catch (WebSocketException)
        {
        }
    }

    private static void HandleSignal(Client sender, string messageType, ByteString payload)
    {
        switch (messageType)
        {
            // Includes comment, gift, enter room, like, follower
            case PushMessage.ACTION_SIGNAL:
                var actionSignal = ZtLiveScActionSignal.Parser.ParseFrom(payload);
                foreach (var item in actionSignal.Item)
                    switch (item.SignalType)
                    {
                        case PushMessage.ActionSignal.COMMENT:
                            foreach (var pl in item.Payload)
                            {
                                var comment = CommonActionSignalComment.Parser.ParseFrom(pl);
                                SendMessage(MessageType.Comment,
                                    new Comment
                                    {
                                        Id = comment.UserInfo.UserId,
                                        Name = comment.UserInfo.Nickname,
                                        Avatar = comment.UserInfo.Avatar[0].Url,
                                        Content = comment.Content,
                                        Timestamp = comment.SendTimeMs
                                    });
                            }

                            break;
                        case PushMessage.ActionSignal.LIKE:
                            if (_config.ShowLike)
                                foreach (var pl in item.Payload)
                                {
                                    var like = CommonActionSignalLike.Parser.ParseFrom(pl);
                                    SendMessage(MessageType.Like,
                                        new Like
                                        {
                                            Id = like.UserInfo.UserId,
                                            Name = like.UserInfo.Nickname,
                                            Avatar = like.UserInfo.Avatar[0].Url,
                                            Timestamp = like.SendTimeMs,
                                        });
                                }

                            break;
                        case PushMessage.ActionSignal.ENTER_ROOM:
                            if (_config.ShowEnter)
                                foreach (var pl in item.Payload)
                                {
                                    var enter = CommonActionSignalUserEnterRoom.Parser.ParseFrom(pl);
                                    SendMessage(MessageType.Enter,
                                        new Enter
                                        {
                                            Id = enter.UserInfo.UserId,
                                            Name = enter.UserInfo.Nickname,
                                            Avatar = enter.UserInfo.Avatar[0].Url,
                                            Timestamp = enter.SendTimeMs,
                                        });
                                }

                            break;
                        case PushMessage.ActionSignal.FOLLOW:
                            if (_config.ShowFollow)
                                foreach (var pl in item.Payload)
                                {
                                    var follow = CommonActionSignalUserFollowAuthor.Parser.ParseFrom(pl);
                                    SendMessage(MessageType.Follow,
                                        new Follow
                                        {
                                            Id = follow.UserInfo.UserId,
                                            Name = follow.UserInfo.Nickname,
                                            Avatar = follow.UserInfo.Avatar[0].Url,
                                            Timestamp = follow.SendTimeMs,
                                        });
                                }

                            break;
                        case PushMessage.ActionSignal.THROW_BANANA:
                            //foreach (var pl in item.Payload)
                            //{
                            //    var banana = AcfunActionSignalThrowBanana.Parser.ParseFrom(pl);
                            //    SendMessage(new Banana { Name = banana.Visitor.Name, Avatar = banana.UserInfo.Avatar[0].Url, Count = banana.Count });
                            //}
                            break;
                        case PushMessage.ActionSignal.GIFT:
                            foreach (var pl in item.Payload)
                            {
                                var gift = CommonActionSignalGift.Parser.ParseFrom(pl);
                                if (_config.GiftList.Contains(gift.GiftId)) continue;
                                var info = Client.Gifts[gift.GiftId];
                                SendMessage(MessageType.Gift,
                                    new Gift
                                    {
                                        Id = gift.UserInfo.UserId,
                                        Name = gift.UserInfo.Nickname,
                                        Avatar = gift.UserInfo.Avatar[0].Url,
                                        Timestamp = gift.SendTimeMs,
                                        ComboId = gift.ComboKey,
                                        Count = gift.BatchSize,
                                        Value = gift.Rank,
                                        Combo = gift.ComboCount,
                                        Detail = info
                                    });
                            }

                            break;
                    }

                break;
            //Includes current banana counts, watching count, like count and top 3 users sent gifts
            case PushMessage.STATE_SIGNAL:
                var signal = ZtLiveScStateSignal.Parser.ParseFrom(payload);

                foreach (var item in signal.Item)
                    switch (item.SignalType)
                    {
                        case PushMessage.StateSignal.ACFUN_DISPLAY_INFO:
                            //var acInfo = AcfunStateSignalDisplayInfo.Parser.ParseFrom(item.Payload);
                            //Console.WriteLine("Current banada count: {0}", acInfo.BananaCount);
                            break;
                        case PushMessage.StateSignal.DISPLAY_INFO:
                            //var stateInfo = CommonStateSignalDisplayInfo.Parser.ParseFrom(item.Payload);
                            //Console.WriteLine("{0} watching, {1} likes", stateInfo.WatchingCount, stateInfo.LikeCount);
                            break;
                        case PushMessage.StateSignal.TOP_USRES:
                            //var users = CommonStateSignalTopUsers.Parser.ParseFrom(item.Payload);
                            //Console.WriteLine("Top 3 users: {0}", string.Join(", ", users.User.Select(user => user.Detail.Name)));
                            break;
                        case PushMessage.StateSignal.RECENT_COMMENT:
                            var comments = CommonStateSignalRecentComment.Parser.ParseFrom(item.Payload);
                            foreach (var comment in comments.Comment)
                                SendMessage(MessageType.Comment,
                                    new Comment
                                    {
                                        Id = comment.UserInfo.UserId,
                                        Name = comment.UserInfo.Nickname,
                                        Avatar = comment.UserInfo.Avatar[0].Url,
                                        Content = comment.Content,
                                        Timestamp = comment.SendTimeMs
                                    });

                            break;
                    }

                break;
        }
    }

    private enum MessageType
    {
        Comment,
        Follow,
        Like,
        Enter,
        Gift,
        Banana,
        Text = 99
    }

    private interface IMessage
    {
        public long Id { get; init; }
        public string Name { get; init; }
        public string Avatar { get; init; }
        public long Timestamp { get; init; }
    }

    private struct Comment : IMessage
    {
        public long Id { get; init; }
        public string Name { get; init; }
        public string Avatar { get; init; }
        public long Timestamp { get; init; }
        public string Content { get; init; }
    }

    private struct Like : IMessage
    {
        public long Id { get; init; }
        public string Name { get; init; }
        public string Avatar { get; init; }
        public long Timestamp { get; init; }
    }

    private struct Enter : IMessage
    {
        public long Id { get; init; }
        public string Name { get; init; }
        public string Avatar { get; init; }
        public long Timestamp { get; init; }
    }

    private struct Follow : IMessage
    {
        public long Id { get; init; }
        public string Name { get; init; }
        public string Avatar { get; init; }
        public long Timestamp { get; init; }
    }

    private struct Banana : IMessage
    {
        public long Id { get; init; }
        public string Name { get; init; }
        public string Avatar { get; init; }
        public long Timestamp { get; init; }
        public int Count { get; init; }
    }

    private struct Gift : IMessage
    {
        public long Id { get; init; }
        public string Name { get; init; }
        public string Avatar { get; init; }
        public long Timestamp { get; init; }
        public string ComboId { get; init; }
        public int Count { get; init; }
        public int Combo { get; init; }
        public long Value { get; init; }
        public GiftInfo Detail { get; init; }
    }
}