namespace DatabaseAccessLayer.Entities
{
    public class Page : Entity
    {
        public int Order { get; set; }

        public byte[] Image {  get; set; }
    }
}
