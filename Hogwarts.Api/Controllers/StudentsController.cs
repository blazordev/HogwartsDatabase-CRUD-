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
using Hogwarts.Api.Models;
using Hogwarts.Api.ResourceParameters;
using Microsoft.AspNetCore.JsonPatch;

namespace Hogwarts.Api.Controllers
{
    [Route("api/students")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private StudentLibraryRepository _repo;
        private IMapper _mapper;

        public StudentsController(StudentLibraryRepository repo, IMapper mapper)
        {
            _repo = repo ?? throw new ArgumentException(nameof(repo));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        // GET: api/students
        [HttpGet]
        public IActionResult GetStudents(
            [FromQuery] StudentResourceParameters studentResourceParameters)
        {
            var studentsFromRepo = _repo.GetStudents(studentResourceParameters);
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
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _mapper.Map(studentToPatch, studentEntity);
            _repo.UpdateStudent(studentEntity);
            _repo.Save();
            return Ok(_mapper.Map<StudentDto>(studentEntity));
        }

    }
}
