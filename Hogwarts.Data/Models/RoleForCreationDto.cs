using Hogwarts.Data.Models;
using Hogwarts.Data.ValidationAttributes;
using Hogwarts.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hogwarts.Data.Models
{
    [NameMustBeDifferentFromDescriptionAttribute]
    public class RoleForCreationDto
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }        
    }
}
