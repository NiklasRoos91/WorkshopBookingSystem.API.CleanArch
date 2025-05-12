using WorkshopBooking.Domain.Enums;

namespace WorkshopBooking.Domain.Entities
{
    public class User
    {
        public int UserId { get; set; }
        public string Email { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public UserRole Role { get; set; } = UserRole.Customer;

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }

        public Customer? Customer { get; set; }
        public Employee? Employee { get; set; }
    }
}
