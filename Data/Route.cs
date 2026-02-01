namespace TfNSWOpenData.Data
{
    public class Route
    {
        public string RouteId { get; set; } = default!;

        public string? AgencyId { get; set; }

        public string? RouteShortName { get; set; }

        public string? RouteLongName { get; set; }

        public string? RouteDesc { get; set; }

        public int RouteType { get; set; }

        public string? RouteColor { get; set; }

        public string? RouteTextColor { get; set; }

        public int? ExactTimes { get; set; }

        public HashSet<string> StopIds { get; set; } = new HashSet<string>();
    }

}
