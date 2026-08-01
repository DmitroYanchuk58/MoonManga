using DatabaseAccessLayer.Entities;

namespace BusinessLogicLayer.DTOs
{
    public class ReadItemTagDTO : DTO<ReadItemTag>
    {
        public Guid IdReadItem {  get; set; }

        public Guid IdTag { get; set; }

        public ReadItemTagDTO() : base() { }

        public ReadItemTagDTO(Guid id, Guid idReadItem, Guid idTag) : this()
        {
            Id = id;
            IdReadItem = idReadItem;
            IdTag = idTag;
        }

        public ReadItemTagDTO(ReadItemTag readItemTag) : base(readItemTag)
        {
            this.Id = readItemTag.Id;
            this.IdReadItem = readItemTag.IdReadItem;
            this.IdTag = readItemTag.IdTag;
        }
    }
}
