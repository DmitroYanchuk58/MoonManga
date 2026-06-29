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

        [HttpGet("GetTag")]
        public async Task<IActionResult> GetTag(Guid id)
        {
            var readItem = await _service.GetTagByIdAsync(id);
            return Ok(readItem);
        }

        [HttpGet("GetTags")]
        public async Task<IActionResult> GetTags()
        {
            var tags = await _service.GetAllTagsAsync();
            return Ok(tags);
        }

        [HttpPost("CreateTag")]
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


        [HttpPut("UpdatePage")]
        public async Task<IActionResult> UpdatePage([FromBody] TagDTO tag)
        {
            await _service.UpdateTagAsync(tag);
            return Ok();
        }

        [HttpDelete("DeleteTag")]
        public async Task<IActionResult> DeleteTag(Guid id)
        {
            await _service.DeleteTagAsync(id);
            return Ok();
        }

        [HttpGet("GetTagByName")]
        public async Task<IActionResult> GetTagByName(string name)
        {
            var tags = await _service.GetTagByNameAsync(name);
            return Ok(tags);
        }
    }
}
