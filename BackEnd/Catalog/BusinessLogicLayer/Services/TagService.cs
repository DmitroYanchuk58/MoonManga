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
    public class TagService : ITagService
    {
        private ICrud<Tag, TagDTO> _crud;
        private IConvertorDTOIntoEntity<Tag, TagDTO> _convertor = new ConvertorFromTagDTOIntoTag();

        public TagService(CatalogDBContext context)
        {
            _crud = new Crud<Tag, TagDTO>(context, new ConvertorFromTagDTOIntoTag(), new TagValidator());
        }

        public async Task CreateTagAsync(TagDTO item)
        {
            await _crud.CreateAsync(item);
        }

        public async Task DeleteTagAsync(Guid id)
        {
            await _crud.DeleteAsync(id);
        }

        public async Task<List<TagDTO>> GetAllTagsAsync()
        {
            return await _crud.GetAllAsync();
        }

        public async Task<TagDTO> GetTagByIdAsync(Guid id)
        {
            return await GetTagByIdAsync(id);
        }

        public async Task UpdateTagAsync(TagDTO item)
        {
            await _crud.UpdateAsync(item);
        }

        public async Task<List<TagDTO>> GetTagByNameAsync(string name)
        {
            return await _crud.GetByConditionAsync(t => t.Name.ToLower() == name.ToLower());
        }
    }
}
