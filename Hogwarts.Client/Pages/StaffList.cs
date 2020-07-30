using Hogwarts.Data;
using Hogwarts.Data.Models;
using Hogwarts.Client.Services;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;


namespace Hogwarts.Client.Pages
{
    public partial class StaffList
    {
        [Inject] StaffDataService StaffDataService { get; set; }
        [Inject] RolesDataService RoleDataService { get; set; }
        [Inject] NavigationManager NavigationManager { get; set; }
        public int Index { get; set; }
        public List<StaffDto> StaffToManipulate { get; set; } = new List<StaffDto>();
        public IEnumerable<RoleDto> Roles { get; set; } = new List<RoleDto>();
        public List<StaffDto> Staff { get; set; }
        private bool _firstIsChecked;
        public bool FirstIsChecked 
        { 
            get { return _firstIsChecked; }
            set { _firstIsChecked = value;
                ToggleAllChecked();
            }
        } 
        public bool AllAreChecked { get; set; } = false;

        public bool Show { get; set; }
       
        [Parameter] public string SearchTerm { get; set; } = "";
         
        private IEnumerable<StaffDto> _filteredStaff;
        public List<StaffDto> FilteredStaff
        {
            get
            { return PreformFilter(); }
            set
            { _filteredStaff = value; }
        }

        public List<StaffDto> PreformFilter()
        {            
            IEnumerable<StaffDto> staffList = new List<StaffDto>();
            staffList = Staff.Where(s =>
            s.FirstName.ToLower().Contains(SearchTerm.ToLower())
            || s.MiddleNames != null && s.MiddleNames.ToLower().Contains(SearchTerm.ToLower())
            || s.LastName.ToLower().Contains(SearchTerm.ToLower()));

            //if a role is selected, filter the list further
            staffList = selectedRole != 0 ? staffList.Where(s => s.Roles.Any(r => r.Id == selectedRole)) : staffList;
            return staffList.ToList();
        }
        protected async override Task OnInitializedAsync()
        {
            
            Staff = new List<StaffDto>();
            Staff = await StaffDataService.GetAllStaffAsync();
            Roles = await RoleDataService.GetAllRolesAsync();
        }
        public void StaffDetailsPage(int staffId)
        {
            NavigationManager.NavigateTo($"staffDetails/{staffId}");
            Console.WriteLine($"Staff Id: {staffId} clicked");
        }
        public int selectedRole { get; set; }
        public void RoleSelected(ChangeEventArgs e)
        {
            if (int.TryParse((string)e.Value, out var index))
            {
                selectedRole = index;
            }
        }
        public void ToggleAllChecked()
        {          
                FilteredStaff.ConvertAll(s => s.IsChecked = FirstIsChecked);
                AllAreChecked = !AllAreChecked;
                StateHasChanged();
        }
        public void AddPage()
        {
            NavigationManager.NavigateTo("addStaff");
        }
        public async Task DeleteSelected()
        {
            var selectedStaff = FilteredStaff.Where(s => s.IsChecked);
            string staffToDelete = string.Join(",", selectedStaff.Select(s => s.Id));
            await StaffDataService.DeleteStaffCollection(staffToDelete);
            //remove local representations of deleted item
            FilteredStaff = FilteredStaff.Except(selectedStaff).ToList();
            FilteredStaff = await StaffDataService.GetAllStaffAsync();
            StateHasChanged();

        }


    }
}


