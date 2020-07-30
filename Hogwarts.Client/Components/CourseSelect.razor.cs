using Hogwarts.Client.Services;
using Hogwarts.Data.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hogwarts.Client.Components
{
    public partial class CourseSelect
    {
        [Parameter] public EventCallback<CourseDto> AddCourse { get; set; }
        [Inject] CourseDataService CourseDataService { get; set; }
        public List<CourseDto> Courses { get; set; } = new List<CourseDto>();
        protected override async Task OnInitializedAsync()
        {
            Courses = await CourseDataService.GetAllCoursesAsync();
        }
        public void CourseClicked(int courseId)
        {
            var course = Courses.FirstOrDefault(c => c.Id == courseId);
            AddCourse.InvokeAsync(course);
        }

    }
}
