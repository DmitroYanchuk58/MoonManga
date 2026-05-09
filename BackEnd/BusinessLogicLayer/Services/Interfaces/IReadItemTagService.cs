using BusinessLogicLayer.DTOs;

namespace BusinessLogicLayer.Services.Interfaces
{
    public interface IReadItemTagService
    {
        public Task CreateReadItemTagAsync(ReadItemTagDTO item);

        public Task<ReadItemTagDTO> GetReadItemTagByIdAsync(Guid id);

        public Task<List<ReadItemTagDTO>> GetAllReadItemTagsAsync();

        public Task UpdateReadItemTagAsync(ReadItemTagDTO item);

        public Task DeleteReadItemTagAsync(Guid id);
    }
}
