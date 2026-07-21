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
        private readonly IReadItemService _service;

        public ReadItemController(IReadItemService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetReadItems(
            [FromQuery] string? title = null,
            [FromQuery] int? page = null,
            [FromQuery] int? pageSize = null,
            [FromQuery] string? sortBy = null)
        {
            if (!string.IsNullOrEmpty(title))
            {
                var searchResults = await _service.FindReadItemsByTitle(title);
                return Ok(searchResults);
            }

            if (page.HasValue && pageSize.HasValue)
            {
                if (sortBy == "rating_desc")
                {
                    var descRatingResult = await _service.GetTopRatedReadItems(page.Value, pageSize.Value);
                    return Ok(descRatingResult);
                }
                if (sortBy == "rating_asc")
                {
                    var ascRatingResult = await _service.GetLessRatedReadItems(page.Value, pageSize.Value);
                    return Ok(ascRatingResult);
                }

                var result = await _service.GetReadItemCollection(page.Value, pageSize.Value);
                return Ok(result);
            }

            var readItems = await _service.GetAllReadItemsAsync();
            return Ok(readItems);
        }

        [HttpGet("{id:guid}")] 
        public async Task<IActionResult> GetReadItem(
            Guid id,
            [FromQuery] bool includeTags = false,
            [FromQuery] bool includeChapters = false)
        {
            var readItem = await _service.GetReadItemByIdAsync(id, includeTags, includeChapters);
            if (readItem == null)
            {
                return NotFound();
            }
            return Ok(readItem);
        }

        [HttpGet("count")]
        public async Task<IActionResult> GetCountReadItems()
        {
            var count = await _service.GetTotalCountAsync();
            return Ok(count);
        }

        [HttpPost]
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

        [HttpPut]
        public async Task<IActionResult> UpdateReadItem([FromBody] ReadItemDTO item)
        {
            await _service.UpdateReadItemAsync(item);
            return Ok();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteReadItem(Guid id)
        {
            await _service.DeleteReadItemAsync(id);
            return Ok();
        }
    }
}