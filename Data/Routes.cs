using System.Text.Json;
using TfNSWOpenData.Models;

namespace TfNSWOpenData.Data
{
    public class Routes
    {
        private static Dictionary<string, RouteDefinition>? _routes;
        private static string _filepath;
        static Routes()
        {
            try
            {
                var folder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Dashboard");
                var dataFolder = Path.Combine(folder, "Data");

                _filepath = Path.Combine(dataFolder, "routes.json");

                if (!File.Exists(_filepath))
                {
                    _routes = new Dictionary<string, RouteDefinition>(); // empty if no file
                    return;
                }

                // Read JSON synchronously
                var json = File.ReadAllText(_filepath);

                // Deserialize to List<RouteDefinition>
                _routes = JsonSerializer.Deserialize<Dictionary<string, RouteDefinition>>(json)
                          ?? new Dictionary<string, RouteDefinition>();
            }
            catch (Exception ex)
            {
                _routes = new Dictionary<string, RouteDefinition>();
            }
        }

        public static Dictionary<string, RouteDefinition> GetRoutes(
            IEnumerable<string>? stopIds = null)
        {
            if (stopIds == null || !stopIds.Any())
                return _routes;

            var stopIdSet = new HashSet<string>(stopIds);

            return _routes
                .Where(kvp => kvp.Value.StopIds.Any(stopIdSet.Contains))
                .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
        }

        public static Dictionary<string, RouteDefinition> GetAllRoutes()
        {
            if (_routes == null)
            {
                try
                {
                    var json = File.ReadAllText(_filepath);
                    _routes = JsonSerializer.Deserialize<Dictionary<string, RouteDefinition>>(json)!;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error loading routes.json: " + ex);
                    _routes = new Dictionary<string, RouteDefinition>();
                }
            }
            return _routes;
        }
    }
}
