
using AutoMapper;
using Hogwarts.Api.DbContexts;
using Hogwarts.Api.Helpers;
using Hogwarts.Data.ResourceParameters;
using Hogwarts.Api.Services.Interfaces;
using Hogwarts.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hogwarts.Api.Services
{
    public class StudentRepository : IStudentRepository
    {
        private HogwartsDbContext _context;
        private IMapper _mapper;

        public StudentRepository(HogwartsDbContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<bool> StudentExistAsync(int studentId)
        {
            return await _context.Students.AnyAsync(s => s.Id == studentId);
        }
        public async Task<IEnumerable<Student>> GetStudentsAsync()
        {
            return await _context.Students.ToListAsync();
        }
        public async Task<IEnumerable<Student>> GetAllStudentsForFileAsync()
        {
            return await _context.Students.OrderBy(s => s.FirstName)
                .OrderBy(s => s.LastName).Include(s => s.House).ToListAsync();
        }
        public PagedList<Student> GetAllStudentsAsync(
            StudentsResourceParameters studentResourceParameters)
        {
            if (studentResourceParameters == null)
            {
                throw new ArgumentNullException(nameof(studentResourceParameters));
            }

            var collection = _context.Students as IQueryable<Student>;
            if (studentResourceParameters.HouseId != 0)
            {
                collection = collection.Where(s => s.HouseId == studentResourceParameters.HouseId);
            }
            if (!String.IsNullOrWhiteSpace(studentResourceParameters.SearchQuery))
            {
                var searchQuery = studentResourceParameters.SearchQuery.ToLower().Trim();
                collection = collection.Where(s => s.FirstName.ToLower().Contains(searchQuery)
                || s.MiddleNames.ToLower().Contains(searchQuery)
                || s.LastName.ToLower().Contains(searchQuery));
            }
            if (studentResourceParameters.IncludeHouse) collection = collection.Include(s => s.House);
            
            return PagedList<Student>.Create(collection,
                studentResourceParameters.PageNumber,
                studentResourceParameters.PageSize);
        }
        public async Task<IEnumerable<Student>> GetStudentsAsync(IEnumerable<int> studentIds)
        {
            return await _context.Students.Where(s => studentIds.Contains(s.Id))
                .OrderBy(s => s.FirstName)
                .OrderBy(s => s.LastName)
                .ToListAsync();
        }

        public async Task<Student> GetStudentByIdAsync(int studentId)
        {
            return await _context.Students.Include(s => s.House)
                .FirstOrDefaultAsync(s => s.Id == studentId);
        }
        public void AddStudent(Student student)
        {
            _context.Students.Add(student);
        }
        public void DeleteStudent(Student studentFromRepo)
        {
            _context.Students.Remove(studentFromRepo);
        }
        public void DeleteManyStudents(IEnumerable<Student> students)
        {
            _context.Students.RemoveRange(students);
        }
        public void UpdateStudent(Student student)
        {
            //no code needed
        }
        public async Task<bool> SaveAsync()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }
    }
}
