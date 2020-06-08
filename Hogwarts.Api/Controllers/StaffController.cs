using System;
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
            _staffRepo.AddStaff(staffEntity);
            _staffRepo.Save();
            return CreatedAtRoute("GetStaff",
                new { id = staffEntity.Id },
                _mapper.Map<StaffDto>(staffEntity));
        }

        //POST api/staff/5/role/6
        [HttpPost("{staffId}/role/{roleId}")]
        public ActionResult<StaffDto> AssignRoleToStaff(int staffId, int roleId)
        {
            if (!_roleRepo.RoleExists(roleId) || !_staffRepo.StaffExists(staffId))
            {
                return NotFound();
            }
            _staffRepo.AddRoleToStaff(staffId, roleId);
            _roleRepo.Save();
            var staffEntity = _staffRepo.GetStaffById(staffId);
            return CreatedAtRoute("GetStaff",
               new { id = staffId },
               _mapper.Map<StaffDto>(staffEntity));// with link of newly created role
        }

        //POST api/staff/5/role/6
        [HttpPost("{staffId}/course/{courseId}")]
        public ActionResult AssignCourseToStaff(int staffId, int courseId)
        {
            if (!_courseRepo.CourseExists(courseId) || !_staffRepo.StaffExists(staffId))
            {
                return NotFound();
            }
            _staffRepo.AddCourseToStaff(staffId, courseId);
            _roleRepo.Save();
            return Ok("Course assigned to Staffmember");
        }


    }
}
