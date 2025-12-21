namespace TfNSWOpenData.API
{
    internal static class UrlBuilder
    {
        public static string WithQuery(string baseAddress, string relativePath, IReadOnlyDictionary<string, string> queryParameters)
        {
            if (string.IsNullOrWhiteSpace(baseAddress))
                throw new ArgumentException("Base address cannot be null or empty.", nameof(baseAddress));
            if (string.IsNullOrWhiteSpace(relativePath))
                throw new ArgumentException("Relative path cannot be null or empty.", nameof(relativePath));

            if (!baseAddress.EndsWith("/")) baseAddress += "/";

            var fullUri = new Uri(new Uri(baseAddress), relativePath);

            using var content = new FormUrlEncodedContent(queryParameters);
            var queryString = content.ReadAsStringAsync().Result;

            return $"{fullUri}?{queryString}";
        }
    }
}
