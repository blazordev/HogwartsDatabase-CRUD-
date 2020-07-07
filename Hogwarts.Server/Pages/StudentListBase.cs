using Hogwarts.Data.Models;
using Hogwarts.Client.Services;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hogwarts.Client.Pages
{
    public class StudentListBase : ComponentBase
    {
        [Inject] public StudentDataService StudentDataService { get; set; }
        public IEnumerable<StudentDto> Students { get; set; } = new List<StudentDto>();

        protected override async Task OnInitializedAsync()
        {
            Students = await StudentDataService.GetAllStudentsAsync();
        }
    }
}
