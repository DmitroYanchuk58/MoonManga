using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.Services;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer.DTOs;

namespace PresentationLayer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PagesController : ControllerBase
    {
        private readonly IPageService _pageService;

        public PagesController(IPageService pageService)
        {
            _pageService = pageService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] Guid chapterId, [FromForm] int order, IFormFile file)
        {
            await using var stream = file.OpenReadStream();
            var dto = new CreatePageDto(chapterId, order, stream, file.FileName, file.ContentType);

            var result = await _pageService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _pageService.GetByIdAsync(id);
            return result == null ? NotFound() : Ok(result);
        }

        [HttpGet("chapter/{chapterId:guid}")]
        public async Task<IActionResult> GetAllByChapterId(Guid chapterId)
        {
            var result = await _pageService.GetAllByChapterId(chapterId);
            return Ok(result);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromForm] int order, IFormFile? file)
        {
            Stream? stream = file != null ? file.OpenReadStream() : null;
            var dto = new UpdatePageDto(id, order, stream, file?.FileName, file?.ContentType);

            var result = await _pageService.UpdateAsync(dto);
            return Ok(result);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleted = await _pageService.DeleteAsync(id);
            return deleted ? NoContent() : NotFound();
        }
    }
}
