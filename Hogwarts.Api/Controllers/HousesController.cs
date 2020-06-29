using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Hogwarts.Api.DbContexts;
using Hogwarts.Data.Models;
using Hogwarts.Api.Services;
using Hogwarts.Api.Services.Interfaces;
using Hogwarts.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hogwarts.Api.Controllers
{
    [Route("api/houses")]
    [ApiController]
    public class HousesController : ControllerBase
    {
        private readonly IHouseRepository _repo;
        private readonly IMapper _mapper;

        public HousesController(IHouseRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        [HttpGet]
        [HttpHead]
        public async Task<ActionResult<IEnumerable<HouseDto>>> GetAllHouses()
        {
            var housesFromRepo = await _repo.GetAllHousesAsync();
            if(housesFromRepo == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<IEnumerable<HouseDto>>(housesFromRepo));
        }

        [HttpGet("{houseId}")]
        public ActionResult<HouseDto> GetHouse(int houseId)
        {
            var houseFromRepo = _repo.GetHouseByIdAsync(houseId);
            if (houseFromRepo == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<HouseDto>(houseFromRepo));
        }

    }
}