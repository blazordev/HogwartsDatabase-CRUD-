using Hogwarts.Client.Services;
using Hogwarts.Data.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Hogwarts.Client.Components
{
    public partial class CoursesOutput
    {
        [Parameter] public StaffForCreationDto Staff { get; set; }
        [Parameter] public bool IncludeDescription { get; set; } = false;
       
        public void RemoveCourse(int id)
        {
            Staff.Courses.RemoveAll(c => c.Id == id);
        }
    }
}
