using BusinessLogicLayer.DTOs;
using DatabaseLogicLayer.Entities;

namespace BusinessLogicLayer.Services
{
    public interface IPageService
    {
        public Task<PageResponseDto> CreateAsync(CreatePageDto dto);

        public Task<PageResponseDto?> GetByIdAsync(Guid id);

        public Task<List<Page_DTO>> GetAllAsync();

        public Task<List<Page_DTO>> GetAllByChapterId(Guid idChapter);

        public Task<PageResponseDto> UpdateAsync(UpdatePageDto dto);

        public Task<bool> DeleteAsync(Guid id);
    }
}
