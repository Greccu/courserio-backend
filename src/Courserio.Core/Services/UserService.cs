using Courserio.Core.Models;
using Courserio.Core.DTOs;
using Courserio.Core.Interfaces;
using Courserio.Core.Interfaces.Repositories;
using Courserio.Keycloak.UserService;
using AutoMapper;
using Courserio.Core.Constants;
using Courserio.Core.DTOs.User;
using Courserio.Keycloak.Models;
using Microsoft.EntityFrameworkCore;
using Courserio.Core.Filters;
using Courserio.Pagination;

namespace Courserio.Core.Services
{
    

    public class UserService : IUserService
    {
        private readonly IGenericRepository<User> _userRepository;
        private readonly IKeycloakUserService _keycloakUserService;
        private readonly IMapper _mapper;

        public UserService(IGenericRepository<User> userRepository, IKeycloakUserService keycloakUserService, IMapper mapper)
        {
            _userRepository = userRepository;
            _keycloakUserService = keycloakUserService;
            _mapper = mapper;
        }
        
        public async Task<List<UserDto>> ListAsync(UserFilter userFilter)
        {
            
            var users = _userRepository
                .ListAllAsQueryable()
                .ApplyPagination(userFilter)
                ;

            var result = await users.Select(user => _mapper.Map<UserDto>(user)).ToListAsync();
            return result;
        }
        public async Task<UserProfileDto> GetByIdAsync(int id)
        {
            var user = await _userRepository.ListAllAsQueryable().Where(x => x.Id == id).FirstOrDefaultAsync();
            var result = _mapper.Map<UserProfileDto>(user);
            return result;
        }

        public async Task RegisterAsync(UserRegisterDto userRegisterDto)
        {
            var keycloakUser = _mapper.Map<RegisterDto>(userRegisterDto);
            var res = await _keycloakUserService.Register(keycloakUser);
            var user = _mapper.Map<User>(userRegisterDto);
            user.ProfilePicture ??= UserConstants.DefaultProfilePicture;
            await _userRepository.AddAsync(user);
        }
    }
}
