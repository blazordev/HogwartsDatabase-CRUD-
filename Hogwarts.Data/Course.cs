using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Hogwarts.Data
{
    public class Course
    {
        public int Id { get; set; }
        [Required] public string Name { get; set; }
        public IEnumerable<StaffCourse> StaffCourse { get; set; } = new List<StaffCourse>();
        
    }
}