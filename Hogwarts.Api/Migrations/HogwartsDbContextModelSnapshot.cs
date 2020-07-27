﻿// <auto-generated />
using Hogwarts.Api.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Hogwarts.Api.Migrations
{
    [DbContext(typeof(HogwartsDbContext))]
    partial class HogwartsDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Hogwarts.Data.Course", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Courses");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "History of Magic"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Muggle Studies"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Transfiguration"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Care of Magical Creatures"
                        },
                        new
                        {
                            Id = 5,
                            Name = "Defence Against the Dark Arts"
                        },
                        new
                        {
                            Id = 6,
                            Name = "Flying"
                        },
                        new
                        {
                            Id = 7,
                            Name = "Charms"
                        },
                        new
                        {
                            Id = 8,
                            Name = "Divination"
                        });
                });

            modelBuilder.Entity("Hogwarts.Data.HeadOfHouse", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("HouseId")
                        .HasColumnType("int");

                    b.Property<int>("StaffId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("HouseId");

                    b.HasIndex("StaffId");

                    b.ToTable("HeadOfHouses");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            HouseId = 1,
                            StaffId = 1
                        },
                        new
                        {
                            Id = 2,
                            HouseId = 1,
                            StaffId = 2
                        },
                        new
                        {
                            Id = 3,
                            HouseId = 2,
                            StaffId = 4
                        },
                        new
                        {
                            Id = 4,
                            HouseId = 2,
                            StaffId = 14
                        },
                        new
                        {
                            Id = 5,
                            HouseId = 4,
                            StaffId = 9
                        },
                        new
                        {
                            Id = 6,
                            HouseId = 3,
                            StaffId = 16
                        });
                });

            modelBuilder.Entity("Hogwarts.Data.House", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("MascotImageLink")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Houses");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            MascotImageLink = "https://cdn.europosters.eu/image/1300/metal-tin-sign/harry-potter-gryffindor-i72420.jpg",
                            Name = "Gryffindor"
                        },
                        new
                        {
                            Id = 2,
                            MascotImageLink = "https://t0.gstatic.com/images?q=tbn%3AANd9GcQTrAsIK_l-uPN1c1Wo5jJK5PJ1xaFDonGcU9MyMRCeSe0gSr2nucmNf2N10L4RIP6InShl4K-G&usqp=CAc",
                            Name = "Slytherin"
                        },
                        new
                        {
                            Id = 3,
                            MascotImageLink = "https://s1.thcdn.com/productimg/1600/1600/12024631-1474653879765789.jpg",
                            Name = "Hufflepuff"
                        },
                        new
                        {
                            Id = 4,
                            MascotImageLink = "https://s1.thcdn.com/productimg/1600/1600/12024630-1024653879759849.jpg",
                            Name = "Ravenclaw"
                        });
                });

            modelBuilder.Entity("Hogwarts.Data.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Headmaster/Headmistress"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Deputy Headmaster/Headmistress"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Teacher"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Patron/Matron"
                        },
                        new
                        {
                            Id = 5,
                            Name = "Grounds Keeper"
                        },
                        new
                        {
                            Id = 6,
                            Name = "House Head"
                        },
                        new
                        {
                            Id = 7,
                            Name = "Librarian"
                        },
                        new
                        {
                            Id = 8,
                            Name = "Caretaker"
                        });
                });

            modelBuilder.Entity("Hogwarts.Data.Staff", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("ImageLink")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("MiddleNames")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Staff");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            FirstName = "Albus",
                            LastName = "Dumbledore",
                            MiddleNames = "Percival Wulfric Brian"
                        },
                        new
                        {
                            Id = 2,
                            FirstName = "Minerva",
                            LastName = "McGonagall",
                            MiddleNames = ""
                        },
                        new
                        {
                            Id = 3,
                            FirstName = "Sybill",
                            LastName = "Trelawny",
                            MiddleNames = "Patricia"
                        },
                        new
                        {
                            Id = 4,
                            FirstName = "Severus",
                            LastName = "Snape",
                            MiddleNames = ""
                        },
                        new
                        {
                            Id = 5,
                            FirstName = "Cuthbert",
                            LastName = "Binns",
                            MiddleNames = ""
                        },
                        new
                        {
                            Id = 6,
                            FirstName = "Charity",
                            LastName = "Burbage",
                            MiddleNames = ""
                        },
                        new
                        {
                            Id = 7,
                            FirstName = "Alecto",
                            LastName = "Carrow",
                            MiddleNames = ""
                        },
                        new
                        {
                            Id = 8,
                            FirstName = "Remus",
                            LastName = "Lupin",
                            MiddleNames = ""
                        },
                        new
                        {
                            Id = 9,
                            FirstName = "Filius",
                            LastName = "Flitwick",
                            MiddleNames = ""
                        },
                        new
                        {
                            Id = 10,
                            FirstName = "Alastor",
                            LastName = "Moody",
                            MiddleNames = ""
                        },
                        new
                        {
                            Id = 11,
                            FirstName = "Wilhelmina",
                            LastName = "Grubbly-Plank",
                            MiddleNames = ""
                        },
                        new
                        {
                            Id = 12,
                            FirstName = "Rubeus",
                            LastName = "Hagrid",
                            MiddleNames = ""
                        },
                        new
                        {
                            Id = 13,
                            FirstName = "Rolanda",
                            LastName = "Hooch",
                            MiddleNames = ""
                        },
                        new
                        {
                            Id = 14,
                            FirstName = "Horace",
                            LastName = "Slughorn",
                            MiddleNames = ""
                        },
                        new
                        {
                            Id = 15,
                            FirstName = "Silvanus",
                            LastName = "Kettleburn",
                            MiddleNames = ""
                        },
                        new
                        {
                            Id = 16,
                            FirstName = "Pomona",
                            LastName = "Sprout",
                            MiddleNames = ""
                        },
                        new
                        {
                            Id = 17,
                            FirstName = "Argus",
                            LastName = "Filch",
                            MiddleNames = ""
                        });
                });

            modelBuilder.Entity("Hogwarts.Data.StaffCourse", b =>
                {
                    b.Property<int>("CourseId")
                        .HasColumnType("int");

                    b.Property<int>("StaffId")
                        .HasColumnType("int");

                    b.HasKey("CourseId", "StaffId");

                    b.HasIndex("StaffId");

                    b.ToTable("StaffCourse");

                    b.HasData(
                        new
                        {
                            CourseId = 3,
                            StaffId = 1
                        },
                        new
                        {
                            CourseId = 8,
                            StaffId = 3
                        },
                        new
                        {
                            CourseId = 5,
                            StaffId = 1
                        },
                        new
                        {
                            CourseId = 3,
                            StaffId = 2
                        },
                        new
                        {
                            CourseId = 1,
                            StaffId = 5
                        },
                        new
                        {
                            CourseId = 2,
                            StaffId = 6
                        },
                        new
                        {
                            CourseId = 4,
                            StaffId = 11
                        },
                        new
                        {
                            CourseId = 5,
                            StaffId = 8
                        },
                        new
                        {
                            CourseId = 5,
                            StaffId = 4
                        },
                        new
                        {
                            CourseId = 5,
                            StaffId = 10
                        });
                });

            modelBuilder.Entity("Hogwarts.Data.StaffRole", b =>
                {
                    b.Property<int>("StaffId")
                        .HasColumnType("int");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("StaffId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("StaffRoles");

                    b.HasData(
                        new
                        {
                            StaffId = 1,
                            RoleId = 1
                        },
                        new
                        {
                            StaffId = 2,
                            RoleId = 1
                        },
                        new
                        {
                            StaffId = 4,
                            RoleId = 1
                        },
                        new
                        {
                            StaffId = 7,
                            RoleId = 2
                        },
                        new
                        {
                            StaffId = 2,
                            RoleId = 2
                        },
                        new
                        {
                            StaffId = 9,
                            RoleId = 3
                        },
                        new
                        {
                            StaffId = 7,
                            RoleId = 3
                        },
                        new
                        {
                            StaffId = 6,
                            RoleId = 3
                        },
                        new
                        {
                            StaffId = 5,
                            RoleId = 3
                        },
                        new
                        {
                            StaffId = 4,
                            RoleId = 3
                        },
                        new
                        {
                            StaffId = 3,
                            RoleId = 3
                        },
                        new
                        {
                            StaffId = 2,
                            RoleId = 3
                        },
                        new
                        {
                            StaffId = 1,
                            RoleId = 3
                        },
                        new
                        {
                            StaffId = 10,
                            RoleId = 3
                        },
                        new
                        {
                            StaffId = 11,
                            RoleId = 3
                        },
                        new
                        {
                            StaffId = 12,
                            RoleId = 3
                        },
                        new
                        {
                            StaffId = 13,
                            RoleId = 3
                        },
                        new
                        {
                            StaffId = 14,
                            RoleId = 3
                        },
                        new
                        {
                            StaffId = 15,
                            RoleId = 3
                        },
                        new
                        {
                            StaffId = 16,
                            RoleId = 3
                        },
                        new
                        {
                            StaffId = 12,
                            RoleId = 5
                        },
                        new
                        {
                            StaffId = 1,
                            RoleId = 6
                        },
                        new
                        {
                            StaffId = 2,
                            RoleId = 6
                        },
                        new
                        {
                            StaffId = 4,
                            RoleId = 6
                        },
                        new
                        {
                            StaffId = 9,
                            RoleId = 6
                        },
                        new
                        {
                            StaffId = 14,
                            RoleId = 6
                        },
                        new
                        {
                            StaffId = 16,
                            RoleId = 6
                        },
                        new
                        {
                            StaffId = 17,
                            RoleId = 8
                        });
                });

            modelBuilder.Entity("Hogwarts.Data.Student", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<int>("HouseId")
                        .HasColumnType("int");

                    b.Property<string>("ImageLink")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("MiddleNames")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("HouseId");

                    b.ToTable("Students");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            FirstName = "Ronald",
                            HouseId = 1,
                            LastName = "Weasely",
                            MiddleNames = "Bilius"
                        },
                        new
                        {
                            Id = 2,
                            FirstName = "Draco",
                            HouseId = 2,
                            LastName = "Malfoy",
                            MiddleNames = "Lucious"
                        },
                        new
                        {
                            Id = 3,
                            FirstName = "Hannah",
                            HouseId = 3,
                            LastName = "Abbott",
                            MiddleNames = ""
                        },
                        new
                        {
                            Id = 4,
                            FirstName = "Katie",
                            HouseId = 1,
                            LastName = "Bell",
                            MiddleNames = ""
                        },
                        new
                        {
                            Id = 5,
                            FirstName = "Susan",
                            HouseId = 3,
                            LastName = "Bones",
                            MiddleNames = ""
                        },
                        new
                        {
                            Id = 6,
                            FirstName = "Terry",
                            HouseId = 4,
                            LastName = "Boot",
                            MiddleNames = ""
                        },
                        new
                        {
                            Id = 7,
                            FirstName = "Lavender",
                            HouseId = 1,
                            LastName = "Brown",
                            MiddleNames = ""
                        },
                        new
                        {
                            Id = 8,
                            FirstName = "Cho",
                            HouseId = 4,
                            LastName = "Chang",
                            MiddleNames = ""
                        },
                        new
                        {
                            Id = 9,
                            FirstName = "Michael",
                            HouseId = 4,
                            LastName = "Corner",
                            MiddleNames = ""
                        },
                        new
                        {
                            Id = 10,
                            FirstName = "Justin",
                            HouseId = 3,
                            LastName = "Finch-Fletchley",
                            MiddleNames = ""
                        },
                        new
                        {
                            Id = 11,
                            FirstName = "Anthony",
                            HouseId = 4,
                            LastName = "Goldstein",
                            MiddleNames = ""
                        },
                        new
                        {
                            Id = 12,
                            FirstName = "Padma",
                            HouseId = 4,
                            LastName = "Patil",
                            MiddleNames = ""
                        });
                });

            modelBuilder.Entity("Hogwarts.Data.HeadOfHouse", b =>
                {
                    b.HasOne("Hogwarts.Data.House", "House")
                        .WithMany("HeadsOfHouse")
                        .HasForeignKey("HouseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Hogwarts.Data.Staff", "Staff")
                        .WithMany()
                        .HasForeignKey("StaffId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Hogwarts.Data.StaffCourse", b =>
                {
                    b.HasOne("Hogwarts.Data.Course", "Course")
                        .WithMany("StaffCourse")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Hogwarts.Data.Staff", "Staff")
                        .WithMany("StaffCourse")
                        .HasForeignKey("StaffId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Hogwarts.Data.StaffRole", b =>
                {
                    b.HasOne("Hogwarts.Data.Role", "Role")
                        .WithMany("StaffRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Hogwarts.Data.Staff", "Staff")
                        .WithMany("StaffRoles")
                        .HasForeignKey("StaffId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Hogwarts.Data.Student", b =>
                {
                    b.HasOne("Hogwarts.Data.House", "House")
                        .WithMany("Students")
                        .HasForeignKey("HouseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
