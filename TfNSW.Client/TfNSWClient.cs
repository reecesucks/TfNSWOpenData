using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RestSharp;
using System.Net;
using TfNSWOpenData.Models;
using TfNSWOpenData.TfNSW.Client;

namespace TfNSWOpenData.API
{

    internal sealed class TfNSWClient : ITfNSWClient
    {
        private readonly HttpClient _http;
        private readonly IOptions<TfNSWOptions> _options;

        public TfNSWClient(HttpClient http, IOptions<TfNSWOptions> options)
        {
            _http = http ?? throw new ArgumentNullException(nameof(http));

            if (_http.BaseAddress == null)
                _http.BaseAddress = new Uri("https://api.transport.nsw.gov.au/v1/tp/");

            _options = options;

        }




        public async Task<StopFinderResponse> FindStopAsync(Dictionary<string, string> parameters, CancellationToken cancellationToken = default)
        {

            var url = UrlBuilder.WithQuery(_options.Value.BaseUri.ToString(), "tp/stop_finder", parameters);
            using var response = await _http.GetAsync(url, cancellationToken);
            response.EnsureSuccessStatusCode();

            await using var stream = await response.Content.ReadAsStreamAsync(cancellationToken);

            var jsonString = await response.Content.ReadAsStringAsync(cancellationToken);
            var result = JsonConvert.DeserializeObject<StopFinderResponse>(jsonString);

            return result
                ?? throw new InvalidOperationException(
                    "Stop finder returned no data.");
            return null;
        }

        public async Task<DepartureMonitorResponse> GetDepartureMonitorAsync(Dictionary<string, string> queryParams, CancellationToken cancellationToken = default)
        {
            var url = UrlBuilder.WithQuery(_options.Value.BaseUri.ToString(), "tp/departure_mon", queryParams);

            using var response = await _http.GetAsync(url, cancellationToken);
            var json = await response.Content.ReadAsStringAsync(cancellationToken);

            response.EnsureSuccessStatusCode();

            await using var stream = await response.Content.ReadAsStreamAsync(cancellationToken);

            var jsonString = await response.Content.ReadAsStringAsync(cancellationToken);
            var result = JsonConvert.DeserializeObject<DepartureMonitorResponse>(jsonString);

            return result ?? throw new InvalidOperationException(
                $"Departure monitor returned no data for stop.");
        }
    }
}

