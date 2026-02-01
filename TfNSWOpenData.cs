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

        public async Task<DepartureMonitorResponse> GetDepartureMonitorAsync(Dictionary<string, string> parameters,
                                                                                CancellationToken cancellationToken = default)
        {
            return await _client.GetDepartureMonitorAsync(parameters, cancellationToken);
        }

        public async Task<AdditionalInfoResponse> GetAdditionalInfoAsync(AdditionalInfoRequest request,
                                                                        CancellationToken cancellationToken = default)
        {
            return await _client.GetAdditionalInfoAsync(request, cancellationToken);
        }

        public void CreateDataFromCSVFiles()
        {
            var test = new GtfsLoader();
            //test.SaveStationsJson();
            test.SaveRoutesJson();

            var routes = Routes.GetAllRoutes();
            var station = TransportStations.GetStations(Enums.TransportMode.Metro);
        }
    }
}
