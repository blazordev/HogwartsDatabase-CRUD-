using Hogwarts.Data;
using Hogwarts.Data.Models;
using Hogwarts.Client.Services;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hogwarts.Client.Pages
{
    public partial class AddStaff 
    {
        [Parameter] public StaffForCreationDto Staff { get; set; } = new StaffForCreationDto();
        public List<RoleDto> Roles { get; set; } = new List<RoleDto>();
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
            Houses = await HouseDataService.GetAllHousesAsync();
            Courses = await CourseDataService.GetAllCoursesAsync();
        }
        public void AddRole(RoleDto role)
        {
            if (!Staff.Roles.Contains(role))
            {
                Staff.Roles.Add(role);
            }
        }
       
        public HouseDto GetHouse(int id)
        {
            return Houses.Find(h => h.Id == id);
        }
        public void RemoveRole(int id)
        {
            if (id == 3)// teacher
            {
                //reset
                Staff.CourseIds = new List<int>();
            }
            else if (id == 6)//head of house
            {
                //reset
                Staff.HouseId = 0;
            }
            Staff.Roles.RemoveAll(r => r.Id == id);
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
