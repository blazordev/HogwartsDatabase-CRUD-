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
using Hogwarts.Data.Models;
using Hogwarts.Data.ResourceParameters;
using Microsoft.AspNetCore.JsonPatch;
using Hogwarts.Api.Helpers;
using System.Text.Json;
using Hogwarts.Api.Services.Interfaces;


namespace Hogwarts.Api.Controllers
{
    [Route("api/students")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private IStudentRepository _repo;
        private IMapper _mapper;

        public StudentsController(IStudentRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        // GET: api/students
        [HttpGet(Name = "GetStudents")]
        public IActionResult GetStudents(
            [FromQuery] StudentsResourceParameters studentsResourceParameters)
        {
            var studentsFromRepo = _repo.GetStudents(studentsResourceParameters);

            var previousPageLink = studentsFromRepo.HasPrevious ?
                CreateStudentsResourceUri(studentsResourceParameters,
                ResourceUriType.PreviousPage) : null;

            var nextPageLink = studentsFromRepo.HasNext ?
                CreateStudentsResourceUri(studentsResourceParameters,
                ResourceUriType.NextPage) : null;

            var paginationMetadata = new PaginationMetadata
            {
                TotalCount = studentsFromRepo.TotalCount,
                PageSize = studentsFromRepo.PageSize,
                CurrentPage = studentsFromRepo.CurrentPage,
                TotalPages = studentsFromRepo.TotalPages,
                PreviousPageLink = previousPageLink,
                NextPageLink = nextPageLink
            };

            Response.Headers.Add("X-Pagination",
                JsonSerializer.Serialize(paginationMetadata));
            return base.Ok(_mapper.Map<IEnumerable<StudentDto>>(studentsFromRepo));
        }

        // GET: api/Students/5
        [HttpGet("{studentId}", Name = "GetStudent")]
        public async Task<ActionResult<StudentDto>> GetStudent(int studentId)
        {
            var studentFromRepo = await _repo.GetStudentByIdAsync(studentId);
            if (studentFromRepo == null)
            {
                return NotFound();
            }
            return base.Ok(_mapper.Map<StudentDto>(studentFromRepo));
        }

        [HttpPost]
        public async Task<ActionResult<StudentDto>> CreateStudent(StudentForCreationDto student)
        {
            var studentEntity = _mapper.Map<Student>(student);
            _repo.AddStudent(studentEntity);
            await _repo.SaveAsync();
            var studentWithHouseName = await _repo.GetStudentByIdAsync(studentEntity.Id);
            var studentToReturn = _mapper.Map<StudentDto>(studentWithHouseName);
            return CreatedAtRoute("GetStudent", new { studentId = studentToReturn.Id },
                studentToReturn);

        }
        [HttpPut("{studentId:int}")]
        public async Task<ActionResult<StudentDto>> UpdateStudent([FromRoute] int studentId,
            [FromBody] StudentForEditDto student)
        {
            var studentEntityFromRepo = await _repo.GetStudentByIdAsync(studentId);
            if (studentEntityFromRepo == null) return NotFound();
            _mapper.Map(student, studentEntityFromRepo);
            _repo.UpdateStudent(studentEntityFromRepo);
            await _repo.SaveAsync();
            var studentWithHouseName = await _repo.GetStudentByIdAsync(studentEntityFromRepo.Id);
            return Ok(_mapper.Map<StudentDto>(studentWithHouseName));
        }

        [HttpPatch("{studentId}")]
        public async Task<ActionResult<StudentDto>> PartiallyUpdateStudent(int studentId,
            JsonPatchDocument<StudentForEditDto> patchDocument)
        {
            var studentEntity = await _repo.GetStudentByIdAsync(studentId);
            if (studentEntity == null)
            {
                return NotFound();
            }
            var studentToPatch = _mapper.Map<StudentForEditDto>(studentEntity);
            patchDocument.ApplyTo(studentToPatch, ModelState);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _mapper.Map(studentToPatch, studentEntity);
            _repo.UpdateStudent(studentEntity);
            await _repo.SaveAsync();
            return Ok(_mapper.Map<StudentDto>(studentEntity));
        }

        [HttpDelete("{studentId}")]
        public async Task<ActionResult> DeleteStudent(int studentId)
        {
            var studentFromRepo = await _repo.GetStudentByIdAsync(studentId);
            if (studentFromRepo == null)
            {
                return NotFound();
            }
            _repo.DeleteStudent(studentFromRepo);
            await _repo.SaveAsync();
            return NoContent();
        }

        private string CreateStudentsResourceUri(
           StudentsResourceParameters studentsResourceParameters,
           ResourceUriType type)
        {
            switch (type)
            {
                case ResourceUriType.PreviousPage:
                    return Url.Link("GetStudents",
                      new
                      {
                          pageNumber = studentsResourceParameters.PageNumber - 1,
                          pageSize = studentsResourceParameters.PageSize,
                          houseId = studentsResourceParameters.HouseId,
                          searchQuery = studentsResourceParameters.SearchQuery
                      });
                case ResourceUriType.NextPage:
                    return Url.Link("GetStudents",
                      new
                      {
                          pageNumber = studentsResourceParameters.PageNumber + 1,
                          pageSize = studentsResourceParameters.PageSize,
                          houseId = studentsResourceParameters.HouseId,
                          searchQuery = studentsResourceParameters.SearchQuery
                      });

                default:
                    return Url.Link("GetStudents",
                    new
                    {
                        pageNumber = studentsResourceParameters.PageNumber,
                        pageSize = studentsResourceParameters.PageSize,
                        houseId = studentsResourceParameters.HouseId,
                        searchQuery = studentsResourceParameters.SearchQuery
                    });
            }

        }

    }
}
