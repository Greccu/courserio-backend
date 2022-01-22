using Courserio.Keycloak.Models;

namespace Courserio.Keycloak.UserService
{
    public interface IKeycloakUserService
    {
        Task<KeycloakResponse> Login(LoginDto loginDto);
        Task<KeycloakResponse> Register(RegisterDto registerDto);
    }
}