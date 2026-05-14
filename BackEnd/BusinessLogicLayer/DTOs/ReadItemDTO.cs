using BusinessLogicLayer.DTOs.Enums;
using DatabaseAccessLayer.Entities;

namespace BusinessLogicLayer.DTOs
{
    public class ReadItemDTO : DTO<ReadItem>
    {
        public string Title { get; set; }

        public ReadItemType Type { get; set; }

        public string Description { get; set; }
        public byte[] CoverImage { get; set; }

        public float Rating { get; set; }

        public List<TagDTO> Tags { get; set; } = new List<TagDTO>();

        public ReadItemDTO() : base() { }

        public ReadItemDTO(Guid id, string title, ReadItemType type, string description, byte[] coverImage, float rating, List<TagDTO> tags) : this()
        {
            Id = id;
            Title = title;
            Type = type;
            Description = description;
            CoverImage = coverImage;
            Rating = rating;
            Tags = tags;
        }

        public ReadItemDTO(ReadItem readItem)
            : base(readItem)
        {
            this.Id = readItem.Id;
            this.Title = readItem.Title;
            this.Type = Enum.Parse<ReadItemType>(readItem.Type, ignoreCase: true);
            this.Description = readItem.Description;
            this.CoverImage = readItem.CoverImage;
            this.Rating = readItem.Rating;
        }

        public ReadItemDTO(ReadItem readItem, List<TagDTO> tags)
            : this(readItem)
        {
            this.Tags = tags;
        }
    }
}
