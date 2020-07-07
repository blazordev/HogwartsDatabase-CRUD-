using Hogwarts.Data.Models;
using Hogwarts.Client.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hogwarts.Client.Pages
{
    public class AddStudentBase : ComponentBase
    {
        [Inject] HouseDataService HouseService { get; set; }
        [Inject] StudentDataService StudentService { get; set; }
        [Inject] NavigationManager NavigationManager { get; set; }
        public StudentForCreationDto Student { get; set; } = new StudentForCreationDto();
        public IEnumerable<HouseDto> Houses { get; set; } = new List<HouseDto>();
        public string HouseId { get; set; }
        protected override async Task OnInitializedAsync()
        {
            Houses = await HouseService.GetAllHousesAsync();
        }
        public async Task HandleValidSubmit()
        {
            Student.HouseId = int.Parse(HouseId);
            await StudentService.AddStudentAsync(Student);
            NavigationManager.NavigateTo("/studentList");
        }
        public void NavigateToStudentList()
        {
            NavigationManager.NavigateTo("/studentList");
        }
    }
}
