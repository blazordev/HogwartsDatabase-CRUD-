using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Hogwarts.Api.Models;
using Hogwarts.Api.Services;
using Hogwarts.Api.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hogwarts.Api.Controllers
{
    [Route("api/RoleAssignments")]
    [ApiController]
    public class RoleAssignmentsController : ControllerBase
    {
        private IStaffRepository _staffRepo;
        private IRoleRepository _roleRepo;
        private IMapper _mapper;

        public RoleAssignmentsController(IStaffRepository staffRepo,
            IRoleRepository roleRepo,
            IMapper mapper)
        {
            _staffRepo = staffRepo;
            _roleRepo = roleRepo;
            _mapper = mapper;
        }
        //api/roleAssignmets/staff/2
        [HttpGet("staff/{staffId}", Name = "GetRolesForStaff")]
        public async Task<ActionResult<IEnumerable<RoleDto>>> GetRolesForStaff(int staffId)
        {
            if (!await _staffRepo.StaffExistsAsync(staffId))
            {
                return NotFound();
            }
            var rolesToReturn = _roleRepo.GetRolesForStaffAsync(staffId);
            return Ok(_mapper.Map<IEnumerable<RoleDto>>(rolesToReturn));
        }

        //POST api/RoleAssignments/staffId/roleId/
        [HttpPost("{staffId}/{roleId}")]
        public async Task<ActionResult<StaffDto>> AssignRoleToStaff(int staffId, int roleId)
        {
            if (!await _roleRepo.RoleExistsAsync(roleId) || !await _staffRepo.StaffExistsAsync(staffId))
            {
                return NotFound();
            }
            if (await _roleRepo.HasRoleAlreadyAsync(staffId, roleId))
            {
                return Conflict("Staffmember already has that role");
            }
            _staffRepo.AddRoleToStaff(staffId, roleId);
            await _staffRepo.SaveAsync();
            return NoContent();
        }

        [HttpDelete("{staffId}/{roleId}")]
        public async Task<ActionResult> UnassignRoleFromStaff(int staffId, int roleId)
        {
            var staffRoleFromRepo = await _staffRepo.GetStaffRoleEntityAsync(staffId, roleId);
            if (staffRoleFromRepo == null)
            {
                return NotFound();
            }
            _staffRepo.DeleteStaffRoleRelationship(staffRoleFromRepo);
            await _staffRepo.SaveAsync();
            return NoContent();
        }
    }
}
