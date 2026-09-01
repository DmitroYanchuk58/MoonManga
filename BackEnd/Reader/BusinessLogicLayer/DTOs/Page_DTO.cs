using DatabaseLogicLayer.Entities;

namespace BusinessLogicLayer.DTOs
{
    public class Page_DTO : DTO
    {
        public int Order { get; set; }

        public string StorageKey { get; set; }

        public Guid IdChapter { get; set; }

        public Page_DTO() { }

        public Page_DTO(Page page)
        {
            Id = page.Id;
            Order = page.Order;
            StorageKey = page.StorageKey;
            IdChapter = page.IdChapter;
        }

        public Page GetPage()
        {
            return new Page() 
            {
                Id = this.Id,
                Order = this.Order,
                StorageKey = this.StorageKey,
                IdChapter = this.IdChapter
            };
        }
    }
}
