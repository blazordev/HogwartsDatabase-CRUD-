using Hogwarts.Data;
using Hogwarts.Data.Models;
using Hogwarts.Server.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;


namespace Hogwarts.Server.Pages
{
    public class StaffListBase : ComponentBase
    {
        [Inject] StaffDataService StaffDataService { get; set; }
        [Inject] RolesDataService RoleDataService { get; set; }
        [Inject] NavigationManager NavigationManager { get; set; }


        public IEnumerable<RoleDto> Roles { get; set; } = new List<RoleDto>();
        public IEnumerable<StaffDto> Staff { get; set; } = new List<StaffDto>();
        [Parameter] public string SearchTerm { get; set; } = "";

        private IEnumerable<StaffDto> _filteredStaff;
        public IEnumerable<StaffDto> FilteredStaff
        {
            get
            { return PreformFilter(); }
            set
            { _filteredStaff = value; }
        }

        public IEnumerable<StaffDto> PreformFilter()
        {
            IEnumerable<StaffDto> list;
            list = Staff.Where(s =>
            s.FirstName.ToLower().Contains(SearchTerm.ToLower())
            || s.MiddleNames.ToLower().Contains(SearchTerm.ToLower())
            || s.LastName.ToLower().Contains(SearchTerm.ToLower()));
            if (selectedRole != 0)
            {
                list = list.Where(s => s.Roles.Any(r => r.Id == selectedRole));
            }
            
            return list;
        }

        protected override async Task OnInitializedAsync()
        {
            Staff = await StaffDataService.GetAllStaffAsync();
            Roles = await RoleDataService.GetAllRolesAsync();
        }
        public void StaffDetailsPage(int staffId)
        {
            NavigationManager.NavigateTo($"staffDetails/{staffId}");
        }
        public int selectedRole { get; set; }
        public void RoleSelected(ChangeEventArgs e)
        {
            if (int.TryParse((string)e.Value, out var index))
            {
                selectedRole = index;
            }
        }
    }
}


