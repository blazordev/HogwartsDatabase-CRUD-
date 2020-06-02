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
        private StaffLibraryRepository _repo;
        private IMapper _mapper;

        public StaffController(StaffLibraryRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        // GET: api/Staffs
        [HttpGet]
        public ActionResult<IEnumerable<StaffDto>> GetStaff(
            [FromQuery] StaffResourceParameter staffResourceParameters)
        {
            var staffFromRepo = _repo.GetAllStaff(staffResourceParameters);
            return Ok(_mapper.Map<IEnumerable<StaffDto>>(staffFromRepo));
        }

        // GET: api/Staffs/5
        [HttpGet("{id}", Name = "GetStaff")]
        public ActionResult<StaffDto> GetStaff(int id)
        {
            var staffFromRepo = _repo.GetStaffById(id);

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
            _repo.AddStaffWithRoles(staffEntity);
            _repo.Save();
            return CreatedAtRoute("GetStaff",
                new { id = staffEntity.Id },
                _mapper.Map<StaffDto>(staffEntity));
        }


    }
}
