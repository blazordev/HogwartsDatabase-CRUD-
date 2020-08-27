using Hogwarts.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hogwarts.Data.Models
{
    public class StudentDto
    {
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        public string MiddleNames { get; set; }
        [Required]
        public string LastName { get; set; }
        public string ImageLink { get; set; }
        public bool IsChecked { get; set; } = false;
        public bool EditModeIsOn { get; set; } = false;
        public HouseDto House { get; set; }
        [Required] public int HouseId { get; set; }






    }
}
