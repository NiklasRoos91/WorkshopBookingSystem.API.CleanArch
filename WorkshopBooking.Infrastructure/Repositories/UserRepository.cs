using Microsoft.EntityFrameworkCore;
using WorkshopBooking.Domain.Entities;
using WorkshopBooking.Domain.Interfaces;
using WorkshopBooking.Infrastructure.Presistence;

namespace WorkshopBooking.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly WorkshopBookingDbContext _context;

        public UserRepository(WorkshopBookingDbContext context)
        {
            _context = context;
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}
