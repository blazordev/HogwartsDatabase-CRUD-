using Hogwarts.Data;
using Hogwarts.Data.Models;
using Hogwarts.Client.Services;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Hogwarts.Client.Components;

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
        public AddStaff AddStaff { get; set; }
        public Confirmation Confirmation { get; set; }
        public bool FirstIsChecked
        {
            get { return _firstIsChecked; }
            set
            {
                _firstIsChecked = value;
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
            {
                return PreformFilter();
            }
            set
            { _filteredStaff = value; }
        }
        IEnumerable<StaffDto> SelectedStaff;
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
        public void Add()
        {
            AllAreChecked = false;
            AddStaff.ShowModal();
            FirstIsChecked = false;
            FilteredStaff.ConvertAll(s => s.IsChecked = false);
            AllAreChecked = false;
        }
        public void DeleteSelected()
        {
            SelectedStaff = Staff.Where(s => s.IsChecked);
            if (SelectedStaff.Count() > 0)
            {
                Confirmation.Show();
            }
        }
        public async Task ConfirmDelete()
        {
            string staffToDelete = string.Join(",", SelectedStaff.Select(s => s.Id));
            await StaffDataService.DeleteStaffCollection(staffToDelete);
            Staff = await StaffDataService.GetAllStaffAsync();
            Confirmation.Hide();
            AllAreChecked = false;
            FirstIsChecked = false;
            SelectedStaff = null;
            StateHasChanged();            
        }
        public void Cancel()
        {
            Confirmation.Hide();
            SelectedStaff = null;
            

        }
        public async Task Submit()
        {
            await StaffDataService.AddStaff(AddStaff.Staff);
            Staff = await StaffDataService.GetAllStaffAsync();            
            AddStaff.HideModal();
            AddStaff.Reset();
            StateHasChanged();
            Console.WriteLine("Submitted");
        }
    }
}


