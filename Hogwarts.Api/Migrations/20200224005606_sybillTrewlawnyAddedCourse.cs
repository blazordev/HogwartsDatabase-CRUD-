using Microsoft.EntityFrameworkCore.Migrations;

namespace Hogwarts.Api.Migrations
{
    public partial class sybillTrewlawnyAddedCourse : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "TeachersCourse",
                columns: new[] { "CourseId", "TeacherId" },
                values: new object[] { 8, 3 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TeachersCourse",
                keyColumns: new[] { "CourseId", "TeacherId" },
                keyValues: new object[] { 8, 3 });
        }
    }
}
