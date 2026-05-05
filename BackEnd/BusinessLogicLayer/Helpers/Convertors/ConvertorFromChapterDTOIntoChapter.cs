using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.Helpers.Convertors.Interfaces;
using DatabaseAccessLayer.Entities;

namespace BusinessLogicLayer.Helpers.Convertors
{
    public class ConvertorFromChapterDTOIntoChapter : IConvertorDTOIntoEntity<Chapter, ChapterDTO>
    {
        public ChapterDTO ConvertToDto(Chapter entity)
        {
            return new ChapterDTO(entity);
        }

        public Chapter ConvertToEntity(ChapterDTO dto)
        {
            return new Chapter
            {
                Id = dto.Id,
                Order = dto.Order
            };
        }
    }
}
