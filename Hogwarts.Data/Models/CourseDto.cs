using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hogwarts.Data.Models
{
    public class CourseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortHandName { get; set; }
        public string Description { get; set; }
        public Level Level { get; set; }
        public IEnumerable<StaffDto> Professors { get; set; } = new List<StaffDto>();
    }
}
