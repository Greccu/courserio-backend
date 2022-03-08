using Courserio.Core.Interfaces.Repositories;
using Courserio.Core.Interfaces.Services;
using Courserio.Core.Models;

namespace Courserio.Core.Services
{
    public class RoleService : IRoleService
    {

        private readonly IGenericRepository<Role> _roleRepository;

        public RoleService(IGenericRepository<Role> roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task ChangeRoleAsync()
        {

        }
    }
}
