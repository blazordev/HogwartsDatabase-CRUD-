using Hogwarts.Data.Models;
using Hogwarts.Client.Services;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hogwarts.Client.Pages
{
    public partial class StaffDetails
    {
        public StaffDto Staff { get; set; } = new StaffDto();

        [Parameter]
        public string StaffId { get; set; }

        [Inject]
        public StaffDataService Service { get; set; }

        protected async override Task OnInitializedAsync()
        {
            Staff = await Service.GetStaffByIdAsync(int.Parse(StaffId));
        }
    }
}
