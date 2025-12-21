namespace TfNSWOpenData.Models
{
    public class Info
    {
        public string priority { get; set; }
        public string id { get; set; }
        public int version { get; set; }
        public string type { get; set; }
        public Properties properties { get; set; }
        public List<InfoLink> infoLinks { get; set; }
        public string urlText { get; set; }
        public string url { get; set; }
        public string content { get; set; }
        public string subtitle { get; set; }
    }
}
