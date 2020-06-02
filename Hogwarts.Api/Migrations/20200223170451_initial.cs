﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace Hogwarts.Api.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Houses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    MascotImageLink = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Houses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RetrievedTeacherRecords",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(nullable: true),
                    MiddleNames = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RetrievedTeacherRecords", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Staff",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(nullable: true),
                    MiddleNames = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    ImageLink = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Staff", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(nullable: true),
                    MiddleNames = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    ImageLink = table.Column<string>(nullable: true),
                    HouseId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Students_Houses_HouseId",
                        column: x => x.HouseId,
                        principalTable: "Houses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HeadOfHouses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StaffId = table.Column<int>(nullable: false),
                    HouseId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HeadOfHouses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HeadOfHouses_Houses_HouseId",
                        column: x => x.HouseId,
                        principalTable: "Houses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HeadOfHouses_Staff_StaffId",
                        column: x => x.StaffId,
                        principalTable: "Staff",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StaffRoles",
                columns: table => new
                {
                    StaffId = table.Column<int>(nullable: false),
                    RoleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StaffRoles", x => new { x.StaffId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_StaffRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StaffRoles_Staff_StaffId",
                        column: x => x.StaffId,
                        principalTable: "Staff",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Teachers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StaffId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teachers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Teachers_Staff_StaffId",
                        column: x => x.StaffId,
                        principalTable: "Staff",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TeachersCourse",
                columns: table => new
                {
                    TeacherId = table.Column<int>(nullable: false),
                    CourseId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeachersCourse", x => new { x.CourseId, x.TeacherId });
                    table.ForeignKey(
                        name: "FK_TeachersCourse_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeachersCourse_Teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "History of Magic" },
                    { 2, "Muggle Studies" },
                    { 3, "Transfiguration" },
                    { 4, "Care of Magical Creatures" },
                    { 5, "Defence Against the Dark Arts" },
                    { 6, "Flying" },
                    { 7, "Charms" },
                    { 8, "Divination" }
                });

            migrationBuilder.InsertData(
                table: "Houses",
                columns: new[] { "Id", "MascotImageLink", "Name" },
                values: new object[,]
                {
                    { 1, "https://cdn.europosters.eu/image/1300/metal-tin-sign/harry-potter-gryffindor-i72420.jpg", "Gryffindor" },
                    { 2, "https://t0.gstatic.com/images?q=tbn%3AANd9GcQTrAsIK_l-uPN1c1Wo5jJK5PJ1xaFDonGcU9MyMRCeSe0gSr2nucmNf2N10L4RIP6InShl4K-G&usqp=CAc", "Slytherin" },
                    { 3, "https://s1.thcdn.com/productimg/1600/1600/12024631-1474653879765789.jpg", "Hufflepuff" },
                    { 4, "https://s1.thcdn.com/productimg/1600/1600/12024630-1024653879759849.jpg", "Ravenclaw" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 8, null, "Caretaker" },
                    { 6, null, "HeadOfHouse" },
                    { 5, null, "Grounds Keeper" },
                    { 7, null, "Librarian" },
                    { 3, null, "Teacher" },
                    { 2, null, "Deputy HeadMaster/HeadMistress" },
                    { 1, null, "Headmaster/Headmistress" },
                    { 4, null, "Patron/Matron" }
                });

            migrationBuilder.InsertData(
                table: "Staff",
                columns: new[] { "Id", "FirstName", "ImageLink", "LastName", "MiddleNames" },
                values: new object[,]
                {
                    { 15, "Silvanus", null, "Kettleburn", null },
                    { 14, "Horace", null, "Slughorn", null },
                    { 13, "Rolanda", null, "Hooch", null },
                    { 12, "Rubeus", null, "Hagrid", null },
                    { 11, "Wilhelmina", null, "Grubbly-Plank", null },
                    { 10, "Alastor", null, "Moody", null },
                    { 9, "Filius", null, "Flitwick", null },
                    { 7, "Alecto", null, "Carrow", null },
                    { 6, "Charity", null, "Burbage", null },
                    { 5, "Cuthbert", null, "Binns", null },
                    { 4, "Severus", null, "Snape", null },
                    { 3, "Sybill", null, "Trelawny", "Patricia" },
                    { 2, "Minerva", null, "McGonagall", null },
                    { 1, "Albus", null, "Dumbledore", "Percival Wulfric Brian" },
                    { 16, "Pomona", null, "Sprout", null },
                    { 8, "Remus", null, "Lupin", null },
                    { 17, "Argus", null, "Filch", null }
                });

            migrationBuilder.InsertData(
                table: "HeadOfHouses",
                columns: new[] { "Id", "HouseId", "StaffId" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 1, 2 },
                    { 5, 4, 9 },
                    { 4, 2, 14 },
                    { 3, 2, 4 },
                    { 6, 3, 16 }
                });

            migrationBuilder.InsertData(
                table: "StaffRoles",
                columns: new[] { "StaffId", "RoleId" },
                values: new object[,]
                {
                    { 14, 3 },
                    { 4, 3 },
                    { 16, 6 },
                    { 5, 3 },
                    { 6, 3 },
                    { 7, 2 },
                    { 16, 3 },
                    { 7, 3 },
                    { 9, 3 },
                    { 9, 6 },
                    { 4, 1 },
                    { 10, 3 },
                    { 11, 3 },
                    { 12, 3 },
                    { 12, 5 },
                    { 13, 3 },
                    { 15, 3 },
                    { 14, 6 },
                    { 4, 6 },
                    { 17, 8 },
                    { 3, 3 },
                    { 1, 1 },
                    { 1, 3 },
                    { 1, 6 },
                    { 2, 2 },
                    { 2, 1 },
                    { 2, 3 },
                    { 2, 6 }
                });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "FirstName", "HouseId", "ImageLink", "LastName", "MiddleNames" },
                values: new object[,]
                {
                    { 3, "Hannah", 3, null, "Abbott", null },
                    { 9, "Michael", 4, null, "Corner", null },
                    { 5, "Susan", 3, null, "Bones", null },
                    { 10, "Justin", 3, null, "Finch-Fletchley", null },
                    { 6, "Terry", 4, null, "Boot", null },
                    { 7, "Lavender", 1, null, "Brown", null },
                    { 4, "Katie", 1, null, "Bell", null },
                    { 8, "Cho", 4, null, "Chang", null },
                    { 2, "Draco", 2, null, "Malfoy", "Lucious" },
                    { 11, "Anthony", 4, null, "Goldstein", null },
                    { 1, "Ronald", 1, null, "Weasely", "Bilius" },
                    { 12, "Padma", 4, null, "Patil", null }
                });

            migrationBuilder.InsertData(
                table: "Teachers",
                columns: new[] { "Id", "StaffId" },
                values: new object[,]
                {
                    { 11, 11 },
                    { 3, 3 },
                    { 8, 8 },
                    { 7, 7 },
                    { 1, 1 },
                    { 6, 6 },
                    { 5, 5 },
                    { 4, 4 },
                    { 2, 2 },
                    { 10, 10 },
                    { 9, 9 }
                });

            migrationBuilder.InsertData(
                table: "TeachersCourse",
                columns: new[] { "CourseId", "TeacherId" },
                values: new object[,]
                {
                    { 3, 1 },
                    { 5, 1 },
                    { 3, 2 },
                    { 5, 4 },
                    { 1, 5 },
                    { 2, 6 },
                    { 5, 8 },
                    { 5, 10 },
                    { 4, 11 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_HeadOfHouses_HouseId",
                table: "HeadOfHouses",
                column: "HouseId");

            migrationBuilder.CreateIndex(
                name: "IX_HeadOfHouses_StaffId",
                table: "HeadOfHouses",
                column: "StaffId");

            migrationBuilder.CreateIndex(
                name: "IX_StaffRoles_RoleId",
                table: "StaffRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_HouseId",
                table: "Students",
                column: "HouseId");

            migrationBuilder.CreateIndex(
                name: "IX_Teachers_StaffId",
                table: "Teachers",
                column: "StaffId");

            migrationBuilder.CreateIndex(
                name: "IX_TeachersCourse_TeacherId",
                table: "TeachersCourse",
                column: "TeacherId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HeadOfHouses");

            migrationBuilder.DropTable(
                name: "RetrievedTeacherRecords");

            migrationBuilder.DropTable(
                name: "StaffRoles");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "TeachersCourse");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Houses");

            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "Teachers");

            migrationBuilder.DropTable(
                name: "Staff");
        }
    }
}
