using Courserio.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Courserio.Core.DTOs.User;
using Courserio.Core.Filters;

namespace Courserio.Core.Interfaces
{
    public interface IUserService
    {
        Task<UserProfileDto> GetByIdAsync(int id);
        Task RegisterAsync(UserRegisterDto userRegisterDto);
        Task<List<UserDto>> ListAsync(UserFilter userFilter);
    }
}
