using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;
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

        public async Task<Employee?> GetEmployeeWithUserByIdAsync(int employeeId)
        {
            return await _context.Employees
                .Where(e => e.EmployeeId == employeeId)
                .Include(e => e.User)  // Inkludera relaterad User
                .FirstOrDefaultAsync();
        }
    }
}
