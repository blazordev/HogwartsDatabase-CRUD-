﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Hogwarts.Api.Models;
using Hogwarts.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hogwarts.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseAssignmentsController : ControllerBase
    {
        private IMapper _mapper;
        private CourseLibraryRepository _courseRepo;
        private StaffLibraryRepository _staffRepo;

        public CourseAssignmentsController(IMapper mapper,
            CourseLibraryRepository courseRepo,
            StaffLibraryRepository staffRepo)
        {
            _mapper = mapper;
            _courseRepo = courseRepo;
            _staffRepo = staffRepo;
        }
        //get assigned courses for teacher
        [HttpGet("Staff/{staffId}", Name = "GetAssignedCoursesForStaff")]
        public ActionResult<IEnumerable<CourseDto>> GetAssignedCoursesForStaff(int staffId)
        {
            if (!_staffRepo.StaffExists(staffId) || !_staffRepo.IsTeacher(staffId))
            {
                return NotFound();
            }
            var courseEntities = _courseRepo.GetCoursesForStaffmember(staffId);
            return Ok(_mapper.Map<IEnumerable<CourseDto>>(courseEntities));
        }
      
        //POST api/staffId/courseId
        [HttpPost("{staffId}/{courseId}")]
        public ActionResult<StaffDto> AssignCourseToStaff(int staffId, int courseId)
        {            
            var staffEntity = _staffRepo.GetStaffById(staffId);
            if (staffEntity == null || !_courseRepo.CourseExists(courseId))
            {
                return NotFound();
            }
            if(!_staffRepo.IsTeacher(staffEntity.Id))
            {
                return BadRequest("Staffmember must be assigned Role Teacher before assigning to them a course");
            }
            _staffRepo.AddCourseToStaff(staffId, courseId);
            _staffRepo.Save();

            return CreatedAtRoute("GetAssignedCoursesForStaff", new { staffId = staffId },
                _mapper.Map<StaffDto>(staffEntity)); 
        }
    }
}