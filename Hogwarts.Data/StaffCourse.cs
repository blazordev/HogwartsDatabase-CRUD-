using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Hogwarts.Data
{
    public class StaffCourse
    {
        [Required] public int StaffId { get; set; }
        public Staff Staff { get; set; }
        [Required] public int CourseId { get; set; }
        public Course Course { get; set; }
    }
}
