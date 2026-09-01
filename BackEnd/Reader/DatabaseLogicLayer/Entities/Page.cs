namespace DatabaseLogicLayer.Entities
{
    public class Page: Entity
    {
        public int Order { get; set; }

        public string StorageKey { get; set; } = string.Empty;

        public Guid IdChapter { get; set; }  
    }
}
