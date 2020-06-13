using AutoMapper;
using Hogwarts.Api.Models;
using Hogwarts.Api.Services;
using Hogwarts.Data;
using Microsoft.AspNetCore.JsonPatch;
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
        [HttpGet("{courseId}", Name = "GetCourse")]
        public ActionResult<CourseDto> GetCourse(int courseId)
        {
            var courseEntity = _coursesRepo.GetCourseById(courseId);
            if (courseEntity == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<CourseDto>(courseEntity));
        }

        public ActionResult<CourseDto> CreateCourse(CourseForCreationDto courseForCreation)
        {
            var courseEntity = _mapper.Map<Course>(courseForCreation);
            _coursesRepo.Add(courseEntity);
            _coursesRepo.Save();
            var courseToReturn = _mapper.Map<CourseDto>(courseEntity);
            return CreatedAtRoute("GetCourse", new { staffId = courseToReturn.Id }, courseToReturn);
        }

        [HttpPatch("{courseId}")]
        public ActionResult<CourseDto> PartiallyEditCourse(int courseId,
            JsonPatchDocument<CourseForEditDto> patchDocument)
        {
            var courseFromRepo = _coursesRepo.GetCourseById(courseId);
            if (courseFromRepo == null)
            {
                return NotFound();
            }
            //create coursetoEditDto to patch apples to apples
            var courseToPatch = new CourseForEditDto();
            
            patchDocument.ApplyTo(courseToPatch, ModelState);
            if (!TryValidateModel(courseToPatch))
            {
                return ValidationProblem(ModelState);
            }
           
            _mapper.Map(courseToPatch, courseFromRepo);

            _coursesRepo.UpdateCourse(courseFromRepo);
            _coursesRepo.Save();
            return Ok(_mapper.Map<CourseDto>(courseFromRepo));
        }

    }
}
