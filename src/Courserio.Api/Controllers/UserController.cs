using Courserio.Core.DTOs;
using Courserio.Core.DTOs.Role;
using Courserio.Core.DTOs.User;
using Courserio.Core.Filters;
using Courserio.Core.Interfaces;
using Courserio.Core.Interfaces.Services;
using Courserio.Keycloak.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Courserio.Api.Controllers
{

    [SwaggerTag("Create a profile, Update it or see other people profiles.")]
    //[Authorize(Roles = "User")]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var res = await _userService.GetByIdAsync(id);
            return Ok(res);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync(UserFilter userFilter)
        {
            var res = await _userService.ListAsync(userFilter);
            return Ok(res);
        }


        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync(UserRegisterDto registerDto)
        {
            await _userService.RegisterAsync(registerDto);
            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync(LoginDto loginDto)
        {
            var res = await _userService.LoginAsync(loginDto);
            return Ok(res);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("role")]
        public async Task<IActionResult> ChangeRoleAsync(RoleChangeDto roleChangeDto)
        {
            await _userService.ChangeRoleAsync(roleChangeDto);
            return Ok();
        }

    }
}
