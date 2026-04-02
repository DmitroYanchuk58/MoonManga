using DatabaseAccessLayer.Entities;

namespace BusinessLogicLayer.DTOs
{
    public class ReadItemDTO
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Title { get; set; }

        public ReadItemType Type { get; set; }

        public ReadItemDTO() { }

        public ReadItemDTO(Guid id, string title, ReadItemType type) : this()
        {
            Id = id;
            Title = title;
            Type = type;
        }

        public ReadItemDTO(ReadItem readItem)
            : this(readItem.Id,
                   readItem.Title,
                   Enum.Parse<ReadItemType>(readItem.Type, ignoreCase: true))
        {
        }

        public ReadItem ToReadItem() => new ReadItem
        {
            Id = this.Id,
            Title = this.Title,
            Type = this.Type.ToString()
        };    
    }
}
