namespace DatabaseAccessLayer.Entities
{
    public class ReadItem : Entity
    {
        public string Title { get; set; }
        //enum on layer higher up, but here is string
        public string Type { get; set; }

        public string Description { get; set; }

        public byte[] CoverImage { get; set; }

        public float Rating { get; set; }

        public ICollection<ReadItemTag> ReadItemTags { get; set; }

        public ICollection<Chapter> Chapters { get; set; }
    }
}
