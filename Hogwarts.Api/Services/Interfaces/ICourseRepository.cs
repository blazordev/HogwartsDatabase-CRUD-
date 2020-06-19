using Hogwarts.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hogwarts.Api.Services.Interfaces
{
    interface ICourseRepository
    {
        public Task<IEnumerable<Course>> GetCoursesAsync();

        public Task<Course> GetCourseByIdAsync(int courseId);

        public void Add(Course course);

        public Task<bool> SaveAsync();

        public Task<bool> CourseExistsAsync(int courseId);

        public void UpdateCourse(Course course);

        public Task<IEnumerable<Course>> GetCoursesForStaffmemberAsync(int staffId);

        public void DeleteCourse(Course courseToDelete);


    }
}
