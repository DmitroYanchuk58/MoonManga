using DatabaseLogicLayer.Entities;

namespace BusinessLogicLayer.DTOs
{
    public class Page_DTO : DTO
    {
        public int Order { get; set; }

        public byte[] Image { get; set; }

        public Guid IdChapter { get; set; }

        public Page_DTO() { }

        public Page_DTO(Page page)
        {
            Id = page.Id;
            Order = page.Order;
            Image = page.Image;
            IdChapter = page.IdChapter;
        }

        public Page GetPage()
        {
            return new Page() 
            {
                Id = this.Id,
                Order = this.Order,
                Image = this.Image,
                IdChapter = this.IdChapter
            };
        }
    }
}
