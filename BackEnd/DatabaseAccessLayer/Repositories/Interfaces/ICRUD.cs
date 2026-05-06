using DatabaseAccessLayer.Entities;
using System.Linq.Expressions;

namespace DatabaseAccessLayer.Repositories.Interfaces
{
    public interface ICRUD<T> where T : Entity
    {
        public Task CreateAsync(T entity);

        // READ (Один за ID)
        public Task<T> GetByIdAsync(Guid id);

        public Task<List<T>> GetByConditionAsync(Expression<Func<T, bool>> predicate);

        // READ (Всі записи)
        public Task<List<T>> GetAllAsync();

        // UPDATE
        public Task UpdateAsync(T entity);

        // DELETE
        public Task DeleteAsync(Guid id);
    }
}
