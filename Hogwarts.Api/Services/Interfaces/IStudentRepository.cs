using Hogwarts.Api.Helpers;
using Hogwarts.Api.ResourceParameters;
using Hogwarts.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hogwarts.Api.Services.Interfaces
{
    public interface IStudentRepository
    {
        public Task<bool> StudentExistAsync(int studentId);
        public PagedList<Student> GetStudents(
            StudentsResourceParameters studentResourceParameters);
        public Task<IEnumerable<Student>> GetStudentsAsync(IEnumerable<int> studentIds);
        public Task<Student> GetStudentByIdAsync(int studentId);
        public void AddStudent(Student student);
        public void DeleteStudent(Student studentFromRepo);
        public void UpdateStudent(Student student);
        public Task<bool> SaveAsync();
        
    }
}
