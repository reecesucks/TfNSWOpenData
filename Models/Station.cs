using TfNSWOpenData.Enums;

namespace TfNSWOpenData.Models
{
    public class Station
    {
        public string Name { get; set; } = string.Empty;
        public List<TransportMode> Modes { get; set; }
        public string? Id { get; set; } // optional unique identifier
        public string? City { get; set; } // optional
        public double Latitude { get; set; } // optional
        public double Longitude { get; set; } // optional


    }
}
