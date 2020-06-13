using Hogwarts.Api.DbContexts;
using Hogwarts.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Hogwarts.Api.Controllers;

namespace Hogwarts.Api.Services
{
    public class CourseLibraryRepository
    {
        private HogwartsDbContext _context;

        public CourseLibraryRepository(HogwartsDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IEnumerable<Course> GetCourses()
        {
            return _context.Courses.ToList();
        }

        public Course GetCourseById(int courseId)
        {
            if(String.IsNullOrWhiteSpace(courseId.ToString()))
            {
                throw new ArgumentNullException(nameof(courseId));
            }
            return _context.Courses.FirstOrDefault(c => c.Id == courseId);
        }
        public void Add(Course course)
        {
            _context.Courses.Add(course);
        }
        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }

        public bool CourseExists(int courseId)
        {
            if (String.IsNullOrWhiteSpace(courseId.ToString()))
            {
                throw new ArgumentNullException(nameof(courseId));
            }
            return _context.Courses.Any(c => c.Id == courseId);
        }
        
        public void UpdateCourse(Course course)
        {
            //no code needed for update in current repo
        }
        
        public IEnumerable<Course> GetCoursesForStaffmember(int staffId)
        {
            //Prettify this into a join
            var staffCourseList = _context.StaffCourse.Where(sc => sc.StaffId == staffId);
            var coursesToReturn = new List<Course>();
            foreach (var staffCourse in staffCourseList)
            {
                coursesToReturn.Add(_context.Courses.FirstOrDefault(c => c.Id == staffCourse.CourseId));
            }
            return coursesToReturn;

        }

        


    }
}
