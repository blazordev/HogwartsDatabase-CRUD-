using AutoMapper;
using Hogwarts.Api.DbContexts;
using Hogwarts.Api.Models;
using Hogwarts.Api.Services;
using Hogwarts.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hogwarts.Api.Controllers
{
    [Route("api/teachers")]
    [ApiController]
    public class TeachersController : ControllerBase
    {
        private TeacherObjectRepository _teacherRepo;
        private CourseLibraryRepository _courseRepo;
        private IMapper _mapper;

        public TeachersController(TeacherObjectRepository teacherRepo,
            CourseLibraryRepository courseRepo,
            IMapper mapper)
        {
            _teacherRepo = teacherRepo;
            _courseRepo = courseRepo;
            _mapper = mapper;
        }

        //GET: api/teachers
        [HttpGet]
        public ActionResult<IEnumerable<RetrievedTeacherObject>> GetTeachers()
        {
            return Ok(_teacherRepo.GetAllTeachers());
        }

        //GET: api/teachers/{teacherId}
        [HttpGet("{teacherId}", Name = "GetTeacher")]
        public ActionResult<RetrievedTeacherObject> GetTeacher(int teacherId)
        {
            var teacherObject = _teacherRepo.GetTeacherById(teacherId);

            if (teacherObject == null)
            {
                return NotFound();
            }
            return Ok(teacherObject);
        }

        // GET: api/teachers/{teacherId}/courses
        [HttpGet("{teacherId}/courses")]
        public ActionResult<IEnumerable<CourseDto>> GetCoursesForTeacher(int teacherId)
        {
            var coursesForTeacher = _teacherRepo.GetCoursesForTeacher(teacherId);

            if (coursesForTeacher == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<IEnumerable<CourseDto>>(coursesForTeacher));
        }
        //POST api/teachers/{teacherId}/courses/{courseId}
        [HttpPost("{teacherId}/courses/{courseId}")]
        public ActionResult<TeacherCourseDto> AssignExistingCourseToExistingTeacher(int teacherId, int courseId)
        {
            var teacherCourseEntity = new TeacherCourse { TeacherId = teacherId, CourseId = courseId };

            return Ok(_mapper.Map<TeacherCourseDto>(teacherCourseEntity));
        }

        


    }
}
