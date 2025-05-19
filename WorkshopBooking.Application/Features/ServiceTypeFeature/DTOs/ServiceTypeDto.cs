namespace WorkshopBooking.Application.Features.ServiceTypeFeature.DTOs
{
    public class ServiceTypeDto
    {
        public int ServiceTypeId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public TimeSpan Duration { get; set; } // "HH:mm:ss"
    }
}
