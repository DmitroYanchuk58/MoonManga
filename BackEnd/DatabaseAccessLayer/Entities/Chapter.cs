namespace DatabaseAccessLayer.Entities
{
    public class Chapter : Entity
    {
        public int Order { get; set; }

        public int Volume { get; set; }

        public string Title { get; set; }
    }
}
