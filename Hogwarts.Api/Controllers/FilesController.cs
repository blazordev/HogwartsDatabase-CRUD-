using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Hogwarts.Api.Helpers;
using Hogwarts.Api.Services;
using Hogwarts.Api.Services.Interfaces;
using Hogwarts.Data.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlazorDownload.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IStudentRepository _studentRepo;
        private readonly IStaffRepository _staffRepo;
        private readonly IMapper _mapper;
        private const string XlsxContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

        public FilesController(IStaffRepository staffRepo, IMapper mapper, IWebHostEnvironment hostingEnvironment, IStudentRepository studentRepo)
        {
            _hostingEnvironment = hostingEnvironment;
            _studentRepo = studentRepo;
            _staffRepo = staffRepo;
            _mapper = mapper;
        }

        [HttpGet("students")]
        public async Task<IActionResult> DownloadStudentsExcel()
        {
            byte[] reportBytes;
            var students = await _studentRepo.GetAllStudentsForFileAsync();
            
            using (var package = Utils.createStudentExcelPackage(_mapper.Map<IEnumerable<StudentDto>>(students).ToList()))
            {
                reportBytes = package.GetAsByteArray();
            }

            return File(reportBytes, XlsxContentType, $"Hogwarts-Students{DateTime.Now.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)}.xlsx");
        }

        [HttpGet("staff")]
        public async Task<IActionResult> DownloadStaffExcel()
        {
            byte[] reportBytes;
            var staff = await _staffRepo.GetAllStaffForFileAsync();

            using (var package = Utils.createStaffExcelPackage(_mapper.Map<IEnumerable<StaffDto>>(staff).ToList()))
            {
                reportBytes = package.GetAsByteArray();
            }

            return File(reportBytes, XlsxContentType, $"Hogwarts-Students{DateTime.Now.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)}.xlsx");
        }
    }
}
