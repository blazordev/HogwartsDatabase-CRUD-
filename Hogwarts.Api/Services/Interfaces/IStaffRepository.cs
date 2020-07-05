using Hogwarts.Api.Helpers;
using Hogwarts.Api.ResourceParameters;
using Hogwarts.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hogwarts.Api.Services.Interfaces
{
    public interface IStaffRepository
    {
        public Task<Staff> GetStaffByIdAsync(int staffId);
        public Task<IEnumerable<Staff>> GetStaffCollectionAsync(IEnumerable<int> staffIds);
        public void DeleteStaffCollection(IEnumerable<Staff> staffEntities);
        public Task<IEnumerable<Staff>> GetAllStaffAsync(StaffResourceParameters staffResourceParameters);
        public Task<IEnumerable<Staff>> GetHeadsOfHouseAsync(int houseId);
        public void DeleteStaffHouseRelationship(HeadOfHouse headOfHouse);
        public Task<IEnumerable<Staff>> GetStaffForCourseAsync(int courseId);
        public void DeleteStaffCourseRelationship(StaffCourse staffCourse);
        public Task<StaffCourse> GetStaffCourseById(int staffId, int courseId);
        public Task<StaffRole> GetStaffRoleEntityAsync(int staffId, int roleId);
        public void DeleteStaffRoleRelationship(StaffRole staffRoleFromRepo);
        public void AddStaff(Staff staff);
        public Task<bool> StaffExistsAsync(int staffId);
        public void UpdateStaff(Staff staff);
        public Task<bool> SaveAsync();
        public void AddRoleToStaff(int staffId, int roleId);
        public void AssignRoleCollectionToStaff(int staffId, IEnumerable<int> roleIds);
        public void AddCourseToStaff(int staffId, int courseId);
        public void AssignCourseCollectionToStaff(int staffId, IEnumerable<int> courseIds);
        public void AddHouseToStaff(int staffId, int houseId);
        public Task<bool> IsTeacherAsync(int staffId);
        public Task<bool> IsHeadOfHouseAsync(int staffId);
        public void DeleteStaff(Staff staffFromRepo);
  

    }
}
