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

        [HttpGet("GetChapters")]
        public async Task<IActionResult> GetChapters()
        {
            var chapters = await _service.GetAllChaptersAsync();
            return Ok(chapters);
        }

        [HttpGet("GetChapter")]
        public async Task<IActionResult> GetChapter(Guid id)
        {
            var chapter = await _service.GetChapterByIdAsync(id);
            return Ok(chapter);
        }

        [HttpPost("CreateChapter")]
        public async Task<IActionResult> CreateChapter([FromBody] CreateChapterDTO request)
        {
            var chapter = new ChapterDTO
            {
                Order = request.Order
            };
            await _service.CreateChapterAsync(chapter);
            return Ok(chapter);
        }

        [HttpPut("UpdateChapter")]
        public async Task<IActionResult> UpdateChapter([FromBody] ChapterDTO chapter)
        {
            await _service.UpdateChapterAsync(chapter);
            return Ok();
        }

        [HttpDelete("DeleteChapter")]
        public async Task<IActionResult> DeleteChapter(Guid id)
        {
            await _service.DeleteChapterAsync(id);
            return Ok();
        }
    }
}
