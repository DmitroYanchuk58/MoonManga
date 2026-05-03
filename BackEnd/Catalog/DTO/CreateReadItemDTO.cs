using BusinessLogicLayer.DTOs.Enums;

namespace API.DTO
{
    public class CreateReadItemDTO
    {
        public string Title { get; set; }

        public ReadItemType Type { get; set; } 

        public string Description { get; set; }

        public byte[] CoverImage { get; set; }
    }
}
