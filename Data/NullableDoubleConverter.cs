using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;

namespace TfNSWOpenData.Data
{
    public class NullableDoubleConverter : DoubleConverter
    {
        public override object? ConvertFromString(string? text, IReaderRow row, MemberMapData memberMapData)
        {
            // If the CSV cell is empty or whitespace, return null
            if (string.IsNullOrWhiteSpace(text))
                return null;

            // Otherwise, use the default DoubleConverter
            return base.ConvertFromString(text, row, memberMapData);
        }
    }
}
