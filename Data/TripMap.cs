using CsvHelper.Configuration;

namespace TfNSWOpenData.Data
{
    public sealed class TripMap : ClassMap<Trip>
    {
        public TripMap()
        {
            Map(m => m.RouteId).Name("route_id");
            Map(m => m.ServiceId).Name("service_id");
            Map(m => m.TripId).Name("trip_id");
            Map(m => m.ShapeId).Name("shape_id");
            Map(m => m.TripHeadsign).Name("trip_headsign");
            Map(m => m.DirectionId).Name("direction_id").TypeConverter<NullableIntConverter>();
            Map(m => m.BlockId).Name("block_id");
            Map(m => m.WheelchairAccessible).Name("wheelchair_accessible").TypeConverter<NullableIntConverter>();
            Map(m => m.RouteDirection).Name("route_direction");
            Map(m => m.TripNote).Name("trip_note");
            Map(m => m.BikesAllowed).Name("bikes_allowed").TypeConverter<NullableIntConverter>();
        }
    }
}
