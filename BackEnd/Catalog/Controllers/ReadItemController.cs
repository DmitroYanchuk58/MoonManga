using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReadItemController : ControllerBase
    {
        private IReadItemService _service;

        public ReadItemController(IReadItemService service)
        {
            _service = service;
        }

        [HttpGet("GetReadItems")]
        public async Task<IActionResult> GetReadItems()
        {
            var readItems = await _service.GetAllReadItemsAsync();
            return Ok(readItems);
        }

        [HttpGet("GetReadItem")]
        public async Task<IActionResult> GetReadItem(Guid id)
        {
            var readItem = await _service.GetReadItemByIdAsync(id);
            return Ok(readItem);
        }

        [HttpPost("CreateReadItem")]
        public async Task<IActionResult> CreateReadItem([FromBody] ReadItemDTO item)
        {
            await _service.CreateReadItemAsync(item);
            return Ok(item);
        }

        [HttpPut("UpdateReadItem")]
        public async Task<IActionResult> UpdateReadItem([FromBody] ReadItemDTO item)
        {
            await _service.UpdateReadItemAsync(item);
            return Ok();
        }

        [HttpDelete("DeleteReadItem")]
        public async Task<IActionResult> DeleteReadItem(Guid id)
        {
            await _service.DeleteReadItemAsync(id);
            return Ok();
        }
    }
}
