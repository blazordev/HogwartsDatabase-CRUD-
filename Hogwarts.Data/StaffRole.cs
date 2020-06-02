using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Hogwarts.Data
{
    public class StaffRole
    {
        [Required] public int StaffId { get; set; }
        public Staff Staff { get; set; }
        [Required] public int RoleId { get; set; }
        public Role Role { get; set; }
    }
}
