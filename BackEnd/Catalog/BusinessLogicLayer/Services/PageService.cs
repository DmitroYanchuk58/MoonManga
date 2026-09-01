using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.Features;
using BusinessLogicLayer.Features.Crud;
using BusinessLogicLayer.Features.ICrud;
using BusinessLogicLayer.Helpers.Convertors;
using BusinessLogicLayer.Services.Interfaces;
using BusinessLogicLayer.Validation;
using DatabaseAccessLayer.DatabaseContext;
using DatabaseAccessLayer.Entities;

namespace BusinessLogicLayer.Services
{
    public class PageService : IPageService
    {
        private ICrud<Page, PageDTO> _crud;

        public PageService(CatalogDBContext context)
        {
            _crud = new Crud<Page, PageDTO>(context, new ConvertorFromPageDTOIntoPage(), new PageValidator());
        }

        public async Task CreatePageAsync(PageDTO item)
        {
            await _crud.CreateAsync(item);
        }

        public async Task DeletePageAsync(Guid id)
        {
            await _crud.DeleteAsync(id);
        }

        public async Task<List<PageDTO>> GetAllPagesAsync()
        {
            return await _crud.GetAllAsync();
        }

        public async Task<PageDTO> GetPageByIdAsync(Guid id)
        {
            return await _crud.GetByIdAsync(id);
        }

        public async Task UpdatePageAsync(PageDTO item)
        {
            await _crud.UpdateAsync(item);
        }
    }
}
