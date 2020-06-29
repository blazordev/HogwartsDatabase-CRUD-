using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hogwarts.Data.Models
{
    public class CourseForCreationDto
    {
        [Required]
        public string Name { get; set; }
    }
}
