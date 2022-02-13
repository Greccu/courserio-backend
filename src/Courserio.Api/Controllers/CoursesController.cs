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
    [SwaggerTag("See the platform's courses or create your own one.")]
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = "User")]
    public class CoursesController : ControllerBase
    {

        private readonly ICourseService _courseService;

        public CoursesController(ICourseService courseService)
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
        public async Task<IActionResult> GetAll([FromQuery] CourseFilter courseFilter)
        {
            var ret = await _courseService.ListAsync(courseFilter);
            return Ok(ret);
        }
        
        [HttpGet("home")]
        public async Task<IActionResult> GetHomeCourses()
        {
            var ret = await _courseService.GetHomeAsync();
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
        public async Task<IActionResult> Post([FromBody] CourseCreateDto course)
        {
            await _courseService.CreateAsync(course);
            return Ok();
        }

        ///// <summary>
        ///// See a course.
        ///// </summary>
        ///// <remarks>
        ///// You need to be registered to access this resource.
        ///// </remarks>
        //[HttpGet("{id}")]
        //public IActionResult Get(int id)
        //{
        //    var ret = _courseService.Get(id);
        //    if (ret == null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(ret);
        //}




        ///// <summary>
        ///// Update a course.
        ///// </summary>
        ///// <remarks>
        ///// You need to be a creator to access this resource.
        ///// </remarks>
        //[HttpPut("{id}")]
        //[Authorize(Roles = "Creator")]
        //public IActionResult Put(int id, [FromBody] CoursePost course)
        //{

        //    CoursePost ret = _courseService.UpdateCourse(id, course, CurrentUserId());
        //    if(ret == null)
        //    {
        //        return Forbid();
        //    }
        //    return Ok();
        //}

        ///// <summary>
        ///// Delete a course.
        ///// </summary>
        ///// <remarks>
        ///// You need to be a creator or a moderator to access this resource.
        ///// </remarks>
        //[Authorize(Roles = "Creator,Moderator")]
        //[HttpDelete("{id}")]
        //public IActionResult Delete(int id)
        //{
        //    string userId = User.IsInRole("Moderator") ? "Moderator" : CurrentUserId();
        //    var ret = _courseService.DeleteCourse(id,userId);
        //    if(ret == null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(ret);
        //}

        //[NonAction]
        //private string CurrentUserId()
        //{
        //    return User.FindFirst(ClaimTypes.NameIdentifier).Value;
        //}


        ///// <summary>
        ///// Join a course to have access to it's resources.
        ///// </summary>
        ///// <remarks>
        ///// You need to be registered to access this resource.
        ///// </remarks>
        //[HttpPost("{id}")]
        //public IActionResult Join(int id)
        //{
        //    var ret = _courseService.AssignUserToCourse(id, CurrentUserId());
        //    if (ret == null)
        //    {
        //        return BadRequest("You have already joined this course or the course does not exists");
        //    }
        //    return Ok(ret);
        //}

        ///// <summary>
        ///// Leave a course.
        ///// </summary>
        ///// <remarks>
        ///// You need to be registered to access this resource.
        ///// </remarks>
        //[HttpPost("{id}")]
        //public IActionResult Leave(int id)
        //{
        //    var ret = _courseService.RemoveUserFromCourse(id, CurrentUserId());
        //    if (ret == null)
        //    {
        //        return BadRequest("You are not part of this course or the course does not exists");
        //    }
        //    return Ok();
        //}
    }
}
