using System.Text.Json.Serialization;

namespace WorkshopBooking.Domain.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum UserRole
    {
        Admin,
        Customer,
        Employee
    }
}
