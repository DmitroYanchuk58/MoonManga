using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.Features;
using BusinessLogicLayer.Features.Crud;
using BusinessLogicLayer.Features.ICrud;
using BusinessLogicLayer.Helpers.Convertors;
using BusinessLogicLayer.Services.Interfaces;
using BusinessLogicLayer.Validation;
using DatabaseAccessLayer.DatabaseContext;
using DatabaseAccessLayer.Entities;
using DatabaseAccessLayer.Repositories;
using DatabaseAccessLayer.Repositories.Interfaces;

namespace BusinessLogicLayer.Services
{
    public class ChapterService : IChapterService
    {
        private ICrud<Chapter, ChapterDTO> _crud;
        public ChapterService(CatalogDBContext context)
        {
            _crud = new Crud<Chapter, ChapterDTO>(context, new ConvertorFromChapterDTOIntoChapter(), new ChapterValidator());
        }
        public async Task CreateChapterAsync(ChapterDTO item)
        {
            await _crud.CreateAsync(item);
        }

        public async Task DeleteChapterAsync(Guid id)
        {
            await _crud.DeleteAsync(id);
        }

        public async Task<List<ChapterDTO>> GetAllChaptersAsync()
        {
            return await _crud.GetAllAsync();
        }

        public async Task<ChapterDTO> GetChapterByIdAsync(Guid id)
        {
            return await _crud.GetByIdAsync(id);
        }

        public async Task UpdateChapterAsync(ChapterDTO item)
        {
            await _crud.UpdateAsync(item);
        }
    }
}
