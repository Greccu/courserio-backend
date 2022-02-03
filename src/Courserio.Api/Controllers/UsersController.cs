using Courserio.Core.DTOs;
using Courserio.Core.DTOs.User;
using Courserio.Core.Filters;
using Courserio.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Courserio.Api.Controllers
{

    [SwaggerTag("Create a profile, Update it or see other people profiles.")]
    //[Authorize(Roles = "User")]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var res = await _userService.GetByIdAsync(id);
            return Ok(res);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(UserFilter userFilter)
        {
            var res = await _userService.ListAsync(userFilter);
            return Ok(res);
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegisterDto registerDto)
        {
            await _userService.RegisterAsync(registerDto);
            return Ok();
        }

        //[NonAction]
        //private string CurrentUserId()
        //{
        //    return User.FindFirst(ClaimTypes.NameIdentifier).Value;
        //}
    }
}
