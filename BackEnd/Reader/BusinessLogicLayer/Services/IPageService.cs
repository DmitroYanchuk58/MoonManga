using BusinessLogicLayer.DTOs;
using DatabaseLogicLayer.Entities;

namespace BusinessLogicLayer.Services
{
    public interface IPageService
    {
        public Task CreateAsync(Page_DTO item);

        public Task<Page_DTO> GetByIdAsync(Guid id);  

        public Task<List<Page_DTO>> GetAllAsync();

        public Task<List<Page_DTO>> GetAllByChapterId(Guid idChapter);

        public Task UpdateAsync(Page_DTO item);

        public Task DeleteAsync(Guid id);
    }
}
