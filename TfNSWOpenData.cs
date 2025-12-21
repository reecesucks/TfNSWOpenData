using Microsoft.Extensions.Options;
using System.Collections;
using System.Collections.Generic;
using TfNSWOpenData.API;
using TfNSWOpenData.Models;
using TfNSWOpenData.Services;
using TfNSWOpenData.TfNSW.Client;
using static System.Net.WebRequestMethods;

namespace TfNSWOpenData
{
    public sealed class TfNSWOpenData
    {
        private readonly ITfNSWClient _client;
        private readonly GTFSLookupService _gtfsService;

        public TfNSWOpenData(string apiKey)
        {
            var options = new TfNSWOptions
            {
                ApiKey = apiKey,
                BaseUri = new Uri("https://api.transport.nsw.gov.au/v1/tp/")
            };

            var http = new HttpClient();
            _client = new TfNSWClient(http, Options.Create(options));
            _gtfsService = new GTFSLookupService();
        }

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
            // Required parameter validation
            //if (string.IsNullOrWhiteSpace(stopId))
            //    throw new ArgumentException("Stop ID cannot be null or empty.", nameof(stopId));
            //if (string.IsNullOrWhiteSpace(outputFormat))
            //    throw new ArgumentException("outputFormat cannot be null or empty.", nameof(outputFormat));
            //if (string.IsNullOrWhiteSpace(coordOutputFormat))
            //    throw new ArgumentException("coordOutputFormat cannot be null or empty.", nameof(coordOutputFormat));
            //if (string.IsNullOrWhiteSpace(typeDm))
            //    throw new ArgumentException("typeDm cannot be null or empty.", nameof(typeDm));

            //// Build query parameters
            //var queryParams = new Dictionary<string, string>
            //{
            //    ["outputFormat"] = outputFormat,
            //    ["coordOutputFormat"] = coordOutputFormat,
            //    ["type_dm"] = typeDm,
            //    ["name_dm"] = stopId
            //};

            //if (!string.IsNullOrWhiteSpace(mode)) queryParams["mode"] = mode;
            //queryParams["nameKey_dm"] = "stopID";
            //queryParams["itdDate"] = itdDate ?? DateTime.Now.ToString("yyyyMMdd");
            //queryParams["itdTime"] = itdTime ?? DateTime.Now.ToString("HHmm");
            //if (!string.IsNullOrWhiteSpace(departureMonitorMacro)) queryParams["departureMonitorMacro"] = departureMonitorMacro;
            //if (!string.IsNullOrWhiteSpace(excludedMeans)) queryParams["excludedMeans"] = excludedMeans;
            //if (!string.IsNullOrWhiteSpace(exclMOT1)) queryParams["exclMOT_1"] = exclMOT1;
            //if (!string.IsNullOrWhiteSpace(exclMOT2)) queryParams["exclMOT_2"] = exclMOT2;
            //if (!string.IsNullOrWhiteSpace(exclMOT4)) queryParams["exclMOT_4"] = exclMOT4;
            //if (!string.IsNullOrWhiteSpace(exclMOT5)) queryParams["exclMOT_5"] = exclMOT5;
            //if (!string.IsNullOrWhiteSpace(exclMOT7)) queryParams["exclMOT_7"] = exclMOT7;
            //if (!string.IsNullOrWhiteSpace(exclMOT9)) queryParams["exclMOT_9"] = exclMOT9;
            //if (!string.IsNullOrWhiteSpace(exclMOT11)) queryParams["exclMOT_11"] = exclMOT11;
            //if (!string.IsNullOrWhiteSpace(tfNSWDM)) queryParams["TfNSWDM"] = tfNSWDM;
            //if (!string.IsNullOrWhiteSpace(version)) queryParams["version"] = version;

            //queryParams["limit"] = limit.ToString();

            return await _client.GetDepartureMonitorAsync(parameters, cancellationToken);
        }
    }
}
