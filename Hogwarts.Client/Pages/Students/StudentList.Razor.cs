using Hogwarts.Client.Services;
using Hogwarts.Data.Models;
using Hogwarts.Data.ResourceParameters;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hogwarts.Client.Pages.Students
{
    public partial class StudentList
    {
        
        List<StudentDto> Students;
         [Inject] public StudentDataService StudentDataService { get; set; }
        public PaginationMetadata PaginationMetadata { get; set; } = new PaginationMetadata();
        private StudentsResourceParameters _studentParameters = new StudentsResourceParameters();
        [Inject] public HouseDataService HouseDataService { get; set; }
        private bool _firstIsChecked;
        public bool FirstIsChecked
        {
            get { return _firstIsChecked; }
            set
            {
                _firstIsChecked = value;
                ToggleAllChecked();
            }
        }
        List<HouseDto> Houses;
        
        protected async override Task OnInitializedAsync()
        {
            Houses = await HouseDataService.GetAllHousesAsync();
            await GetStudents();
        }
        private async Task SelectedPage(int page)
        {
            _studentParameters.PageNumber = page;
            await GetStudents();
        }
        private async Task GetStudents()
        {
            var response = await StudentDataService.GetAllStudentsAsync(_studentParameters);
            PaginationMetadata = response.MetaData;
            Students = response.Items;
        }
        public void ToggleAllChecked()
        {
            Students.ConvertAll(s => s.IsChecked = FirstIsChecked);
            StateHasChanged();
        }
        public async Task PreformSearch()
        {
            await GetStudents();
        }
    }
}
