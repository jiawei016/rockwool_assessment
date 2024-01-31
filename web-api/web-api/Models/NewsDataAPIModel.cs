namespace web_api.Models
{
    public class NewsDataAPIModel
    {
        public string status { get; set; } = "";
        public long totalResults { get; set; } = 0;
        public Results results { get; set; } = new Results();

        public class Results
        {
            public string article_id { get; set; } = "";
            public string title { get; set; } = "";
            public string link { get; set; } = "";
            public string description { get; set; } = "";
            public string content { get; set; } = "";
            public string pubDate { get; set; } = "";
            public string image_url { get; set; } = "";
            public string[] country { get; set; } = new string[0];
            public string[] category { get; set; } = new string[0];
        }
    }
}
