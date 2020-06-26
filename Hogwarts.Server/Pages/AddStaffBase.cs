using Hogwarts.Server.Models;
using Hogwarts.Server.Services;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hogwarts.Server.Pages
{
    public class AddStaffBase: ComponentBase
    {
        [Parameter] public StaffForCreationDto Staff { get; set; } = new StaffForCreationDto();
        public List<RoleDto> Roles { get; set; } = new List<RoleDto>();
        [Inject] RolesDataService RolesDataService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Roles = await RolesDataService.GetAllRolesAsync();
        }
        public void HandleValidSubmit()
        {

        }
    }
}
