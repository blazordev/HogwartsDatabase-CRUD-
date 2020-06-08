using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Hogwarts.Api.Helpers;
using Hogwarts.Api.Models;
using Hogwarts.Api.Services;
using Hogwarts.Data;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Hogwarts.Api.Controllers
{
    [ApiController]
    [Route("api/staffCollections")]
    public class StaffCollectionsController : Controller
    {
        private IMapper _mapper;
        private StaffLibraryRepository _repo;

        public StaffCollectionsController(IMapper mapper, StaffLibraryRepository repo)
        {
            _mapper = mapper;
            _repo = repo;
        }
        //GET: api/StaffCollections/id,id,id...
        [HttpGet("({ids})", Name = "GetStaffCollection")]
        public IActionResult GetStaffCollection(
        [FromQuery] 
        //have to do our own model-binding since .net core cant auto-bind arrays from string
        [ModelBinder(BinderType = typeof(ArrayModelBinder))]
        IEnumerable<int> ids)
        {
            //usually, the [ApiController] attribute checks if parameter is null 
            //and automatically sends back badrequest
            //But here we have do a null-check ourselves
            if (ids == null)
            {
                return BadRequest();
            }
            var staffEntities = _repo.GetStaff(ids);
            //check if any are missing
            if (staffEntities.Count() != ids.Count())
            {
                return NotFound();
            }
            return Ok(_mapper.Map<IEnumerable<StaffDto>>(staffEntities));
        }

        //POST: api/StaffCollections
        [HttpPost]
        public ActionResult<IEnumerable<StaffDto>> CreateStaffCollection(StaffForCreationDto staffForCreationDto)
        {
            var staffEntities = _mapper.Map<IEnumerable<Staff>>(staffForCreationDto);
            foreach (var staffEntity in staffEntities)
            {
                _repo.AddStaff(staffEntity);
                _repo.Save();
            }
            var staffCollectionToReturn = _mapper.Map<IEnumerable<StaffDto>>(staffEntities);
            var idsAsString = string.Join(",", staffCollectionToReturn.Select(s => s.Id));
            return CreatedAtRoute("GetStaffCollection",
                new { ids = idsAsString },
                staffCollectionToReturn);
        }
    }
}
