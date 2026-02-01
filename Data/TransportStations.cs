using System.Text.Json;
using TfNSWOpenData.Enums;
using TfNSWOpenData.Models;

namespace TfNSWOpenData.Data
{
    public static class TransportStations
    {
        private static List<Station>? _stations;
        private static string _filepath;
        static TransportStations()
        {
            try 
            {
                var folder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Dashboard");
                var dataFolder = Path.Combine(folder, "Data");

                _filepath = Path.Combine(dataFolder, "stations.json");
                
                if (!File.Exists(_filepath))
                {
                    _stations = new List<Station>();
                    return;
                }

                var json = File.ReadAllText(_filepath);

                _stations = JsonSerializer.Deserialize<List<Station>>(json)
                          ?? new List<Station>();
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
                    var json = File.ReadAllText(_filepath);
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
