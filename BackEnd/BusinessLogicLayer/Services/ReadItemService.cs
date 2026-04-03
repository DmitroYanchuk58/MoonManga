using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.Validation;
using DatabaseAccessLayer.DatabaseContext;
using DatabaseAccessLayer.Entities;
using DatabaseAccessLayer.Repositories;
using System.ComponentModel.DataAnnotations;

namespace BusinessLogicLayer.Services
{
    public class ReadItemService
    {
        private ICRUD<ReadItem> _repository;
        public ReadItemService(CatalogDBContext context)
        {
            _repository = new CrudRepository<ReadItem>(context);
        }

        public async Task CreateReadItemAsync(ReadItemDTO item)
        {
            var validator = new ReadItemValidator();
            var validationResult = await validator.ValidateAsync(item);
            ArgumentNullException.ThrowIfNull(item.Title, nameof(item.Title));
            if (!validationResult.IsValid)
            {
                var errors = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
                throw new ArgumentException($"Validation error: {errors}");
            }
            await _repository.CreateAsync(item.ToReadItem());
        }

        public async Task<ReadItemDTO> GetReadItemByIdAsync(Guid id)
        {
            var readItem = await _repository.GetByIdAsync(id);
            if (readItem == null)
            {
                throw new KeyNotFoundException();
            }

            return new ReadItemDTO(readItem);
        }

        public async Task<List<ReadItemDTO>> GetAllReadItemsAsync()
        {
            var readItems = await _repository.GetAllAsync();
            var items = readItems
                            .Select(r => new ReadItemDTO(r))
                            .ToList();
            return items;
        }

        public async Task UpdateReadItemAsync(ReadItemDTO item)
        {
            if(item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            var userFromDb = await _repository.GetByIdAsync(item.Id);
            if (userFromDb == null)
            {
                throw new KeyNotFoundException($"ReadItem with id {item.Id} not found");
            }

            if (!Enum.IsDefined(typeof(ReadItemType), item.Type))
            {
                throw new ArgumentException($"Value {item.Type} is not a valid ReadItemType");
            }

            var validator = new ReadItemValidator();
            var validationResult = await validator.ValidateAsync(item);
            ArgumentNullException.ThrowIfNull(item.Title, nameof(item.Title));
            if (!validationResult.IsValid)
            {
                var errors = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
                throw new ArgumentException($"Validation error: {errors}");
            }

            await _repository.UpdateAsync(item.ToReadItem());
        }

        public async Task DeleteReadItemAsync(Guid id)
        {
            if(id == Guid.Empty)
            {
                throw new ArgumentException(nameof(id));
            }
            await _repository.DeleteAsync(id);
        }
    }
}
