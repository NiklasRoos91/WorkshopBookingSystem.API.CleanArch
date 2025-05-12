namespace WorkshopBooking.Domain.Entities
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public string JobTitle { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}
