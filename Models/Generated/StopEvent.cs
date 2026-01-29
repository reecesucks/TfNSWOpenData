namespace TfNSWOpenData.Models.Generated
{
    public class StopEvent
    {
        public List<string> realtimeStatus { get; set; }
        public bool isRealtimeControlled { get; set; }
        public Location location { get; set; }
        public DateTime departureTimePlanned { get; set; }
        public DateTime departureTimeBaseTimetable { get; set; }
        public DateTime departureTimeEstimated { get; set; }
        public Transportation transportation { get; set; }
        public List<Info> infos { get; set; }
        public Properties properties { get; set; }
    }
}
