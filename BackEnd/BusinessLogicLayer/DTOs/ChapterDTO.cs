using DatabaseAccessLayer.Entities;

namespace BusinessLogicLayer.DTOs
{
    public class ChapterDTO : DTO<Chapter>
    {
        public int Order { get; set; }



        public ChapterDTO() : base() { }

        public ChapterDTO(Guid id, int order) : this()
        {
            this.Id = id;
            this.Order = order;
        }

        public ChapterDTO(Chapter chapter)
            : base(chapter)
        {
            this.Id = chapter.Id;
            this.Order = chapter.Order;
        }
    }
}
