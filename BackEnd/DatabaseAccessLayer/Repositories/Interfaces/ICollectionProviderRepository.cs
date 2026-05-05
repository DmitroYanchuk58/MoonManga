using DatabaseAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace DatabaseAccessLayer.Repositories.Interfaces
{
    public interface ICollectionProviderRepository<T> where T : Entity
    {
        public Task<List<T>> GetItemsCollectionAsync(int collectionNumber, int collectionSize = 30);

        public Task<int> GetTotalCountAsync();

        public Task<List<ReadItem>> GetTopRatedReadItems(int collectionNumber, int collectionSize = 30);

        public Task<List<ReadItem>> GetLessRatedReadItems(int collectionNumber, int collectionSize = 30);
    }
}
