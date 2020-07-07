using Hogwarts.Data.Models;
using Hogwarts.Client.Services;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hogwarts.Client.Pages
{
    public class StudentDetailsBase : ComponentBase
    {
        public StudentDto Student { get; set; } = new StudentDto();
        
        [Parameter]
        public string StudentId { get; set; }

        [Inject]
        public StudentDataService Service { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Student = await Service.GetStudentByIdAsync(int.Parse(StudentId));
        }
    }
}
