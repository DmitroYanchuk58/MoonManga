using BusinessLogicLayer.DTOs;
using DatabaseAccessLayer.Entities;

namespace BusinessLogicLayer.Helpers.Convertors.Interfaces
{
    public interface IConvertorDTOIntoEntity<TEntity, TDto>
        where TEntity : Entity
        where TDto : DTO<TEntity>
    {
        TEntity ConvertToEntity(TDto dto);

        TDto ConvertToDto(TEntity entity);
    }
}
