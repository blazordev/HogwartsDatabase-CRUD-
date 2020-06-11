using Hogwarts.Api.DbContexts;
using Hogwarts.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hogwarts.Api.Services
{
    public class RoleRepository
    {
        private HogwartsDbContext _context;

        public RoleRepository(HogwartsDbContext context)
        {
            _context = context;
        }

        public void AddRole(Role role)
        {
            if(role== null)
            {
                throw new ArgumentNullException(nameof(role));
            }
            _context.Roles.Add(role);
        }
        public List<Role> GetRoles()
        {
            return _context.Roles.OrderBy(r => r.Name).OrderBy(r => r.Name).ToList();
        }
        public Role GetRoleById(int roleId)
        {
            if(String.IsNullOrWhiteSpace(roleId.ToString()))
            {
                throw new ArgumentNullException(nameof(roleId));
            }
            return _context.Roles.FirstOrDefault(r => r.Id == roleId);
        }
        
        public bool RoleExists(int roleId)
        {
            if (String.IsNullOrWhiteSpace(roleId.ToString()))
            {
                throw new ArgumentNullException(nameof(roleId));
            }
            return _context.Roles.Any(r => r.Id == roleId);
        }

        public IEnumerable<Role> GetRolesForStaff(int staffId)
        {
            var staffRoles = _context.StaffRoles.Where(sr => sr.StaffId == staffId);
            var rolesToReturn = new List<Role>();
            foreach (var staffRole in staffRoles)
            {
                var role = _context.Roles.FirstOrDefault(r => r.Id == staffRole.RoleId);
                rolesToReturn.Add(role);
            }
            return rolesToReturn;
        }

        public bool Save()
        {
            return _context.SaveChanges() >= 0;
        }

        public bool HasRoleAlready(int staffId, int roleId)
        {
            return _context.StaffRoles.Any(sr => sr.StaffId == staffId && sr.RoleId == roleId);
        }
    }
}
