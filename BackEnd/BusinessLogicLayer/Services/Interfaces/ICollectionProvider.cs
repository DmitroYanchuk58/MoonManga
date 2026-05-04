using BusinessLogicLayer.DTOs;

namespace BusinessLogicLayer.Services.Interfaces
{
    public interface ICollectionProvider<T> where T : DTO
    {
        public Task<List<T>> GetItemsCollectionAsync(int collectionNumber, int collectionSize);

        public Task<int> GetTotalCountAsync();

        public Task<List<ReadItemDTO>> GetLessRatedReadItems(int collectionNumber, int collectionSize = 30);

        public Task<List<ReadItemDTO>> GetTopRatedReadItems(int collectionNumber, int collectionSize = 30);
    }
}
