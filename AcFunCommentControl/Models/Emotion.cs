using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text.Json;

namespace AcFunCommentControl.Models
{
    struct EmotionUrl
    {
        public string cdn { get; set; }
        public Uri url { get; set; }
    }
    struct Emotion
    {
        public string id { get; set; }
        public string packageId { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public EmotionUrl[] emotionImageBigUrl { get; set; }
        public EmotionUrl[] emotionImageSmallUrl { get; set; }

    }

    struct EmotIconList
    {
        public string id { get; set; }
        public string name { get; set; }
        public Emotion[] emotions { get; set; }
    }

    struct EmotIconListResult
    {
        public EmotIconList[] emotionPackageList { get; set; }
        public int result { get; set; }
    }

    public static class EmotIconModel
    {
        private const string EMOT_ICON_URL = "https://zt.gifshow.com/rest/zt/emoticon/package/list?kpn=ACFUN_APP";

        public static ReadOnlyDictionary<string, Uri> Emotions { get; private set; }

        static EmotIconModel()
        {

        }

        public static async void Fetch()
        {
            var dict = new Dictionary<string, Uri>();
            using var client = new HttpClient(new HttpClientHandler { AutomaticDecompression = System.Net.DecompressionMethods.All });
            client.DefaultRequestHeaders.AcceptEncoding.ParseAdd("gzip, deflate, br");

            using var emotIcon = await client.GetAsync(EMOT_ICON_URL);

            var emotions = await JsonSerializer.DeserializeAsync<EmotIconListResult>(await emotIcon.Content.ReadAsStreamAsync());

            if (emotions.result == 1)
            {
                foreach (var list in emotions.emotionPackageList)
                {
                    foreach (var emotion in list.emotions)
                    {
                        dict.Add(emotion.id, emotion.emotionImageBigUrl[0].url);
                    }
                }
                Emotions = new ReadOnlyDictionary<string, Uri>(dict);
            }
            Emotions = new ReadOnlyDictionary<string, Uri>(dict);
        }
    }
}
