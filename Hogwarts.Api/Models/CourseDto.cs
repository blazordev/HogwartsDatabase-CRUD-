using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hogwarts.Api.Models
{
    public class CourseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<int> TeacherIds { get; set; }

    }
}
