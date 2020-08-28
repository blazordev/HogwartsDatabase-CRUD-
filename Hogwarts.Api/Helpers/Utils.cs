using Hogwarts.Api.Services;
using Hogwarts.Data;
using Hogwarts.Data.Models;
using OfficeOpenXml;
using OfficeOpenXml.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hogwarts.Api.Helpers
{
    public class Utils
    {
        public static ExcelPackage createStudentExcelPackage(List<StudentDto> students)
        {
            var package = new ExcelPackage();
            package.Workbook.Properties.Title = "Students";
            package.Workbook.Properties.Author = "generated from blazor app";
            package.Workbook.Properties.Subject = "Hogwarts Student List";
            var worksheet = package.Workbook.Worksheets.Add("Students");
            //First add the headers
            worksheet.Cells[1, 1].Value = "ID";
            worksheet.Cells[1, 2].Value = "First Name";
            worksheet.Cells[1, 3].Value = "Middle Name";
            worksheet.Cells[1, 4].Value = "Last Name";
            worksheet.Cells[1, 5].Value = "House";
            //Add values
            var numberformat = "#,##0";
            var dataCellStyleName = "TableNumber";
            var numStyle = package.Workbook.Styles.CreateNamedStyle(dataCellStyleName);
            numStyle.Style.Numberformat.Format = numberformat;

            for (int i = 0; i < students.Count(); i++)
            {
                var j = i + 2;
                worksheet.Cells[j, 1].Value = students[i].Id;
                worksheet.Cells[j, 2].Value = students[i].FirstName;
                worksheet.Cells[j, 3].Value = students[i].MiddleNames;
                worksheet.Cells[j, 4].Value = students[i].LastName;
                worksheet.Cells[j, 5].Value = students[i].House.Name;
            }


            //worksheet.Cells[4, 4].Value = 45000;
            //worksheet.Cells[4, 4].Style.Numberformat.Format = numberformat;
            // Add to table / Add summary row
            //var tbl = worksheet.Tables.Add(new ExcelAddressBase(fromRow: 1, fromCol: 1, toRow: 4, toColumn: 4), "Data");
            //tbl.ShowHeader = true;
            //tbl.TableStyle = TableStyles.Dark9;
            //tbl.ShowTotal = true;
            //tbl.Columns[3].DataCellStyleName = dataCellStyleName;
            //tbl.Columns[3].TotalsRowFunction = RowFunctions.Sum;
            //worksheet.Cells[5, 4].Style.Numberformat.Format = numberformat;
            // AutoFitColumns
            worksheet.Cells[1, 1, 4, 4].AutoFitColumns();
            return package;
        }
        public static ExcelPackage createStaffExcelPackage(List<StaffDto> staff)
        {
            var package = new ExcelPackage();
            package.Workbook.Properties.Title = "Faculty";
            package.Workbook.Properties.Author = "generated from blazor app";
            package.Workbook.Properties.Subject = "Hogwarts Faculty List";
            var worksheet = package.Workbook.Worksheets.Add("Faculty");
            //First add the headers
            worksheet.Cells[1, 1].Value = "ID";
            worksheet.Cells[1, 2].Value = "First Name";
            worksheet.Cells[1, 3].Value = "Middle Name";
            worksheet.Cells[1, 4].Value = "Last Name";
            worksheet.Cells[1, 5].Value = "Role Count";
            //Add values
            var numberformat = "#,##0";
            var dataCellStyleName = "TableNumber";
            var numStyle = package.Workbook.Styles.CreateNamedStyle(dataCellStyleName);
            numStyle.Style.Numberformat.Format = numberformat;

            for (int i = 0; i < staff.Count(); i++)
            {
                var j = i + 2;
                worksheet.Cells[j, 1].Value = staff[i].Id;
                worksheet.Cells[j, 2].Value = staff[i].FirstName;
                worksheet.Cells[j, 3].Value = staff[i].MiddleNames;
                worksheet.Cells[j, 4].Value = staff[i].LastName;
                worksheet.Cells[j, 5].Value = staff[i].Roles.Count();
            }


            //worksheet.Cells[4, 4].Value = 45000;
            //worksheet.Cells[4, 4].Style.Numberformat.Format = numberformat;
            // Add to table / Add summary row
            //var tbl = worksheet.Tables.Add(new ExcelAddressBase(fromRow: 1, fromCol: 1, toRow: 4, toColumn: 4), "Data");
            //tbl.ShowHeader = true;
            //tbl.TableStyle = TableStyles.Dark9;
            //tbl.ShowTotal = true;
            //tbl.Columns[3].DataCellStyleName = dataCellStyleName;
            //tbl.Columns[3].TotalsRowFunction = RowFunctions.Sum;
            //worksheet.Cells[5, 4].Style.Numberformat.Format = numberformat;
            // AutoFitColumns
            worksheet.Cells[1, 1, 4, 4].AutoFitColumns();
            return package;
        }
    }
}
