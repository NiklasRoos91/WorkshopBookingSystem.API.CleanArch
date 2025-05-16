using WorkshopBooking.Domain.Entities;

namespace WorkshopBooking.Domain.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<Employee?> GetEmployeeWithUserByEmployeeIdAsync(int employeeId);
        Task<Employee?> GetEmployeeWithUserByUserIdAsync(int userId);
    }
}
