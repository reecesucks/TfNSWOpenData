namespace TfNSWOpenData.Data
{
    public class Stop
    {
        public string StopId { get; set; } = default!;
        public string? StopCode { get; set; }
        public string StopName { get; set; } = default!;
        public double? StopLat { get; set; }
        public double? StopLon { get; set; }
        public int? LocationType { get; set; } 
        public string? ParentStation { get; set; }
        public int? WheelchairBoarding { get; set; }
        public string? LevelId { get; set; }
        public string? PlatformCode { get; set; }

        public List<Stop> ChildStops 
        {
            get
            {
                return TfNSWDataSource.Stops.Where(s => s.ParentStation == StopId).ToList();
            } 
        }
    }
}
