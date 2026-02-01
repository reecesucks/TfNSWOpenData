namespace TfNSWOpenData.Data
{
    public class StopTime
    {
        public string TripId { get; set; } = default!;
        public string? ArrivalTime { get; set; }
        public string? DepartureTime { get; set; }
        public string StopId { get; set; } = default!;
        public int? StopSequence { get; set; }
        public string? StopHeadsign { get; set; }
        public int? PickupType { get; set; }
        public int? DropOffType { get; set; }
        public double? ShapeDistTraveled { get; set; }
        public int? Timepoint { get; set; }
        public string? StopNote { get; set; }
    }
}
