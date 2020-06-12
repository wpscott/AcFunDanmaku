using AcFunDanmuSongRequest.Platform.Interfaces;
using System;
using System.Threading.Tasks;

namespace AcFunDanmuSongRequest.Platform.QQ
{
    class QQPlatform : BasePlatform
    {
        private int Uin;
        private int Guid;
        private string VKey;
        private DateTime ExpireTime;

        public QQPlatform(Config config) : base(config)
        {
            var rnd = new Random();
            Guid = rnd.Next(16777216, int.MaxValue);
            Uin = rnd.Next(131072, int.MaxValue);
            ExpireTime = DateTime.Now;
        }
        public override async Task<ISong> AddSong(string keyword)
        {
            var result = await GetAsync<SearchResponse>(new SearchRequest { Keyword = keyword }, SearchResponse.Options);
            if (result.Data.Song.List.Length > 0)
            {
                var item = result.Data.Song.List[0];
                var song = new Song
                {
                    SongMid = item.Songmid,
                    Album = item.Albumname,
                    Artist = item.Singer[0].Name,
                    Name = item.Songname,
                    Duration = item.Interval
                };
                Songs.Enqueue(song);
                return song;
            }
            else
            {
                return null;
            }
        }

        public override async Task<ISong> NextSong()
        {
            if (Songs.Count > 0)
            {
                var song = (Song)Songs.Dequeue();

                var resp = await GetAsync<SongResponse>(new SongRequest(Uin, Guid, song.SongMid), SongResponse.Options);

                if (!string.IsNullOrEmpty(resp.Data.Data.MidUrlInfo[0].PUrl))
                {
                    if (resp.Data.Data.SIP.Length == 0)
                    {
                        song.Source = "http://isure.stream.qqmusic.qq.com/";
                    }
                    else
                    {
                        song.Source = resp.Data.Data.SIP[0];
                    }
                    song.Source += resp.Data.Data.MidUrlInfo[0].PUrl;
                    if (ExpireTime < DateTime.Now)
                    {
                        VKey = resp.Data.Data.MidUrlInfo[0].VKey;
                        ExpireTime = DateTime.Now.AddHours(2);
                    }
                    return song;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

    }
}
