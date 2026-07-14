using API.DTO;
using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TagController : ControllerBase
    {
        private readonly ITagService _service;

        public TagController(ITagService service)
        {
            _service = service;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTag(Guid id)
        {
            var readItem = await _service.GetTagByIdAsync(id);
            return Ok(readItem);
        }

        [HttpGet]
        public async Task<IActionResult> GetTags()
        {
            var tags = await _service.GetAllTagsAsync();
            return Ok(tags);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTag([FromBody] CreateTagDTO request)
        {
            var item = new TagDTO
            {
                Id = request.Id != Guid.Empty ? request.Id : Guid.NewGuid(),
                Name = request.Tag
            };
            await _service.CreateTagAsync(item);
            return Ok(item);
        }


        [HttpPut]
        public async Task<IActionResult> UpdatePage([FromBody] TagDTO tag)
        {
            await _service.UpdateTagAsync(tag);
            return Ok();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteTag(Guid id)
        {
            await _service.DeleteTagAsync(id);
            return Ok();
        }

        [HttpGet("by-name")]
        public async Task<IActionResult> GetTagByName([FromQuery]string name)
        {
            var tags = await _service.GetTagByNameAsync(name);
            return Ok(tags);
        }
    }
}
