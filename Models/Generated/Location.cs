namespace TfNSWOpenData.Models.Generated
{
    public class Location
    {
        public string id { get; set; }
        public bool isGlobalId { get; set; }
        public string name { get; set; }
        public string disassembledName { get; set; }
        public List<double> coord { get; set; }
        public string type { get; set; }
        public int matchQuality { get; set; }
        public bool isBest { get; set; }
        public Parent parent { get; set; }
        public List<AssignedStop> assignedStops { get; set; }
        public Properties properties { get; set; }
    }
}
