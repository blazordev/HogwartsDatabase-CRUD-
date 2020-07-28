using Hogwarts.Api.DbContexts;
using Hogwarts.Api.Helpers;
using Hogwarts.Data.Models;
using Hogwarts.Api.ResourceParameters;
using Hogwarts.Api.Services.Interfaces;
using Hogwarts.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hogwarts.Api.Services
{
    public class StaffRepository : IStaffRepository
    {
        private HogwartsDbContext _context;

        public StaffRepository(HogwartsDbContext context)
        {
            _context = context;
        }
        public async Task<Staff> GetStaffByIdAsync(int staffId)
        {
            return await _context.Staff
                .Include(s => s.StaffRoles)
                .ThenInclude(sr => sr.Role)
                .FirstOrDefaultAsync(s => s.Id == staffId);
        }
        public async Task<IEnumerable<Staff>> GetStaffCollectionAsync(IEnumerable<int> staffIds)
        {
            return await _context.Staff.Where(s => staffIds.Contains(s.Id))
                .OrderBy(s => s.FirstName)
                .OrderBy(s => s.LastName)
                .ToListAsync();
        }
        public void DeleteStaffCollection(IEnumerable<Staff> staffEntities)
        {
            _context.Staff.RemoveRange(staffEntities);
        }
        
        public async Task<IEnumerable<Staff>> GetAllStaffAsync(StaffResourceParameters staffResourceParameters)
        {
            var staffToReturn = _context.Staff as IQueryable<Staff>;
            if (staffResourceParameters.RoleId != 0)
            {
                var roleId = staffResourceParameters.RoleId;
                staffToReturn = _context.StaffRoles
                    .Where(sr => sr.RoleId == roleId)
                    .Include(sr => sr.Staff)
                    .Select(sr => sr.Staff)
                    .OrderBy(s => s.LastName)
                    .ThenBy(s => s.FirstName)
                    .ThenBy(s => s.MiddleNames);
            }
            if (!String.IsNullOrWhiteSpace(staffResourceParameters.SearchQuery))
            {
                var queryString = staffResourceParameters.SearchQuery.ToLower().Trim();
                staffToReturn = staffToReturn.Where(s => s.FirstName.ToLower().Contains(queryString)
                            || s.MiddleNames.ToLower().Contains(queryString)
                            || s.LastName.ToLower().Contains(queryString));
            }
         
            var pageSize = staffResourceParameters.PageSize;
            var pageNumber = staffResourceParameters.PageNumber;
            return await staffToReturn.Skip((pageNumber - 1) * pageSize)
                .Take(pageSize).Include(s => s.StaffRoles)
               .ThenInclude(sr => sr.Role).
               OrderBy(s => s.LastName)
               .ThenBy(s => s.FirstName)
               .ThenBy(s => s.MiddleNames)
               .ToListAsync();
        }
        public async Task<IEnumerable<Staff>> GetHeadsOfHouseAsync(int houseId)
        {
            return await _context.HeadOfHouses.Include(h => h.Staff).
                Where(h => h.HouseId == houseId).Select(h => h.Staff).ToListAsync();
        }
        public void DeleteStaffHouseRelationship(HeadOfHouse headOfHouse)
        {
            _context.HeadOfHouses.Remove(headOfHouse);
        }
        public async Task<IEnumerable<Staff>> GetStaffForCourseAsync(int courseId)
        {
            return await _context.StaffCourse
                .Include(sc => sc.Staff)
                .Where(sc => sc.CourseId == courseId)
                .Select(sc => sc.Staff).ToListAsync();
        }
        public void DeleteStaffCourseRelationship(StaffCourse staffCourse)
        {
            _context.StaffCourse.Remove(staffCourse);
        }
        public async Task<StaffCourse> GetStaffCourseById(int staffId, int courseId)
        {
            return await _context.StaffCourse.FirstOrDefaultAsync(sc =>
            sc.StaffId == staffId && sc.CourseId == courseId);
        }
        public async Task<StaffRole> GetStaffRoleEntityAsync(int staffId, int roleId)
        {
            return await _context.StaffRoles.FirstOrDefaultAsync(sr =>
                 sr.StaffId == staffId && sr.RoleId == roleId);
        }
        public void DeleteStaffRoleRelationship(StaffRole staffRole)
        {
            _context.StaffRoles.Remove(staffRole);
        }
        public void AddStaff(Staff staff)
        {
            _context.Staff.Add(staff);
        }
        public async Task<bool> StaffExistsAsync(int staffId)
        {
            return await _context.Staff.AnyAsync(s => s.Id == staffId);
        }
        public void UpdateStaff(Staff staff)
        {
            //nothing needed here
        }
        public async Task<bool> SaveAsync()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }
        public void AddRoleToStaff(int staffId, int roleId)
        {
            _context.StaffRoles.Add(new StaffRole { RoleId = roleId, StaffId = staffId });
        }
        public void AssignRoleCollectionToStaff(int staffId, IEnumerable<RoleDto> roles)
        {
            foreach (var role in roles)
            {
               _context.StaffRoles.Add(new StaffRole { RoleId = role.Id, StaffId = staffId });
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
        public async Task<bool> IsTeacherAsync(int staffId)
        {
            return await _context.StaffRoles
                .AnyAsync(sr => sr.StaffId == staffId && sr.RoleId == 3);
        }
        public async Task<bool> IsHeadOfHouseAsync(int staffId)
        {
            return await _context.StaffRoles
                .AnyAsync(sr => sr.StaffId == staffId && sr.RoleId == 6);
        }
        public void DeleteStaff(Staff staffFromRepo)
        {
            _context.Staff.Remove(staffFromRepo);
        }
       
    }
}
