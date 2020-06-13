using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hogwarts.Api.Models
{
    public class RoleForEditDto
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
