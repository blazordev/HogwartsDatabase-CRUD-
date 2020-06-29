using Hogwarts.Data;
using System.Collections.Generic;

namespace Hogwarts.Data.Models
{
    public class StaffDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleNames { get; set; }
        public string LastName { get; set; }
        public IEnumerable<RoleDto> Roles { get; set; } = new List<RoleDto>();


    }
}