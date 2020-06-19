using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Hogwarts.Api.DbContexts;
using Hogwarts.Api.Models;
using Hogwarts.Api.Services;
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
        private readonly HouseRepository _repo;
        private readonly IMapper _mapper;

        public HousesController(HouseRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        [HttpGet]
        [HttpHead]
        public ActionResult<IEnumerable<HouseDto>> GetHouses()
        {
            var housesFromRepo = _repo.GetAllHousesAsync();
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