using Hogwarts.Data;
using Hogwarts.Data.Models;
using Hogwarts.Client.Services;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hogwarts.Client.Components
{
    public partial class AddStaff 
    {
        [Parameter] public StaffDto Staff { get; set; } = new StaffDto();

        [Parameter] public EventCallback OnSubmit { get; set; }
        [Inject] StaffDataService StaffDataService { get; set; }       
        public bool IsChecked { get; set; } = true;
        public bool ResetSelect { get; set; }
        public bool DisplayCourses { get; set; }    
        private bool display = false;
        public void ShowModal() => display = true;
        public void HideModal() => display = false;
        
        
        public void AddRole(RoleDto role)
        {
            if (!Staff.Roles.Contains(role))
            {
                Staff.Roles.Add(role);
            }
        }
        public void AddHouse(HouseDto house)
        {
            Staff.House = house;
        }
       
        public void RemoveRole(int id)
        {
            if (id == 3)// teacher
            {
                //reset
                Staff.Courses = new List<CourseDto>();
            }
            else if (id == 6)//head of house
            {
                //reset
                Staff.House = new HouseDto();
            }
            Staff.Roles.RemoveAll(r => r.Id == id);
        }
        public void AddCourse(CourseDto course)
        {
            if (!Staff.Courses.Contains(course))
            {
                Staff.Courses.Add(course);
            }
        }

        public void Cancel()
        {
            HideModal();
            Staff = new StaffDto();
        }
        public void Reset()
        {
            Staff = new StaffDto();
        }

    }
}
