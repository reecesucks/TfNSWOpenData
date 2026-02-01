using CsvHelper.Configuration;
using TfNSWOpenData.Data;

public sealed class StopMap : ClassMap<Stop>
{
    public StopMap()
    {
        Map(m => m.StopId).Name("stop_id");
        Map(m => m.StopCode).Name("stop_code");
        Map(m => m.StopName).Name("stop_name");

        Map(m => m.StopLat).Name("stop_lat");
        Map(m => m.StopLon).Name("stop_lon");

        Map(m => m.LocationType).Name("location_type");
        Map(m => m.ParentStation).Name("parent_station");

        Map(m => m.WheelchairBoarding).Name("wheelchair_boarding");
        Map(m => m.LevelId).Name("level_id");
        Map(m => m.PlatformCode).Name("platform_code");
    }
}