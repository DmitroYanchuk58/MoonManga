using BusinessLogicLayer.DTOs;
using DatabaseAccessLayer.Entities;

namespace BusinessLogicLayer.Features.CollectionProvider
{
    public interface ICollectionProvider
    {
        public Task<List<ReadItemDTO>> GetItemsCollectionAsync(int collectionNumber, int collectionSize);

        public Task<int> GetTotalCountAsync();

        public Task<List<ReadItemDTO>> GetLessRatedReadItems(int collectionNumber, int collectionSize = 30);

        public Task<List<ReadItemDTO>> GetTopRatedReadItems(int collectionNumber, int collectionSize = 30);
    }
}
