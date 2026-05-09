using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.Features.Crud;
using BusinessLogicLayer.Features.ICrud;
using BusinessLogicLayer.Helpers.Convertors;
using BusinessLogicLayer.Helpers.Convertors.Interfaces;
using BusinessLogicLayer.Services.Interfaces;
using BusinessLogicLayer.Validation;
using DatabaseAccessLayer.DatabaseContext;
using DatabaseAccessLayer.Entities;

namespace BusinessLogicLayer.Services
{
    public class ReadItemTagService : IReadItemTagService
    {
        private ICrud<ReadItemTag, ReadItemTagDTO> _crud;
        private IConvertorDTOIntoEntity<ReadItemTag, ReadItemTagDTO> _convertor = new ConvertorFromReadItemTagDTOIntoReadItemTag();

        public ReadItemTagService(CatalogDBContext context)
        {
            _crud = new Crud<ReadItemTag, ReadItemTagDTO>(context, new ConvertorFromReadItemTagDTOIntoReadItemTag(), new ReadItemTagValidator());
        }

        public async Task CreateReadItemTagAsync(ReadItemTagDTO item)
        {
            await _crud.CreateAsync(item);  
        }

        public async Task DeleteReadItemTagAsync(Guid id)
        {
            await _crud.DeleteAsync(id);
        }

        public async Task<List<ReadItemTagDTO>> GetAllReadItemTagsAsync()
        {
            return await _crud.GetAllAsync();
        }

        public async Task<ReadItemTagDTO> GetReadItemTagByIdAsync(Guid id)
        {
            return await _crud.GetByIdAsync(id);
        }

        public async Task UpdateReadItemTagAsync(ReadItemTagDTO item)
        {
            await _crud.UpdateAsync(item);
        }
    }
}
