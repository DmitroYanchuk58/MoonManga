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

        public ReadItemDTO() : base() { }

        public ReadItemDTO(Guid id, string title, ReadItemType type, string description, byte[] coverImage, float rating) : this()
        {
            Id = id;
            Title = title;
            Type = type;
            Description = description;
            CoverImage = coverImage;
            Rating = rating;
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
    }
}
