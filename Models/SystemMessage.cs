using System.Text.Json.Serialization;

namespace TfNSWOpenData.Models
{
    public class SystemMessage
    {
        [JsonPropertyName("type")]
        public string Type { get; set; } = default!;

        [JsonPropertyName("module")]
        public string Module { get; set; } = default!;

        [JsonPropertyName("code")]
        public int Code { get; set; }

        [JsonPropertyName("text")]
        public string Text { get; set; } = string.Empty;
    }
}
