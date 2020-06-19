using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Hogwarts.Api.Helpers;
using Hogwarts.Api.Models;
using Hogwarts.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hogwarts.Api.Controllers
{
    [Route("api/StaffCollections")]
    [ApiController]
    public class StaffCollectionsController : ControllerBase
    {
        private IMapper _mapper;
        private StaffRepository _staffRepo;

        public StaffCollectionsController(IMapper mapper, StaffRepository staffRepo)
        {
            _mapper = mapper;
            _staffRepo = staffRepo;
        }

        //DELETE api/StaffCollections/id,id,id
        [HttpDelete("({ids})")]
        public async Task<ActionResult> DeleteStaffCollection([FromRoute]
            [ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<int> ids)
        {
            if(ids == null)
            {
                return BadRequest();
            }
            var staffEntities = await _staffRepo.GetStaffCollectionAsync(ids);

            if (ids.Count() != staffEntities.Count())
            {
                return NotFound();
            }
            _staffRepo.DeleteStaffCollection(staffEntities);
            await _staffRepo.SaveAsync();
            return NoContent();
        }
    }
}
