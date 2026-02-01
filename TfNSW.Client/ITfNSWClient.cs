using TfNSWOpenData.Models.Generated;
using TfNSWOpenData.Models.Requests;

namespace TfNSWOpenData.TfNSW.Client
{
    public interface ITfNSWClient
    {
        Task<StopFinderResponse> FindStopAsync(Dictionary<string, string> paramaters, CancellationToken cancellationToken = default);
        Task<DepartureMonitorResponse> GetDepartureMonitorAsync(Dictionary<string, string> queryParams, CancellationToken cancellationToken = default);
        Task<AdditionalInfoResponse> GetAdditionalInfoAsync(AdditionalInfoRequest request, CancellationToken cancellationToken = default);

    }
}
