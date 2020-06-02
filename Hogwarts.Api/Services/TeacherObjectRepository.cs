using Hogwarts.Api.DbContexts;
using Hogwarts.Api.Models;
using Hogwarts.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hogwarts.Api.Services
{
    public class TeacherObjectRepository
    {
        private HogwartsDbContext _context;

        public TeacherObjectRepository(HogwartsDbContext context)
        {
            _context = context;
        }
        
        public IEnumerable<RetrievedTeacherObject> GetAllTeachers()
        {
            return _context.RetrievedTeacherRecords
            .FromSqlRaw("EXEC RetrieveTeacherObjects");
        }
        public RetrievedTeacherObject GetTeacherById(int teacherId)
        {
            return _context.RetrievedTeacherRecords
                .FromSqlRaw(
                "SELECT t.Id, s.FirstName, s.MiddleNames, s.LastName " +
                "FROM Staff s JOIN Teachers t " +
                "ON S.Id = t.StaffId " +
                "WHERE s.Id = {0}", teacherId).
                FirstOrDefault();                
        }
        public IEnumerable<Course> GetCoursesForTeacher(int teacherId)
        {
            if (String.IsNullOrWhiteSpace(teacherId.ToString()))
            {
                throw new ArgumentNullException(nameof(teacherId));
            }
            var coursesForTeacher = _context.Courses.FromSqlRaw(
                "SELECT c.Id, c.Name FROM " +
                "Courses c JOIN TeachersCourse tc " +
                "ON tc.CourseId = c.Id " +
                "WHERE tc.TeacherId = {0}", teacherId);
            return coursesForTeacher;
        }

        public void AssignCourseToTeacher(TeacherCourse teacherCourse)
        {
            if(teacherCourse == null)
            {
                throw new ArgumentNullException(nameof(teacherCourse)); 
            }
            _context.TeachersCourse.Add(teacherCourse);
        }
               
    }
}
