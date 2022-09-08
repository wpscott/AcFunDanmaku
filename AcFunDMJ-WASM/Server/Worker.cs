using AcFunDanmu;
using AcFunDanmu.Enums;
using AcFunDMJ_WASM.Server.Hubs;
using AcFunDMJ_WASM.Shared;
using Google.Protobuf;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace AcFunDMJ_WASM.Server
{
    public struct Liver
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }

    struct Channel
    {
        public const string Url = "https://live.acfun.cn/api/channel/list";

        [JsonPropertyName("liveList")] public Live[] LiveList { get; set; }

        public struct Live
        {
            [JsonPropertyName("authorId")] public long AuthorId { get; set; }
            [JsonPropertyName("title")] public string Title { get; set; }
        }
    }

    public class Worker : IHostedService, IDisposable
    {
        private const string USER_AGENT =
            "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/83.0.4103.61 Safari/537.36";

        private const string ACCEPT_ENCODING = "gzip, deflate, br";

        private readonly ILogger<Worker> _logger;
        private readonly IHubContext<DanmakuHub, IDanmaku> _hub;
        private readonly IConfiguration _configuration;
        private readonly HashSet<long> _monitorIds = new();
        private readonly Dictionary<long, AcFunDanmu.Client> _monitoring = new();
        private Timer _timer;

        public Worker(ILogger<Worker> logger, IConfiguration configuration, IHubContext<DanmakuHub, IDanmaku> hub)
        {
            _logger = logger;
            _configuration = configuration;
            _hub = hub;
            foreach (var avup in _configuration.GetSection("AVUP").GetChildren())
            {
                _monitorIds.Add(avup.GetValue<long>("Id"));
            }
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(StartMonitor, null, TimeSpan.Zero, TimeSpan.FromSeconds(10));
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            foreach (var client in _monitoring)
            {
                client.Value.Stop("dispose");
            }

            _monitoring.Clear();
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing) return;
            _timer?.Dispose();
            _timer = null;
        }

        private async void StartMonitor(object state)
        {
            _logger.LogInformation("Fetch live list");
            using var client = new HttpClient(new HttpClientHandler
                { AutomaticDecompression = System.Net.DecompressionMethods.All });
            client.DefaultRequestHeaders.UserAgent.ParseAdd(USER_AGENT);
            client.DefaultRequestHeaders.AcceptEncoding.ParseAdd(ACCEPT_ENCODING);

            using var resp = await client.GetAsync(Channel.Url);
            var list = await JsonSerializer.DeserializeAsync<Channel>(await resp.Content.ReadAsStreamAsync());
            var available = list.LiveList.Where(live =>
                _monitorIds.Contains(live.AuthorId) && !_monitoring.ContainsKey(live.AuthorId)).ToArray();
            _logger.LogInformation("Found {Count} up(s)", available.Count());
            foreach (var live in available)
            {
                Monitor(live.AuthorId);
            }
        }

        private void Monitor(long id)
        {
            Task.Run(() =>
            {
                using EventWaitHandle ewh = new(false, EventResetMode.ManualReset);

                _logger.LogInformation("Connect to {Id}", id);
                var client = new AcFunDanmu.Client();
                client.Handler += HandleSignal;
                client.OnInitialize += () =>
                {
                    _monitoring.Add(id, client);
                    _logger.LogInformation("Start monitoring {Id}", id);
                };
                client.OnEnd += () =>
                {
                    _logger.LogInformation("End monitoring {Id}", id);
                    _monitoring.Remove(id);
                    ewh.Set();
                };
                client.Start(id);

                ewh.WaitOne();
            });
        }

        private void HandleSignal(AcFunDanmu.Client sender, string messagetType, ByteString payload)
        {
            switch (messagetType)
            {
                // Includes comment, gift, enter room, like, follower
                case PushMessage.ACTION_SIGNAL:
                    var actionSignal = ZtLiveScActionSignal.Parser.ParseFrom(payload);

                    foreach (var item in actionSignal.Item)
                    {
                        switch (item.SignalType)
                        {
                            case PushMessage.ActionSignal.COMMENT:
                                foreach (var pl in item.Payload)
                                {
                                    var comment = CommonActionSignalComment.Parser.ParseFrom(pl);
                                    _hub.Clients.Group(sender.Host).SendComment(new Comment
                                        { Name = comment.UserInfo.Nickname, Content = comment.Content });
                                    _logger.LogDebug("{Data}", comment.ToString());
                                }

                                break;
                            case PushMessage.ActionSignal.LIKE:
                                foreach (var pl in item.Payload)
                                {
                                    var like = CommonActionSignalLike.Parser.ParseFrom(pl);
                                    _hub.Clients.Group(sender.Host)
                                        .SendLike(new Like { Name = like.UserInfo.Nickname });
                                    _logger.LogDebug("{Data}", like.ToString());
                                }

                                break;
                            case PushMessage.ActionSignal.ENTER_ROOM:
                                foreach (var pl in item.Payload)
                                {
                                    var enter = CommonActionSignalUserEnterRoom.Parser.ParseFrom(pl);
                                    _hub.Clients.Group(sender.Host)
                                        .SendEnter(new Enter { Name = enter.UserInfo.Nickname });
                                    _logger.LogDebug("{Data}", enter.ToString());
                                }

                                break;
                            case PushMessage.ActionSignal.FOLLOW:
                                foreach (var pl in item.Payload)
                                {
                                    var follower = CommonActionSignalUserFollowAuthor.Parser.ParseFrom(pl);
                                    _hub.Clients.Group(sender.Host).SendFollow(new Follow
                                        { Name = follower.UserInfo.Nickname });
                                    _logger.LogDebug("{Data}", follower.ToString());
                                }

                                break;
                            case PushMessage.ActionSignal.THROW_BANANA:
                                //foreach (var pl in item.Payload)
                                //{
                                //    var banana = AcfunActionSignalThrowBanana.Parser.ParseFrom(pl);
                                //    UpdateDanmaku(new Banana { Name = banana.Visitor.Name, Count = banana.Count });
                                //}
                                break;
                            case PushMessage.ActionSignal.GIFT:
                                foreach (var pl in item.Payload)
                                {
                                    var gift = CommonActionSignalGift.Parser.ParseFrom(pl);
                                    var info = AcFunDanmu.Client.Gifts[gift.GiftId];
                                    _hub.Clients.Group(sender.Host).SendGift(new Gift
                                    {
                                        Name = gift.UserInfo.Nickname, ComboId = gift.ComboKey, Count = gift.BatchSize,
                                        Detail = new Gift.GiftInfo { Name = info.Name, Pic = info.Pic }
                                    });
                                    _logger.LogDebug("{Data}", gift.ToString());
                                }

                                break;
                            default:
                                break;
                        }
                    }

                    break;
                //Includes current banana counts, watching count, like count and top 3 users sent gifts
                case PushMessage.STATE_SIGNAL:
                    ZtLiveScStateSignal signal = ZtLiveScStateSignal.Parser.ParseFrom(payload);

                    foreach (var item in signal.Item)
                    {
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
                                {
                                    _hub.Clients.Group(sender.Host).SendComment(new Comment
                                        { Name = comment.UserInfo.Nickname, Content = comment.Content });
                                    _logger.LogDebug("{Data}", comment.ToString());
                                }

                                break;
                            default:
                                //                            var pi = Parse(item.SignalType, item.Payload);
                                //#if DEBUG
                                //                            Console.WriteLine("Unhandled state type: {0}, content: {1}", item.SignalType, pi);
                                //#endif
                                break;
                        }
                    }

                    break;
            }
        }
    }
}