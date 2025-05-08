using WorkshopBooking.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using WorkshopBooking.Infrastructure.Presistence;

namespace WorkshopBooking.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericInterface<T> where T : class
    {
        private readonly WorkshopBookingDbContext _context;
        private readonly DbSet<T> _dbSet;
        public GenericRepository(WorkshopBookingDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                // Find the entity by its ID
                var entity = await _dbSet.FindAsync(id);
                // If the entity is not found, return false
                if (entity == null) return false;

                // Remove the entity from the DbSet and save changes
                _dbSet.Remove(entity);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while deleting the entity.", ex);
            }
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }
        public async Task<bool> ExistsAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            return entity != null;
        }
    }
}
