using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hogwarts.Data.Models
{
    public class StudentForCreationDto
    {
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }
        public string MiddleNames { get; set; }
        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }
        [Required] public int HouseId { get; set; }
    }
}
