using Hogwarts.Api.DbContexts;
using Hogwarts.Api.Services.Interfaces;
using Hogwarts.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hogwarts.Api.Services
{
    public class RoleRepository : IRoleRepository
    {
        private HogwartsDbContext _context;

        public RoleRepository(HogwartsDbContext context)
        {
            _context = context;
        }
        public void AddRole(Role role)
        {           
            _context.Roles.Add(role);
        }
        public async Task<IEnumerable<Role>> GetRolesAsync()
        {
            return await _context.Roles.ToListAsync();
        }
        public async Task<Role> GetRoleByIdAsync(int roleId)
        {            
            return await _context.Roles.FirstOrDefaultAsync(r => r.Id == roleId);
        }        
        public async Task<bool> RoleExistsAsync(int roleId)
        {            
            return await _context.Roles.AnyAsync(r => r.Id == roleId);
        }
        public async Task<IEnumerable<Role>> GetRolesForStaffAsync(int staffId)
        {
            return await _context.StaffRoles
                .Include(sr => sr.Role)
                .Where(sr => sr.StaffId == staffId)
                .Select(sr => sr.Role).ToListAsync();            
        }
        
        public async Task<bool> SaveAsync()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }

        public async Task<bool> HasRoleAlreadyAsync(int staffId, int roleId)
        {
            return await _context.StaffRoles
                .AnyAsync(sr => sr.StaffId == staffId && sr.RoleId == roleId);
        }
        public void DeleteRole(Role roleFromRepo)
        {
            _context.Roles.Remove(roleFromRepo);
        }
        public void UpdateRole(int roleId)
        {
            throw new NotImplementedException();
        }
    }
}
