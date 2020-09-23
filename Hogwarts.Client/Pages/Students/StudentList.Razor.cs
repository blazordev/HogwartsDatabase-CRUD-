using Hogwarts.Client.Components;
using Hogwarts.Client.Components.Students;
using Hogwarts.Client.Services;
using Hogwarts.Client.Services.ToastService;
using Hogwarts.Data.Models;
using Hogwarts.Data.ResourceParameters;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.IO.Enumeration;
using System.Linq;
using System.Threading.Tasks;

namespace Hogwarts.Client.Pages.Students
{
    public partial class StudentList
    {
        List<StudentDto> Students;
        [Inject] public StudentDataService StudentDataService { get; set; }
        [Inject] public IJSRuntime jSRuntime { get; set; }
        public PaginationMetadata PaginationMetadata { get; set; } = new PaginationMetadata();
        private StudentsResourceParameters _studentParameters = new StudentsResourceParameters();
        [Inject] public HouseDataService HouseDataService { get; set; }
        [Inject] public ToastService ToastService { get; set; }
        private bool _firstIsChecked;
        public AddStudent AddStudent { get; set; }
        IEnumerable<StudentDto> SelectedStudents = new List<StudentDto>();
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

        public Confirmation Confirmation { get; set; }
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
            StateHasChanged();
        }
        public void ToggleAllChecked()
        {
            Students.ConvertAll(s => s.IsChecked = FirstIsChecked);
            StateHasChanged();
        }
        public async Task PreformSearch()
        {
            _studentParameters.PageNumber = 1;
            await GetStudents();
        }
        public void OnHouseSelected(int houseId)
        {
            _studentParameters.HouseId = houseId;
        }

        public void Add()
        {
            AddStudent.ShowModal();
            FirstIsChecked = false;
            Students.ConvertAll(s => s.IsChecked = false);
        }
        public void EditSelected()
        {
            var studentsToConvert = Students.Where(s => s.IsChecked).ToList();
            studentsToConvert.ConvertAll(s => s.EditModeIsOn = true);
            StateHasChanged();
        }
        public void Reset()
        {
            FirstIsChecked = false;
            Students.ConvertAll(s => s.IsChecked = false);
            Students.ConvertAll(s => s.EditModeIsOn = false);
        }

        public async Task CancelSelected()
        {
            if (Students.Any(fs => fs.EditModeIsOn)
            || _studentParameters.HouseId != 0
            || _studentParameters.SearchQuery != "")
            {

                _studentParameters.SearchQuery = "";
                _studentParameters.HouseId = 0;
                await GetStudents();
                StateHasChanged();
            }
            Reset();
        }
        public void DeleteSelected()
        {
            SelectedStudents = Students.Where(s => s.IsChecked);
            if (SelectedStudents.Count() > 0)
            {
                Confirmation.Show();
            }
        }
        public async Task SaveSelected()
        {
            //if any selected
            if (Students.Any(s => s.IsChecked))
            {
                FirstIsChecked = false;
                Students.ConvertAll(s => s.IsChecked = false);
            }
            //if any are potentially edited
            var studentsInEditMode = Students.Where(s => s.EditModeIsOn);
            if (studentsInEditMode.Count() > 0)
            {
                var message = await StudentDataService.UpdateStudentCollection(studentsInEditMode);
                await GetStudents();
                StateHasChanged();
                ToastService.ShowToast(message, ToastLevel.Success);
            }
        }
        public async Task ConfirmDelete()
        {
            string studentsToDelete = string.Join(",", SelectedStudents.Select(s => s.Id));
            var message = await StudentDataService.DeleteStudentCollection(studentsToDelete);
            await GetStudents();
            ToastService.ShowToast(message, ToastLevel.Success);
            Confirmation.Hide();
            FirstIsChecked = false;
            SelectedStudents = null;
            StateHasChanged();            
        }
        public async Task Cancel()
        {
            Confirmation.Hide();
            SelectedStudents = null;
            FirstIsChecked = false;
            Students.ConvertAll(s => s.IsChecked = false);
            //reset any unsaved changes in memory
            await GetStudents();
        }
        public async Task SubmitAdd()
        {
            var message = await StudentDataService.AddStudentAsync(AddStudent.Student);
            await GetStudents();
            AddStudent.HideModal();
            AddStudent.Reset();
            ToastService.ShowToast(message, ToastLevel.Success);
        }
        public int IsDownloadStarted { get; set; } = 0;

        protected async Task DownloadFile()
        {
            if (await jSRuntime.InvokeAsync<bool>("confirm", $"Do you want to Export?"))
            {
                IsDownloadStarted = 1;
                var fileBytes = await StudentDataService.Download();
                var fileName = $"Students.xlsx";
                await jSRuntime.InvokeAsync<object>("saveAsFile", fileName, Convert.ToBase64String(fileBytes));
                IsDownloadStarted = 2;
            }
        }
    }

}




