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
        //GET house of Staff
        [HttpGet("staff/{staffId}")]
        public async Task<ActionResult<HouseDto>> GetHouseForStaff(int staffId)
        {
            if(! await _staffRepo.StaffExistsAsync(staffId) || ! await _staffRepo.IsHeadOfHouseAsync(staffId))
            {
                return NotFound();
            }
            var houseToReturn = await _houseRepo.GetHouseAssignedToHeadOfHouseAsync(staffId);
            return Ok(_mapper.Map<HouseDto>(houseToReturn));
        }

        //Get staff who are Headof particular house
        [HttpGet("houses/{houseId}")]
        public ActionResult<IEnumerable<StaffDto>> GetHeadsOfHouse(int houseId)
        {
            if (!_houseRepo.HouseExistsAsync(houseId).Result)
            {
                return NotFound();
            }
            var staffToReturn = _staffRepo.GetHeadsOfHouseAsync(houseId);
            return Ok(_mapper.Map<IEnumerable<StaffDto>>(staffToReturn));
        }

        //POST api/staffId/houseId

        [HttpPost("{staffId}/{houseId}")]
        public async Task<ActionResult<StaffDto>> AssignHouseToStaff(int staffId, int houseId)
        {           
            if (!await _staffRepo.StaffExistsAsync(staffId) || !await _houseRepo.HouseExistsAsync(houseId))
            {
                return NotFound();
            }
            if (! await _staffRepo.IsHeadOfHouseAsync(staffId))
            {
                return BadRequest("Staffmember must be assigned " +
                    "Role HeadOfHouse before assigning to them a House");
            }
            _staffRepo.AddHouseToStaff(staffId, houseId);
            await _staffRepo.SaveAsync();

            return Ok(); 
        }

        [HttpDelete("{staffId}/{houseId}")]
        public async Task<ActionResult<StaffDto>> UnassignHouseFromStaff(int staffId, int houseId)
        {
            var headOfHouseEntity = await _houseRepo.GetHeadOfHouseAsync(staffId, houseId);
            if (headOfHouseEntity == null)
            {
                return NotFound();
            }            
            _staffRepo.DeleteStaffHouseRelationship(headOfHouseEntity);
            await _staffRepo.SaveAsync();

            return Ok();
        }
    }
}
