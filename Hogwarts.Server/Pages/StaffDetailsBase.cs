using Hogwarts.Data.Models;
using Hogwarts.Server.Services;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hogwarts.Server.Pages
{
    public class StaffDetailsBase : ComponentBase
    {
        public StaffDto Staff { get; set; } = new StaffDto();

        [Parameter]
        public string StaffId { get; set; }

        [Inject]
        public StaffDataService Service { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Staff = await Service.GetStaffByIdAsync(int.Parse(StaffId));
        }
    }
}
