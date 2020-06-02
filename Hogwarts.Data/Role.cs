using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Hogwarts.Data
{
    public class Role
    {
        public int Id { get; set; }
        [Required] public string Name { get; set; }                
        public string Description { get; set; }
        public IEnumerable<StaffRole> StaffRoles { get; set; } = new List<StaffRole>();
    }
}
