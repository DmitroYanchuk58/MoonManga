using BusinessLogicLayer.DTOs;
using DatabaseAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
