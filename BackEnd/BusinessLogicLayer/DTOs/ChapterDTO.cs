using BusinessLogicLayer.DTOs.Interfaces;
using DatabaseAccessLayer.Entities;

namespace BusinessLogicLayer.DTOs
{
    public class ChapterDTO : IConvertorIntoEntity<Chapter>
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public int Order { get; set; }

        public ChapterDTO() { }

        public ChapterDTO(Guid id, int order) : this()
        {
            this.Id = id;
            this.Order = order;
        }

        public ChapterDTO(Chapter chapter)
            : this(chapter.Id,
                   chapter.Order)
        {
        }

        public Chapter ConvertToEntity() => new Chapter
        {
            Id = this.Id,
            Order = this.Order
        };
    }
}
