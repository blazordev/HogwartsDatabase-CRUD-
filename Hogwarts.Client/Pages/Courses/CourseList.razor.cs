using Hogwarts.Client.Services;
using Hogwarts.Data.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Hogwarts.Client.Pages.Courses
{
    public partial class CourseList
    {
        List<CourseDto> Courses;
        public CourseDto NewCourse = new CourseDto();
        private bool display;
        public void Show() => display = true;
        public void Cancel() => display = false;
        [Inject] public CourseDataService CourseDataService { get; set; }
        protected async override Task OnInitializedAsync()
        {
            Courses = await CourseDataService.GetAllCoursesAsync();
        }
        public async Task OnSubmit()
        {
            await CourseDataService.AddCourseAsync(NewCourse);
        }
        public async Task DeleteCourse(int courseId)
        {
            await CourseDataService.DeleteCourseAsync(courseId);
            Courses = await CourseDataService.GetAllCoursesAsync();
        }

    }
}
