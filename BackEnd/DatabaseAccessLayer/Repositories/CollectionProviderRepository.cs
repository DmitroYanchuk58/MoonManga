using DatabaseAccessLayer.DatabaseContext;
using DatabaseAccessLayer.Entities;
using DatabaseAccessLayer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DatabaseAccessLayer.Repositories
{
    public class CollectionProviderRepository<T> : ICollectionProviderRepository<T> where T : Entity
    {
        private readonly CatalogDBContext _context;

        public CollectionProviderRepository(CatalogDBContext context)
        {
            _context = context;
        }

        public async Task<List<T>> GetItemsCollectionAsync(int collectionNumber, int collectionSize = 30)
        {
            return await _context.Set<T>()
                .Skip((collectionNumber - 1) * collectionSize)
                .Take(collectionSize)
                .ToListAsync();
        }

        public async Task<int> GetTotalCountAsync()
        {
            return await _context.Set<T>().CountAsync();
        }
    }
}