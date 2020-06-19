using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Hogwarts.Api.Helpers;
using Hogwarts.Api.Models;
using Hogwarts.Api.Services;
using Hogwarts.Api.Services.Interfaces;
using Hogwarts.Data;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Hogwarts.Api.Controllers
{
    [ApiController]
    [Route("api/studentcollections")]
    public class StudentCollectionsController : Controller
    {
        private IMapper _mapper;
        private IStudentRepository _repo;

        public StudentCollectionsController(IMapper mapper, IStudentRepository repo)
        {
            _mapper = mapper;
            _repo = repo;
        }
        //GET: api/StudentCollections/id,id,id...
        [HttpGet("({ids})", Name = "GetStudentCollection")]
        public async Task<ActionResult<IEnumerable<StudentDto>>> GetStudentCollection(
            [FromRoute]
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
            var studentEntities = await _repo.GetStudentsAsync(ids);
            //check if any are missing
            if (studentEntities.Count() != ids.Count())
            {
                return NotFound();
            }
            return Ok(_mapper.Map<IEnumerable<StudentDto>>(studentEntities));

        }
        // POST: api/StudentCollections
        [HttpPost]
        public async Task<ActionResult<IEnumerable<StudentDto>>> CreateStudentCollection(
            IEnumerable<StudentForCreationDto> studentCollection)
        {
            var studentEntities = _mapper.Map<IEnumerable<Student>>(studentCollection);
            foreach (var student in studentEntities)
            {
                _repo.AddStudent(student);
            }
            await _repo.SaveAsync();

            var studentCollectionToReturn = _mapper.Map<IEnumerable<StudentDto>>(studentEntities);
            var idsAsString = string.Join(",", studentCollectionToReturn.Select(a => a.Id));
            return CreatedAtRoute("GetStudentCollection",
                new { ids = idsAsString },
                studentCollectionToReturn);
        }
        [HttpOptions]
        public ActionResult GetAuthorCollectionsOptions()
        {
            Response.Headers.Add("Allow", "GET,OPTIONS,POST");
            return Ok();
        }
    }
}
