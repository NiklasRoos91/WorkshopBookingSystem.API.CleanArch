
namespace WorkshopBooking.Application.Features.BookingFeature.DTOs
{
    public class BookingDto
    {
        public int BookingId { get; set; }
        public int CustomerId { get; set; }
        public int EmployeeId { get; set; }
        public int ServiceTypeId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
