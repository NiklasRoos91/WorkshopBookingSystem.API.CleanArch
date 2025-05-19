namespace WorkshopBooking.Application.Features.ServiceTypeFeature.DTOs
{
    public class ServiceTypeInputDto
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public TimeSpan Duration { get; set; } // "HH:mm:ss"
    }
}
