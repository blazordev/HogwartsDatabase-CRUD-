﻿using Hogwarts.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hogwarts.Data.Models
{
    public class StaffForCreationDto
    {
        [Required] public string FirstName { get; set; }
        public string MiddleNames { get; set; }
        [Required] public string LastName { get; set; }
        public List<int> RoleIds { get; set; } = new List<int>();
        public int HouseId { get; set; }
        public List<int> CourseIds { get; set; } = new List<int>();

    }
}
