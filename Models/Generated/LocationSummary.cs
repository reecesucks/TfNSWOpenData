namespace TfNSWOpenData.Models.Generated
{
    public class LocationSummary
    {
        public string id { get; set; }
        public bool isGlobalId { get; set; }
        public string name { get; set; }
        public string disassembledName { get; set; }
        public string type { get; set; }
        public List<double> coord { get; set; }
        public Properties properties { get; set; }
        public Parent parent { get; set; }
    }
}
