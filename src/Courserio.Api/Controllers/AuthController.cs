using System.Net;
using Courserio.Keycloak.Models;
using Courserio.Keycloak.UserService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Courserio.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IKeycloakUserService _keycloakUserService;

        public AuthController(IKeycloakUserService keycloakUserService)
        {
            _keycloakUserService = keycloakUserService;
        }
    
        [HttpPost("token")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var res = await _keycloakUserService.Login(loginDto);
            return res.HttpStatusCode switch
            {
                HttpStatusCode.OK => Ok(res.Content),
                HttpStatusCode.Unauthorized => Unauthorized(res.Content),
                _ => BadRequest(res.Content)
            };
        }
    }
}
