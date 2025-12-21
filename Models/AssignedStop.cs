namespace TfNSWOpenData.Models
{
    public class AssignedStop
    {
        public string id { get; set; }
        public bool isGlobalId { get; set; }
        public string name { get; set; }
        public string disassembledName { get; set; }
        public string type { get; set; }
        public List<double> coord { get; set; }
        public Parent parent { get; set; }
        public int connectingMode { get; set; }
        public Properties properties { get; set; }
        public List<int> modes { get; set; }
    }
}
