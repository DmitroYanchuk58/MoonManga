namespace DatabaseLogicLayer.Entities
{
    public class Page: Entity
    {
        public int Order { get; set; }

        public byte[] Image { get; set; }

        public Guid IdChapter { get; set; } = 
    }
}
