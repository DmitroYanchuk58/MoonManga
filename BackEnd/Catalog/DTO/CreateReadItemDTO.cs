using BusinessLogicLayer.DTOs;

namespace API.DTO
{
    public class CreateReadItemDTO
    {
        public string Title { get; set; }

        public ReadItemType Type { get; set; } 
    }
}
