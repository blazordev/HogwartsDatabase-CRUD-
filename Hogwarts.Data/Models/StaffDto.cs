using Hogwarts.Data;
using Hogwarts.Data.ValidationAttributes;
using System.Collections.Generic;

namespace Hogwarts.Data.Models
{
    [HeadMustSelectHouse]
    public class StaffDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleNames { get; set; } = "";
        public string LastName { get; set; }       
        public bool IsChecked { get; set; } = false;
        public bool ShowRoles { get; set; } = false;
        public bool EditModeIsOn { get; set; } = false;
        public bool ShowCourses { get; set; } = false;
        public List<RoleDto> Roles { get; set; } = new List<RoleDto>();
        public HouseDto House { get; set; }
        public List<CourseDto> Courses { get; set; } = new List<CourseDto>();


    }
}