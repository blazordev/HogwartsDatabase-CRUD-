using Hogwarts.Server.Models;
using Hogwarts.Server.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Hogwarts.Server.Pages
{
    public class StaffListBase : ComponentBase
    {
        [Inject] StaffDataService StaffDataService { get; set; }
        [Inject] RolesDataService RoleDataService { get; set; }
        [Inject] NavigationManager NavigationManager { get; set; }
        public IEnumerable<StaffDto> Staff { get; set; } = new List<StaffDto>();
        [Parameter] public string SearchTerm { get; set; } = "";
        [Parameter] public RoleDto RoleSelected { get; set; }
        public IEnumerable<RoleDto> Roles { get; set; } = new List<RoleDto>();
        public List<StaffDto> FilteredStaff => Staff.Where(s => 
        s.FirstName.ToLower().Contains(SearchTerm.ToLower())
        || s.MiddleNames.ToLower().Contains(SearchTerm.ToLower()) 
        || s.LastName.ToLower().Contains(SearchTerm.ToLower())).ToList();

        protected override async Task OnInitializedAsync()
        {
            Staff = await StaffDataService.GetAllStaffAsync();
            Roles = await RoleDataService.GetAllRolesAsync();
        }
        
        public void StaffDetailsPage(int staffId)
        {
            NavigationManager.NavigateTo($"staffDetails/{staffId}");
        }
        
    }

}

