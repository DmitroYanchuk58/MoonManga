using BusinessLogicLayer.DTOs;

namespace BusinessLogicLayer.Features.Join
{
    public interface IJoinProvider
    {
        public Task<ReadItemDTO> GetReadItemWithTagAsync(Guid idReadItem);

        public Task<ReadItemDTO> GetReadItemWithChaptersAsync(Guid idReadItem);

        public Task<ReadItemDTO> GetReadItemFullInfo(Guid idReadItem);
    }
}

