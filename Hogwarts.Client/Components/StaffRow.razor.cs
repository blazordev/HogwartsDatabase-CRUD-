using Hogwarts.Client.Services;
using Hogwarts.Data.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hogwarts.Client.Components
{
    public partial class StaffRow
    {
        private string _highlighted;        
        [Inject] public NavigationManager NavigationManager { get; set; }
        [Parameter] public StaffDto Staff { get; set; }
        List<CourseDto> Courses;
        public async Task ToggleCourses()
        {
            Staff.ShowCourses = !Staff.ShowCourses;
            if (Staff.ShowCourses)
            {
                await GetCourses();
            }
        }
        public string Highlighted
        {
            get { return Staff.IsChecked ? "background-color:#ffede3 !important;" : ""; }
            set { _highlighted = value; }
        }
        [Inject] CourseDataService CourseDataService { get; set; }
        public async Task GetCourses()
        {
            Courses = await CourseDataService.GetCoursesForStaff(Staff.Id);
        }
        public void StaffDetailsPage()
        {
            NavigationManager.NavigateTo($"staffDetails/{Staff.Id}");
            Console.WriteLine($"Staff Id: {Staff.Id} clicked");
        }
    }
}
