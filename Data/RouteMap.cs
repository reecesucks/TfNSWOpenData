using CsvHelper.Configuration;
using TfNSWOpenData.Data;

public sealed class RouteMap : ClassMap<Route>
{
    public RouteMap()
    {
        Map(m => m.RouteId).Name("route_id");
        Map(m => m.AgencyId).Name("agency_id");
        Map(m => m.RouteShortName).Name("route_short_name");
        Map(m => m.RouteLongName).Name("route_long_name");
        Map(m => m.RouteDesc).Name("route_desc");
        Map(m => m.RouteType).Name("route_type");
        Map(m => m.RouteColor).Name("route_color");
        Map(m => m.RouteTextColor).Name("route_text_color");
        Map(m => m.ExactTimes).Name("exact_times");
    }
}