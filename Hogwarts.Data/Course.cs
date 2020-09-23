using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net.Security;

namespace Hogwarts.Data
{
    public class Course
    {
        public int Id { get; set; }
        [Required] public string Name { get; set; }
        public string ShortHandName { get; set; }
        public string Description { get; set; }
        public Level Level { get; set; }
        public IEnumerable<StaffCourse> StaffCourse { get; set; } = new List<StaffCourse>();
        
    }
    public enum Level
    {
        Core,
        Elective,
        ExtraCurricular
    }
}