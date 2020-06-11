using AutoMapper;
using Hogwarts.Api.Models;
using Hogwarts.Api.Services;
using Hogwarts.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Hogwarts.Api.Controllers
{
    [Route("api/courses")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private CourseLibraryRepository _coursesRepo;
        private IMapper _mapper;

        public CoursesController(CourseLibraryRepository coursesRepo, IMapper mapper)
        {
            _coursesRepo = coursesRepo ?? throw new ArgumentNullException(nameof(coursesRepo));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        // GET: api/courses 
        [HttpGet]
        public ActionResult<IEnumerable<CourseDto>> GetCourses()
        {
            return Ok(_mapper.Map<IEnumerable<CourseDto>>(_coursesRepo.GetCourses()));
        }

        // GET: api/courses/{courseId}
        [HttpGet("{courseId}", Name ="GetCourse")]
        public ActionResult<CourseDto> GetCourse(int courseId)
        {
            var courseEntity = _coursesRepo.GetCourseById(courseId);
            if(courseEntity == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<CourseDto>(courseEntity));
        }

        public ActionResult<CourseDto>CreateCourse(CourseForCreationDto courseForCreation)
        {
            var courseEntity = _mapper.Map<Course>(courseForCreation);
            _coursesRepo.Add(courseEntity);
            _coursesRepo.Save();
            var courseToReturn = _mapper.Map<CourseDto>(courseEntity);
            return CreatedAtRoute("GetCourse", new { staffId = courseToReturn.Id }, courseToReturn);
        }       



    }
}
