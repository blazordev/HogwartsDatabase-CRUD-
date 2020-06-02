using Hogwarts.Api.DbContexts;
using Hogwarts.Api.Models;
using Hogwarts.Api.ResourceParameters;
using Hogwarts.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hogwarts.Api.Services
{
    public class StaffLibraryRepository
    {
        private HogwartsDbContext _context;

        public StaffLibraryRepository(HogwartsDbContext context)
        {
            _context = context;
        }
        public Staff GetStaffById(int staffId)
        {
            if (String.IsNullOrWhiteSpace(staffId.ToString()))
            {
                throw new ArgumentNullException(nameof(staffId));
            }
            return _context.Staff.FirstOrDefault(s => s.Id == staffId);
        }
        public IEnumerable<Staff> GetStaff(IEnumerable<int> staffIds)
        {
            if (staffIds == null)
            {
                throw new ArgumentNullException(nameof(staffIds));
            }
            return _context.Staff.Where(s => staffIds.Contains(s.Id))
                .OrderBy(s => s.FirstName)
                .OrderBy(s => s.LastName)
                .ToList();
        }

        public IEnumerable<Staff> GetAllStaff()
        {
            return _context.Staff;
        }

        public IEnumerable<Staff> GetAllStaff(StaffResourceParameter staffResourceParameter)
        {
            if (String.IsNullOrWhiteSpace(staffResourceParameter.SearchQuery)
                && String.IsNullOrWhiteSpace(staffResourceParameter.RoleId.ToString()))
            {
                return GetAllStaff();
            }
            var staffCollection = _context.Staff as IQueryable<Staff>;
            if (!String.IsNullOrWhiteSpace(staffResourceParameter.RoleId.ToString()))
            {
                var roleId = staffResourceParameter.RoleId;
                var staffRoles = _context.StaffRoles.Where(sr => sr.RoleId == roleId);
                staffCollection = staffCollection.Where(s => staffRoles.All(sr => sr.StaffId == s.Id));
            }
            if (!String.IsNullOrWhiteSpace(staffResourceParameter.SearchQuery))
            {
                var queryString = staffResourceParameter.SearchQuery.Trim();
                staffCollection = staffCollection.Where(s => s.FirstName.Contains(queryString)
                            || s.MiddleNames.Contains(queryString)
                            || s.LastName.Contains(queryString));
            }
            return staffCollection.ToList();
        }

        public void AddStaffWithRoles(Staff staff)
        {
            _context.Staff.Add(staff);
            foreach (var role in staff.StaffRoles)
            {
                if (role.RoleId == 3)
                {
                    var teacher = new Teacher { StaffId = staff.Id };
                    _context.Teachers.Add(teacher);
                }
                if (role.RoleId == 6)
                {
                    var headOfHouse = new HeadOfHouse { StaffId = staff.Id };
                    _context.HeadOfHouses.Add(headOfHouse);
                }
            }
        }

        public bool StaffExists(int staffId)
        {
            if (String.IsNullOrWhiteSpace(staffId.ToString()))
            {
                throw new ArgumentNullException(nameof(staffId));
            }

            return _context.Staff.Any(s => s.Id == staffId);
        }


        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }





    }
}
