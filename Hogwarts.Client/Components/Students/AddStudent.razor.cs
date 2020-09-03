using Hogwarts.Client.Services;
using Hogwarts.Data.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hogwarts.Client.Components.Students
{
    public partial class AddStudent
    {
        [Parameter] public EventCallback OnSubmit { get; set; }
        public StudentDto Student { get; set; } = new StudentDto();
        [Inject] HouseDataService HouseDataService { get; set; }
        List<HouseDto> Houses;
        private bool display = false;
        public void ShowModal() => display = true;
        public void HideModal() => display = false;
        public void Reset() => Student = new StudentDto();
        protected async override Task OnInitializedAsync()
        {
            Houses = await HouseDataService.GetAllHousesAsync();
        }
        public void SelectHouse(int houseId)
        {
            Student.HouseId = houseId;
        }
        public void Cancel()
        {
            HideModal();
            Reset();
        }

    }
}
