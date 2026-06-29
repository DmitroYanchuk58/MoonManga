using API.DTO;
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

        [HttpGet("GetReadItemWithTags")]
        public async Task<IActionResult> GetReadItemWithTags(Guid id)
        {
            var readItem = await _service.GetReadItemByIdAsync(id, includeTags: true);
            return Ok(readItem);
        }

        [HttpGet("GetReadItemFullInfo")]
        public async Task<IActionResult> GetReadItemsFullInfo(Guid id)
        {
            var readItems = await _service.GetReadItemByIdAsync(id, includeTags: true, includeChapters: true);
            return Ok(readItems);
        }

        [HttpGet("GetReadItemsCollection")]
        public async Task<IActionResult> GetReadItemsCollection(int collectionNumber, int collectionSize)
        {
            var readItems = await _service.GetReadItemCollection(collectionNumber, collectionSize);
            return Ok(readItems);
        }

        [HttpGet("GetCountReadItems")]
        public async Task<IActionResult> GetCountReadItems()
        {
            var count = await _service.GetTotalCountAsync();
            return Ok(count);
        }

        [HttpGet("FindReadItemByTitle")]
        public async Task<IActionResult> FindReadItemByTitle(string title)
        {
            var readItems = await _service.FindReadItemsByTitle(title);
            return Ok(readItems);
        }

        [HttpGet("GetTopRatedReadItems")]
        public async Task<IActionResult> GetTopRatedReadItems(int collectionNumber, int collectionSize)
        {
            var readItems = await _service.GetTopRatedReadItems(collectionNumber, collectionSize);
            return Ok(readItems);
        }

        [HttpGet("GetLessRatedReadItems")]
        public async Task<IActionResult> GetLessRatedReadItems(int collectionNumber, int collectionSize)
        {
            var readItems = await _service.GetLessRatedReadItems(collectionNumber, collectionSize);
            return Ok(readItems);
        }

        [HttpPost("CreateReadItem")]
        public async Task<IActionResult> CreateReadItem([FromBody] CreateReadItemDTO request)
        {
            var item = new ReadItemDTO
            {
                Id = request.Id != Guid.Empty ? request.Id : Guid.NewGuid(),
                Title = request.Title,
                Type = request.Type,
                CoverImage = request.CoverImage,
                Description = request.Description,
                Rating = request.Rating,
            };
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
