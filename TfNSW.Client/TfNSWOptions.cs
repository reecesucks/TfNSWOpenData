namespace TfNSWOpenData.TfNSW.Client
{
    public sealed class TfNSWOptions
    {
        public string ApiKey { get; set; } = default!;
        public Uri BaseUri { get; set; } = new("https://api.transport.nsw.gov.au/v1/");
    }
}
