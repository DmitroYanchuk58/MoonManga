namespace API.DTO
{
    public class CreateChapterDTO
    {
        public Guid Id { get; set; }

        public int Order { get; set; }  

        public string Title { get; set; }

        public int Volume { get; set; }

        public Guid? IdReadItem { get; set; }
    }
}
