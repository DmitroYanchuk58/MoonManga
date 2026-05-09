using API.DTO;
using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReadItemTagController : ControllerBase
    {
        private readonly IReadItemTagService _service;

        public ReadItemTagController(IReadItemTagService service)
        {
            _service = service;
        }

        [HttpGet("GetReadItemTag")]
        public async Task<IActionResult> GetReadItemTag(Guid id)
        {
            var readItem = await _service.GetReadItemTagByIdAsync(id);
            return Ok(readItem);
        }

        [HttpGet("GetReadItemTags")]
        public async Task<IActionResult> GetReadItemTags()
        {
            var tags = await _service.GetAllReadItemTagsAsync();
            return Ok(tags);
        }

        [HttpPost("CreateReadItemTag")]
        public async Task<IActionResult> CreateReadItemTag([FromBody] CreateReadItemTagDTO request)
        {
            var item = new ReadItemTagDTO
            {
                Id = Guid.NewGuid(),
                IdReadItem = request.IdReadItem,
                IdTag = request.IdTag
            };
            await _service.CreateReadItemTagAsync(item);
            return Ok(item);
        }


        [HttpPut("UpdateReadItemTag")]
        public async Task<IActionResult> UpdateReadItemTag([FromBody] ReadItemTagDTO readItemTag)
        {
            await _service.UpdateReadItemTagAsync(readItemTag);
            return Ok();
        }

        [HttpDelete("DeleteReadItemTag")]
        public async Task<IActionResult> DeleteReadItemTag(Guid id)
        {
            await _service.DeleteReadItemTagAsync(id);
            return Ok();
        }
    }
}
