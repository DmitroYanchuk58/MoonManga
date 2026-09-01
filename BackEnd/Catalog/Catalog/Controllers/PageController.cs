using API.DTO;
using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
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
            var pages = await _service.GetAllPagesAsync();
            return Ok(pages);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPage(Guid id)
        {
            var page = await _service.GetPageByIdAsync(id);
            return Ok(page);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePage([FromBody]CreatePageDTO request)
        {
            var page = new PageDTO  
            {
                Id = request.Id != Guid.Empty ? request.Id : Guid.NewGuid(),
                Order = request.Order,
                Image = request.Image,
                IdChapter = request.IdChapter   
            };
            await _service.CreatePageAsync(page);
            return Ok(page);
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePage([FromBody] PageDTO page)
        {
            await _service.UpdatePageAsync(page);
            return Ok();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeletePage(Guid id)
        {
            await _service.DeletePageAsync(id);
            return Ok();
        }
    }
}
