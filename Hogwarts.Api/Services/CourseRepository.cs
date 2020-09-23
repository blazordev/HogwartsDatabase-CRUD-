using Hogwarts.Api.DbContexts;
using Hogwarts.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Hogwarts.Api.Controllers;
using Hogwarts.Api.Services.Interfaces;

namespace Hogwarts.Api.Services
{
    public class CourseRepository : ICourseRepository
    {
        private HogwartsDbContext _context;

        public CourseRepository(HogwartsDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<Course>> GetCoursesAsync()
        {
            return await _context.Courses
                .Include(c => c.StaffCourse)
                .ThenInclude(sc => sc.Staff)
                .ToListAsync();
        }

        public async Task<Course> GetCourseByIdAsync(int courseId)
        {
            if (String.IsNullOrWhiteSpace(courseId.ToString()))
            {
                throw new ArgumentNullException(nameof(courseId));
            }
            return await _context.Courses.FirstOrDefaultAsync(c => c.Id == courseId);
        }
        public void Add(Course course)
        {
            _context.Courses.Add(course);
        }
        public async Task<bool> SaveAsync()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }

        public async Task<bool> CourseExistsAsync(int courseId)
        {
            if (String.IsNullOrWhiteSpace(courseId.ToString()))
            {
                throw new ArgumentNullException(nameof(courseId));
            }
            return await _context.Courses.AnyAsync(c => c.Id == courseId);
        }

        public void UpdateCourse(Course course)
        {
            //no code needed for update in current repo
        }

        public async Task<IEnumerable<Course>> GetCoursesForStaffmemberAsync(int staffId)
        {
            return await _context.StaffCourse
                .Include(sc => sc.Course)
                .Where(sc => sc.StaffId == staffId)
                .Select(sc => sc.Course).ToListAsync();

        }

        public void DeleteCourse(Course courseToDelete)
        {
            _context.Courses.Remove(courseToDelete);
        }
    }
}
