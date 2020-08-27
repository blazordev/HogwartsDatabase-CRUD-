using Hogwarts.Client.Services;
using Hogwarts.Data.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hogwarts.Client.Components.Students
{
    public partial class StudentRowEditMode
    {
        [Parameter] public StudentDto Student { get; set; }
        public string backgroundColor = "#ffede3";
        [Inject] public HouseDataService HouseDataService { get; set; }
        List<HouseDto> Houses = new List<HouseDto>();
        protected async override Task OnInitializedAsync()
        {
            Houses = await HouseDataService.GetAllHousesAsync();
        }
    }
}
