namespace DatabaseAccessLayer.Entities
{
    public class ReadItemTag: Entity
    {
        public Guid IdTag;

        public Tag Tag;

        public Guid IdReadItem;

        public ReadItem ReadItem;
    }
}
