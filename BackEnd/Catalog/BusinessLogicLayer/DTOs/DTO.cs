using DatabaseAccessLayer.Entities;

namespace BusinessLogicLayer.DTOs
{
    public abstract class DTO <TEntity> where TEntity : Entity
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        protected DTO(){}

        protected DTO(TEntity entity)
        {
            this.Id = entity.Id;
        }
    }
}
