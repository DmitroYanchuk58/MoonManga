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

        [HttpGet("GetPages")]
        public async Task<IActionResult> GetPages()
        {
            var pages = await _service.GetAllPagesAsync();
            return Ok(pages);
        }

        [HttpGet("GetPage")]
        public async Task<IActionResult> GetPage(Guid id)
        {
            var page = await _service.GetPageByIdAsync(id);
            return Ok(page);
        }

        [HttpPost("CreatePage")]
        public async Task<IActionResult> CreatePage([FromBody]CreatePageDTO request)
        {
            var page = new PageDTO  
            {
                Id = request.Id != Guid.Empty ? request.Id : Guid.NewGuid(),
                Order = request.Order,
                Image = request.Image   
            };
            await _service.CreatePageAsync(page);
            return Ok(page);
        }

        [HttpPut("UpdatePage")]
        public async Task<IActionResult> UpdatePage([FromBody] PageDTO page)
        {
            await _service.UpdatePageAsync(page);
            return Ok();
        }

        [HttpDelete("DeletePage")]
        public async Task<IActionResult> DeletePage(Guid id)
        {
            await _service.DeletePageAsync(id);
            return Ok();
        }
    }
}
