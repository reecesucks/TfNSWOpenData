# TfNSW Open Data Library

Small .NET library for retrieving live train departure information from the Transport for NSW Open Data API.

---

## Installation

Register the library with dependency injection in `Program.cs`.

```csharp
builder.Services.AddTransportNsw(options =>
{
    options.ApiKey = builder.Configuration["TransportApi:ApiKey"];
});
```

---

## Example Usage

Inject `ITfNSWClient` into your service:

```csharp
public class TrainDashboardService
{
    private readonly TfNSWOpenData.TfNSWOpenData _tfNsw;

    public TrainDashboardService(ITfNSWClient tfNSWClient)
    {
        _tfNsw = new TfNSWOpenData.TfNSWOpenData(tfNSWClient);
    }

    public async Task GetDepartures()
    {
        var response = await _tfNsw.GetDepartureMonitorAsync(
            stopId: "200060",
            limit: 10);

        var departures = response.StopEvents
            .OrderBy(x => x.DepartureTimePlanned);

        foreach (var departure in departures)
        {
            Console.WriteLine(
                $"{departure.Transportation.DisassembledName} " +
                $"to {departure.Transportation.Destination.Name}");
        }
    }
}
```

---

## Features

- Simple dependency injection setup
- Typed API client
- Retrieve departures by stop ID
- Train and metro support
- Platform and destination information
- Line colour helper methods

---

## Requirements

- .NET 8+
- Transport for NSW API key

Get an API key here:  
https://opendata.transport.nsw.gov.au/

See full dashboard implementation here
https://github.com/reecesucks/Dashboard
