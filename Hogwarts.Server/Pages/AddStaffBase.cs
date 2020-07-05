using Hogwarts.Data;
using Hogwarts.Data.Models;
using Hogwarts.Server.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hogwarts.Server.Pages
{
    public class AddStaffBase : ComponentBase
    {
        [Parameter] public StaffForCreationDto Staff { get; set; } = new StaffForCreationDto();
        public List<RoleDto> Roles { get; set; } = new List<RoleDto>();
        public IEnumerable<string> SelectedRoles { get; set; } = new List<string>();
        [Inject] RolesDataService RolesDataService { get; set; }
        [Inject] HouseDataService HouseDataService { get; set; }
        [Inject] CourseDataService CourseDataService { get; set; }
        [Inject] NavigationManager NavigationManager { get; set; }
        [Inject] StaffDataService StaffDataService { get; set; }
        public bool IsChecked { get; set; } = true;
        public bool ResetSelect { get; set; }
        public bool DisplayCourses { get; set; }
        public List<CourseDto> Courses { get; set; } = new List<CourseDto>();
        public List<HouseDto> Houses { get; set; } = new List<HouseDto>();
        protected override async Task OnInitializedAsync()
        {
            Roles = await RolesDataService.GetAllRolesAsync();
            Houses = await HouseDataService.GetAllHousesAsync();
            Courses = await CourseDataService.GetAllCoursesAsync();
        }
        public void RoleSelected(int id)
        {
            AddRole(id);
        }
        public void AddRole(int roleId)
        {
            if (Staff.RoleIds.Find(r => r == roleId) == 0)
            {
                Staff.RoleIds.Add(roleId);
            }
        }
        public RoleDto GetRole(int id)
        {
            return Roles.Find(r => r.Id == id);
        }
        public HouseDto GetHouse(int id)
        {
            return Houses.Find(h => h.Id == id);
        }
        public void RemoveRole(int id)
        {
            if (id == 3)// teacher
            {
                Staff.CourseIds = new List<int>();
            }
            else if (id == 6)//head of house
            {
                Staff.HouseId = 0;
            }
            Staff.RoleIds.RemoveAll(rId => rId == id);
        }
        public void SelectHouse(ChangeEventArgs e)
        {
            if (int.TryParse((string)e.Value, out var id) && id >= 0)
            {
                Staff.HouseId = id;
            }
        }
        public void CourseSelected(int courseId)
        {
            if (Staff.CourseIds.Find(cId => cId == courseId) == 0)
            {
                Staff.CourseIds.Add(courseId);
            }
        }
        public CourseDto GetCourse(int courseId)
        {
            return Courses.Find(c => c.Id == courseId);
        }
        public void RemoveCourse(int id)
        {
            Staff.CourseIds.RemoveAll(cId => cId == id);
        }
        public async Task HandleValidSubmit()
        {
            await StaffDataService.AddStaff(Staff);
            NavigationManager.NavigateTo("staffList");
        }

    }
}
