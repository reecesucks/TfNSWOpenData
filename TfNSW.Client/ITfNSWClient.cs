using TfNSWOpenData.Models;

namespace TfNSWOpenData.TfNSW.Client
{
    public interface ITfNSWClient
    {
        Task<StopFinderResponse> FindStopAsync(Dictionary<string, string> paramaters, CancellationToken cancellationToken = default);
        Task<DepartureMonitorResponse> GetDepartureMonitorAsync(Dictionary<string, string> queryParams, CancellationToken cancellationToken = default);
    }
}
