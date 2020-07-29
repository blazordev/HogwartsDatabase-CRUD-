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
        [Parameter] public StaffDto Staff { get; set; }
        private string _highlighted; 
        public string Highlighted 
        { 
            get { return Staff.IsChecked? "font-weight:bold; color:dimgray" : ""; } 
            set { _highlighted = value; }
        }
        [Inject] CourseDataService CourseDataService { get; set; }        
        public List<CourseDto> Courses { get; set; } = new List<CourseDto>();
        protected async override Task OnInitializedAsync()
        {
            Courses = await CourseDataService.GetCoursesForStaff(Staff.Id);
        }
        
    }
}
