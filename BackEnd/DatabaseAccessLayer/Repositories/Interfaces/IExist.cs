using DatabaseAccessLayer.Entities;

namespace DatabaseAccessLayer.Repositories.Interfaces
{
    public interface IExist <T> where T : Entity
    {
        public Task<bool> ExistAsync(Guid id);
    }
}
