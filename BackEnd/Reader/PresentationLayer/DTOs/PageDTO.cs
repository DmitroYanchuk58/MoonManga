using BusinessLogicLayer.DTOs;

namespace PresentationLayer.DTOs
{
    public class PageDTO
    {
        public Guid Id { get; set; }

        public int Order { get; set; }

        public byte[] Image { get; set; }

        public Guid IdChapter { get; set; }

        public Page_DTO GetPageDTO() => new Page_DTO() { Id = this.Id, IdChapter = this.IdChapter, Image = this.Image, Order = this.Order};
    }
}
