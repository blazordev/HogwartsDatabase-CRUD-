using Hogwarts.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hogwarts.Api.Services.Interfaces
{
    public interface IRoleRepository
    {
        public void AddRole(Role role);
        public Task<IEnumerable<Role>> GetRolesAsync();
        public Task<Role> GetRoleByIdAsync(int roleId);
        public Task<bool> RoleExistsAsync(int roleId);
        public Task<bool> HasRoleAlreadyAsync(int staffId, int roleId);
        public Task<IEnumerable<Role>> GetRolesForStaffAsync(int staffId);
        public void UpdateRole(int roleId);
        public Task<bool> SaveAsync();
        public void DeleteRole(Role roleFromRepo);
    }
}
