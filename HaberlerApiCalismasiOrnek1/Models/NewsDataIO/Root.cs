namespace HaberlerApiCalismasiOrnek1.Models.NewsDataIO
{
    public class Root
    {
        public string status { get; set; }
        public int totalResults { get; set; }
        public List<Result> results { get; set; }
        public string nextPage { get; set; }
    }
}
