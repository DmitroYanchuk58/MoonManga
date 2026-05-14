using DatabaseAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseAccessLayer.Repositories.Interfaces
{
    public interface IJoin<TMain, TLink, TTarget>
        where TMain : Entity
        where TLink : Entity
        where TTarget : Entity
    {
        public Task<(TMain?, List<TTarget>?)> GetCombinedDataByIdAsync(
            Guid idEntity,
            Expression<Func<TMain, IEnumerable<TLink>>> linkProperty,
            Expression<Func<TLink, TTarget>> targetProperty);
    }
}
