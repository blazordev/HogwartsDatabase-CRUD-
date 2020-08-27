using Hogwarts.Client.Services;
using Hogwarts.Data.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hogwarts.Client.Components.Staff
{
    public partial class HeadOfHouseSelect
    {
        public List<HouseDto> Houses { get; set; } = new List<HouseDto>();
        [Parameter] public EventCallback<HouseDto> AddHouse { get; set; }
        [Inject] HouseDataService HouseDataService { get; set; }

        protected async override Task OnInitializedAsync()
        {
            Houses = await HouseDataService.GetAllHousesAsync();
        }
        public void SelectHouse(ChangeEventArgs e)
        {
            if (int.TryParse((string)e.Value, out var index))
            {
                AddHouse.InvokeAsync(Houses[index]);
            }
        }
    }
}
