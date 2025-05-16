namespace WorkshopBooking.Application.Features.AvailableSlotFeature.DTOs
{
    public class AvailableSlotInputDto
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int ServiceTypeId { get; set; }
    }
}
