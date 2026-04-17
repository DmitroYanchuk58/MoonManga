using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.Services.Interfaces;
using DatabaseAccessLayer.DatabaseContext;
using DatabaseAccessLayer.Entities;
using DatabaseAccessLayer.Repositories;

namespace BusinessLogicLayer.Services
{
    public class ChapterService : IChapterService
    {
        private ICRUD<Chapter> _readItemRepository;
        private IExist<Chapter> _existRepository;
        public ChapterService(CatalogDBContext context)
        {
            _readItemRepository = new CrudRepository<Chapter>(context);
            _existRepository = new ExistRepository<Chapter>(context);
        }
        public async Task CreateChapterAsync(ChapterDTO item)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteChapterAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ChapterDTO>> GetAllChaptersAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<ChapterDTO> GetChapterByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateChapterAsync(ChapterDTO item)
        {
            throw new NotImplementedException();
        }
    }
}
