using Hogwarts.Client.Services;
using Hogwarts.Data.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hogwarts.Client.Components.Students
{
    public partial class SelectHouse
    {
        List<HouseDto> Houses = new List<HouseDto>();
        [Parameter] public EventCallback<int> OnSelect { get; set; }
        [Inject] public HouseDataService HouseDataService { get; set; }
        protected async override Task OnInitializedAsync()
        {
            Houses = await HouseDataService.GetAllHousesAsync();
        }
        [Parameter] public int Selection { get; set; }
        public void HouseSelected(ChangeEventArgs e)
        {
            if (int.TryParse((string)e.Value, out var houseId))
            {
                OnSelect.InvokeAsync(houseId);
            }
        }

    }
}
