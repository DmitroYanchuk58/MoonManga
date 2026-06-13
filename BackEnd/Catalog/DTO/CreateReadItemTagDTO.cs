namespace API.DTO
{
    public class CreateReadItemTagDTO
    {
        public Guid Id { get; set; }

        public Guid IdReadItem { get; set; }

        public Guid IdTag { get; set; }
    }
}
