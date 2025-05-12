namespace WorkshopBooking.Domain.Entities
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string VehicleMake { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}
