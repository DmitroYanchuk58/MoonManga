using BusinessLogicLayer.DTOs;

namespace BusinessLogicLayer.Services.Interfaces 
{
    public interface IReadItemService 
    {
        public Task CreateReadItemAsync(ReadItemDTO item);

        public Task<ReadItemDTO> GetReadItemByIdAsync(Guid id);

        public Task<List<ReadItemDTO>> GetAllReadItemsAsync();

        public Task UpdateReadItemAsync(ReadItemDTO item);

        public Task DeleteReadItemAsync(Guid id);

        public Task<List<ReadItemDTO>> FindReadItemsByTitle(string title);

        public Task<List<ReadItemDTO>> GetReadItemCollection(int collectionNumber, int collectionSize = 30);

        public Task<int> GetTotalCountAsync();

        public Task<List<ReadItemDTO>> GetTopRatedReadItems(int collectionNumber, int collectionSize = 30);

        public Task<List<ReadItemDTO>> GetLessRatedReadItems(int collectionNumber, int collectionSize = 30);
    }
}
