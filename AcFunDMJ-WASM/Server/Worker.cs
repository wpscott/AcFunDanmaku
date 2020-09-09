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

        public Live[] liveList { get; set; }

        public struct Live
        {
            public long authorId { get; set; }
            public string title { get; set; }
        }
    }

    public class Worker : IHostedService, IDisposable
    {
        private const string UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/83.0.4103.61 Safari/537.36";
        private const string AcceptEncoding = "gzip, deflate, br";

        private readonly ILogger<Worker> _logger;
        private readonly IHubContext<DanmakuHub, IDanmaku> _hub;
        private readonly IConfiguration _configuration;
        private readonly HashSet<long> MonitorIds = new HashSet<long>();
        private readonly Dictionary<long, AcFunDanmu.Client> Monitoring = new Dictionary<long, AcFunDanmu.Client>();
        private Timer _timer;

        public Worker(ILogger<Worker> logger, IConfiguration configuration, IHubContext<DanmakuHub, IDanmaku> hub)
        {
            _logger = logger;
            _configuration = configuration;
            _hub = hub;
            foreach (var avup in _configuration.GetSection("AVUP").GetChildren())
            {
                MonitorIds.Add(avup.GetValue<long>("Id"));
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
            foreach (var client in Monitoring)
            {
                _ = client.Value.Stop("dispose");
            }
            Monitoring.Clear();
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }

        private async void StartMonitor(object state)
        {
            _logger.LogInformation("Fech live list");
            using var client = new HttpClient(new HttpClientHandler { AutomaticDecompression = System.Net.DecompressionMethods.All });
            client.DefaultRequestHeaders.UserAgent.ParseAdd(UserAgent);
            client.DefaultRequestHeaders.AcceptEncoding.ParseAdd(AcceptEncoding);

            using var resp = await client.GetAsync(Channel.Url);
            var list = await JsonSerializer.DeserializeAsync<Channel>(await resp.Content.ReadAsStreamAsync());
            var available = list.liveList.Where(live => MonitorIds.Contains(live.authorId) && !Monitoring.ContainsKey(live.authorId));
            _logger.LogInformation($"Found {available.Count()} up(s)");
            foreach (var live in available)
            {
                Monitor(live.authorId);
            }
        }

        private async void Monitor(long Id)
        {
            _logger.LogInformation($"Connect to {Id}");
            var client = new AcFunDanmu.Client();
            client.DedicatedHandler += HandleSignal;

            await client.Initialize($"{Id}");
            Monitoring.Add(Id, client);
            _logger.LogInformation($"Start monitoring {Id}");
            await client.Start();
            _logger.LogInformation($"End monitoring {Id}");
            Monitoring.Remove(Id);
        }

        private void HandleSignal(string avupId, string messagetType, ByteString payload)
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
                                    _hub.Clients.Group(avupId).SendComment(new Comment { Name = comment.UserInfo.Nickname, Content = comment.Content });
                                    _logger.LogDebug(comment.ToString());
                                }
                                break;
                            case PushMessage.ActionSignal.LIKE:
                                foreach (var pl in item.Payload)
                                {
                                    var like = CommonActionSignalLike.Parser.ParseFrom(pl);
                                    _hub.Clients.Group(avupId).SendLike(new Like { Name = like.UserInfo.Nickname });
                                    _logger.LogDebug(like.ToString());
                                }
                                break;
                            case PushMessage.ActionSignal.ENTER_ROOM:
                                foreach (var pl in item.Payload)
                                {
                                    var enter = CommonActionSignalUserEnterRoom.Parser.ParseFrom(pl);
                                    _hub.Clients.Group(avupId).SendEnter(new Enter { Name = enter.UserInfo.Nickname });
                                    _logger.LogDebug(enter.ToString());
                                }
                                break;
                            case PushMessage.ActionSignal.FOLLOW:
                                foreach (var pl in item.Payload)
                                {
                                    var follower = CommonActionSignalUserFollowAuthor.Parser.ParseFrom(pl);
                                    _hub.Clients.Group(avupId).SendFollow(new Follow { Name = follower.UserInfo.Nickname });
                                    _logger.LogDebug(follower.ToString());
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
                                    _hub.Clients.Group(avupId).SendGift(new Gift { Name = gift.User.Nickname, ComboId = gift.ComboId, Count = gift.Combo, Detail = new Gift.GiftInfo { Name = info.Name, Pic = info.Pic } });
                                    _logger.LogDebug(gift.ToString());
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
                                    _hub.Clients.Group(avupId).SendComment(new Comment { Name = comment.UserInfo.Nickname, Content = comment.Content });
                                    _logger.LogDebug(comment.ToString());
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
