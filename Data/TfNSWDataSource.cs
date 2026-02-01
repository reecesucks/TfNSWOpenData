using CsvHelper;
using System.Globalization;

namespace TfNSWOpenData.Data
{
    public static class TfNSWDataSource
    {
        private static readonly string _readDataPath =
            @"C:\Users\reece\source\repos\TfNSWOpenData\Data\gtfs\";

        static TfNSWDataSource()
        {
            LoadStops();
            LoadRoutes();
            LoadTrips();
            LoadStopTimes();
            BuildRouteStopIds();
            BuildParentChildIndex();
        }

        public static IReadOnlyList<Stop> Stops { get; private set; } = new List<Stop>();


        public static IReadOnlyDictionary<string, List<string>> ParentToChildren { get; private set; } =
            new Dictionary<string, List<string>>();

        public static IReadOnlyDictionary<string, Route> RoutesById { get; private set; } =
            new Dictionary<string, Route>();

        public static IReadOnlyList<Trip> Trips { get; private set; } = new List<Trip>();
        public static IReadOnlyList<StopTime> StopTimes { get; private set; } = new List<StopTime>();


        private static void LoadStops()
        {
            var path = Path.Combine(_readDataPath, "stops.txt");

            using var reader = new StreamReader(path);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

            csv.Context.RegisterClassMap<StopMap>();

            Stops = csv.GetRecords<Stop>()
                       .Where(s => !string.IsNullOrWhiteSpace(s.StopId))
                       .ToList();
        }

        private static void LoadRoutes()
        {
            var path = Path.Combine(_readDataPath, "routes.txt");

            if (!File.Exists(path))
            {
                RoutesById = new Dictionary<string, Route>();
                return;
            }

            using var reader = new StreamReader(path);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

            csv.Context.RegisterClassMap<RouteMap>();

            RoutesById = csv.GetRecords<Route>()
                            .Where(r => !string.IsNullOrWhiteSpace(r.RouteId))
                            .ToDictionary(r => r.RouteId, StringComparer.Ordinal);
        }
        public static void BuildRouteStopIds()
        {
            var tripToRoute = Trips.ToDictionary(t => t.TripId, t => t.RouteId);

            foreach (var stopTime in StopTimes)
            {

                if (!tripToRoute.TryGetValue(stopTime.TripId, out var routeId))
                    continue; // skip unknown trips

                if (!RoutesById.TryGetValue(routeId, out var route))
                    continue; // skip unknown routes

                route.StopIds.Add(stopTime.StopId);
            }
        }
        private static void BuildParentChildIndex()
        {
            ParentToChildren = Stops
                .Where(s => !string.IsNullOrEmpty(s.ParentStation))
                .GroupBy(s => s.ParentStation!)
                .ToDictionary(g => g.Key, g => g.Select(s => s.StopId).ToList());
        }

        public static void LoadTrips()
        {
            var path = Path.Combine(_readDataPath, "trips.txt");

            if (!File.Exists(path))
            {
                Trips = new List<Trip>();
                return;
            }

            using var reader = new StreamReader(path);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

            csv.Context.RegisterClassMap<TripMap>();

            var tripsList = new List<Trip>();

            foreach (var trip in csv.GetRecords<Trip>())
            {
                try
                {
                    if (!string.IsNullOrWhiteSpace(trip.TripId))
                    {
                        tripsList.Add(trip);
                    }
                }
                catch (CsvHelper.TypeConversion.TypeConverterException ex)
                {

                }
            }

            Trips = tripsList;
        }

        public static void LoadStopTimes()
        {
            var path = Path.Combine(_readDataPath, "stop_times.txt");

            if (!File.Exists(path))
            {
                StopTimes = new List<StopTime>();
                return;
            }

            using var reader = new StreamReader(path);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

            csv.Context.RegisterClassMap<StopTimeMap>();

            var stopTimesList = new List<StopTime>();

            foreach (var stopTime in csv.GetRecords<StopTime>())
            {
                try
                {
                    if (!string.IsNullOrWhiteSpace(stopTime.TripId) && !string.IsNullOrWhiteSpace(stopTime.StopId))
                    {
                        stopTimesList.Add(stopTime);
                    }
                }
                catch (CsvHelper.TypeConversion.TypeConverterException ex)
                {

                }
            }

            StopTimes = stopTimesList;
        }

        /// <summary>
        /// Returns all parent stations that have at least one metro platform.
        /// </summary>
        public static List<Stop> GetStopsByRouteEnums(IEnumerable<string> routeEnums)
        {
            var routeEnumSet = new HashSet<string>(routeEnums);

            var test = RoutesById.Where(x => routeEnumSet.Any(e => x.Key.Contains($"-{e}-"))).ToDictionary();
            var vals = test.Values;
            var stps = vals.Select(x => x.StopIds);

            var platformStops = Stops.Where(s => s.LocationType == 0).ToList();
            platformStops = Stops.ToList();
            var matchingPlatformStops = platformStops
                .Where(s => routeEnumSet.Any(e => s.StopId.Contains($"-{e}-")))
                .ToList();

            var parentStationIds = matchingPlatformStops
                .Where(s => !string.IsNullOrEmpty(s.ParentStation))
                .Select(s => s.ParentStation!)
                .Distinct()
                .ToList();

            var parentStops = Stops
                .Where(s => s.LocationType == 1 && parentStationIds.Contains(s.StopId))
                .ToList();

            return parentStops;
        }

        public static List<Route> GetRoutesByStopIds(List<string> stopIds)
        {
            var childStopIds = TfNSWDataSource.Stops
                .Where(stop => stopIds.Contains(stop.StopId))
                .SelectMany(stop => stop.ChildStops)
                .Where(child => !string.IsNullOrWhiteSpace(child.PlatformCode))
                .Select(child => child.StopId)
                .ToHashSet();

            var matchingRoutes = RoutesById.Values
                .Where(route => route.StopIds.Overlaps(childStopIds))
                .ToList();

            return matchingRoutes;
        }
    }
}
