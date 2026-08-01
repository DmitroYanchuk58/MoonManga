using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.Helpers.Convertors.Interfaces;
using DatabaseAccessLayer.Entities;

namespace BusinessLogicLayer.Helpers.Convertors
{
    public class ConvertorFromReadItemTagDTOIntoReadItemTag : IConvertorDTOIntoEntity<ReadItemTag, ReadItemTagDTO>
    {
        public ReadItemTagDTO ConvertToDto(ReadItemTag entity) => new ReadItemTagDTO(entity);

        public ReadItemTag ConvertToEntity(ReadItemTagDTO dto)
        {
            return new ReadItemTag
            {
                Id = dto.Id,
                IdReadItem = dto.IdReadItem,
                IdTag = dto.IdTag
            };
        }
    }
}
