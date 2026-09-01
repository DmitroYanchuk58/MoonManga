using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.Helpers.Convertors.Interfaces;
using DatabaseAccessLayer.Entities;

namespace BusinessLogicLayer.Helpers.Convertors
{
    public class ConvertorReadItemDTOIntoReadItem : IConvertorDTOIntoEntity<ReadItem, ReadItemDTO>
    {
        public ReadItem ConvertToEntity(ReadItemDTO dto)
        {
            return new ReadItem
            {
                Id = dto.Id,
                Title = dto.Title,
                Type = dto.Type.ToString(),
                Description = dto.Description,
                CoverImage = dto.CoverImage,
                Rating = dto.Rating
            };
        }

        public ReadItemDTO ConvertToDto(ReadItem entity)
        {
            return new ReadItemDTO(entity);
        }
    }
}
