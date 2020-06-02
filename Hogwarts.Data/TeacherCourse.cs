using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Hogwarts.Data
{
    public class TeacherCourse
    {
        [Required] public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }
        [Required] public int CourseId { get; set; }
        public Course Course { get; set; }
    }
}
