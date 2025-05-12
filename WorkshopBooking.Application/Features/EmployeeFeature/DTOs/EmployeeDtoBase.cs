
namespace WorkshopBooking.Application.Features.EmployeeFeature.DTOs
{
    public abstract class EmployeeDtoBase
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }

        public string JobTitle { get; set; }
    }
}
