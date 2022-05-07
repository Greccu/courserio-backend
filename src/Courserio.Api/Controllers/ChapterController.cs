using Courserio.Core.DTOs.Chapter;
using Courserio.Core.DTOs.Course;
using Courserio.Core.Filters;
using Courserio.Core.Interfaces;
using Courserio.Core.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;



namespace Courserio.Api.Controllers
{
    [SwaggerTag("See the platform's chapters or create your own one.")]
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = "User")]
    public class ChapterController : ControllerBase
    {

        private readonly IChapterService _chapterService;

        public ChapterController(IChapterService chapterService)
        {
            _chapterService = chapterService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var ret = await _chapterService.GetByIdAsync(id);
            return Ok(ret);
        }

        //[Authorize(Roles = "Creator")]
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] ChapterCreateDto chapter)
        {
            await _chapterService.CreateAsync(chapter);

            return Ok();
        }


        //[Authorize]
        //[HttpGet]
        //public async Task<IActionResult> GetAllAsync([FromQuery] ChapterFilter chapterFilter)
        //{
        //    var ret = await _chapterService.ListAsync(chapterFilter);
        //    return Ok(ret);
        //}

        //[HttpGet("home")]
        //public async Task<IActionResult> GetHomeChaptersAsync()
        //{
        //    var ret = await _chapterService.GetHomeAsync();
        //    return Ok(ret);
        //}

    }
}
