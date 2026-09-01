using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.Helpers.Convertors.Interfaces;
using DatabaseAccessLayer.Entities;

namespace BusinessLogicLayer.Helpers.Convertors
{
    public class ConvertorFromPageDTOIntoPage : IConvertorDTOIntoEntity<Page, PageDTO>
    {
        public PageDTO ConvertToDto(Page entity) => new PageDTO(entity);

        public Page ConvertToEntity(PageDTO dto)
        {
            return new Page
            {
                Id = dto.Id,
                Order = dto.Order,
                Image = dto.Image,
                IdChapter = dto.IdChapter,
            };
        }
    }
}
