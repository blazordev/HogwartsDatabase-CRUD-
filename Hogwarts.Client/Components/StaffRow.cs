using Hogwarts.Client.Services;
using Hogwarts.Data.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hogwarts.Client.Components
{
    public partial class StaffRow
    {
        [Parameter] public StaffDto Staff { get; set; }
        [Inject] HouseHeadDataService HouseHeadDataService { get; set; }
        public HouseDto House { get; set; }
        protected async override Task OnInitializedAsync()
        {
            House = await HouseHeadDataService.GetHouseForStaffAsync(Staff.Id); 
        }
    }
}
