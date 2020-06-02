using Hogwarts.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hogwarts.Api.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<House>().HasData(
                new House { Id = 1, Name = "Gryffindor", MascotImageLink = "https://cdn.europosters.eu/image/1300/metal-tin-sign/harry-potter-gryffindor-i72420.jpg" },
            new House { Id = 2, Name = "Slytherin", MascotImageLink = "https://t0.gstatic.com/images?q=tbn%3AANd9GcQTrAsIK_l-uPN1c1Wo5jJK5PJ1xaFDonGcU9MyMRCeSe0gSr2nucmNf2N10L4RIP6InShl4K-G&usqp=CAc" },
            new House { Id = 3, Name = "Hufflepuff", MascotImageLink = "https://s1.thcdn.com/productimg/1600/1600/12024631-1474653879765789.jpg" },
            new House { Id = 4, Name = "Ravenclaw", MascotImageLink = "https://s1.thcdn.com/productimg/1600/1600/12024630-1024653879759849.jpg" }
                            );

            modelBuilder.Entity<Student>().HasData(
                new Student { Id = 1, FirstName = "Ronald", MiddleNames = "Bilius", LastName = "Weasely", HouseId = 1 },
                new Student { Id = 2, FirstName = "Draco", MiddleNames = "Lucious", LastName = "Malfoy", HouseId = 2 },
                new Student { Id = 3, FirstName = "Hannah", LastName = "Abbott", HouseId = 3 },
                 new Student { Id = 4, FirstName = "Katie", LastName = "Bell", HouseId = 1 },
                new Student { Id = 5, FirstName = "Susan", LastName = "Bones", HouseId = 3 },
                new Student { Id = 6, FirstName = "Terry", LastName = "Boot", HouseId = 4 },
                 new Student { Id = 7, FirstName = "Lavender", LastName = "Brown", HouseId = 1 },
                new Student { Id = 8, FirstName = "Cho", LastName = "Chang", HouseId = 4 },
                new Student { Id = 9, FirstName = "Michael", LastName = "Corner", HouseId = 4 },
                 new Student { Id = 10, FirstName = "Justin", LastName = "Finch-Fletchley", HouseId = 3 },
                 new Student { Id = 11, FirstName = "Anthony", LastName = "Goldstein", HouseId = 4 },
                 new Student { Id = 12, FirstName = "Padma", LastName = "Patil", HouseId = 4 }
                );

            modelBuilder.Entity<Staff>().HasData(
            new Staff { Id = 1, FirstName = "Albus", MiddleNames = "Percival Wulfric Brian", LastName = "Dumbledore" },
            new Staff { Id = 2, FirstName = "Minerva", LastName = "McGonagall" },
            new Staff { Id = 3, FirstName = "Sybill", MiddleNames = "Patricia", LastName = "Trelawny" },
            new Staff { Id = 4, FirstName = "Severus", LastName = "Snape" },
            new Staff { Id = 5, FirstName = "Cuthbert", LastName = "Binns" },
            new Staff { Id = 6, FirstName = "Charity", LastName = "Burbage" },
            new Staff { Id = 7, FirstName = "Alecto", LastName = "Carrow" },
            new Staff { Id = 8, FirstName = "Remus", LastName = "Lupin" },
            new Staff { Id = 9, FirstName = "Filius", LastName = "Flitwick" },
            new Staff { Id = 10, FirstName = "Alastor", LastName = "Moody" },
            new Staff { Id = 11, FirstName = "Wilhelmina", LastName = "Grubbly-Plank" },
            new Staff { Id = 12, FirstName = "Rubeus", LastName = "Hagrid" },
            new Staff { Id = 13, FirstName = "Rolanda", LastName = "Hooch" },
            new Staff { Id = 14, FirstName = "Horace", LastName = "Slughorn" },
            new Staff { Id = 15, FirstName = "Silvanus", LastName = "Kettleburn" },
            new Staff { Id = 16, FirstName = "Pomona", LastName = "Sprout" },
            new Staff { Id = 17, FirstName = "Argus", LastName = "Filch", });

            //Seed Courses
            modelBuilder.Entity<Course>().HasData(
                new Course { Id = 1, Name = "History of Magic" },
                new Course { Id = 2, Name = "Muggle Studies" },
                new Course { Id = 3, Name = "Transfiguration" },
                new Course { Id = 4, Name = "Care of Magical Creatures" },
                new Course { Id = 5, Name = "Defence Against the Dark Arts" },
                new Course { Id = 6, Name = "Flying" },
                new Course { Id = 7, Name = "Charms" },
                new Course { Id = 8, Name = "Divination" }
                );

            modelBuilder.Entity<HeadOfHouse>().HasData(
                new HeadOfHouse { Id = 1, HouseId = 1, StaffId = 1 },
                new HeadOfHouse { Id = 2, HouseId = 1, StaffId = 2 },
                new HeadOfHouse { Id = 3, HouseId = 2, StaffId = 4 },
                new HeadOfHouse { Id = 4, HouseId = 2, StaffId = 14 },
                new HeadOfHouse { Id = 5, HouseId = 4, StaffId = 9 },
                new HeadOfHouse { Id = 6, HouseId = 3, StaffId = 16 }
                );

            modelBuilder.Entity<Teacher>().HasData(
                new Teacher { Id = 1, StaffId = 1 },
                new Teacher { Id = 2, StaffId = 2 },
                new Teacher { Id = 3, StaffId = 3 },
                new Teacher { Id = 4, StaffId = 4 },
                new Teacher { Id = 5, StaffId = 5 },
                new Teacher { Id = 6, StaffId = 6 },
                new Teacher { Id = 7, StaffId = 7 },
                new Teacher { Id = 8, StaffId = 8 },
                new Teacher { Id = 9, StaffId = 9 },
                new Teacher { Id = 10, StaffId = 10 },
                new Teacher { Id = 11, StaffId = 11 }
                );

            modelBuilder.Entity<TeacherCourse>().HasData(
                new TeacherCourse { CourseId = 3, TeacherId = 1 },
                new TeacherCourse { CourseId = 8, TeacherId = 3 },
                new TeacherCourse { CourseId = 5, TeacherId = 1 },
                new TeacherCourse { CourseId = 3, TeacherId = 2 },
                new TeacherCourse { CourseId = 1, TeacherId = 5 },
                new TeacherCourse { CourseId = 2, TeacherId = 6 },
                new TeacherCourse { CourseId = 4, TeacherId = 11 },
                new TeacherCourse { CourseId = 5, TeacherId = 8 },
                new TeacherCourse { CourseId = 5, TeacherId = 4 },
                new TeacherCourse { CourseId = 5, TeacherId = 10 }
                );

            modelBuilder.Entity<Role>().HasData(
                new Role { Id = 1, Name = "Headmaster/Headmistress" },
                new Role { Id = 2, Name = "Deputy HeadMaster/HeadMistress" },
                new Role { Id = 3, Name = "Teacher" },
                new Role { Id = 4, Name = "Patron/Matron" },
                new Role { Id = 5, Name = "Grounds Keeper" },
                new Role { Id = 6, Name = "HeadOfHouse" },
                new Role { Id = 7, Name = "Librarian" },
                new Role { Id = 8, Name = "Caretaker" }
                );
            modelBuilder.Entity<StaffRole>().HasData(
                new StaffRole { StaffId = 1, RoleId = 1 },
                new StaffRole { StaffId = 2, RoleId = 1 },
                new StaffRole { StaffId = 4, RoleId = 1 },
                new StaffRole { StaffId = 7, RoleId = 2 },
                new StaffRole { StaffId = 2, RoleId = 2 },
                new StaffRole { StaffId = 9, RoleId = 3 },                
                new StaffRole { StaffId = 7, RoleId = 3 },
                new StaffRole { StaffId = 6, RoleId = 3 },
                new StaffRole { StaffId = 5, RoleId = 3 },
                new StaffRole { StaffId = 4, RoleId = 3 },
                new StaffRole { StaffId = 3, RoleId = 3 },
                new StaffRole { StaffId = 2, RoleId = 3 },
                new StaffRole { StaffId = 1, RoleId = 3 },
                new StaffRole { StaffId = 10, RoleId = 3 },
                new StaffRole { StaffId = 11, RoleId = 3 },
                new StaffRole { StaffId = 12, RoleId = 3 },
                new StaffRole { StaffId = 13, RoleId = 3 },
                new StaffRole { StaffId = 14, RoleId = 3 },
                new StaffRole { StaffId = 15, RoleId = 3 },
                new StaffRole { StaffId = 16, RoleId = 3 },
                new StaffRole { StaffId = 12, RoleId = 5 },
                new StaffRole { StaffId = 1, RoleId = 6 },
                new StaffRole { StaffId = 2, RoleId = 6 },
                new StaffRole { StaffId = 4, RoleId = 6 },
                new StaffRole { StaffId = 9, RoleId = 6 },
                new StaffRole { StaffId = 14, RoleId = 6 },
                new StaffRole { StaffId = 16, RoleId = 6 },
                new StaffRole { StaffId = 17, RoleId = 8 }
               );

        }

    }
}

