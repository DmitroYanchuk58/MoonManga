using BusinessLogicLayer.DTOs;
using DatabaseAccessLayer.Entities;
using DatabaseAccessLayer.Repositories.Interfaces;

namespace BusinessLogicLayer.Services.Interfaces
{
    public interface IReadItemService : ICollectionProvider<ReadItemDTO>
    {
        public Task CreateReadItemAsync(ReadItemDTO item);

        public Task<ReadItemDTO> GetReadItemByIdAsync(Guid id);

        public Task<List<ReadItemDTO>> GetAllReadItemsAsync();

        public Task UpdateReadItemAsync(ReadItemDTO item);

        public Task DeleteReadItemAsync(Guid id);

        public Task<List<ReadItemDTO>> FindReadItemsByTitle(string title);
    }
}
