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
        public void AddRoleToStaff(int staffId, int roleId)
        {
            _context.StaffRoles.Add(new StaffRole { RoleId = roleId, StaffId = staffId });
        }
        public bool RoleExists(int roleId)
        {
            if (String.IsNullOrWhiteSpace(roleId.ToString()))
            {
                throw new ArgumentNullException(nameof(roleId));
            }
            return _context.Roles.Any(r => r.Id == roleId);
        }

        public bool Save()
        {
            return _context.SaveChanges() >= 0;
        }
    }
}
