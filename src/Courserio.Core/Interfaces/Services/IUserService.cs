using Courserio.Core.DTOs.Auth;
using Courserio.Core.DTOs.Role;
using Courserio.Core.DTOs.User;
using Courserio.Core.Filters;
using Courserio.Keycloak.Models;

namespace Courserio.Core.Interfaces.Services
{
    public interface IUserService
    {
        Task<UserProfileDto> GetByIdAsync(int id);
        Task RegisterAsync(UserRegisterDto userRegisterDto);
        Task<List<UserDto>> ListAsync(UserFilter userFilter);
        Task<LoginResponseDto> LoginAsync(LoginDto loginDto);
        Task ChangeRoleAsync(RoleChangeDto roleChangeDto);
    }
}
