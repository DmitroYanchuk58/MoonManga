using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.Services;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer.DTOs;

namespace PresentationLayer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PageController : ControllerBase
    {
        private readonly IPageService _service;

        public PageController(IPageService pageService)
        {
            _service = pageService;
        }

        [HttpGet]
        public async Task<IActionResult> GetPages()
        {
            var pages = await _service.GetAllAsync();
            return Ok(pages);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPage(Guid id)
        {
            var page = await _service.GetByIdAsync(id);
            return Ok(page);
        }

        [HttpGet("chapter/{idChapter}")]
        public async Task<IActionResult> GetPagesByIdChapter(Guid idChapter)
        {
            var pages = await _service.GetAllByChapterId(idChapter);
            return Ok(pages);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePage([FromBody]PageDTO page)
        {
            await _service.CreateAsync(page.GetPageDTO());
            return Ok(page);
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePage([FromBody] PageDTO page)
        {
            await _service.UpdateAsync(page.GetPageDTO());
            return Ok();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeletePage(Guid id)
        {
            await _service.DeleteAsync(id);
            return Ok();
        }
    }
}
