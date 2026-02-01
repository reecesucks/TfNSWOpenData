using CsvHelper.Configuration;
using CsvHelper.TypeConversion;

namespace TfNSWOpenData.Data
{
    public class NullableIntConverter : Int32Converter
    {
        public override object? ConvertFromString(string? text, CsvHelper.IReaderRow row, MemberMapData memberMapData)
        {
            if (string.IsNullOrWhiteSpace(text))
                return null;
            return base.ConvertFromString(text, row, memberMapData);
        }
    }
}
