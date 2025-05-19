namespace WorkshopBooking.Application.Features.BookingFeature.DTOs
{
    public class UpdateBookingDto
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int ServiceTypeId { get; set; }
    }
}
