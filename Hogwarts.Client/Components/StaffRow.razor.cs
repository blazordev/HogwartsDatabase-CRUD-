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
        public bool ShowCourses { get; set; }
        [Inject] public NavigationManager NavigationManager { get; set; }
        [Parameter] public StaffDto Staff { get; set; }
        public async Task ToggleCourses()
        {
            ShowCourses = !ShowCourses;
            if (ShowCourses)
            {
                await GetCourses();
            }
        }
        public string Highlighted
        {
            get { return Staff.IsChecked ? "font-weight:bold; color:dimgray" : ""; }
            set { _highlighted = value; }
        }
        [Inject] CourseDataService CourseDataService { get; set; }
        public async Task GetCourses()
        {
            Staff.Courses = await CourseDataService.GetCoursesForStaff(Staff.Id);
        }
        public void StaffDetailsPage()
        {
            NavigationManager.NavigateTo($"staffDetails/{Staff.Id}");
            Console.WriteLine($"Staff Id: {Staff.Id} clicked");
        }
    }
}
