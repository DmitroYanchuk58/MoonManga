using DatabaseAccessLayer.Entities;

namespace DatabaseAccessLayer.Repositories.Interfaces
{
    public interface ICRUD<T> where T : Entity
    {
        public Task CreateAsync(T entity);

        // READ (Один за ID)
        public Task<T> GetByIdAsync(Guid id);

        // READ (Всі записи)
        public Task<List<T>> GetAllAsync();

        // UPDATE
        public Task UpdateAsync(T entity);

        // DELETE
        public Task DeleteAsync(Guid id);
    }
}
