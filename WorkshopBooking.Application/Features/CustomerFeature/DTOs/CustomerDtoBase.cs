namespace WorkshopBooking.Application.Features.CustomerFeature.DTOs
{
    public abstract class CustomerDtoBase
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string VehicleMake { get; set; }
    }
}
