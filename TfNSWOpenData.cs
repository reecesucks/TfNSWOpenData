using TfNSWOpenData.Data;
using TfNSWOpenData.Models.Generated;
using TfNSWOpenData.Models.Requests;
using TfNSWOpenData.TfNSW.Client;

namespace TfNSWOpenData
{
    public sealed class TfNSWOpenData
    {
        private readonly ITfNSWClient _client;

        public string GetLineColor(string line)
        {
            if (Enum.TryParse<LineColors>(line, out var lineEnum))
            {
                string color = lineEnum.GetColor();
                return color;
            }
            return null;
        }

        public TfNSWOpenData(ITfNSWClient client)
        {
            _client = client;
        }

        public Task<StopFinderResponse> FindStopAsync(Dictionary<string, string> parameters, int maxResults = 10, CancellationToken cancellationToken = default) 
        {
            return _client.FindStopAsync(parameters,  cancellationToken);
        }

        public async Task<DepartureMonitorResponse> GetDepartureMonitorAsync(string stopId, 
            string outputFormat = "rapidJSON",
            string coordOutputFormat = "EPSG:4326",
            string mode = "direct",
            string type_dm = "stop",
            string depArrMacro = "dep",
            DateTime? idtDateTime = null,
            bool TfNSWDM = true,
            int limit = 15,
            CancellationToken cancellationToken = default)
        {
            var dt = idtDateTime ?? DateTime.Now;
            var paramteters = new Dictionary<string, string>
            {
                ["outputFormat"] = outputFormat,
                ["coordOutputFormat"] = coordOutputFormat,
                ["mode"] = mode,
                ["type_dm"] = type_dm,
                ["name_dm"] = stopId,
                ["depArrMacro"] = depArrMacro,
                ["itdDate"] = dt.ToString("yyyyMMdd"),
                ["itdTime"] = dt.ToString("HHmm"),
                ["TfNSWDM"] = TfNSWDM.ToString(),
                ["limit"] = limit.ToString()
            };

            return await GetDepartureMonitorAsync(paramteters, cancellationToken);
        }
        public async Task<DepartureMonitorResponse> GetDepartureMonitorAsync(Dictionary<string, string> parameters, CancellationToken cancellationToken = default)
        {
            return await _client.GetDepartureMonitorAsync(parameters, cancellationToken);
        }

        public async Task<AdditionalInfoResponse> GetAdditionalInfoAsync(AdditionalInfoRequest request,
                                                                        CancellationToken cancellationToken = default)
        {
            return await _client.GetAdditionalInfoAsync(request, cancellationToken);
        }

        public static void CreateDataFromCSVFiles()
        {
            //var test = new GtfsLoader();
            ////test.SaveStationsJson();
            //test.SaveRoutesJson();

            var routes = Routes.GetAllRoutes();
            var station = TransportStations.GetStations(Enums.TransportMode.Metro);
        }
    }
}
