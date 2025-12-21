namespace TfNSWOpenData.Enums
{
    public enum TransportMode
    {
        Train = 0,       // Sydney Trains suburban lines
        Bus = 1,         // Standard buses
        Coach = 2,       // Long-distance coaches
        Tram = 3,        // Tram / Light Rail (older feeds)
        Ferry_Low = 4,   // Minor / less common ferry type
        LightRail = 5,   // Sydney Light Rail lines
        Cable = 6,       // Cable / Funicular (rare)
        Ferry = 7,       // Common ferries (Sydney Harbour)
        SchoolBus = 8,   // School Bus (rare)
        Reserved9 = 9,   // Reserved / Special
        Reserved10 = 10, // Reserved / Special
        Metro = 11,       // Sydney Metro
        Unknown = 12
    }
}
