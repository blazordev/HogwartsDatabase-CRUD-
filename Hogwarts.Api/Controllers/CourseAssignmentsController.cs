using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Hogwarts.Data.Models;
using Hogwarts.Api.Services;
using Hogwarts.Api.Services.Interfaces;
using Hogwarts.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hogwarts.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseAssignmentsController : ControllerBase
    {
        private IMapper _mapper;
        private ICourseRepository _courseRepo;
        private IStaffRepository _staffRepo;

        public CourseAssignmentsController(IMapper mapper,
            ICourseRepository courseRepo,
            IStaffRepository staffRepo)
        {
            _mapper = mapper;
            _courseRepo = courseRepo;
            _staffRepo = staffRepo;
        }
        //get assigned courses for teacher
        [HttpGet("Staff/{staffId}", Name = "GetAssignedCoursesForStaff")]
        public async Task<ActionResult<IEnumerable<CourseDto>>> GetAssignedCoursesForStaff(int staffId)
        {
            if (!await _staffRepo.StaffExistsAsync(staffId) || !await _staffRepo.IsTeacherAsync(staffId))
            {
                return NotFound();
            }
            var courseEntities = await _courseRepo.GetCoursesForStaffmemberAsync(staffId);
            return Ok(_mapper.Map<IEnumerable<CourseDto>>(courseEntities));
        }
        [HttpGet("Courses/{courseId}", Name = "GetAllStaffForCourse")]
        public async Task<ActionResult<IEnumerable<CourseDto>>> GetAllStaffForCourse(int courseId)
        {
            if (!_courseRepo.CourseExistsAsync(courseId).Result)
            {
                return NotFound();
            }
            var staffEntities = await _staffRepo.GetStaffForCourseAsync(courseId);
            return Ok(_mapper.Map<IEnumerable<StaffDto>>(staffEntities));
        }

        //POST api/staffId/courseId
        [HttpPost("{staffId}/{courseId}")]
        public async Task<ActionResult<StaffDto>> AssignCourseToStaff(int staffId, int courseId)
        {
            var staffEntity = _staffRepo.GetStaffByIdAsync(staffId);
            if (staffEntity == null || !_courseRepo.CourseExistsAsync(courseId).Result)
            {
                return NotFound();
            }
            if (!await _staffRepo.IsTeacherAsync(staffEntity.Id))
            {
                return BadRequest("Staffmember must be assigned Role Teacher before assigning to them a course");
            }
            _staffRepo.AddCourseToStaff(staffId, courseId);
            await _staffRepo.SaveAsync();

            return CreatedAtRoute("GetAssignedCoursesForStaff", new { staffId = staffId },
                _mapper.Map<StaffDto>(staffEntity));
        }

        [HttpDelete("{staffId}/{courseId}")]
        public async Task<ActionResult> UnassignCourseFromStaff(int staffId, int courseId)
        {
            var staffCourse = await _staffRepo.GetStaffCourseById(staffId, courseId);
            if (staffCourse == null)
            {
                return NotFound();
            }
            _staffRepo.DeleteStaffCourseRelationship(staffCourse);
            await _staffRepo.SaveAsync();
            return NoContent();
        }
    }
}
