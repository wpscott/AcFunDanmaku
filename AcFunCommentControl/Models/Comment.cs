using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web;

namespace AcFunCommentControl.Models
{
    public struct Comment
    {
        public int floor { get; set; }
        public long userId { get; set; }
        public string userName { get; set; }
        public string content { get; set; }

        public string Header => $"# {floor}\t{userName}({userId})";
        public string Content => HttpUtility.HtmlDecode(content);
    }

    struct CommentList
    {
        public string pcursor { get; set; }
        public int curPage { get; set; }
        public int totalPage { get; set; }
        public Comment[] rootComments
        {
            get; set;
        }
    }

    public static class CommentModel
    {
        private const int Count = 50;
        private const string FETCH_URL = "https://api-new.acfunchina.com/rest/app/comment/list?sourceId={0}&sourceType={1}&count={2}&pcursor={3}&supportZtEmot=true";
        private const string FETCH_COMMENT_URL = "https://www.acfun.cn/rest/pc-direct/comment/list?sourceId={0}&sourceType=3&page={1}&t={2}&supportZtEmot=true";
        private const string FETCH_MOMENT_URL = "https://api-new.acfunchina.com/rest/app/comment/list?sourceId={0}&sourceType=4&count=100&pcursor={1}&supportZtEmot=true";
        private const string FETCH_MOMENT_DETAIL = "https://api-new.acfunchina.com/rest/app/moment/detail?momentId={0}";
        static CommentModel()
        {

        }

        public static async ValueTask<Comment[]> Fetch(long acId, Action<int> progress)
        {
            return await Fetch(acId, 3, progress);
        }

        public static async ValueTask<Comment[]> FetchMoment(long momentId, Action<int> progress)
        {
            return await Fetch(momentId, 4, progress);
        }

        private static async ValueTask<Comment[]> Fetch(long id, int type, Action<int> progress)
        {
            IEnumerable<Comment> result = Array.Empty<Comment>();
            CommentList cList = new CommentList { pcursor = "0" };

            var count = 0;
            using var client = new HttpClient(new HttpClientHandler { AutomaticDecompression = System.Net.DecompressionMethods.All });
            client.DefaultRequestHeaders.AcceptEncoding.ParseAdd("gzip, deflate, br");
            do
            {
                using var list = await client.GetAsync(string.Format(FETCH_URL, id, type, Count, cList.pcursor));
                cList = await JsonSerializer.DeserializeAsync<CommentList>(await list.Content.ReadAsStreamAsync());

                result = result.Concat(cList.rootComments);
                count += cList.rootComments.Length;
                progress(count);
            } while (cList.pcursor != "no_more");

            return result.ToArray();
        }
    }
}
