using DatabaseAccessLayer.DatabaseContext;
using DatabaseAccessLayer.Entities;
using DatabaseAccessLayer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseAccessLayer.Repositories
{
    public class ReadItemFinder : IFinder<ReadItem>
    {
        private readonly CatalogDBContext _context;

        public ReadItemFinder(CatalogDBContext context)
        {
            _context = context;
        }
        public async Task<List<ReadItem>> SearhByTitle(string title)
        {
            return await _context.Set<ReadItem>()
                    .AsNoTracking()
                    .Where(r => r.Title.Contains(title)).ToListAsync();
        }
    }
}
