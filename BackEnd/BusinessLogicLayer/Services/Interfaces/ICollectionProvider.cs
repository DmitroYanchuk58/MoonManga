using BusinessLogicLayer.DTOs;

namespace BusinessLogicLayer.Services.Interfaces
{
    public interface ICollectionProvider<T> where T : DTO
    {
        public Task<List<T>> GetItemsCollectionAsync(int collectionNumber, int collectionSize);

        public Task<int> GetTotalCountAsync();
    }
}
