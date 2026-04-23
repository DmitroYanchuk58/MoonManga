using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.Services.Interfaces;
using BusinessLogicLayer.Validation;
using DatabaseAccessLayer.DatabaseContext;
using DatabaseAccessLayer.Entities;
using DatabaseAccessLayer.Repositories;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    public class PageService : IPageService
    {
        private ICRUD<Page> _crudRepository;
        private IExist<Page> _existRepository;

        public PageService(CatalogDBContext context)
        {
            _crudRepository = new CrudRepository<Page>(context);
            _existRepository = new ExistRepository<Page>(context);
        }

        public async Task CreatePageAsync(PageDTO item)
        {
            ArgumentNullException.ThrowIfNull(item);

            var validator = new PageValidator();
            var validationResult = await validator.ValidateAsync(item);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            if (await _existRepository.ExistAsync(item.Id))
            {
                throw new InvalidOperationException($"Item with ID {item.Id} already exists.");
            }

            await _crudRepository.CreateAsync(item.ConvertToEntity());
        }

        public async Task DeletePageAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException(nameof(id));
            }
            await _crudRepository.DeleteAsync(id);
        }

        public async Task<List<PageDTO>> GetAllPagesAsync()
        {
            var pages = await _crudRepository.GetAllAsync();
            var items = pages.Select(r => new PageDTO(r))
                            .ToList();
            return items;
        }

        public async Task<PageDTO> GetPageByIdAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("Invalid identifier. Guid cannot be empty.", nameof(id));
            }
            var page = await _crudRepository.GetByIdAsync(id);
            if (page == null)
            {
                throw new KeyNotFoundException($"Page with ID {id} was not found.");
            }

            return new PageDTO(page);
        }

        public async Task UpdatePageAsync(PageDTO item)
        {
            ArgumentNullException.ThrowIfNull(item);
            var validator = new PageValidator();

            var validationResult = await validator.ValidateAsync(item);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors.ToString());
            }

            if (!await _existRepository.ExistAsync(item.Id))
            {
                throw new KeyNotFoundException($"Page with id {item.Id} not found");
            }

            await _crudRepository.UpdateAsync(item.ConvertToEntity());
        }
    }
}
