namespace WorkshopBooking.Application.Features.AvailableSlotFeature.DTOs
{
    public class AvailableSlotDto
    {
        public int EmployeeId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int ServiceTypeId { get; set; }
        public bool IsAvailable { get; set; } = true;
    }
}
