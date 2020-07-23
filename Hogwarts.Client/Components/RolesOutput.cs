using Hogwarts.Client.Services;
using Hogwarts.Data.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Hogwarts.Client.Components
{
    public partial class RolesOutput
    {
        [Inject] RolesDataService RolesDataService { get; set; }
        public List<RoleDto> Roles { get; set; } = new List<RoleDto>();
        [Parameter] public RoleDto Role { get; set; }
        [Parameter] public EventCallback<int> RemoveRole { get; set; }
        protected override async Task OnInitializedAsync()
        {
            Roles = await RolesDataService.GetAllRolesAsync();
        }
       
    }
}
