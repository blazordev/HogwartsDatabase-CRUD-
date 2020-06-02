using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Hogwarts.Api.Models;
using Hogwarts.Api.Services;
using Hogwarts.Data;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Hogwarts.Api.Controllers
{
    [Route("api/Roles")]
    public class RolesController : Controller
    {
        RoleRepository _roleRepository;
        private StaffLibraryRepository _staffRepository;
        IMapper _mapper;
        public RolesController(IMapper mapper, RoleRepository roleRepository,
            StaffLibraryRepository staffRepository)
        {
            _mapper = mapper;
            _roleRepository = roleRepository;
            _staffRepository = staffRepository;
        }
        public ActionResult<IEnumerable<RoleDto>> GetRoles()
        {
            var rolesToReturn = _mapper.Map<IEnumerable<RoleDto>>(_roleRepository.GetRoles());
            if (rolesToReturn == null)
            {
                return NotFound();
            }
            return Ok(rolesToReturn);
        }
        // GET api/roles/5
        [HttpGet("{id}", Name = "GetRole")]
        public ActionResult<RoleDto> GetRole(int id)
        {
            var roleEntity = _roleRepository.GetRoleById(id);
            if (roleEntity == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<RoleDto>(roleEntity));
        }
        // POST: api/roles
        [HttpPost]
        public ActionResult<RoleDto> CreateRole([FromBody] RoleForCreationDto roleForCreation)
        {
            var roleEntity = _mapper.Map<Role>(roleForCreation);
            _roleRepository.AddRole(roleEntity);
            _roleRepository.Save();
            var roleToReturn = _mapper.Map<RoleDto>(roleEntity);
            return CreatedAtRoute("GetRole", new { id = roleToReturn.Id }, roleToReturn);
        }

        //POSTL api/staff/5/role/6
        [HttpPost("staff/{staffId}/role/{roleId}")]
        public ActionResult<RoleDto>AssignRoleToStaff(int staffId, int roleId)
        {
            if (!_roleRepository.RoleExists(roleId) || !_staffRepository.StaffExists(staffId))
            {
                return NotFound();
            }
            _roleRepository.AddRoleToStaff(staffId, roleId);
            _roleRepository.Save();
            return Ok();
        }



    }
}
