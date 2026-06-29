using DatabaseAccessLayer.Entities;

namespace BusinessLogicLayer.DTOs
{
    public class ChapterDTO : DTO<Chapter>
    {
        public int Order { get; set; }

        public int Volume { get; set; }

        public string Title { get; set; }   

        public Guid? IdReadItem { get; set; }

        public List<Page>? Pages { get; set; } = new List<Page>();

        public ChapterDTO() : base() { }

        public ChapterDTO(Guid id, int order, int volume, string title, Guid? idReadItem, List<Page>? pages) : this()
        {
            this.Id = id;
            this.Order = order;
            this.Volume = volume;
            this.Title = title;
            this.IdReadItem = idReadItem;
            this.Pages = pages ?? new List<Page>();
        }

        public ChapterDTO(Chapter chapter)
            : base(chapter)
        {
            this.Id = chapter.Id;
            this.Order = chapter.Order;
            this.Title = chapter.Title;
            this.Volume = chapter.Volume;
            this.IdReadItem = chapter.IdReadItem;
            this.Pages = chapter.Pages ?? new List<Page>();
        }
    }
}
