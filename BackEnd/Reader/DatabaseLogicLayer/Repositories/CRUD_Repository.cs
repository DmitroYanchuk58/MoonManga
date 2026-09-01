using DatabaseAccessLayer.DatabaseContext;
using DatabaseLogicLayer.Entities;
using DatabaseLogicLayer.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DatabaseAccessLayer.Repositories
{
    public class CRUD_Repository<T> : ICRUD_Repository<T> where T : Entity
    {
        private readonly ReaderDBContext _context;
        private readonly DbSet<T> _dbSet;

        public CRUD_Repository(ReaderDBContext context)
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
            return await _dbSet.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        // READ (Всі записи)
        public async Task<List<T>> GetAllAsync()
        {
            return await _dbSet.AsNoTracking().ToListAsync();
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
            var entity = _dbSet.Find(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        // Custom get method
        public async Task<List<T>> GetByConditionAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.AsNoTracking()
                       .Where(predicate)
                       .ToListAsync();
        }
    }
}
