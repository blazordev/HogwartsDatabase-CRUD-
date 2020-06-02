using Hogwarts.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hogwarts.Api.Models
{
    public class TeacherForCreationDto
    {
        [Required] public int StaffId { get; set; }
        public IEnumerable<Course> Courses { get; set; } = new List<Course>();
    }
}
