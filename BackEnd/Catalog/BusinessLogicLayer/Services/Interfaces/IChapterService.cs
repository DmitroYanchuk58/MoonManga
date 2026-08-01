using BusinessLogicLayer.DTOs;

namespace BusinessLogicLayer.Services.Interfaces
{
    public interface IChapterService
    {
        public Task CreateChapterAsync(ChapterDTO item);

        public Task<ChapterDTO> GetChapterByIdAsync(Guid id);

        public Task<List<ChapterDTO>> GetAllChaptersAsync();

        public Task<List<ChapterDTO>> GetChaptersByReadItemIdAsync(Guid readItemId);

        public Task UpdateChapterAsync(ChapterDTO item);

        public Task DeleteChapterAsync(Guid id);
    }
}
