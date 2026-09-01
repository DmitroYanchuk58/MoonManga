using BusinessLogicLayer.DTOs;

namespace PresentationLayer.DTOs
{
    public class PageDTO
    {
        public Guid Id { get; set; }

        public int Order { get; set; }

        public string StorageKey { get; set; }

        public Guid IdChapter { get; set; }

        public Page_DTO GetPageDTO() => new Page_DTO() { Id = this.Id, IdChapter = this.IdChapter, StorageKey = this.StorageKey, Order = this.Order};
    }
}
