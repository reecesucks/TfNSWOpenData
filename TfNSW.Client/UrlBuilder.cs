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

        public static string WithQuery(string baseAddress, string relativePath, string queryString)
        {
            if (string.IsNullOrWhiteSpace(baseAddress))
                throw new ArgumentException("Base address cannot be null or empty.", nameof(baseAddress));
            if (string.IsNullOrWhiteSpace(relativePath))
                throw new ArgumentException("Relative path cannot be null or empty.", nameof(relativePath));
            if (string.IsNullOrWhiteSpace(queryString))
                throw new ArgumentException("Query string cannot be null or empty.", nameof(queryString));

            if (!baseAddress.EndsWith("/"))
                baseAddress += "/";

            var fullUri = new Uri(new Uri(baseAddress), relativePath);

            // Remove leading '?' if caller included it
            queryString = queryString.TrimStart('?');

            return $"{fullUri}?{queryString}";
        }
    }
}
