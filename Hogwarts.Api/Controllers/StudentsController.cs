﻿using System;
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
using Microsoft.AspNetCore.JsonPatch;
using Hogwarts.Api.Helpers;
using System.Text.Json;

namespace Hogwarts.Api.Controllers
{
    [Route("api/students")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private StudentRepository _repo;
        private IMapper _mapper;

        public StudentsController(StudentRepository repo, IMapper mapper)
        {
            _repo = repo ?? throw new ArgumentException(nameof(repo));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
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

            var paginationMetadata = new
            {
                totalCount = studentsFromRepo.TotalCount,
                pageSize = studentsFromRepo.PageSize,
                currentPage = studentsFromRepo.CurrentPage,
                totalPages = studentsFromRepo.TotalPages,
                previousPageLink,
                nextPageLink
            };

            Response.Headers.Add("X-Pagination",
                JsonSerializer.Serialize(paginationMetadata));
            return base.Ok(_mapper.Map<IEnumerable<StudentDto>>(studentsFromRepo));
        }

        // GET: api/Students/5
        [HttpGet("{studentId}", Name = "GetStudent")]
        public ActionResult<StudentDto> GetStudent(int studentId)
        {
            var studentFromRepo = _repo.GetStudentById(studentId);

            if (studentFromRepo == null)
            {
                return NotFound();
            }
            return base.Ok(_mapper.Map<StudentDto>(studentFromRepo));
        }

        [HttpPost]
        public ActionResult<StudentDto> CreateStudent(StudentForCreationDto student)
        {
            var studentEntity = _mapper.Map<Student>(student);
            _repo.AddStudent(studentEntity);
            _repo.Save();
            var studentToReturn = _mapper.Map<StudentDto>(studentEntity);
            return CreatedAtRoute("GetStudent", new { studentId = studentToReturn.Id },
                studentToReturn);

        }
        [HttpPut("{studentId:int}")]
        public ActionResult<StudentDto> UpdateStudent([FromRoute] int studentId,
            [FromBody] StudentForEditDto student)
        {
            var studentEntityFromRepo = _repo.GetStudentById(studentId);
            if (studentEntityFromRepo == null) return NotFound();
            _mapper.Map(student, studentEntityFromRepo);
            _repo.UpdateStudent(studentEntityFromRepo);
            _repo.Save();
            return Ok(_mapper.Map<StudentDto>(studentEntityFromRepo));
        }

        [HttpPatch("{studentId}")]
        public ActionResult<StudentDto> PartiallyUpdateStudent(int studentId,
            JsonPatchDocument<StudentForEditDto> patchDocument)
        {
            var studentEntity = _repo.GetStudentById(studentId);
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
            _repo.Save();
            return Ok(_mapper.Map<StudentDto>(studentEntity));
        }

        [HttpDelete("{studentId}")]
        public ActionResult DeleteStudent(int studentId)
        {
            var studentFromRepo = _repo.GetStudentById(studentId);
            if (studentFromRepo == null)
            {
                return NotFound();
            }
            _repo.DeleteStudent(studentFromRepo);
            var deleted = _repo.Save();
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
                          mainCategory = studentsResourceParameters.HouseName,
                          searchQuery = studentsResourceParameters.SearchQuery
                      });
                case ResourceUriType.NextPage:
                    return Url.Link("GetStudents",
                      new
                      {
                          pageNumber = studentsResourceParameters.PageNumber + 1,
                          pageSize = studentsResourceParameters.PageSize,
                          mainCategory = studentsResourceParameters.HouseName,
                          searchQuery = studentsResourceParameters.SearchQuery
                      });

                default:
                    return Url.Link("GetStudents",
                    new
                    {
                        pageNumber = studentsResourceParameters.PageNumber,
                        pageSize = studentsResourceParameters.PageSize,
                        mainCategory = studentsResourceParameters.HouseName,
                        searchQuery = studentsResourceParameters.SearchQuery
                    });
            }

        }

    }
}
