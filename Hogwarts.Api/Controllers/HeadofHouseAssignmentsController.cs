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
    [Route("api/[controller]")]
    [ApiController]
    public class HeadofHouseAssignmentsController : ControllerBase
    {
        private IMapper _mapper;
        private StaffRepository _staffRepo;
        private HouseRepository _houseRepo;
        

        public HeadofHouseAssignmentsController(IMapper mapper,
            StaffRepository staffRepo, 
            HouseRepository houseRepo)
        {
            _mapper = mapper;
            _staffRepo = staffRepo;
            _houseRepo = houseRepo;
            
        }

        //POST api/staffId/houseId
        [HttpPost("{staffId}/{houseId}")]
        public ActionResult<StaffDto> AssignHouseToStaff(int staffId, int houseId)
        {           
            if (_staffRepo.StaffExists(staffId) || !_houseRepo.HouseExists(houseId))
            {
                return NotFound();
            }
            if (!_staffRepo.IsHeadOfHouse(staffId))
            {
                return BadRequest("Staffmember must be assigned " +
                    "Role HeadOfHouse before assigning to them a House");
            }
            _staffRepo.AddHouseToStaff(staffId, houseId);
            _staffRepo.Save();

            return Ok(); 
        }
    }
}
