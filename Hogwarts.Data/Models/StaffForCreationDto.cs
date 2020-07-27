using Hogwarts.Data;
using Hogwarts.Data.ValidationAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hogwarts.Data.Models
{ 
    [HeadMustSelectHouse]
    public class StaffForCreationDto
    {
        [Required] public string FirstName { get; set; }
        public string MiddleNames { get; set; }
        [Required] public string LastName { get; set; }
        public List<RoleDto> Roles { get; set; } = new List<RoleDto>();
        public HouseDto House { get; set; } 
        public List<CourseDto> Courses { get; set; } = new List<CourseDto>();

    }
}
