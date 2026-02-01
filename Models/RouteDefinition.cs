namespace TfNSWOpenData.Models
{
    public class RouteDefinition
    {
        /// <summary>
        /// Public route code (T1, T9, M1, L2, B333, etc)
        /// </summary>
        public string RouteCode { get; init; }

        /// <summary>
        /// GTFS route type (2=train, 1=metro, 0=tram, 3=bus, etc)
        /// </summary>
        public int RouteType { get; init; }

        /// <summary>
        /// All stops serviced by this route
        /// </summary>
        public HashSet<string> StopIds { get; init; } = new();

        /// <summary>
        /// Full descriptive name of the route
        /// </summary>
        public string RouteLongName { get; init; }
    }
}
