namespace BusinessLogicLayer.DTOs
{
    public class ReadItemDTO
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public ReadItemType Type { get; set; }
    }
}
