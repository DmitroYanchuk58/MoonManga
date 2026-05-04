using BusinessLogicLayer.DTOs.Enums;
using BusinessLogicLayer.DTOs.Interfaces;
using DatabaseAccessLayer.Entities;

namespace BusinessLogicLayer.DTOs
{
    public class ReadItemDTO : DTO, IConvertorIntoEntity<ReadItem>
    {
        public string Title { get; set; }

        public ReadItemType Type { get; set; }

        public string Description { get; set; }
        public byte[] CoverImage { get; set; }

        public float Rating { get; set; }

        public ReadItemDTO() { }

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
            : this(readItem.Id,
                   readItem.Title,
                   Enum.Parse<ReadItemType>(readItem.Type, ignoreCase: true),
                   readItem.Description,
                   readItem.CoverImage,
                   readItem.Rating)
        {
        }

        public ReadItem ConvertToEntity() => new ReadItem
        {
            Id = this.Id,
            Title = this.Title,
            Type = this.Type.ToString(),
            Description = this.Description,
            CoverImage = this.CoverImage,
            Rating = this.Rating         
        };
    }
}
