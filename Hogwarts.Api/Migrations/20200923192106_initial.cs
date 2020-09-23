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
                    Name = table.Column<string>(nullable: false),
                    ShortHandName = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Level = table.Column<int>(nullable: false)
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
                    Gender = table.Column<int>(nullable: false),
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
                columns: new[] { "Id", "Description", "Level", "Name", "ShortHandName" },
                values: new object[,]
                {
                    { 1, null, 0, "History of Magic", null },
                    { 25, null, 2, "Xylomancy", null },
                    { 24, null, 2, "Music", null },
                    { 23, null, 2, "Muggle Music", null },
                    { 22, null, 2, "Muggle Art", null },
                    { 21, null, 2, "Magical Theory", null },
                    { 20, null, 2, "Ghoul Studies", null },
                    { 19, null, 2, "Art", null },
                    { 18, null, 2, "Ancient Studies", null },
                    { 16, null, 2, "Apparition", null },
                    { 15, null, 1, "Study of Ancient Runes", null },
                    { 14, null, 1, "Arithmancy", null },
                    { 17, null, 2, "Advanced Arithmancy Studies", null },
                    { 12, null, 0, "Potions", null },
                    { 2, null, 1, "Muggle Studies", null },
                    { 13, null, 1, "Alchemy", null },
                    { 4, null, 1, "Care of Magical Creatures", null },
                    { 5, null, 0, "Defence Against the Dark Arts", null },
                    { 6, null, 2, "Flying", null },
                    { 3, null, 0, "Transfiguration", null },
                    { 8, null, 0, "Astronomy", null },
                    { 9, null, 1, "Divination", null },
                    { 10, null, 0, "Herbology", null },
                    { 11, null, 1, "Divination", null },
                    { 7, null, 0, "Charms", null }
                });

            migrationBuilder.InsertData(
                table: "Houses",
                columns: new[] { "Id", "MascotImageLink", "Name" },
                values: new object[,]
                {
                    { 1, "https://cdn.europosters.eu/image/1300/metal-tin-sign/harry-potter-gryffindor-i72420.jpg", "Gryffindor" },
                    { 2, "https://t0.gstatic.com/images?q=tbn%3AANd9GcQTrAsIK_l-uPN1c1Wo5jJK5PJ1xaFDonGcU9MyMRCeSe0gSr2nucmNf2N10L4RIP6InShl4K-G&usqp=CAc", "Slytherin" },
                    { 3, "https://s1.thcdn.com/productimg/1600/1600/12024631-1474653879765789.jpg", "Hufflepuff" },
                    { 4, "https://s1.thcdn.com/productimg/1600/1600/12024630-1024653879759849.jpg", "Ravenclaw" },
                    { 5, null, "Unknown" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 6, null, "House Head" },
                    { 8, null, "Caretaker" },
                    { 7, null, "Librarian" },
                    { 5, null, "Grounds Keeper" },
                    { 2, null, "Deputy Headmaster/Headmistress" },
                    { 3, null, "Professor" },
                    { 1, null, "Headmaster/Headmistress" },
                    { 4, null, "Patron/Matron" }
                });

            migrationBuilder.InsertData(
                table: "Staff",
                columns: new[] { "Id", "FirstName", "Gender", "ImageLink", "LastName", "MiddleNames" },
                values: new object[,]
                {
                    { 25, "Aurora", 1, null, "Sinistra", "" },
                    { 24, "Irma", 1, null, "Pince", "" },
                    { 23, "Firenze", 0, null, "", "" },
                    { 21, "Amycus", 0, null, "Carrow", "" },
                    { 20, "Poppy", 1, null, "Pomfrey", "" },
                    { 19, "Quirinus", 0, null, "Quirrell", "" },
                    { 18, "Gilderoy", 0, null, "Lockhart", "" },
                    { 17, "Argus", 0, null, "Filch", "" },
                    { 16, "Pomona", 1, null, "Sprout", "" },
                    { 15, "Silvanus", 0, null, "Kettleburn", "" },
                    { 14, "Horace", 0, null, "Slughorn", "" },
                    { 13, "Rolanda", 1, null, "Hooch", "" },
                    { 12, "Rubeus", 0, null, "Hagrid", "" },
                    { 10, "Alastor", 0, null, "Moody", "" },
                    { 9, "Filius", 0, null, "Flitwick", "" },
                    { 8, "Remus", 0, null, "Lupin", "" },
                    { 7, "Alecto", 1, null, "Carrow", "" },
                    { 6, "Charity", 1, null, "Burbage", "" },
                    { 5, "Cuthbert", 0, null, "Binns", "" },
                    { 4, "Severus", 0, null, "Snape", "" },
                    { 3, "Sybill", 1, null, "Trelawny", "Patricia" },
                    { 2, "Minerva", 1, null, "McGonagall", "" },
                    { 1, "Albus", 0, null, "Dumbledore", "Percival Wulfric Brian" },
                    { 26, "Dolores", 1, null, "Umbridge", "" },
                    { 11, "Wilhelmina", 1, null, "Grubbly-Plank", "" },
                    { 27, "Septima", 1, null, "Vector", "" }
                });

            migrationBuilder.InsertData(
                table: "HeadOfHouses",
                columns: new[] { "Id", "HouseId", "StaffId" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 4, 2, 14 },
                    { 5, 4, 9 },
                    { 3, 2, 4 },
                    { 6, 3, 16 },
                    { 2, 1, 2 }
                });

            migrationBuilder.InsertData(
                table: "StaffCourse",
                columns: new[] { "CourseId", "StaffId" },
                values: new object[,]
                {
                    { 3, 2 },
                    { 12, 14 },
                    { 5, 1 },
                    { 6, 13 },
                    { 4, 12 },
                    { 3, 1 },
                    { 5, 10 },
                    { 7, 9 },
                    { 5, 8 },
                    { 4, 15 },
                    { 2, 6 },
                    { 1, 5 },
                    { 12, 4 },
                    { 5, 4 },
                    { 9, 3 },
                    { 2, 7 },
                    { 4, 11 },
                    { 14, 27 },
                    { 5, 21 },
                    { 5, 18 },
                    { 5, 26 },
                    { 8, 25 },
                    { 2, 19 },
                    { 11, 23 },
                    { 5, 19 }
                });

            migrationBuilder.InsertData(
                table: "StaffRoles",
                columns: new[] { "StaffId", "RoleId" },
                values: new object[,]
                {
                    { 4, 6 },
                    { 4, 3 },
                    { 4, 1 },
                    { 23, 3 },
                    { 24, 7 },
                    { 3, 3 },
                    { 2, 6 },
                    { 2, 3 },
                    { 2, 2 },
                    { 2, 1 },
                    { 26, 1 },
                    { 1, 6 },
                    { 1, 3 },
                    { 1, 1 },
                    { 26, 3 },
                    { 25, 3 },
                    { 5, 3 },
                    { 6, 3 },
                    { 15, 3 },
                    { 14, 6 },
                    { 14, 3 },
                    { 16, 6 },
                    { 17, 8 },
                    { 13, 3 },
                    { 12, 5 },
                    { 12, 3 },
                    { 18, 3 },
                    { 21, 2 },
                    { 11, 3 },
                    { 9, 6 },
                    { 9, 3 },
                    { 19, 3 },
                    { 20, 4 },
                    { 8, 3 },
                    { 7, 3 },
                    { 7, 2 },
                    { 21, 3 },
                    { 10, 3 },
                    { 16, 3 },
                    { 27, 3 }
                });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "FirstName", "HouseId", "ImageLink", "LastName", "MiddleNames" },
                values: new object[,]
                {
                    { 37, "Kevin", 5, null, "Entwhistle", "" },
                    { 20, "Adrian", 2, null, "Pucey", "" },
                    { 19, "Tracey", 2, null, "Davis", "" },
                    { 18, "Lilian", 2, null, "Moon", "" },
                    { 17, "Daphne", 2, null, "Greengrass", "" },
                    { 16, "Theodore", 2, null, "Nott", "" },
                    { 15, "Astoria", 2, null, "Greengrass", "" },
                    { 14, "Malcolm", 2, null, "Baddock", "" },
                    { 13, "Graham", 2, null, "Pritchard", "" },
                    { 2, "Draco", 2, null, "Malfoy", "Lucious" },
                    { 72, "Harry", 1, null, "Potter", "" },
                    { 71, "Hermione", 1, null, "Granger", "" },
                    { 70, "Colin", 1, null, "Creevey", "" },
                    { 69, "Seamus", 1, null, "Finnigan", "" },
                    { 65, "Dean", 1, null, "Thomas", "" },
                    { 60, "Dennis", 1, null, "Creevey", "" },
                    { 58, "Ritchie", 1, null, "Coote", "" },
                    { 57, "Jack", 1, null, "Sloper", "" },
                    { 4, "Katie", 1, null, "Bell", "" },
                    { 7, "Lavender", 1, null, "Brown", "" },
                    { 45, "Mary", 1, null, "MacDonald", "" },
                    { 46, "Euan", 1, null, "Abercrombie", "" },
                    { 47, "Jimmy", 1, null, "Peakes", "" },
                    { 48, "Natalie", 1, null, "McDonald", "" },
                    { 21, "Terence", 2, null, "Higgs", "" },
                    { 49, "Romilda", 1, null, "Vane", "" },
                    { 51, "Neville", 1, null, "Longbottom", "" },
                    { 52, "Cormac", 1, null, "McLaggen", "" },
                    { 53, "Demelza", 1, null, "Robins", "" },
                    { 54, "Andrew", 1, null, "Kirke", "" },
                    { 55, "Geoffrey", 1, null, "Hooper", "" },
                    { 56, "Victoria", 1, null, "Frobisher", "" },
                    { 50, "Ginny", 1, null, "Weasley", "" },
                    { 61, "Gregory", 2, null, "Goyle", "" },
                    { 62, "Vincent", 2, null, "Crabbe", "" },
                    { 64, "Millicent", 2, null, "Bulstrode", "" },
                    { 30, "Stewart", 4, null, "Ackerley", "" },
                    { 31, "Orla", 4, null, "Quirke", "" },
                    { 32, "Luna", 4, null, "Lovegood", "" },
                    { 33, "Lisa", 4, null, "Turpin", "" },
                    { 34, "Padma", 4, null, "Patil", "" },
                    { 35, "Morag", 4, null, "McDougal", "" },
                    { 11, "Anthony", 4, null, "Goldstein", "" },
                    { 36, "Su", 4, null, "Li", "" },
                    { 39, "Michael", 4, null, "Corner", "" },
                    { 40, "Mandy", 4, null, "Brocklehurst", "" },
                    { 41, "Marietta", 4, null, "Edgecombe", "" },
                    { 42, "Eddie", 4, null, "Carmichael", "" },
                    { 43, "Roger", 4, null, "Davies", "" },
                    { 44, "Penelope", 4, null, "Clearwater", "" },
                    { 38, "Stephen", 4, null, "Cornfoot", "" },
                    { 59, "Eloise", 5, null, "Midgen", "" },
                    { 9, "Michael", 4, null, "Corner", "" },
                    { 6, "Terry", 4, null, "Boot", "" },
                    { 66, "Blaise", 2, null, "Zabini", "" },
                    { 68, "Pansy", 2, null, "Parkinson", "" },
                    { 3, "Hannah", 3, null, "Abbott", "" },
                    { 5, "Susan", 3, null, "Bones", "" },
                    { 10, "Justin", 3, null, "Finch-Fletchley", "" },
                    { 22, "Rose", 3, null, "Zeller", "" },
                    { 8, "Cho", 4, null, "Chang", "" },
                    { 23, "Kevin", 3, null, "Whitby", "" },
                    { 25, "Megan", 3, null, "Jones", "" },
                    { 26, "Wayne", 3, null, "Hopkins", "" },
                    { 27, "Owen", 3, null, "Cauldwell", "" },
                    { 28, "Eleanor", 3, null, "Branstone", "" },
                    { 63, "Ernie", 3, null, "Macmillan", "" },
                    { 67, "Zacharias", 3, null, "Smith", "" },
                    { 24, "Laura", 3, null, "Madley", "" },
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
