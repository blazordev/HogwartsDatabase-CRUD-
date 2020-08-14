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
using System.Threading;

namespace Hogwarts.Client.Pages
{
    public partial class StaffList
    {
        [Inject] StaffDataService StaffDataService { get; set; }
        [Inject] RolesDataService RoleDataService { get; set; }
        [Inject] NavigationManager NavigationManager { get; set; }
        [Inject] CourseDataService CourseDataService { get; set; }
        public bool EditMode { get; set; } = false;
        public int Index { get; set; }
        public IEnumerable<RoleDto> Roles { get; set; } = new List<RoleDto>();
        List<StaffDto> Staff;
        private bool _firstIsChecked;
        public bool IsOpenToEdit { get; set; } = false;
        public AddStaff AddStaff { get; set; }
        public bool EditWasClicked { get; set; } = false;
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
        IEnumerable<StaffDto> SelectedStaff = new List<StaffDto>();
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
            Staff = await StaffDataService.GetAllStaffAsync();
            Roles = await RoleDataService.GetAllRolesAsync();
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
            AddStaff.ShowModal();
            FirstIsChecked = false;
            FilteredStaff.ConvertAll(s => s.IsChecked = false);
            AllAreChecked = false;
        }
        public void EditSelected()
        {
            EditWasClicked = true;
            var staffToConvert = FilteredStaff.Where(s => s.IsChecked).ToList();
            staffToConvert.ConvertAll(s => s.EditModeIsOn = true);
            Console.WriteLine("EditSelected");
            StateHasChanged();
        }
        public void Reset()
        {
            FirstIsChecked = false;
            FilteredStaff.ConvertAll(s => s.IsChecked = false);
            FilteredStaff.ConvertAll(s => s.EditModeIsOn = false);
            AllAreChecked = false;
        }
        public async Task CancelSelected()
        {
            Reset();
            if (FilteredStaff.Any(fs => fs.EditModeIsOn))
            {                
                Staff = await StaffDataService.GetAllStaffAsync();
                StateHasChanged();
            } 
        }
        public void DeleteSelected()
        {
            SelectedStaff = Staff.Where(s => s.IsChecked);
            if (SelectedStaff.Count() > 0)
            {
                Confirmation.Show();
            }
        }
        public async Task SaveSelected()
        {
            SelectedStaff = Staff.Where(s => s.IsChecked);
            await StaffDataService.UpdateStaffCollection(SelectedStaff);
            FirstIsChecked = false;
            FilteredStaff.ConvertAll(s => s.IsChecked = false); //just for visual purposes
            Staff = await StaffDataService.GetAllStaffAsync();
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
        public async Task Cancel()
        {
            Confirmation.Hide();
            SelectedStaff = null;
            FirstIsChecked = false;
            FilteredStaff.ConvertAll(s => s.IsChecked = false);
            AllAreChecked = false;
            Staff = await StaffDataService.GetAllStaffAsync();
            StateHasChanged();
        }
        public async Task SubmitAdd()
        {
            await StaffDataService.AddStaff(AddStaff.Staff);
            Staff = await StaffDataService.GetAllStaffAsync();
            AddStaff.HideModal();
            AddStaff.Reset();
            StateHasChanged();
            Console.WriteLine("Added");
        }
        public async Task ExitEditMode(StaffDto staff)
        {
            staff.IsChecked = false;
            staff.EditModeIsOn = false;
            Staff = await StaffDataService.GetAllStaffAsync();
            StateHasChanged();
        }

    }
}


