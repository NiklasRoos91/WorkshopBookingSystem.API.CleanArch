namespace WorkshopBooking.Application.Features.EmployeeFeature.DTOs
{
    public class EmployeeWithUserDto
    {
        public int EmployeeId { get; set; }
        public string JobTitle { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
    }
}
