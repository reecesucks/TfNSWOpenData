namespace TfNSWOpenData.Data
{
    public class Trip
    {
        public string RouteId { get; set; } = default!;
        public string ServiceId { get; set; } = default!;
        public string TripId { get; set; } = default!;
        public string? ShapeId { get; set; }
        public string? TripHeadsign { get; set; }
        public int? DirectionId { get; set; }
        public string? BlockId { get; set; }
        public int? WheelchairAccessible { get; set; }
        public string? RouteDirection { get; set; }
        public string? TripNote { get; set; }
        public int? BikesAllowed { get; set; }
    }
}
