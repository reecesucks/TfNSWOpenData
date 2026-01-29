namespace TfNSWOpenData.Models.Generated
{
    public class Transportation
    {
        public string id { get; set; }
        public string name { get; set; }
        public string disassembledName { get; set; }
        public string number { get; set; }
        public string description { get; set; }
        public Product product { get; set; }
        public Operator @operator { get; set; }
        public Destination destination { get; set; }
        public Properties properties { get; set; }
        public Origin origin { get; set; }
        public int iconId { get; set; }
    }
}
