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
        public ActionResult<HouseDto> GetHouseForStaff(int staffId)
        {
            if(!_staffRepo.StaffExists(staffId) || !_staffRepo.IsHeadOfHouse(staffId))
            {
                return NotFound();
            }
            var houseToReturn = _houseRepo.GetHouseOfHeadOfHouse(staffId);
            return Ok(_mapper.Map<HouseDto>(houseToReturn));
        }

        //Get staff who are Headof particular house
        [HttpGet("houses/{houseId}")]
        public ActionResult<IEnumerable<StaffDto>> GetHeadsOfHouse(int houseId)
        {
            if (!_houseRepo.HouseExists(houseId))
            {
                return NotFound();
            }
            var staffToReturn = _staffRepo.GetHeadsOfHouse(houseId);
            return Ok(_mapper.Map<IEnumerable<StaffDto>>(staffToReturn));
        }

        //POST api/staffId/houseId

        [HttpPost("{staffId}/{houseId}")]
        public ActionResult<StaffDto> AssignHouseToStaff(int staffId, int houseId)
        {           
            if (!_staffRepo.StaffExists(staffId) || !_houseRepo.HouseExists(houseId))
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

        [HttpDelete("{staffId}/{houseId}")]
        public ActionResult<StaffDto> UnassignHouseFromStaff(int staffId, int houseId)
        {
            var headOfHouseEntity = _houseRepo.GetHeadOfHouse(staffId, houseId);
            if (headOfHouseEntity == null)
            {
                return NotFound();
            }
            
            _staffRepo.DeleteStaffHouseRelationship(headOfHouseEntity);
            _staffRepo.Save();

            return Ok();
        }
    }
}
