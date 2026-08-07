using DatabaseLogicLayer.Entities;
using System.Linq.Expressions;

namespace DatabaseLogicLayer.Repositories
{
    public interface ICRUD_Repository<T> where T : Entity
    {
        public Task CreateAsync(T entity);

        public Task<T> GetByIdAsync(Guid id);

        public Task<List<T>> GetAllAsync();

        public Task UpdateAsync(T entity);

        public Task DeleteAsync(Guid id);

        public Task<List<T>> GetByConditionAsync(Expression<Func<T, bool>> predicate);
    }
}
