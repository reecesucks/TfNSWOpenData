using System.Reflection;
using System.Text.Json;
using TfNSWOpenData.Enums;
using TfNSWOpenData.Models;

namespace TfNSWOpenData.Data
{
    public static class TransportStations
    {
        private static List<Station>? _stations;

        static TransportStations()
        {
            try
            {
                var assembly = Assembly.GetExecutingAssembly();
                var resourceName = "TfNSWOpenData.Data.stations.json";

                using var stream = assembly.GetManifestResourceStream(resourceName)
                    ?? throw new FileNotFoundException($"Embedded resource {resourceName} not found.");

                using var reader = new StreamReader(stream);
                var json = reader.ReadToEnd();
                _stations = JsonSerializer.Deserialize<List<Station>>(json)!;
            }
            catch (Exception ex)
            {
                _stations = new List<Station>();
            }
        }

        public static List<Station> GetStations(TransportMode mode) =>
            _stations.Where(s => s.Modes.Contains(mode)).ToList();


        public static List<Station> GetAllStations()
        {
            if (_stations == null)
            {
                try
                {
                    var json = File.ReadAllText("Data/stations.json");
                    _stations = JsonSerializer.Deserialize<List<Station>>(json)!;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error loading stations.json: " + ex);
                    _stations = new List<Station>();
                }
            }

            return _stations;
        }
    }

}
