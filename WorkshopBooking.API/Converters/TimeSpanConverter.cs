using System.Text.Json;
using System.Text.Json.Serialization;

namespace WorkshopBooking.API.Converters
{
    public class TimeSpanConverter : JsonConverter<TimeSpan>
    {
        private const string FormatString = @"hh\:mm\:ss";

        public override TimeSpan Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return TimeSpan.Parse(reader.GetString()!);
        }

        public override void Write(Utf8JsonWriter writer, TimeSpan value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString(FormatString));
        }
    }
}
