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
    public class StaffListBase : ComponentBase
    {
        [Inject] StaffDataService StaffDataService { get; set; }
        [Inject] RolesDataService RoleDataService { get; set; }
        [Inject] NavigationManager NavigationManager { get; set; }
        public List<StaffDto> StaffToManipulate { get; set; } = new List<StaffDto>();
        public string RowBackgroundColor { get; set; }
        public IEnumerable<RoleDto> Roles { get; set; } = new List<RoleDto>();
        public IEnumerable<StaffDto> Staff { get; set; } = new List<StaffDto>();

        public bool Show { get; set; }
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
            //below code gives null reference exceptionon on middle name which is not required
            //IEnumerable<StaffDto> list = new List<StaffDto>();
            //list = Staff.Where(s =>
            //s.FirstName.ToLower().Contains(SearchTerm.ToLower())
            //|| s.MiddleNames.ToLower().Contains(SearchTerm.ToLower())
            //|| s.LastName.ToLower().Contains(SearchTerm.ToLower()));
            //list = list.Where(s => s.Roles.Any(r => r.Id == selectedRole));
            //return list;
            //Instead, doing it manually
            List<StaffDto> list = new List<StaffDto>();
            foreach (var staffItem in Staff)
            {
                if (staffItem.FirstName != null)
                {
                    if (staffItem.FirstName.ToLower().Contains(SearchTerm.ToLower()))
                    {
                        list.Add(staffItem);
                    }
                }
                if (staffItem.MiddleNames != null)
                {
                    if (staffItem.MiddleNames.ToLower().Contains(SearchTerm.ToLower()))
                    {
                        if (!list.Contains(staffItem))
                        {
                            list.Add(staffItem);
                        }
                    }
                }
                if (staffItem.LastName != null)
                {
                    if (staffItem.LastName.ToLower().Contains(SearchTerm.ToLower()))
                    {
                        if (!list.Contains(staffItem))
                        {
                            list.Add(staffItem);
                        }
                    }
                }
            }
            List<StaffDto> listWithRole = new List<StaffDto>();
            if (selectedRole != 0)
            {
                foreach (var item in list)
                {
                    foreach (var role in item.Roles)
                    {
                        if (role.Id == selectedRole)
                        {
                            listWithRole.Add(item);
                        }
                    }
                }
                return listWithRole;
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


