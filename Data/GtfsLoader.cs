using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using System.Reflection;
using System.Text.Json;
using TfNSWOpenData.Enums;
using TfNSWOpenData.Models;

namespace TfNSWOpenData.Data
{
    public class GtfsLoader
    {
        private readonly string _readDataPath;
        private readonly string _storeDataPath;

        public GtfsLoader(string dataPath = "Data/gtfs")
        {

            var folder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Dashboard");
            Directory.CreateDirectory(folder);
            var dataFolder = Path.Combine(folder, "Data");
            Directory.CreateDirectory(dataFolder);


            var filePath = Path.Combine(folder, "data.json");
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "TfNSWOpenData";

            _storeDataPath = dataFolder;
            _readDataPath = "C:\\Users\\reece\\source\\repos\\TfNSWOpenData\\Data\\gtfs\\";
        }

        //public record Stop
        //{
        //    public string stop_id { get; init; } = "";
        //    public string stop_name { get; init; } = "";
        //    public double stop_lat { get; init; }
        //    public double stop_lon { get; init; }
        //    public int? location_type { get; init; }   // nullable now
        //    public string? parent_station { get; init; }
        //}
        //public sealed class StopMap : CsvHelper.Configuration.ClassMap<Stop>
        //{
        //    public StopMap()
        //    {
        //        Map(m => m.stop_id).Name("stop_id");
        //        Map(m => m.stop_name).Name("stop_name");
        //        Map(m => m.stop_lat).Name("stop_lat");
        //        Map(m => m.stop_lon).Name("stop_lon");
        //        Map(m => m.location_type)
        //            .Name("location_type")
        //            .Optional();               // allow empty
        //        Map(m => m.parent_station).Name("parent_station");
        //    }
        //}
        record Route(string route_id, int route_type);
        record Trip(string trip_id, string route_id);
        record StopTime(string trip_id, string stop_id);

        //public List<Station> LoadTrainAndMetroStationsOptimized()
        //{
        //    var names = new List<String>();

        //    // 1️⃣ Load stops, routes, trips
        //    var stops = ReadCsv<Stop>(Path.Combine(_readDataPath, "stops.txt"), new StopMap());
        //    var routes = ReadCsv<Route>(Path.Combine(_readDataPath, "routes.txt"));
        //    var trips = ReadCsv<Trip>(Path.Combine(_readDataPath, "trips.txt"));

        //    // 2️⃣ Build dictionaries for fast lookup
        //    var routeTypeByRouteId = routes.ToDictionary(r => r.route_id, r => r.route_type);
        //    var routeTypeByTripId = trips
        //        .Where(t => routeTypeByRouteId.ContainsKey(t.route_id))
        //        .ToDictionary(t => t.trip_id, t => routeTypeByRouteId[t.route_id]);

        //    // 3️⃣ Build stop_id -> set of TransportModes by streaming stop_times.txt
        //    var stopModes = new Dictionary<string, HashSet<TransportMode>>();
        //    using (var reader = new StreamReader(Path.Combine(_readDataPath, "stop_times.txt")))
        //    using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
        //    {
        //        HeaderValidated = null,
        //        MissingFieldFound = null,
        //        BadDataFound = null
        //    }))
        //    {
        //        foreach (var st in csv.GetRecords<StopTime>())
        //        {
        //            if (!routeTypeByTripId.TryGetValue(st.trip_id, out var routeType))
        //                continue;

        //            var mode = MapMode(routeType);

        //            // Only care about Train & Metro
        //            if (mode != TransportMode.Train && mode != TransportMode.Metro)
        //                continue;

        //            if (!stopModes.TryGetValue(st.stop_id, out var set))
        //            {
        //                set = new HashSet<TransportMode>();
        //                stopModes[st.stop_id] = set;
        //            }
        //            set.Add(mode);
        //        }
        //    }

        //    var platformsByStation = new Dictionary<string, List<string>>();
        //    foreach (var stop in stops)
        //    {
        //        if (!string.IsNullOrEmpty(stop.parent_station))
        //        {
        //            if (!platformsByStation.TryGetValue(stop.parent_station, out var list))
        //            {
        //                list = new List<string>();
        //                platformsByStation[stop.parent_station] = list;
        //            }
        //            list.Add(stop.stop_id);
        //        }
        //    }

        //    var stationStops = stops.Where(s => (s.location_type ?? 0) == 1);
        //    var result = new List<Station>();

        //    foreach (var station in stationStops)
        //    {
        //        if (!platformsByStation.TryGetValue(station.stop_id, out var platforms))
        //            continue;

        //        var modes = new HashSet<TransportMode>();

