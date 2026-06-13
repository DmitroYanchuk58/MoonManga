namespace API.DTO
{
    public class CreatePageDTO
    {
        public Guid Id { get; set; }

        public int Order { get; set; }

        public byte[] Image { get; set; }
    }
}
