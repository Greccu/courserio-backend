using Courserio.Core.DTOs.Chapter;
using Courserio.Core.DTOs.Course;
using Courserio.Core.Filters;
using Courserio.Core.Interfaces;
using Courserio.Core.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var ret = await _chapterService.GetByIdAsync(id);
            return Ok(ret);
        }

        //[Authorize(Roles = "Creator")]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ChapterCreateDto chapter)
        {
            await _chapterService.CreateAsync(chapter);

            return Ok();
        }


    }
}
