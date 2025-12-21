namespace TfNSWOpenData.Models
{
    public class Parent
    {
        public string id { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public bool isGlobalId { get; set; }
        public string disassembledName { get; set; }
        public Parent parent { get; set; }
        public Properties properties { get; set; }
    }
}
