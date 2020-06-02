using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Hogwarts.Data
{
    public class Student
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }
        public string MiddleNames { get; set; }
        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }
        public string ImageLink { get; set; }
        public House House { get; set; }
        [Required] public int HouseId { get; set; }

    }
}
