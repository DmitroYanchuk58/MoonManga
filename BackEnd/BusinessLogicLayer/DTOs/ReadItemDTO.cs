using BusinessLogicLayer.DTOs.Enums;
using BusinessLogicLayer.DTOs.Interfaces;
using DatabaseAccessLayer.Entities;

namespace BusinessLogicLayer.DTOs
{
    public class ReadItemDTO : DTO, IConvertorIntoEntity<ReadItem>
    {
        public string Title { get; set; }

        public ReadItemType Type { get; set; }

        public byte[] CoverImage { get; set; }

        public ReadItemDTO() { }

        public ReadItemDTO(Guid id, string title, ReadItemType type, byte[] coverImage) : this()
        {
            Id = id;
            Title = title;
            Type = type;
            CoverImage = coverImage;
        }

        public ReadItemDTO(ReadItem readItem)
            : this(readItem.Id,
                   readItem.Title,
                   Enum.Parse<ReadItemType>(readItem.Type, ignoreCase: true),
                   readItem.CoverImage)
        {
        }

        public ReadItem ConvertToEntity() => new ReadItem
        {
            Id = this.Id,
            Title = this.Title,
            Type = this.Type.ToString(),
            CoverImage = this.CoverImage
        };
    }
}
