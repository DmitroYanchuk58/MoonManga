using DatabaseAccessLayer.Entities;

namespace DatabaseAccessLayer.Repositories.Interfaces
{
    public interface IFinder<T> where T : Entity
    {
        public Task<List<ReadItem>> SearhByTitle(string data);
    }
}
