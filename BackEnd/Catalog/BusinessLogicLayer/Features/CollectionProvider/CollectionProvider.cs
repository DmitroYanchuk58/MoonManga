using BusinessLogicLayer.DTOs;
using DatabaseAccessLayer.Entities;
using DatabaseAccessLayer.Repositories.Interfaces;

namespace BusinessLogicLayer.Features.CollectionProvider
{
    public class CollectionProvider : ICollectionProvider
    {
        private readonly ICollectionProviderRepository<ReadItem> _collectionProvider;

        public CollectionProvider(ICollectionProviderRepository<ReadItem> collectionProvider)
        {
            _collectionProvider = collectionProvider;
        }

        public async Task<List<ReadItem>> GetReadItemCollection(int collectionNumber, int collectionSize = 30)
        {
            if (collectionNumber <= 0)
            {
                throw new ArgumentException("Collection number must be greater than zero.", nameof(collectionNumber));
            }
            if (collectionSize <= 0)
            {
                throw new ArgumentException("Collection size must be greater than zero.", nameof(collectionSize));
            }
            return await _collectionProvider.GetItemsCollectionAsync(collectionNumber, collectionSize);
        }

        public async Task<List<ReadItemDTO>> GetItemsCollectionAsync(int collectionNumber, int collectionSize = 30)
        {
            if (collectionNumber <= 0)
            {
                throw new ArgumentException("Collection number must be greater than zero.", nameof(collectionNumber));
            }
            if (collectionSize <= 0)
            {
                throw new ArgumentException("Collection size must be greater than zero.", nameof(collectionSize));
            }
            var items = await _collectionProvider.GetItemsCollectionAsync(collectionNumber, collectionSize);
            return items.Select(item => new ReadItemDTO(item)).ToList();
        }

        public async Task<int> GetTotalCountAsync()
        {
            return await _collectionProvider.GetTotalCountAsync();
        }

        public async Task<List<ReadItemDTO>> GetTopRatedReadItems(int collectionNumber, int collectionSize = 30)
        {
            if (collectionNumber <= 0)
            {
                throw new ArgumentException("Collection number must be greater than zero.", nameof(collectionNumber));
            }
            if (collectionSize <= 0)
            {
                throw new ArgumentException("Collection size must be greater than zero.", nameof(collectionSize));
            }
            var items = await _collectionProvider.GetTopRatedReadItems(collectionNumber, collectionSize);
            return items.Select(item => new ReadItemDTO(item)).ToList();
        }

        public async Task<List<ReadItemDTO>> GetLessRatedReadItems(int collectionNumber, int collectionSize = 30)
        {
            if (collectionNumber <= 0)
            {
                throw new ArgumentException("Collection number must be greater than zero.", nameof(collectionNumber));
            }
            if (collectionSize <= 0)
            {
                throw new ArgumentException("Collection size must be greater than zero.", nameof(collectionSize));
            }
            var items = await _collectionProvider.GetLessRatedReadItems(collectionNumber, collectionSize);
            return items.Select(item => new ReadItemDTO(item)).ToList();
        }
    }
}
