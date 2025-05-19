using Microsoft.EntityFrameworkCore;
using WorkshopBooking.Domain.Entities;
using WorkshopBooking.Domain.Interfaces;
using WorkshopBooking.Infrastructure.Presistence;

namespace WorkshopBooking.Infrastructure.Repositories
{
    class BookingRepository : IBookingRepository
    {
        private readonly WorkshopBookingDbContext _context;

        public BookingRepository(WorkshopBookingDbContext context)
        {
            _context = context;
        }

        public Task<List<Booking>> GetBookingsByCustomerIdAsync(int customerId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Booking>> GetBookingsByDateAsync(DateTime date)
        {
            throw new NotImplementedException();
        }

        public Task<List<Booking>> GetBookingsByDateRangeAsync(DateTime from, DateTime to)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Booking>> GetBookingsByEmployeeIdAsync(int employeeId)
        {
            return await _context.Bookings
                .Where(b => b.EmployeeId == employeeId)
                .Include(b => b.Employee)
                .Include(b => b.Customer)
                .Include(b => b.ServiceType)
                .ToListAsync();
        }

        public Task<List<Booking>> GetBookingsByServiceTypeIdAsync(int serviceTypeId)
        {
            throw new NotImplementedException();
        }
    }
}
