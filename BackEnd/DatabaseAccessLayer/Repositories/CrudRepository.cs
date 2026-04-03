using DatabaseAccessLayer.DatabaseContext;
using DatabaseAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace DatabaseAccessLayer.Repositories
{
    public class CrudRepository<T> : ICRUD<T> where T : Entity
    {
        private readonly CatalogDBContext _context;
        private readonly DbSet<T> _dbSet;

        public CrudRepository(CatalogDBContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        // CREATE
        public async Task CreateAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        // READ (Один за ID)
        public async Task<T> GetByIdAsync(Guid id)
        {
            var entity = await _dbSet.FindAsync(id);
            _context.ChangeTracker.Clear();
            return entity;
        }

        // READ (Всі записи)
        public async Task<List<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        // UPDATE
        public async Task UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }

        // DELETE
        public async Task DeleteAsync(Guid id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