        //        foreach (var platformId in platforms)
        //            if (stopModes.TryGetValue(platformId, out var platformModes))
        //                modes.UnionWith(platformModes);

        //        var filteredModes = modes.Where(m => m == TransportMode.Train || m == TransportMode.Metro).ToList();
                
        //        if (!filteredModes.Any() || names.Contains(station.stop_name))
        //            continue;
        //        names.Add(station.stop_name);
        //        result.Add(new Station
        //        {
        //            Id = station.stop_id,
        //            Name = station.stop_name,
        //            Modes = filteredModes,
        //            Latitude = station.stop_lat,
        //            Longitude = station.stop_lon,
        //            City = null
        //        });
        //    }

        //    return result;
        //}

        static List<T> ReadCsv<T>(string path, ClassMap<T>? map = null)
        {
            using var reader = new StreamReader(path);
            using var csv = new CsvHelper.CsvReader(reader,
                new CsvHelper.Configuration.CsvConfiguration(
                    System.Globalization.CultureInfo.InvariantCulture)
                {
                    HeaderValidated = null,
                    MissingFieldFound = null,
                    BadDataFound = null
                });

            if (map != null)
                csv.Context.RegisterClassMap(map);

            return csv.GetRecords<T>().ToList();
        }

        /// <summary>
        /// Maps GTFS route_type to your custom enum.
        /// </summary>
        //private TransportMode MapMode(int routeType) => routeType switch
        //{
        //    0 => TransportMode.Train,       // Sydney Trains suburban lines
        //    1 => TransportMode.Bus,         // Standard buses
        //    2 => TransportMode.Coach,       // Long-distance coaches
        //    3 => TransportMode.Tram,        // Tram / Light Rail (older feeds)
        //    4 => TransportMode.Ferry_Low,   // Minor / less common ferry type
        //    5 => TransportMode.LightRail,   // Sydney Light Rail lines
        //    6 => TransportMode.Cable,       // Cable / Funicular
        //    7 => TransportMode.Ferry,       // Common ferries (Sydney Harbour)
        //    8 => TransportMode.SchoolBus,   // School Bus
        //    9 => TransportMode.Reserved9,
        //    10 => TransportMode.Reserved10,
        //    11 => TransportMode.Metro,      // Sydney Metro
        //    _ => TransportMode.Unknown
        //};
        private TransportMode MapMode(int routeType) => routeType switch
        {
            0 => TransportMode.Train,       // Sydney Trains suburban lines
            1 => TransportMode.Bus,         // Standard buses
           // 2 => TransportMode.Coach,       // Long-distance coaches
            3 => TransportMode.Tram,        // Tram / Light Rail (older feeds)
            4 => TransportMode.Ferry_Low,   // Minor / less common ferry type
            5 => TransportMode.LightRail,   // Sydney Light Rail lines
            6 => TransportMode.Cable,       // Cable / Funicular
            7 => TransportMode.Ferry,       // Common ferries (Sydney Harbour)
            8 => TransportMode.SchoolBus,   // School Bus
            9 => TransportMode.Reserved9,
            10 => TransportMode.Reserved10,
            11 => TransportMode.Metro,
            401 => TransportMode.Metro,
            2 => TransportMode.Train,
            // Sydney Metro
            _ => TransportMode.Unknown
        };

        private List<Dictionary<string, string>> ReadCsv(string path)
        {
            using var reader = new StreamReader(path);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            csv.Read();
            csv.ReadHeader();
            var records = new List<Dictionary<string, string>>();
            while (csv.Read())
            {
                var dict = csv.HeaderRecord.ToDictionary(h => h, h => csv.GetField(h));
                records.Add(dict);
            }
            return records;
        }

        public HashSet<string> GetRailStopIds()
        {
            var routes = ReadCsv(_readDataPath + "/routes.txt")
                .Where(r => r["route_type"] == "2" || r["route_type"] == "11")
                .Select(r => r["route_id"])
                .ToHashSet();

            var trips = ReadCsv(_readDataPath + "/trips.txt")
                .Where(t => routes.Contains(t["route_id"]))
                .Select(t => t["trip_id"])
                .ToHashSet();

            var stopIds = ReadCsv(_readDataPath + "/stop_times.txt")
                .Where(st => trips.Contains(st["trip_id"]))
                .Select(st => st["stop_id"])
                .ToHashSet();

            return stopIds;
        }

