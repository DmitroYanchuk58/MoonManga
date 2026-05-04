namespace DatabaseAccessLayer.Entities
{
    public class ReadItem : Entity
    {
        public string Title { get; set; }
        //Gonna be enum on layer higher up, but for now, string is fine
        public string Type { get; set; }

        public string Description { get; set; }

        public byte[] CoverImage { get; set; }

        public float Rating { get; set; }
    }
}
