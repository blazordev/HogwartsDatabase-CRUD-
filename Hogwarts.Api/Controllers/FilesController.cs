using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Hogwarts.Api.Helpers;
using Hogwarts.Api.Services;
using Hogwarts.Api.Services.Interfaces;
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
        private const string XlsxContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

        public FilesController(IWebHostEnvironment hostingEnvironment, IStudentRepository studentRepo)
        {
            _hostingEnvironment = hostingEnvironment;
            _studentRepo = studentRepo;
        }

        [HttpGet]
        public async Task<IActionResult> DownloadExcel()
        {
            byte[] reportBytes;
            var students = await _studentRepo.GetAllStudentsAsyncForFile();
            using (var package = StudentUtils.createExcelPackage(students))
            {
                reportBytes = package.GetAsByteArray();
            }

            return File(reportBytes, XlsxContentType, $"Hogwarts-Students{DateTime.Now.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)}.xlsx");
        }
    }
}
