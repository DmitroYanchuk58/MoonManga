using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.Services.Interfaces;
using BusinessLogicLayer.Validation;
using DatabaseAccessLayer.DatabaseContext;
using DatabaseAccessLayer.Entities;
using DatabaseAccessLayer.Repositories;
using DatabaseAccessLayer.Repositories.Interfaces;
using FluentValidation;

namespace BusinessLogicLayer.Services
{
    public class ReadItemService : IReadItemService
    {
        private ICRUD<ReadItem> _readItemRepository;
        private IExist<ReadItem> _existRepository;
        private ICollectionProviderRepository<ReadItem> _collectionProvider;
        public ReadItemService(CatalogDBContext context)
        {
            _readItemRepository = new CrudRepository<ReadItem>(context);
            _existRepository = new ExistRepository<ReadItem>(context);
            _collectionProvider = new CollectionProviderRepository<ReadItem>(context);
        }

        public async Task CreateReadItemAsync(ReadItemDTO item)
        {
            ArgumentNullException.ThrowIfNull(item);

            var validator = new ReadItemValidator();
            var validationResult = await validator.ValidateAsync(item);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            if (await _existRepository.ExistAsync(item.Id))
            {
                throw new InvalidOperationException($"Item with ID {item.Id} already exists.");
            }

            await _readItemRepository.CreateAsync(item.ConvertToEntity());
        }

        public async Task<ReadItemDTO> GetReadItemByIdAsync(Guid id)
        {
            if(id == Guid.Empty)
            {
                throw new ArgumentException("Invalid identifier. Guid cannot be empty.", nameof(id));
            }
            var readItem = await _readItemRepository.GetByIdAsync(id);
            if (readItem == null)
            {
                throw new KeyNotFoundException($"ReadItem with ID {id} was not found.");
            }

            return new ReadItemDTO(readItem);
        }

        public async Task<List<ReadItemDTO>> GetAllReadItemsAsync()
        {
            var readItems = await _readItemRepository.GetAllAsync();
            var items = readItems
                            .Select(r => new ReadItemDTO(r))
                            .ToList();
            return items;
        }

        public async Task UpdateReadItemAsync(ReadItemDTO item)
        {
            ArgumentNullException.ThrowIfNull(item);
            var validator = new ReadItemValidator();

            var validationResult = await validator.ValidateAsync(item);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors.ToString()); 
            }

            if (!await _existRepository.ExistAsync(item.Id))
            {
                throw new KeyNotFoundException($"ReadItem with id {item.Id} not found");
            }

            await _readItemRepository.UpdateAsync(item.ConvertToEntity());
        }

        public async Task DeleteReadItemAsync(Guid id)
        {
            if(id == Guid.Empty)
            {
                throw new ArgumentException(nameof(id));
            }
            await _readItemRepository.DeleteAsync(id);
        }

        public async Task<List<ReadItem>> GetReadItemCollection(int collectionNumber, int collectionSize = 30)
        {
            if(collectionNumber <= 0)
            {
                throw new ArgumentException("Collection number must be greater than zero.", nameof(collectionNumber));
            }
            if(collectionSize <= 0)
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
    }
}
