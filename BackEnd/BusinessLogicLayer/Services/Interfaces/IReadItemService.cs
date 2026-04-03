using BusinessLogicLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services.Interfaces
{
    public interface IReadItemService
    {
        public Task CreateReadItemAsync(ReadItemDTO item);

        public Task<ReadItemDTO> GetReadItemByIdAsync(Guid id);

        public Task<List<ReadItemDTO>> GetAllReadItemsAsync();

        public Task UpdateReadItemAsync(ReadItemDTO item);

        public Task DeleteReadItemAsync(Guid id);
    }
}
