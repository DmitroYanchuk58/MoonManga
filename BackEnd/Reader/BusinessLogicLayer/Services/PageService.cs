using BusinessLogicLayer.DTOs;
using DatabaseAccessLayer.DatabaseContext;
using DatabaseAccessLayer.Repositories;
using DatabaseLogicLayer.Entities;
using DatabaseLogicLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    public class PageService : IPageService
    {
        private readonly ICRUD_Repository<Page> _pageRepository;

        public PageService(ReaderDBContext context)
        {
            _pageRepository = new CRUD_Repository<Page>(context);
        }

        public async Task CreateAsync(Page_DTO item)
        {
            ArgumentNullException.ThrowIfNull(item);

            await _pageRepository.CreateAsync(item.GetPage());
        }

        public async Task DeleteAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("Page ID cannot be an empty GUID.", nameof(id));
            }

            await _pageRepository.DeleteAsync(id);
        }

        public async Task<List<Page_DTO>> GetAllAsync()
        {
            var dbPages = await _pageRepository.GetAllAsync();
            return dbPages.Select(page => new Page_DTO(page)).ToList();
        }

        public async Task<Page_DTO?> GetByIdAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("Page ID cannot be an empty GUID.", nameof(id));
            }

            var dbPage = await _pageRepository.GetByIdAsync(id);
            return dbPage is null ? null : new Page_DTO(dbPage);
        }

        public async Task<List<Page_DTO>> GetAllByChapterId(Guid idChapter)
        {
            if (idChapter == Guid.Empty)
            {
                throw new ArgumentException("Chapter ID cannot be an empty GUID.", nameof(idChapter));
            }

            var dbPages = await _pageRepository.GetByConditionAsync(p => p.IdChapter == idChapter);

            return dbPages.Select(page => new Page_DTO(page)).ToList();
        }

        public async Task UpdateAsync(Page_DTO item)
        {
            ArgumentNullException.ThrowIfNull(item);

            await _pageRepository.UpdateAsync(item.GetPage());
        }
    }
}
