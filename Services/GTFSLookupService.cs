using CsvHelper;
using System.Formats.Asn1;
using System.Globalization;
using TfNSWOpenData.Enums;
using TfNSWOpenData.Models;

namespace TfNSWOpenData.Services
{
    public class GTFSLookupService
    {
        private readonly Dictionary<string, Trip> _trips = new();
        private readonly Dictionary<string, Route> _routes = new();

        /// <summary>
        /// Load GTFS static CSVs (trips.txt and routes.txt)
        /// </summary>
        public void LoadGTFSData(string tripsPath, string routesPath)
        {
            // Load trips
            using var tripReader = new StreamReader(tripsPath);
            using var tripCsv = new CsvReader(tripReader, CultureInfo.InvariantCulture);
            var tripRecords = tripCsv.GetRecords<Trip>();
            foreach (var trip in tripRecords)
                _trips[trip.trip_id] = trip;

            // Load routes
            using var routeReader = new StreamReader(routesPath);
            using var routeCsv = new CsvReader(routeReader, CultureInfo.InvariantCulture);
            var routeRecords = routeCsv.GetRecords<Route>();
            foreach (var route in routeRecords)
                _routes[route.route_id] = route;
        }

        /// <summary>
        /// Resolve a StopEvent ServiceJourneyId to a TfNSWMode
        /// </summary>
        public TransportMode GetModeForStopEvent(string serviceJourneyId)
        {
            if (string.IsNullOrEmpty(serviceJourneyId))
                return TransportMode.Unknown;

            if (!_trips.TryGetValue(serviceJourneyId, out var trip))
                return TransportMode.Unknown;

            if (!_routes.TryGetValue(trip.route_id, out var route))
                return TransportMode.Unknown;

            return route.route_type switch
            {
                0 => TransportMode.Tram,
                1 => TransportMode.Train,
                3 => TransportMode.Bus,
                4 => TransportMode.Ferry,
                11 => TransportMode.Metro,
                _ => TransportMode.Unknown
            };
        }

        /// <summary>
        /// Resolve a list of ServiceJourneyIds to modes
        /// </summary>
        public Dictionary<string, TransportMode> GetModesForStopEvents(IEnumerable<string> serviceJourneyIds)
        {
            return serviceJourneyIds.ToDictionary(id => id, id => GetModeForStopEvent(id));
        }
    }
}
