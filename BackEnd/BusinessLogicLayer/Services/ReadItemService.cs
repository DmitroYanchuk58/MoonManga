using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.Validation;
using DatabaseAccessLayer.DatabaseContext;
using DatabaseAccessLayer.Entities;
using DatabaseAccessLayer.Repositories;

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
            ArgumentNullException.ThrowIfNull(item.Type, nameof(item.Type));
            if (!validationResult.IsValid)
            {
                var errors = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
                throw new ArgumentException($"Validation error: {errors}");
            }
            await _repository.CreateAsync(item.ToReadItem());
        }

        public async Task<ReadItemDTO> GetReadItemByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ReadItemDTO>> GetAllReadItemsAsync()
        {
            throw new NotImplementedException();
        }

        public async Task UpdateReadItemAsync(ReadItemDTO item)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteReadItemAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
