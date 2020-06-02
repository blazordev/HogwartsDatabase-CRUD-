using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Hogwarts.Data
{
    public class Teacher
    {
        public int Id { get; set; }
        public Staff Staff { get; set; }
        [Required] public int StaffId { get; set; }
        public IEnumerable<TeacherCourse> TeachersCourses { get; set; } = new List<TeacherCourse>();
    }
}