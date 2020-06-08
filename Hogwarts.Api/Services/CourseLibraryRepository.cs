using Hogwarts.Api.DbContexts;
using Hogwarts.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

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

        


    }
}
