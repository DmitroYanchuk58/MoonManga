using BusinessLogicLayer.DTOs;

namespace BusinessLogicLayer.Features.Join
{
    public interface IJoinProvider
    {
        public Task<ReadItemDTO> GetReadItemWithTagAsync(Guid idReadItem);
    }
}
