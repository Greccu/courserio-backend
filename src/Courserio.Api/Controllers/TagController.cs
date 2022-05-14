using Courserio.Core.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Courserio.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagController : ControllerBase
    {
        private readonly ITagService _tagService;

        public TagController(ITagService tagService)
        {
            _tagService = tagService;
        }

        [HttpGet]
        public async Task<IActionResult> ListAsync()
        {
            var tags = await _tagService.ListAsync();
            return Ok(tags);
        }

        [HttpPost]
        public async Task<IActionResult> CreateManyAsync([FromBody] List<string> tags)
        {
            await _tagService.CreateManyAsync(tags);
            return Ok();
        }

    }
}
