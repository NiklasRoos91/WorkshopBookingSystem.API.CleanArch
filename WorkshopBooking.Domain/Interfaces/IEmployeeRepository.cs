using WorkshopBooking.Domain.Entities;

namespace WorkshopBooking.Domain.Interfaces
{
    public interface IEmployeeRepository
    {
        public Task<List<Employee>> GetAllEmployees();
        public Task<Employee?> GetEmployeeById(int id);
        public Task<Employee> CreateEmployee(Employee employee);
        public Task<Employee> UpdateEmployee(int employeeId, Employee employee);
        public Task<bool> DeleteEmployee(int employeeId);
    }
}
