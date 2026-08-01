using BusinessLogicLayer.DTOs;
using DatabaseAccessLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

        public Task<List<TDto>> GetByConditionAsync(Expression<Func<TEntity, bool>> predicate);
    }
}
