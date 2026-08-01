using BusinessLogicLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services.Interfaces
{
    public interface IPageService
    {
        public Task CreatePageAsync(PageDTO item);

        public Task<PageDTO> GetPageByIdAsync(Guid id);

        public Task<List<PageDTO>> GetAllPagesAsync();  

        public Task UpdatePageAsync(PageDTO item);

        public Task DeletePageAsync(Guid id);
    }
}
