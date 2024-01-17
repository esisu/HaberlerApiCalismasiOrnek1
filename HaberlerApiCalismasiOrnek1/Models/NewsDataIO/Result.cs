namespace HaberlerApiCalismasiOrnek1.Models.NewsDataIO
{
    public class Result
    {
        public string article_id { get; set; }
        public string title { get; set; }
        public string link { get; set; }
        public List<string> keywords { get; set; }
        public object creator { get; set; }
        public object video_url { get; set; }
        public string description { get; set; }
        public string content { get; set; }
        public string pubDate { get; set; }
        public string image_url { get; set; }
        public string source_id { get; set; }
        public int source_priority { get; set; }
        public List<string> country { get; set; }
        public List<string> category { get; set; }
        public string language { get; set; }
        public string ai_tag { get; set; }
        public string sentiment { get; set; }
        public string sentiment_stats { get; set; }
    }
}
