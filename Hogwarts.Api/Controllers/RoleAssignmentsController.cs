using System;
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
    [Route("api/RoleAssignments")]
    [ApiController]
    public class RoleAssignmentsController : ControllerBase
    {
        private StaffRepository _staffRepo;
        private RoleRepository _roleRepo;
        private IMapper _mapper;

        public RoleAssignmentsController(StaffRepository staffRepo,
            RoleRepository roleRepo,
            IMapper mapper)
        {
            _staffRepo = staffRepo;
            _roleRepo = roleRepo;
            _mapper = mapper;
        }
        //api/roleAssignmets/staff/2
        [HttpGet("staff/{staffId}", Name = "GetRolesForStaff")]
        public ActionResult<IEnumerable<RoleDto>>GetRolesForStaff(int staffId)
        {
            if(!_staffRepo.StaffExists(staffId))
            {
                return NotFound();
            }
            var rolesToReturn = _roleRepo.GetRolesForStaff(staffId);
            return Ok(_mapper.Map<IEnumerable<RoleDto>>(rolesToReturn));
        }

        //POST api/RoleAssignments/staffId/roleId/
        [HttpPost("{staffId}/{roleId}")]
        public ActionResult<StaffDto> AssignRoleToStaff(int staffId, int roleId)
        {
            if (!_roleRepo.RoleExists(roleId) || !_staffRepo.StaffExists(staffId))
            {
                return NotFound();
            }
            if(_roleRepo.HasRoleAlready(staffId, roleId))
            {
                return Conflict("Staffmember already has that role");
            }
            _staffRepo.AddRoleToStaff(staffId, roleId);
            _staffRepo.Save();
            var roleToReturn = _roleRepo.GetRoleById(staffId);
            return CreatedAtRoute("GetRolesForStaff",
               new { staffId = staffId },
               _mapper.Map<StaffDto>(roleToReturn));
        }
    }
}
