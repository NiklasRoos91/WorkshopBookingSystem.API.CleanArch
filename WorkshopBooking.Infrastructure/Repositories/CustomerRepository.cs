using Microsoft.EntityFrameworkCore;
using WorkshopBooking.Domain.Entities;
using WorkshopBooking.Domain.Interfaces;
using WorkshopBooking.Infrastructure.Presistence;

namespace WorkshopBooking.Infrastructure.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly WorkshopBookingDbContext _context;
        
        public CustomerRepository(WorkshopBookingDbContext context)
        {
            _context = context;
        }

        public async Task<Customer?> GetCustomerWithUserByIdAsync(int customerId)
        {
            return await _context.Customers
               .Where(c => c.CustomerId == customerId)
               .Include(c => c.User)  // Inkludera relaterad User
               .FirstOrDefaultAsync();
        }
    }
}
