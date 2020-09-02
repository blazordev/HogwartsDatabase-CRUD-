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
using Hogwarts.Client.Components.Staff;
using Microsoft.JSInterop;
using Hogwarts.Client.Services.ToastService;

namespace Hogwarts.Client.Pages.Staff
{
    public partial class StaffList
    {
        [Inject] StaffDataService StaffDataService { get; set; }
        [Inject] RolesDataService RoleDataService { get; set; }
        [Inject] IJSRuntime jSRuntime { get; set; }
        [Inject] ToastService toastService { get; set; }
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
            staffList = SelectedRole != 0 ? staffList.Where(s => s.Roles.Any(r => r.Id == SelectedRole)) : staffList;
            return staffList.ToList();
        }
        protected async override Task OnInitializedAsync()
        {
            Staff = await StaffDataService.GetAllStaffAsync();
            Roles = await RoleDataService.GetAllRolesAsync();
        }
        
        public int SelectedRole { get; set; }
        
        public void ToggleAllChecked()
        {
            FilteredStaff.ConvertAll(s => s.IsChecked = FirstIsChecked);
            StateHasChanged();
        }
        public void Add()
        {
            AddStaff.ShowModal();
            FirstIsChecked = false;
            FilteredStaff.ConvertAll(s => s.IsChecked = false);

        }
        public void EditSelected()
        {
            EditWasClicked = true;
            var staffToConvert = FilteredStaff.Where(s => s.IsChecked).ToList();
            staffToConvert.ConvertAll(s => s.EditModeIsOn = true);
            StateHasChanged();
        }
        public void Reset()
        {
            FirstIsChecked = false;
            FilteredStaff.ConvertAll(s => s.IsChecked = false);
            FilteredStaff.ConvertAll(s => s.EditModeIsOn = false);
            SelectedRole = 0;
            SearchTerm = "";

        }

        public async Task CancelSelected()
        {
            if (FilteredStaff.Any(fs => fs.EditModeIsOn))
            {
                Staff = await StaffDataService.GetAllStaffAsync();
                StateHasChanged();
            }
            Reset();
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
            //if any selected
            if (Staff.Any(s => s.IsChecked))
            {
                FirstIsChecked = false;
                FilteredStaff.ConvertAll(s => s.IsChecked = false);
            }
            //if any are potentially edited
            var staffInEditMode = Staff.Where(s => s.EditModeIsOn);
            if (staffInEditMode.Count() > 0)
            {
                var message = await StaffDataService.UpdateStaffCollection(staffInEditMode);
                Staff = await StaffDataService.GetAllStaffAsync();
                toastService.ShowToast(message, ToastLevel.Success);
                StateHasChanged();
            }
        }
        public async Task ConfirmDelete()
        {
            string staffToDelete = string.Join(",", SelectedStaff.Select(s => s.Id));
            var message = await StaffDataService.DeleteStaffCollection(staffToDelete);
            Staff = await StaffDataService.GetAllStaffAsync();
            Confirmation.Hide();
            FirstIsChecked = false;
            SelectedStaff = null;
            StateHasChanged();
            toastService.ShowToast(message, ToastLevel.Success);
        }
        public async Task Cancel()
        {
            Confirmation.Hide();
            SelectedStaff = null;
            FirstIsChecked = false;
            FilteredStaff.ConvertAll(s => s.IsChecked = false);
            //reset any unsaved changes in memory
            Staff = await StaffDataService.GetAllStaffAsync();
            StateHasChanged();
        }
        public async Task SubmitAdd()
        {
            var message = await StaffDataService.AddStaff(AddStaff.Staff);
            Staff = await StaffDataService.GetAllStaffAsync();
            AddStaff.HideModal();
            AddStaff.Reset();
            toastService.ShowToast(message, ToastLevel.Success);
            StateHasChanged();            
        }
        public int IsDownloadStarted { get; set; } = 0;

        protected async Task DownloadFile()
        {
            if (await jSRuntime.InvokeAsync<bool>("confirm", $"Do you want to Export?"))
            {
                IsDownloadStarted = 1;
                var fileBytes = await StaffDataService.Download();
                var fileName = $"Faculty.xlsx";
                await jSRuntime.InvokeAsync<object>("saveAsFile", fileName, Convert.ToBase64String(fileBytes));
                IsDownloadStarted = 2;
            }
        }



    }
}


