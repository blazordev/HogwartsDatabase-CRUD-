using Hogwarts.Api.DbContexts;
using Hogwarts.Api.Models;
using Hogwarts.Api.ResourceParameters;
using Hogwarts.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hogwarts.Api.Services
{
    public class StaffRepository
    {
        private HogwartsDbContext _context;

        public StaffRepository(HogwartsDbContext context)
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

        public IEnumerable<Staff> GetAllStaff(StaffResourceParameters staffResourceParameters)
        {
            if (staffResourceParameters == null)
            {
                throw new ArgumentNullException(nameof(staffResourceParameters));
            }
            if (string.IsNullOrWhiteSpace(staffResourceParameters.SearchQuery)
                && staffResourceParameters.RoleId == 0)
            {
                return GetAllStaff();
            }
            var staffToReturn = new List<Staff>();
            var staffCollection = _context.Staff.ToList();
            if (staffResourceParameters.RoleId != 0)
            {
                var roleId = staffResourceParameters.RoleId;
                var staffRoles = _context.StaffRoles;
                foreach (var staff in staffCollection)
                {
                    foreach (var role in staffRoles)
                    {
                        if (role.RoleId == roleId && staff.Id == role.StaffId)
                        {
                            staffToReturn.Add(staff);
                            break;
                        }
                    }
                }

            }
            if (!String.IsNullOrWhiteSpace(staffResourceParameters.SearchQuery))
            {
                var queryString = staffResourceParameters.SearchQuery.ToLower().Trim();
                staffToReturn = staffToReturn.Where(s => s.FirstName.ToLower().Contains(queryString)
                            || s.MiddleNames.ToLower().Contains(queryString)
                            || s.LastName.ToLower().Contains(queryString)).ToList();
            }

            return staffToReturn;
        }

        public IEnumerable<Staff> GetStaffForCourse(int courseId)
        {
            var staffCourseCollection = _context.StaffCourse.Where(sc => sc.CourseId == courseId);
            var staffToReturn = new List<Staff>(); 
            foreach (var staffCourse in staffCourseCollection)
            {
                staffToReturn.Add(_context.Staff.FirstOrDefault(s => s.Id == staffCourse.StaffId));
            }
            return staffToReturn;
        }

        public void DeleteStaffCourseRelationship(StaffCourse staffCourse)
        {
            _context.StaffCourse.Remove(staffCourse);
        }

        public StaffCourse GetStaffCourseById(int staffId, int courseId)
        {
            return _context.StaffCourse.FirstOrDefault(sc =>
            sc.StaffId == staffId && sc.CourseId == courseId);
        }

        public StaffRole GetStaffRoleEntity(int staffId, int roleId)
        {
            return _context.StaffRoles.FirstOrDefault(sr =>
                 sr.StaffId == staffId && sr.RoleId == roleId);
        }

        public void DeleteStaffRoleRelationship(StaffRole staffRoleFromRepo)
        {
            _context.StaffRoles.Remove(staffRoleFromRepo);
        }        

        public void AddStaff(Staff staff)
        {
            _context.Staff.Add(staff);
        }

        public bool StaffExists(int staffId)
        {
            if (String.IsNullOrWhiteSpace(staffId.ToString()))
            {
                throw new ArgumentNullException(nameof(staffId));
            }

            return _context.Staff.Any(s => s.Id == staffId);
        }
        public void UpdateStaff(Staff staff)
        {
            //nothing needed here
        }

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }

        public void AddRoleToStaff(int staffId, int roleId)
        {
            _context.StaffRoles.Add(new StaffRole { RoleId = roleId, StaffId = staffId });
        }
        public void AssignRoleCollectionToStaff(int staffId, IEnumerable<int> roleIds)
        {
            foreach (var roleId in roleIds)
            {
                _context.StaffRoles.Add(new StaffRole { RoleId = roleId, StaffId = staffId });
            }
        }
        public void AddCourseToStaff(int staffId, int courseId)
        {
            _context.StaffCourse.Add(new StaffCourse { CourseId = courseId, StaffId = staffId });
        }

        public void AssignCourseCollectionToStaff(int staffId, IEnumerable<int> courseIds)
        {
            foreach (var courseId in courseIds)
            {
                _context.StaffCourse.Add(
                    new StaffCourse { CourseId = courseId, StaffId = staffId });
            }

        }
        public void AddHouseToStaff(int staffId, int houseId)
        {
            _context.HeadOfHouses.Add(new HeadOfHouse { HouseId = houseId, StaffId = staffId });
        }

        public bool IsTeacher(int staffId)
        {
            var staffRole = _context.StaffRoles.Where(sr => sr.StaffId == staffId);
            return staffRole.Any(sr => sr.RoleId == 3); // headofhouse
        }

        public bool IsHeadOfHouse(int staffId)
        {
            var staffRole = _context.StaffRoles.Where(sr => sr.StaffId == staffId);
            return staffRole.Any(sr => sr.RoleId == 6); //headofhouse
        }

        public void DeleteStaff(Staff staffFromRepo)
        { 
            _context.Staff.Remove(staffFromRepo);
        }
    }
}
