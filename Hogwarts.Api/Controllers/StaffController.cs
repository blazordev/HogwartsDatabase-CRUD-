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
using Hogwarts.Data.Models;
using Hogwarts.Api.ResourceParameters;
using Microsoft.AspNetCore.JsonPatch;
using Hogwarts.Api.Helpers;
using System.Text.Json;
using Hogwarts.Api.Services.Interfaces;

namespace Hogwarts.Api.Controllers
{
    [Route("api/staff")]
    [ApiController]
    public class StaffController : ControllerBase
    {
        private IStaffRepository _staffRepo;
        private IRoleRepository _roleRepo;
        private ICourseRepository _courseRepo;
        private IMapper _mapper;

        public StaffController(IStaffRepository staffRepo,
            IRoleRepository roleRepo, IMapper mapper, ICourseRepository courseRepo)
        {
            _staffRepo = staffRepo;
            _roleRepo = roleRepo;
            _courseRepo = courseRepo;
            _mapper = mapper;
        }

        // GET: api/Staff
        [HttpGet(Name = "GetStaff")]
        public async Task<ActionResult<IEnumerable<StaffDto>>> GetStaff(
            [FromQuery] StaffResourceParameters staffResourceParameters)
        {
            var staffFromRepo = await _staffRepo.GetAllStaffAsync(staffResourceParameters);
            return Ok(_mapper.Map<IEnumerable<StaffDto>>(staffFromRepo));
        }

        // GET: api/Staff/5
        [HttpGet("{staffId}", Name = "GetStaffMember")]
        public async Task<ActionResult<StaffDto>> GetStaff(int staffId)
        {
            var staffFromRepo = await _staffRepo.GetStaffByIdAsync(staffId);
            if (staffFromRepo == null)
            {
                return NotFound();
            }
            var staffToReturn = _mapper.Map<StaffDto>(staffFromRepo);

            return Ok(staffToReturn);
        }
        //POST: api/staff
        [HttpPost]
        public async Task<ActionResult<StaffDto>> CreateStaff(StaffForCreationDto staff)
        {
            var staffEntity = _mapper.Map<Staff>(staff);
            //first Add staff
            _staffRepo.AddStaff(staffEntity);
            await _staffRepo.SaveAsync();
            var createdStaffId = staffEntity.Id;
            if (staff.RoleIds != null)
            {
                _staffRepo.AssignRoleCollectionToStaff(createdStaffId, staff.RoleIds);
                await _staffRepo.SaveAsync();
                if (staff.HouseId != 0)
                {
                    if (await _staffRepo.IsHeadOfHouseAsync(createdStaffId))
                    {
                        _staffRepo.AddHouseToStaff(createdStaffId, staff.HouseId);
                    }
                    else
                    {
                        return BadRequest("StaffMember must have Role HeadOfHouse to assign House");
                    }
                }
                if (staff.CourseIds.Any())
                {
                    if (await _staffRepo.IsTeacherAsync(createdStaffId))
                    {
                        _staffRepo.AssignCourseCollectionToStaff(createdStaffId, staff.CourseIds);
                    }
                    else
                    {
                        return BadRequest("Staffmember must have role Teacher to assign Courses");
                    }
                }
            }
            //if all goes well
            await _staffRepo.SaveAsync();
            var staffWithRoles = await _staffRepo.GetStaffByIdAsync(staffEntity.Id);
            return CreatedAtRoute("GetStaffMember",
                new { staffId = createdStaffId },
                _mapper.Map<StaffDto>(staffWithRoles));
        }

        [HttpGet("{staffId}/courses")]
        public async Task<ActionResult<IEnumerable<CourseDto>>> GetCoursesForStaff(int staffId)
        {
            var staffEntity = _staffRepo.GetStaffByIdAsync(staffId);
            if (staffEntity == null)
            {
                return NotFound();
            }
            var courses = await _courseRepo.GetCoursesForStaffmemberAsync(staffId);
            return Ok(_mapper.Map<IEnumerable<CourseDto>>(courses));
        }

        [HttpPut("{staffId}")]
        public async Task<ActionResult<StaffDto>> EditStaff([FromRoute] int staffId,
            [FromBody] StaffForEditDto staff)
        {
            var staffEntityToEdit = await _staffRepo.GetStaffByIdAsync(staffId);
            if (staffEntityToEdit == null) return NotFound();
            _mapper.Map(staff, staffEntityToEdit);
            _staffRepo.UpdateStaff(staffEntityToEdit);
            await _staffRepo.SaveAsync();
            return Ok(_mapper.Map<StaffDto>(staffEntityToEdit));
        }
        [HttpPatch("{staffId}")]
        public async Task<ActionResult<StaffDto>> PartiallyEditStaff(int staffId,
            JsonPatchDocument<StaffForEditDto> patchDocument)
        {
            var staffFromRepo = await _staffRepo.GetStaffByIdAsync(staffId);
            if (staffFromRepo == null)
            {
                return NotFound();
            }
            var staffToPatch = _mapper.Map<StaffForEditDto>(staffFromRepo);
            patchDocument.ApplyTo(staffToPatch, ModelState);
            if (!TryValidateModel(staffToPatch))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(staffToPatch, staffFromRepo);
            _staffRepo.UpdateStaff(staffFromRepo);
            await _staffRepo.SaveAsync();
            return Ok(_mapper.Map<StaffDto>(staffFromRepo));
        }

        [HttpDelete("{staffId}")]
        public async Task<ActionResult> DeleteStaff(int staffId)
        {
            var staffFromRepo = await _staffRepo.GetStaffByIdAsync(staffId);
            if (staffFromRepo == null)
            {
                return NotFound();
            }
            _staffRepo.DeleteStaff(staffFromRepo);
            await _staffRepo.SaveAsync();
            return NoContent();
        }


    }
}

