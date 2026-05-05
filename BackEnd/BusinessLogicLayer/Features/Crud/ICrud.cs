using BusinessLogicLayer.DTOs;
using DatabaseAccessLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Features.ICrud
{
    public interface ICrud<TEntity, TDto>
    {
        public Task CreateAsync(TDto item);

        public Task DeleteAsync(Guid id);

        public Task<List<TDto>> GetAllAsync();

        public Task<TDto> GetByIdAsync(Guid id);

        public Task UpdateAsync(TDto item);
    }
}
