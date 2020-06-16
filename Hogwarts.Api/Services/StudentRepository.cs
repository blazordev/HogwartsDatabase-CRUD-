
using AutoMapper;
using Hogwarts.Api.DbContexts;
using Hogwarts.Api.Helpers;
using Hogwarts.Api.ResourceParameters;
using Hogwarts.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hogwarts.Api.Services
{
    public class StudentRepository
    {
        private HogwartsDbContext _context;
        private IMapper _mapper;

        public StudentRepository(HogwartsDbContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public bool StudentExist(int studentId)
        {
            if(String.IsNullOrWhiteSpace(studentId.ToString()))
            {
                throw new ArgumentNullException(nameof(studentId));
            }
            return _context.Students.Any(s => s.Id == studentId);
        }
        public IEnumerable<Student> GetStudents()
        {
            return _context.Students.ToList();
        }

        public PagedList<Student>GetStudents(
            StudentsResourceParameters studentResourceParameters)
        {
            if(studentResourceParameters == null)
            {
                throw new ArgumentNullException(nameof(studentResourceParameters));
            }
            
            var collection = _context.Students as IQueryable<Student>;
            if(!String.IsNullOrWhiteSpace(studentResourceParameters.HouseName))
            {
                var houseName = studentResourceParameters.HouseName.Trim();
                collection = collection.Where(s => s.House.Name == houseName);
            }
            if(!String.IsNullOrWhiteSpace(studentResourceParameters.SearchQuery))
            {
                var searchQuery = studentResourceParameters.SearchQuery.ToLower().Trim();
                collection = collection.Where(s => s.FirstName.ToLower().Contains(searchQuery)
                || s.MiddleNames.ToLower().Contains(searchQuery)
                || s.LastName.ToLower().Contains(searchQuery));
            }
            return PagedList<Student>.Create(collection,
                studentResourceParameters.PageNumber,
                studentResourceParameters.PageSize);
        }
        public IEnumerable<Student> GetStudents(IEnumerable<int> studentIds)
        {
            if (studentIds == null)
            {
                throw new ArgumentNullException(nameof(studentIds));
            }

            return _context.Students.Where(s => studentIds.Contains(s.Id))
                .OrderBy(s => s.FirstName)
                .OrderBy(s => s.LastName)
                .ToList();
        }

        public Student GetStudentById(int studentId)
        {
            if(String.IsNullOrEmpty(studentId.ToString()))
            {
                throw new ArgumentNullException(nameof(studentId));
            }
            return _context.Students.FirstOrDefault(s => s.Id == studentId);
        }

        public void AddStudent(Student student)
        {
            if(student == null)
            {
                throw new ArgumentNullException(nameof(student));
            }
            _context.Students.Add(student);
        }

        public void DeleteStudent(Student studentFromRepo)
        {
            _context.Students.Remove(studentFromRepo);
        }

        public void UpdateStudent(Student student)
        {
            //no code needed
        }
        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}
