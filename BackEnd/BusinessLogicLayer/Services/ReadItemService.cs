using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.Features;
using BusinessLogicLayer.Features.CollectionProvider;
using BusinessLogicLayer.Features.Crud;
using BusinessLogicLayer.Features.ICrud;
using BusinessLogicLayer.Features.Join;
using BusinessLogicLayer.Helpers.Convertors;
using BusinessLogicLayer.Services.Interfaces;
using BusinessLogicLayer.Validation;
using DatabaseAccessLayer.DatabaseContext;
using DatabaseAccessLayer.Entities;
using DatabaseAccessLayer.Repositories;
using DatabaseAccessLayer.Repositories.Interfaces;


namespace BusinessLogicLayer.Services
{
    public class ReadItemService : IReadItemService
    {
        private ICrud<ReadItem, ReadItemDTO> _crud;
        private ICollectionProvider _collectionProvider;
        private IFinder<ReadItem> _finder;
        private IJoinProvider _joinProvider;

        public ReadItemService(CatalogDBContext context)
        {
            _crud = new Crud<ReadItem, ReadItemDTO>(context, new ConvertorReadItemDTOIntoReadItem(), new ReadItemValidator());
            _collectionProvider = new CollectionProvider(new CollectionProviderRepository<ReadItem>(context));
            _joinProvider = new ReadItemJoinProvider(context);
            _finder = new ReadItemFinder(context);
        }

        public async Task CreateReadItemAsync(ReadItemDTO item)
        {
            await _crud.CreateAsync(item);
        }

        public async Task<ReadItemDTO> GetReadItemByIdAsync(Guid id, bool includeTags = false)
        {
            if (includeTags)
            {
                var result = await GetReadItemWithTagsAsync(id);
                return result;
            }
            return await _crud.GetByIdAsync(id);
        }

        public async Task<List<ReadItemDTO>> GetAllReadItemsAsync()
        {
            return await _crud.GetAllAsync();
        }

        public async Task UpdateReadItemAsync(ReadItemDTO item)
        {
            await _crud.UpdateAsync(item);
        }

        public async Task DeleteReadItemAsync(Guid id)
        {
            await _crud.DeleteAsync(id);
        }

        public async Task<List<ReadItemDTO>> GetReadItemCollection(int collectionNumber, int collectionSize = 30)
        {
            return await _collectionProvider.GetItemsCollectionAsync(collectionNumber, collectionSize);
        }

        public async Task<int> GetTotalCountAsync()
        {
            return await _collectionProvider.GetTotalCountAsync();
        }

        public async Task<List<ReadItemDTO>> GetTopRatedReadItems(int collectionNumber, int collectionSize = 30)
        {
            return await _collectionProvider.GetTopRatedReadItems(collectionNumber, collectionSize);
        }

        public async Task<List<ReadItemDTO>> GetLessRatedReadItems(int collectionNumber, int collectionSize = 30)
        {
            return await _collectionProvider.GetLessRatedReadItems(collectionNumber, collectionSize);
        }

        public async Task<List<ReadItemDTO>> FindReadItemsByTitle(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                throw new ArgumentException("Title cannot be null or whitespace.", nameof(title));
            }
            var readItems = await _finder.SearhByTitle(title);

            if (readItems == null || !readItems.Any())
            {
                throw new KeyNotFoundException($"ReadItem with title '{title}' was not found.");
            }

            return readItems.Select(r => new ReadItemDTO(r)).ToList();
        }

        private async Task<ReadItemDTO> GetReadItemWithTagsAsync(Guid idReadItem)
        {
            return await _joinProvider.GetReadItemWithTagAsync(idReadItem);
        }
    }
}
