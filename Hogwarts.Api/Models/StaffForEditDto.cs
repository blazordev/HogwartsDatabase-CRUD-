using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hogwarts.Api.Models
{
    public class StaffForEditDto
    {
        [Required]
        public string FirstName { get; set; }
        public string MiddleNames { get; set; } = "";
        [Required]
        public string LastName { get; set; }
    }
}
