using Microsoft.EntityFrameworkCore;
using WorkshopBooking.Domain.Entities;
using WorkshopBooking.Domain.Interfaces;
using WorkshopBooking.Infrastructure.Presistence;

namespace WorkshopBooking.Infrastructure.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly WorkshopBookingDbContext _context;

        public EmployeeRepository(WorkshopBookingDbContext context)
        {
            _context = context;
        }

        public async Task<Employee?> GetEmployeeWithUserByEmployeeIdAsync(int employeeId)
        {
            return await _context.Employees
                .Where(e => e.EmployeeId == employeeId)
                .Include(e => e.User)  // Inkludera relaterad User
                .FirstOrDefaultAsync();
        }
        public async Task<Employee?> GetEmployeeWithUserByUserIdAsync(int userId)
        {
            return await _context.Employees
                .Where(e => e.UserId == userId)
                .Include(e => e.User)  // Inkludera relaterad User
                .FirstOrDefaultAsync();
        }
    }
}
