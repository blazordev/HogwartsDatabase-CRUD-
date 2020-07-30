using Hogwarts.Client.Services;
using Hogwarts.Data.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hogwarts.Client.Components
{
    public partial class SelectRoles
    {
        public List<RoleDto> Roles { get; set; } = new List<RoleDto>();
        [Parameter] public EventCallback<RoleDto> AddRole { get; set; }
        [Inject] public RolesDataService RolesDataService { get; set; }

        protected async override Task OnInitializedAsync()
        {
            Roles = await RolesDataService.GetAllRolesAsync();
        }
    }
}