        public List<Station> LoadRailStations()
        {
            var railStopIds = GetRailStopIds();
            var stopsCsv = ReadCsv(_readDataPath + "/stops.txt");

            var stations = new List<Station>();
            var names = new List<string>();

            foreach (var row in stopsCsv)
            {
                // Only real stations
                if (row["location_type"] != "1")
                    continue;

                var stationId = row["stop_id"];

                // Does this station have any child stop used by rail?
                bool isRailStation = stopsCsv.Any(s =>
                    (s["parent_station"] == stationId || s["stop_id"] == stationId) &&
                    railStopIds.Contains(s["stop_id"])
                );

                if (!isRailStation || names.Contains(row["stop_name"]))
                    continue;

                names.Add(row["stop_name"]);

                stations.Add(new Station
                {
                    Id = stationId,
                    Name = row["stop_name"],
                    Latitude = double.Parse(row["stop_lat"], CultureInfo.InvariantCulture),
                    Longitude = double.Parse(row["stop_lon"], CultureInfo.InvariantCulture),
                });
            }

            return stations;
        }

        public Dictionary<string, RouteDefinition> BuildRoutes()
        {
            var routesPath = Path.Combine(_readDataPath, "routes.txt");
            var tripsPath = Path.Combine(_readDataPath, "trips.txt");
            var stopTimesPath = Path.Combine(_readDataPath, "stop_times.txt");

            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                IgnoreBlankLines = true,
                BadDataFound = null
            };

            // --- Load routes ---
            var routeIndex = new Dictionary<string, (string Code, int Type, string LongName)>();
            using (var reader = new StreamReader(routesPath))
            using (var csv = new CsvReader(reader, config))
            {
                csv.Read();
                csv.ReadHeader();
                while (csv.Read())
                {
                    var routeId = csv.GetField("route_id");
                    var routeShortName = csv.GetField("route_short_name");
                    var routeLongName = csv.GetField("route_long_name");

                    if (!int.TryParse(csv.GetField("route_type"), out var routeType))
                        continue;

                    // Filter train + metro
                    if ((routeType == 2 || routeType == 1) && !string.IsNullOrWhiteSpace(routeShortName))
                        routeIndex[routeId] = (routeShortName, routeType, LongName: routeLongName);
                }
            }

            // --- Load trips ---
            var tripsByRoute = new Dictionary<string, HashSet<string>>();
            using (var reader = new StreamReader(tripsPath))
            using (var csv = new CsvReader(reader, config))
            {
                csv.Read();
                csv.ReadHeader();
                while (csv.Read())
                {
                    var routeId = csv.GetField("route_id");
                    var tripId = csv.GetField("trip_id");

                    if (!routeIndex.ContainsKey(routeId))
                        continue;

                    if (!tripsByRoute.TryGetValue(routeId, out var tripSet))
                    {
                        tripSet = new HashSet<string>();
                        tripsByRoute[routeId] = tripSet;
                    }

                    tripSet.Add(tripId);
                }
            }

            // --- Load stop_times ---
            var stopsByTrip = new Dictionary<string, HashSet<string>>();
            using (var reader = new StreamReader(stopTimesPath))
            using (var csv = new CsvReader(reader, config))
            {
                csv.Read();
                csv.ReadHeader();
                while (csv.Read())
                {
                    var tripId = csv.GetField("trip_id");
                    var stopId = csv.GetField("stop_id");

                    if (!stopsByTrip.TryGetValue(tripId, out var stopSet))
                    {
                        stopSet = new HashSet<string>();
                        stopsByTrip[tripId] = stopSet;
                    }

                    stopSet.Add(stopId);
                }
            }

            // --- Build routes dictionary ---
            var result = new Dictionary<string, RouteDefinition>();

            foreach (var (routeId, meta) in routeIndex)
            {
                if (!tripsByRoute.TryGetValue(routeId, out var tripIds))
                    continue;

                if (!result.TryGetValue(meta.Code, out var route))
                {
                    route = new RouteDefinition
                    {
                        RouteCode = meta.Code,
                        RouteType = meta.Type,
                        RouteLongName = meta.LongName
                    };
                    result[meta.Code] = route;
                }

                foreach (var tripId in tripIds)
                {
                    if (!stopsByTrip.TryGetValue(tripId, out var stopIds))
                        continue;

                    foreach (var stopId in stopIds)
                        route.StopIds.Add(stopId);
                }
            }

            return result;
        }

        public void SaveRoutesJson(string outputPath = "routes.json")
        {
            // Build the routes dictionary
            var routes = BuildRoutes();

            // Serialize with indentation
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            var json = JsonSerializer.Serialize(routes, options);
            File.WriteAllText(Path.Combine(_storeDataPath, outputPath), json);
        }

        public void SaveStationsJson(string outputPath = "stations.json")
        {
            //var stations = LoadTrainAndMetroStationsOptimized();
            //File.WriteAllText(Path.Combine(_storeDataPath, outputPath), JsonSerializer.Serialize(stations, new JsonSerializerOptions { WriteIndented = true }));
        }
    }
}
