using Courserio.Core.DTOs.Course;
using Courserio.Core.Filters;
using Courserio.Core.Helpers;
using Courserio.Core.Interfaces;
using Courserio.Core.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Courserio.Api.Controllers
{
    [SwaggerTag("See the platform's courses or create your own one.")]
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = "User")]
    public class CourseController : ControllerBase
    {

        private readonly ICourseService _courseService;

        public CourseController(ICourseService courseService)
        {
            _courseService = courseService;
        }


        /// <summary>
        /// See all courses.
        /// </summary>
        /// <remarks>
        /// You need to be registered to access this resource. <br/>
        /// You can filter courses by tags and sort them by different filters. <br/>
        /// The available filters are : titleA, titleD, dateA, dateD.
        /// </remarks>
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> ListAsync([FromQuery] CourseFilter courseFilter)
        {
            var ret = await _courseService.ListAsync(courseFilter);
            return Ok(ret);
        }
        
        [HttpGet("home")]
        public async Task<IActionResult> GetHomeCoursesAsync()
        {
            var ret = await _courseService.GetHomeAsync();
            return Ok(ret);
        }

        [Authorize]
        [HttpGet("recommended")]
        public async Task<IActionResult> GetRecommendedAsync()
        {
            var ret = await _courseService.GetRecommendedAsync(User.GetUsername());
            return Ok(ret);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var ret = await _courseService.GetByIdAsync(id, User.GetUsername());
            return Ok(ret);
        }

        

        /// <summary>
        /// Create a new course.
        /// </summary>
        /// <remarks>
        /// You need to be a creator to access this resource.
        /// </remarks>
        //[Authorize(Roles = "Creator")]
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] CourseCreateDto course)
        {
            await _courseService.CreateAsync(course);
            return Ok();
        }

        [HttpGet("update-ratings")]
        public async Task<IActionResult> UpdateRatingsAsync()
        {
            await _courseService.UpdateRatingsAsync();
            return Ok();
        }
    }
}
