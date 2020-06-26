using Microsoft.EntityFrameworkCore.Migrations;

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
                    Name = table.Column<string>(nullable: false)
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
                    Name = table.Column<string>(nullable: false),
                    MascotImageLink = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Houses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
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
                    FirstName = table.Column<string>(maxLength: 50, nullable: false),
                    MiddleNames = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(maxLength: 50, nullable: false),
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
                    FirstName = table.Column<string>(maxLength: 50, nullable: false),
                    MiddleNames = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(maxLength: 50, nullable: false),
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
                name: "StaffCourse",
                columns: table => new
                {
                    StaffId = table.Column<int>(nullable: false),
                    CourseId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StaffCourse", x => new { x.CourseId, x.StaffId });
                    table.ForeignKey(
                        name: "FK_StaffCourse_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StaffCourse_Staff_StaffId",
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
                    { 15, "Silvanus", null, "Kettleburn", "" },
                    { 14, "Horace", null, "Slughorn", "" },
                    { 13, "Rolanda", null, "Hooch", "" },
                    { 12, "Rubeus", null, "Hagrid", "" },
                    { 11, "Wilhelmina", null, "Grubbly-Plank", "" },
                    { 10, "Alastor", null, "Moody", "" },
                    { 9, "Filius", null, "Flitwick", "" },
                    { 7, "Alecto", null, "Carrow", "" },
                    { 6, "Charity", null, "Burbage", "" },
                    { 5, "Cuthbert", null, "Binns", "" },
                    { 4, "Severus", null, "Snape", "" },
                    { 3, "Sybill", null, "Trelawny", "Patricia" },
                    { 2, "Minerva", null, "McGonagall", "" },
                    { 1, "Albus", null, "Dumbledore", "Percival Wulfric Brian" },
                    { 16, "Pomona", null, "Sprout", "" },
                    { 8, "Remus", null, "Lupin", "" },
                    { 17, "Argus", null, "Filch", "" }
                });

            migrationBuilder.InsertData(
                table: "HeadOfHouses",
                columns: new[] { "Id", "HouseId", "StaffId" },
                values: new object[,]
                {
                    { 6, 3, 16 },
                    { 3, 2, 4 },
                    { 4, 2, 14 },
                    { 5, 4, 9 },
                    { 2, 1, 2 },
                    { 1, 1, 1 }
                });

            migrationBuilder.InsertData(
                table: "StaffCourse",
                columns: new[] { "CourseId", "StaffId" },
                values: new object[,]
                {
                    { 5, 4 },
                    { 2, 6 },
                    { 8, 3 },
                    { 5, 8 },
                    { 3, 2 },
                    { 5, 10 },
                    { 5, 1 },
                    { 1, 5 },
                    { 4, 11 },
                    { 3, 1 }
                });

            migrationBuilder.InsertData(
                table: "StaffRoles",
                columns: new[] { "StaffId", "RoleId" },
                values: new object[,]
                {
                    { 16, 3 },
                    { 6, 3 },
                    { 15, 3 },
                    { 7, 2 },
                    { 14, 6 },
                    { 7, 3 },
                    { 14, 3 },
                    { 9, 3 },
                    { 12, 5 },
                    { 12, 3 },
                    { 9, 6 },
                    { 11, 3 },
                    { 5, 3 },
                    { 13, 3 },
                    { 10, 3 },
                    { 17, 8 },
                    { 4, 3 },
                    { 4, 6 },
                    { 1, 1 },
                    { 1, 3 },
                    { 2, 1 },
                    { 2, 2 },
                    { 1, 6 },
                    { 2, 6 },
                    { 3, 3 },
                    { 16, 6 },
                    { 4, 1 },
                    { 2, 3 }
                });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "FirstName", "HouseId", "ImageLink", "LastName", "MiddleNames" },
                values: new object[,]
                {
                    { 3, "Hannah", 3, null, "Abbott", "" },
                    { 4, "Katie", 1, null, "Bell", "" },
                    { 7, "Lavender", 1, null, "Brown", "" },
                    { 2, "Draco", 2, null, "Malfoy", "Lucious" },
                    { 5, "Susan", 3, null, "Bones", "" },
                    { 12, "Padma", 4, null, "Patil", "" },
                    { 6, "Terry", 4, null, "Boot", "" },
                    { 8, "Cho", 4, null, "Chang", "" },
                    { 9, "Michael", 4, null, "Corner", "" },
                    { 11, "Anthony", 4, null, "Goldstein", "" },
                    { 10, "Justin", 3, null, "Finch-Fletchley", "" },
                    { 1, "Ronald", 1, null, "Weasely", "Bilius" }
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
                name: "IX_StaffCourse_StaffId",
                table: "StaffCourse",
                column: "StaffId");

            migrationBuilder.CreateIndex(
                name: "IX_StaffRoles_RoleId",
                table: "StaffRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_HouseId",
                table: "Students",
                column: "HouseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HeadOfHouses");

            migrationBuilder.DropTable(
                name: "StaffCourse");

            migrationBuilder.DropTable(
                name: "StaffRoles");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Staff");

            migrationBuilder.DropTable(
                name: "Houses");
        }
    }
}
