using BusinessLogicLayer.DTOs;

namespace BusinessLogicLayer.Services
{
    public interface IService <Entity> where Entity : DTO
    {
        public Task CreateAsync(Entity item);

        public Task<Entity> GetByIdAsync(Guid id);  

        public Task<List<Entity>> GetAllAsync();

        public Task UpdateAsync(Entity item);

        public Task DeleteAsync(Guid id);
    }
}
