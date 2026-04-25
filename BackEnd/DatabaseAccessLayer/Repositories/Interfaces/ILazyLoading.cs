using DatabaseAccessLayer.Entities;

namespace DatabaseAccessLayer.Repositories.Interfaces
{
    public interface ICollectionProvider<T> where T : Entity
    {
        public Task<List<T>> GetItemsCollectionAsync(int collectionNumber, int collectionSize = 30);
    }
}
