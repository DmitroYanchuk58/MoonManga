using DatabaseAccessLayer.DatabaseContext;
using DatabaseAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseAccessLayer.Repositories
{
    public class ExistRepository<T> : IExist<T> where T : Entity
    {
        private readonly CatalogDBContext _context;
        private readonly DbSet<T> _dbSet;

        public ExistRepository(CatalogDBContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<bool> ExistAsync(Guid id)
        {
            return await _dbSet.AnyAsync(x => x.Id == id);
        }
    }
}
