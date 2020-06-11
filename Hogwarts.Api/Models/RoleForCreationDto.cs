using Hogwarts.Api.Models;
using Hogwarts.Api.ValidationAttributes;
using Hogwarts.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hogwarts.Api.Models
{
    [NameMustBeDifferentFromDescriptionAttribute]
    public class RoleForCreationDto
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }        
    }
}
