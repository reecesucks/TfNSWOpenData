namespace TfNSWOpenData.Models.Requests
{
    /// <summary>
    /// Request model for TfNSW Trip Planner add_info (Service Alerts).
    /// Uses modern, human-friendly parameter names and maps internally
    /// to the underlying Trip Planner API.
    /// </summary>
    public class AdditionalInfoRequest
    {
        /// <summary>
        /// Output format of the response.
        /// Always use "rapidJSON" for JSON output.
        /// </summary>
        public string OutputFormat { get; set; } = "rapidJSON";

        /// <summary>
        /// Filter alerts affecting a specific stop.
        /// Uses the public TfNSW stop ID.
        /// Example: "212210"
        /// </summary>
        public string StopId { get; set; }

        /// <summary>
        /// Filter alerts by transport mode.
        /// Common values: "rail", "metro", "bus", "ferry", "light_rail".
        /// </summary>
        public string RouteType { get; set; }

        /// <summary>
        /// Filter alerts by severity level.
        /// Common values: "low", "medium", "high".
        /// </summary>
        public string Severity { get; set; }

        /// <summary>
        /// Whether to include alerts that start in the future.
        /// </summary>
        public bool? IncludeFuture { get; set; }

        /// <summary>
        /// Limit the number of alerts returned.
        /// </summary>
        public int? MaxResults { get; set; }

        /// <summary>
        /// Optional language code for returned text.
        /// Default is English ("en").
        /// </summary>
        public string Language { get; set; } = "en";

        /// <summary>
        /// Allows passing through additional query parameters
        /// without modifying this model.
        /// </summary>
        public Dictionary<string, string> CustomParameters { get; set; } = new();

        /// <summary>
        /// Converts the request into a query string compatible
        /// with the TfNSW add_info endpoint.
        /// </summary>
        public string ToQueryString()
        {
            var parameters = new List<string>();

            void Add(string key, object value)
            {
                if (value == null) return;
                parameters.Add($"{Uri.EscapeDataString(key)}={Uri.EscapeDataString(value.ToString())}");
            }

            // Core / universal
            Add("outputFormat", OutputFormat);

            // Modern / alias parameters (these DO work with add_info)
            Add("stop_id", StopId);
            Add("route_type", RouteType);
            Add("severity", Severity);
            Add("include_future", IncludeFuture?.ToString().ToLowerInvariant());

            // Optional helpers
            Add("maxResults", MaxResults);
            Add("language", Language);

            // Pass-through
            foreach (var kv in CustomParameters)
                Add(kv.Key, kv.Value);

            return string.Join("&", parameters);
        }
    }
}
