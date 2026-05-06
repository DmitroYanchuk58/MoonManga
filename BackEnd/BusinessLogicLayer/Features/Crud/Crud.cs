using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.Features.ICrud;
using BusinessLogicLayer.Helpers.Convertors.Interfaces;
using DatabaseAccessLayer.DatabaseContext;
using DatabaseAccessLayer.Entities;
using DatabaseAccessLayer.Repositories;
using DatabaseAccessLayer.Repositories.Interfaces;
using FluentValidation;
using System.Linq.Expressions;

namespace BusinessLogicLayer.Features.Crud
{
    public class Crud<TEntity, TDto> : ICrud<TEntity, TDto>
        where TEntity : Entity
        where TDto : DTO<TEntity>
    {
        private ICRUD<TEntity> _crudRepository;
        private IExist<TEntity> _existRepository;
        private AbstractValidator<TDto> _validator;
        private readonly IConvertorDTOIntoEntity<TEntity, TDto> _convertor;

        public Crud(CatalogDBContext context, IConvertorDTOIntoEntity<TEntity, TDto> convertor, AbstractValidator<TDto> validator)
        {
            _crudRepository = new CrudRepository<TEntity>(context);
            _existRepository = new ExistRepository<TEntity>(context);
            this._convertor = convertor;
            this._validator = validator;
        }

        public async Task CreateAsync(TDto item)
        {
            ArgumentNullException.ThrowIfNull(item);

            var validationResult = await _validator.ValidateAsync(item);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            if (await _existRepository.ExistAsync(item.Id))
            {
                throw new InvalidOperationException($"Item with ID {item.Id} already exists.");
            }

            await _crudRepository.CreateAsync(_convertor.ConvertToEntity(item));
        }

        public async Task DeleteAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException(nameof(id));
            }
            await _crudRepository.DeleteAsync(id);
        }

        public async Task<List<TDto>> GetAllAsync()
        {
            var entities = await _crudRepository.GetAllAsync();
            return entities.Select(_convertor.ConvertToDto).ToList();
        }

        public async Task<TDto> GetByIdAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("Invalid identifier. Guid cannot be empty.", nameof(id));
            }
            var entity = await _crudRepository.GetByIdAsync(id);
            if (entity == null)
            {
                throw new KeyNotFoundException($"Entity with ID {id} was not found.");
            }

            return _convertor.ConvertToDto(entity);
        }

        public async Task UpdateAsync(TDto item)
        {
            ArgumentNullException.ThrowIfNull(item);

            var validationResult = await _validator.ValidateAsync(item);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors.ToString());
            }

            if (!await _existRepository.ExistAsync(item.Id))
            {
                throw new KeyNotFoundException($"Page with id {item.Id} not found");
            }

            await _crudRepository.UpdateAsync(_convertor.ConvertToEntity(item));
        }

        public async Task<List<TDto>> GetByConditionAsync(Expression<Func<TEntity, bool>> predicate)
        {
            var entities = await _crudRepository.GetByConditionAsync(predicate);
            if(entities == null || !entities.Any())
            {
                throw new KeyNotFoundException("No entities found matching the specified condition.");
            }
            return entities.Select(_convertor.ConvertToDto).ToList();
        }
    }
}
