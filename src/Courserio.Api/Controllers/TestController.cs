using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Courserio.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpGet]
        public IActionResult Test()
        {
            return Ok("It works!!");
        }

        [Authorize]
        [HttpGet("auth")]
        public IActionResult TestAuth()
        {
            return Ok("Auth works!!");
        }
        
        
    }
}
