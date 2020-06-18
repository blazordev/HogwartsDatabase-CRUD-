using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hogwarts.Server.Models
{
    public class CourseForEditDto
    {
        [Required] public string Name { get; set; }
    }
}
