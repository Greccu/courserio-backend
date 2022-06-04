using Courserio.Core.Helpers;
using Courserio.Core.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize]
        [HttpPost("follow/{id}")]
        public async Task<IActionResult> FollowAsync(int id)
        {
            var username = User.GetUsername();
            await _tagService.FollowAsync(id, username);
            return Ok();
        }

        [Authorize]
        [HttpPost("unfollow/{id}")]
        public async Task<IActionResult> UnfollowAsync(int id)
        {
            var username = User.GetUsername();
            await _tagService.UnfollowAsync(id, username);
            return Ok();
        }

    }
}
