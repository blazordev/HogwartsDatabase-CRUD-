﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Hogwarts.Api.DbContexts;
using Hogwarts.Data;
using Hogwarts.Api.Services;
using AutoMapper;
using Hogwarts.Api.Models;
using Hogwarts.Api.ResourceParameters;

namespace Hogwarts.Api.Controllers
{
    [Route("api/staff")]
    [ApiController]
    public class StaffController : ControllerBase
    {
        private StaffLibraryRepository _staffRepo;
        private RoleRepository _roleRepo;
        private CourseLibraryRepository _courseRepo;
        private IMapper _mapper;

        public StaffController(StaffLibraryRepository staffRepo,
            RoleRepository roleRepo, IMapper mapper, CourseLibraryRepository courseRepo)
        {
            _staffRepo = staffRepo;
            _roleRepo = roleRepo;
            _courseRepo = courseRepo;
            _mapper = mapper;
        }

        // GET: api/Staff
        [HttpGet]
        public ActionResult<IEnumerable<StaffDto>> GetStaff(
            [FromQuery] StaffResourceParameters staffResourceParameters)
        {
            var staffFromRepo = _staffRepo.GetAllStaff(staffResourceParameters);
            return Ok(_mapper.Map<IEnumerable<StaffDto>>(staffFromRepo));
        }

        // GET: api/Staffs/5
        [HttpGet("{id}", Name = "GetStaff")]
        public ActionResult<StaffDto> GetStaff(int id)
        {
            var staffFromRepo = _staffRepo.GetStaffById(id);

            if (staffFromRepo == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<StaffDto>(staffFromRepo));
        }
        //POST: api/staff
        [HttpPost]
        public ActionResult<StaffDto> CreateStaff(StaffForCreationDto staff)
        {
            var staffEntity = _mapper.Map<Staff>(staff);
            //first Add staff
            _staffRepo.AddStaff(staffEntity);
            var createdStaffId = staffEntity.Id;
            if (staff.RoleIds != null)
            {
                _staffRepo.AddRoleCollectionToStaff(createdStaffId, staff.RoleIds);

                if (!String.IsNullOrEmpty(staff.HouseId.ToString()))
                {
                    //if role is HeadOfHouse, add house to staff
                    if (_staffRepo.IsHeadOfHouse(createdStaffId))
                    {
                        _staffRepo.AddHouseToStaff(createdStaffId, staff.HouseId);
                    }
                    else
                    {
                        return BadRequest("StaffMember must have Role HeadOfHouse to assign House");
                    }
                }
                if (staff.CourseIds != null)
                {
                    //if role is Teacher, add courses to staff
                    if (_staffRepo.IsTeacher(createdStaffId))
                    {
                        _staffRepo.AssignCourseCollectionToStaff(staffEntity.Id, staff.CourseIds);
                    }
                    else
                    {
                        return BadRequest("Staffmember must have role Teacher to assign Courses");
                    }
                }
            }
            //if all goes well
            _staffRepo.Save();
            return CreatedAtRoute("GetStaff",
                new { staffId = createdStaffId },
                _mapper.Map<StaffDto>(staffEntity));
        }

        [HttpGet("{staffId}/courses")]
        public ActionResult<IEnumerable<CourseDto>> GetCoursesForStaff(int staffId)
        {
            var staffEntity = _staffRepo.GetStaffById(staffId);
            if (staffEntity == null)
            {
                return NotFound();
            }
            var courses = _courseRepo.GetCoursesForStaffmember(staffId);
            return Ok(_mapper.Map<IEnumerable<CourseDto>>(courses));
        }

    }
}
