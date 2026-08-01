using BusinessLogicLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services.Interfaces
{
    public interface ITagService
    {
        public Task CreateTagAsync(TagDTO item);

        public Task<TagDTO> GetTagByIdAsync(Guid id);

        public Task<List<TagDTO>> GetAllTagsAsync();

        public Task UpdateTagAsync(TagDTO item);

        public Task DeleteTagAsync(Guid id);

        public Task<List<TagDTO>> GetTagByNameAsync(string name);
    }
}
