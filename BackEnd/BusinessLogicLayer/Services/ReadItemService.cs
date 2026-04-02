using BusinessLogicLayer.DTOs;
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
            throw new NotImplementedException();
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
