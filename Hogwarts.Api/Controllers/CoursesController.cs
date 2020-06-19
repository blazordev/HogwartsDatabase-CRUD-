using AutoMapper;
using Hogwarts.Api.Models;
using Hogwarts.Api.Services;
using Hogwarts.Data;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hogwarts.Api.Controllers
{
    [Route("api/courses")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private CourseRepository _coursesRepo;
        private IMapper _mapper;

        public CoursesController(CourseRepository coursesRepo, IMapper mapper)
        {
            _coursesRepo = coursesRepo;
            _mapper = mapper;
        }

        // GET: api/courses 
        [HttpGet]
        public ActionResult<IEnumerable<CourseDto>> GetCourses()
        {
            return Ok(_mapper.Map<IEnumerable<CourseDto>>(_coursesRepo.GetCoursesAsync()));
        }

        // GET: api/courses/{courseId}
        [HttpGet("{courseId}", Name = "GetCourse")]
        public ActionResult<CourseDto> GetCourse(int courseId)
        {
            var courseEntity = _coursesRepo.GetCourseByIdAsync(courseId);
            if (courseEntity == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<CourseDto>(courseEntity));
        }

        public async Task<ActionResult<CourseDto>> CreateCourse(CourseForCreationDto courseForCreation)
        {
            var courseEntity = _mapper.Map<Course>(courseForCreation);
            _coursesRepo.Add(courseEntity);
            await _coursesRepo.SaveAsync();
            var courseToReturn = _mapper.Map<CourseDto>(courseEntity);
            return CreatedAtRoute("GetCourse", new { staffId = courseToReturn.Id }, courseToReturn);
        }
        [HttpPut("{courseId}")]
        public async Task<ActionResult<CourseDto>> EditCourse([FromRoute] int courseId, 
            [FromBody] CourseForEditDto course)
        {
            var courseToEdit = await _coursesRepo.GetCourseByIdAsync(courseId);
            if (courseToEdit == null) return NotFound();
            _mapper.Map(course, courseToEdit);
            _coursesRepo.UpdateCourse(courseToEdit);
            await _coursesRepo.SaveAsync();
            return Ok(_mapper.Map<CourseDto>(courseToEdit));
        }
        [HttpPatch("{courseId}")]
        public async Task<ActionResult<CourseDto>> PartiallyEditCourse(int courseId,
            JsonPatchDocument<CourseForEditDto> patchDocument)
        {
            var courseFromRepo = await _coursesRepo.GetCourseByIdAsync(courseId);
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
            await _coursesRepo.SaveAsync();
            return Ok(_mapper.Map<CourseDto>(courseFromRepo));
        }

        [HttpDelete("{courseId}")]
        public async Task<ActionResult> DeleteCourse(int courseId)
        {
            var courseToDelete = await _coursesRepo.GetCourseByIdAsync(courseId);
            if(courseToDelete == null)
            {
                return NotFound();
            }
            _coursesRepo.DeleteCourse(courseToDelete);
            await _coursesRepo.SaveAsync();
            return NoContent();
        }

    }
}
