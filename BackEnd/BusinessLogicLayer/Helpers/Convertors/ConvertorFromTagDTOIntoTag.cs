using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.Helpers.Convertors.Interfaces;
using DatabaseAccessLayer.Entities;

namespace BusinessLogicLayer.Helpers.Convertors
{
    public class ConvertorFromTagDTOIntoTag : IConvertorDTOIntoEntity<Tag, TagDTO>
    {
        public TagDTO ConvertToDto(Tag entity) => new TagDTO(entity);


        public Tag ConvertToEntity(TagDTO dto)
        {
            return new Tag
            {
                Id = dto.Id,
                Name = dto.Name
            };
        }
    }
}
