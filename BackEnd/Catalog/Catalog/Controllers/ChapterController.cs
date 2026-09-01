using API.DTO;
using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChapterController : ControllerBase
    {
        private IChapterService _service;

        public ChapterController(IChapterService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetChapters()
        {
            var chapters = await _service.GetAllChaptersAsync();
            return Ok(chapters);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetChapter(Guid id)
        {
            var chapter = await _service.GetChapterByIdAsync(id);
            return Ok(chapter);
        }

        [HttpGet("by-read-item/{readItemId:guid}")]
        public async Task<IActionResult> GetChaptersByReadItemId(Guid readItemId)
        {
            var chapters = await _service.GetChaptersByReadItemIdAsync(readItemId);
            return Ok(chapters);
        }

        [HttpPost]
        public async Task<IActionResult> CreateChapter([FromBody] CreateChapterDTO request)
        {
            var chapter = new ChapterDTO
            {
                Id = request.Id != Guid.Empty ? request.Id : Guid.NewGuid(),
                Order = request.Order,
                Volume = request.Volume,
                Title = request.Title,
                IdReadItem = request.IdReadItem 
            };
            await _service.CreateChapterAsync(chapter);
            return Ok(chapter);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateChapter([FromBody] ChapterDTO chapter)
        {
            await _service.UpdateChapterAsync(chapter);
            return Ok();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteChapter(Guid id)
        {
            await _service.DeleteChapterAsync(id);
            return Ok();
        }
    }
}
