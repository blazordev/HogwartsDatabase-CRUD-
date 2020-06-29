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
        public bool isSortedAscending;
        public string activeSortColumn;
        [Inject] StaffDataService StaffDataService { get; set; }
        [Inject] RolesDataService RoleDataService { get; set; }
        [Inject] NavigationManager NavigationManager { get; set; }
        public IEnumerable<StaffDto> Staff { get; set; } = new List<StaffDto>();
        [Parameter] public string SearchTerm { get; set; } = "";
        public int SearchRole { get; set; }
        public IEnumerable<RoleDto> Roles { get; set; } = new List<RoleDto>();

        public List<StaffDto> FilteredStaff
        {
            get { return GetFilteredStaff().ToList(); }
            set { FilteredStaff = value; }
        }
        public IEnumerable<StaffDto> GetFilteredStaff()
        {
            Console.WriteLine("Filtered");
            return Staff.Where(s =>
            s.FirstName.ToLower().Contains(SearchTerm.ToLower())
            || s.MiddleNames.ToLower().Contains(SearchTerm.ToLower())
            || s.LastName.ToLower().Contains(SearchTerm.ToLower()));

            

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
        public void RoleSelected(ChangeEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("whatever");
            //if (int.TryParse((string)e.Value, out var index) && index >= 0)
            //{
            //    List<StaffDto> List = new List<StaffDto>();
            //    foreach (var staff in FilteredStaff)
            //    {
            //        foreach (var role in staff.Roles)
            //        {
            //            if (role.Id == index)
            //            {
            //                List.Add(staff);
            //                break;
            //            }
            //        }
            //    }
            //    FilteredStaff = List;
            //}

        }
    }
}

