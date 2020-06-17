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
using Microsoft.AspNetCore.JsonPatch;
using Hogwarts.Api.Helpers;
using System.Text.Json;

namespace Hogwarts.Api.Controllers
{
    [Route("api/staff")]
    [ApiController]
    public class StaffController : ControllerBase
    {
        private StaffRepository _staffRepo;
        private RoleRepository _roleRepo;
        private CourseRepository _courseRepo;
        private IMapper _mapper;

        public StaffController(StaffRepository staffRepo,
            RoleRepository roleRepo, IMapper mapper, CourseRepository courseRepo)
        {
            _staffRepo = staffRepo;
            _roleRepo = roleRepo;
            _courseRepo = courseRepo;
            _mapper = mapper;
        }

        // GET: api/Staff
        [HttpGet(Name = "GetStaff")]
        public ActionResult<IEnumerable<StaffDto>> GetStaff(
            [FromQuery] StaffResourceParameters staffResourceParameters)
        {
            var staffFromRepo = _staffRepo.GetAllStaff(staffResourceParameters);

            var previousPageLink = staffFromRepo.HasPrevious ?
                CreateStaffResourceUri(staffResourceParameters,
                ResourceUriType.PreviousPage) : null;

            var nextPageLink = staffFromRepo.HasNext ?
                CreateStaffResourceUri(staffResourceParameters,
                ResourceUriType.NextPage) : null;

            var paginationMetadata = new
            {
                totalCount = staffFromRepo.TotalCount,
                pageSize = staffFromRepo.PageSize,
                currentPage = staffFromRepo.CurrentPage,
                totalPages = staffFromRepo.TotalPages,
                previousPageLink,
                nextPageLink
            };

            Response.Headers.Add("X-Pagination",
                JsonSerializer.Serialize(paginationMetadata));
            return Ok(_mapper.Map<IEnumerable<StaffDto>>(staffFromRepo));
        }

        // GET: api/Staffs/5
        [HttpGet("{id}", Name = "GetStaffMember")]
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
                _staffRepo.AssignRoleCollectionToStaff(createdStaffId, staff.RoleIds);

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
            return CreatedAtRoute("GetStaffMember",
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
        [HttpPatch("{staffId}")]
        public ActionResult<StaffDto> PartiallyEditStaff(int staffId,
            JsonPatchDocument<StaffForEditDto> patchDocument)
        {
            var staffFromRepo = _staffRepo.GetStaffById(staffId);
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
            _staffRepo.Save();
            return Ok(_mapper.Map<StaffDto>(staffFromRepo));
        }

        [HttpDelete("{staffId}")]
        public ActionResult DeleteStaff(int staffId)
        {
            var staffFromRepo = _staffRepo.GetStaffById(staffId);
            if (staffFromRepo == null)
            {
                return NotFound();
            }
            _staffRepo.DeleteStaff(staffFromRepo);
            _staffRepo.Save();
            return NoContent();
        }
        private string CreateStaffResourceUri(
           StaffResourceParameters staffResourceParameters,
           ResourceUriType type)
        {
            switch (type)
            {
                case ResourceUriType.PreviousPage:
                    return Url.Link("GetStaff",
                      new
                      {
                          pageNumber = staffResourceParameters.PageNumber - 1,
                          pageSize = staffResourceParameters.PageSize,
                          mainCategory = staffResourceParameters.RoleId,
                          searchQuery = staffResourceParameters.SearchQuery
                      });
                case ResourceUriType.NextPage:
                    return Url.Link("GetStaff",
                      new
                      {
                          pageNumber = staffResourceParameters.PageNumber + 1,
                          pageSize = staffResourceParameters.PageSize,
                          mainCategory = staffResourceParameters.RoleId,
                          searchQuery = staffResourceParameters.SearchQuery
                      });

                default:
                    return Url.Link("GetStaff",
                    new
                    {
                        pageNumber = staffResourceParameters.PageNumber,
                        pageSize = staffResourceParameters.PageSize,
                        mainCategory = staffResourceParameters.RoleId,
                        searchQuery = staffResourceParameters.SearchQuery
                    });
            }
        }
    }
}

