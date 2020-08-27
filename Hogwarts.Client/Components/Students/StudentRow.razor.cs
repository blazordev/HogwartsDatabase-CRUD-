using Hogwarts.Data.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hogwarts.Client.Components.Students
{
    public partial class StudentRow
    {
        [Parameter] public StudentDto Student { get; set; }
        public string Highlighted =>
            Student.IsChecked ? "background-color:#ffede3 !important;" : ""; 
    }
}
