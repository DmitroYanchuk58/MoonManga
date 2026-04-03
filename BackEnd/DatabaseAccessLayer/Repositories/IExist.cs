using DatabaseAccessLayer.Entities;

namespace DatabaseAccessLayer.Repositories
{
    public interface IExist <T> where T : Entity
    {
        public Task<bool> ExistAsync(Guid id);
    }
}
