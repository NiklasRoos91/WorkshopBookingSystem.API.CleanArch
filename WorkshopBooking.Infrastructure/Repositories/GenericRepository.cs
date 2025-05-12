using WorkshopBooking.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using WorkshopBooking.Infrastructure.Presistence;
using System.Linq.Expressions;

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
                var entity = await _dbSet.FindAsync(id);

                if (entity == null) return false;

                _dbSet.Remove(entity);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while deleting the entity.", ex);
            }
        }

        public async Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbSet;

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        //public async Task<T> GetByIdAsync<T>(int id, params Expression<Func<T, object>>[] includes) where T : class
        //{
        //    IQueryable<T> query = _dbSet.OfType<T>();

        //    // Lägg till alla includes till frågan
        //    foreach (var include in includes)
        //    {
        //        query = query.Include(include);
        //    }

        //    // Hämta entiteten baserat på ID
        //    return await query.Where(entity => EF.Property<int>(entity, "Id") == id).FirstOrDefaultAsync();
        //}

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
