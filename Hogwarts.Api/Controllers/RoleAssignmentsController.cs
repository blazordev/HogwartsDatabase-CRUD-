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
        public ActionResult<IEnumerable<RoleDto>> GetRolesForStaff(int staffId)
        {
            if (!_staffRepo.StaffExists(staffId))
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
            if (_roleRepo.HasRoleAlreadyAsync(staffId, roleId).Result)
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
