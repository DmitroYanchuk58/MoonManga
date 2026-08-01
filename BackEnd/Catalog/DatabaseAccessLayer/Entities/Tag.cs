namespace DatabaseAccessLayer.Entities
{
    public class Tag: Entity
    {
        public string Name { get; set; }    

        public ICollection<ReadItemTag> ReadItemTags { get; set; }
    }
}
