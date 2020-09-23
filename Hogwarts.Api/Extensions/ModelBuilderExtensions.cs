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
            new House { Id = 4, Name = "Ravenclaw", MascotImageLink = "https://s1.thcdn.com/productimg/1600/1600/12024630-1024653879759849.jpg" },
             new House { Id = 5, Name = "Unknown" }
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
                 new Student { Id = 13, FirstName = "Graham", LastName = "Pritchard", HouseId = 2 },
                 new Student { Id = 14, FirstName = "Malcolm", LastName = "Baddock", HouseId = 2 },
                 new Student { Id = 15, FirstName = "Astoria", LastName = "Greengrass", HouseId = 2 },
                 new Student { Id = 16, FirstName = "Theodore", LastName = "Nott", HouseId = 2 },
                 new Student { Id = 17, FirstName = "Daphne", LastName = "Greengrass", HouseId = 2 },
                 new Student { Id = 18, FirstName = "Lilian", LastName = "Moon", HouseId = 2 },
                 new Student { Id = 19, FirstName = "Tracey", LastName = "Davis", HouseId = 2 },
                 new Student { Id = 20, FirstName = "Adrian", LastName = "Pucey", HouseId = 2 },
                 new Student { Id = 21, FirstName = "Terence", LastName = "Higgs", HouseId = 2 },
                 new Student { Id = 22, FirstName = "Rose", LastName = "Zeller", HouseId = 3 },
                 new Student { Id = 23, FirstName = "Kevin", LastName = "Whitby", HouseId = 3 },
                 new Student { Id = 24, FirstName = "Laura", LastName = "Madley", HouseId = 3 },
                 new Student { Id = 25, FirstName = "Megan", LastName = "Jones", HouseId = 3 },
                 new Student { Id = 26, FirstName = "Wayne", LastName = "Hopkins", HouseId = 3 },
                 new Student { Id = 27, FirstName = "Owen", LastName = "Cauldwell", HouseId = 3 },
                 new Student { Id = 28, FirstName = "Eleanor", LastName = "Branstone", HouseId = 3 },
                 new Student { Id = 30, FirstName = "Stewart", LastName = "Ackerley", HouseId = 4 },
                 new Student { Id = 31, FirstName = "Orla", LastName = "Quirke", HouseId = 4 },
                 new Student { Id = 32, FirstName = "Luna", LastName = "Lovegood", HouseId = 4 },
                 new Student { Id = 33, FirstName = "Lisa", LastName = "Turpin", HouseId = 4 },
                 new Student { Id = 34, FirstName = "Padma", LastName = "Patil", HouseId = 4 },
                 new Student { Id = 35, FirstName = "Morag", LastName = "McDougal", HouseId = 4 },
                 new Student { Id = 36, FirstName = "Su", LastName = "Li", HouseId = 4 },
                 new Student { Id = 37, FirstName = "Kevin", LastName = "Entwhistle", HouseId = 5 },
                 new Student { Id = 38, FirstName = "Stephen", LastName = "Cornfoot", HouseId = 4 },
                 new Student { Id = 39, FirstName = "Michael", LastName = "Corner", HouseId = 4 },
                 new Student { Id = 40, FirstName = "Mandy", LastName = "Brocklehurst", HouseId = 4 },
                 new Student { Id = 41, FirstName = "Marietta", LastName = "Edgecombe", HouseId = 4 },
                 new Student { Id = 42, FirstName = "Eddie", LastName = "Carmichael", HouseId = 4 },
                 new Student { Id = 43, FirstName = "Roger", LastName = "Davies", HouseId = 4 },
                 new Student { Id = 44, FirstName = "Penelope", LastName = "Clearwater", HouseId = 4 },
                 new Student { Id = 45, FirstName = "Mary", LastName = "MacDonald", HouseId = 1 },
                 new Student { Id = 46, FirstName = "Euan", LastName = "Abercrombie", HouseId = 1 },
                 new Student { Id = 47, FirstName = "Jimmy", LastName = "Peakes", HouseId = 1 },
                 new Student { Id = 48, FirstName = "Natalie", LastName = "McDonald", HouseId = 1 },
                 new Student { Id = 49, FirstName = "Romilda", LastName = "Vane", HouseId = 1 },
                 new Student { Id = 50, FirstName = "Ginny", LastName = "Weasley", HouseId = 1 },
                 new Student { Id = 51, FirstName = "Neville", LastName = "Longbottom", HouseId = 1 },
                 new Student { Id = 52, FirstName = "Cormac", LastName = "McLaggen", HouseId = 1 },
                 new Student { Id = 53, FirstName = "Demelza", LastName = "Robins", HouseId = 1 },
                 new Student { Id = 54, FirstName = "Andrew", LastName = "Kirke", HouseId = 1 },
                 new Student { Id = 55, FirstName = "Geoffrey", LastName = "Hooper", HouseId = 1 },
                 new Student { Id = 56, FirstName = "Victoria", LastName = "Frobisher", HouseId = 1 },
                 new Student { Id = 57, FirstName = "Jack", LastName = "Sloper", HouseId = 1 },
                 new Student { Id = 58, FirstName = "Ritchie", LastName = "Coote", HouseId = 1 },
                 new Student { Id = 59, FirstName = "Eloise", LastName = "Midgen", HouseId = 5 },
                 new Student { Id = 60, FirstName = "Dennis", LastName = "Creevey", HouseId = 1 },
                 new Student { Id = 61, FirstName = "Gregory", LastName = "Goyle", HouseId = 2 },
                 new Student { Id = 62, FirstName = "Vincent", LastName = "Crabbe", HouseId = 2 },
                 new Student { Id = 63, FirstName = "Ernie", LastName = "Macmillan", HouseId = 3 },
                 new Student { Id = 64, FirstName = "Millicent", LastName = "Bulstrode", HouseId = 2 },
                 new Student { Id = 65, FirstName = "Dean", LastName = "Thomas", HouseId = 1 },
                 new Student { Id = 66, FirstName = "Blaise", LastName = "Zabini", HouseId = 2 },
                 new Student { Id = 67, FirstName = "Zacharias", LastName = "Smith", HouseId = 3 },
                 new Student { Id = 68, FirstName = "Pansy", LastName = "Parkinson", HouseId = 2 },
                 new Student { Id = 69, FirstName = "Seamus", LastName = "Finnigan", HouseId = 1 },
                 new Student { Id = 70, FirstName = "Colin", LastName = "Creevey", HouseId = 1 },
                 new Student { Id = 71, FirstName = "Hermione", LastName = "Granger", HouseId = 1 },
                 new Student { Id = 72, FirstName = "Harry", LastName = "Potter", HouseId = 1 }
                );

            modelBuilder.Entity<Staff>().HasData(
            new Staff { Id = 1, Gender = Gender.Male, FirstName = "Albus", MiddleNames = "Percival Wulfric Brian", LastName = "Dumbledore" },
            new Staff { Id = 2, Gender = Gender.Female, FirstName = "Minerva", LastName = "McGonagall" },
            new Staff { Id = 3, Gender = Gender.Female, FirstName = "Sybill", MiddleNames = "Patricia", LastName = "Trelawny" },
            new Staff { Id = 4, Gender = Gender.Male, FirstName = "Severus", LastName = "Snape" },
            new Staff { Id = 5, Gender = Gender.Male, FirstName = "Cuthbert", LastName = "Binns" },
            new Staff { Id = 6, Gender = Gender.Female, FirstName = "Charity", LastName = "Burbage" },
            new Staff { Id = 7, Gender = Gender.Female, FirstName = "Alecto", LastName = "Carrow" },
            new Staff { Id = 8, Gender = Gender.Male, FirstName = "Remus", LastName = "Lupin" },
            new Staff { Id = 9, Gender = Gender.Male, FirstName = "Filius", LastName = "Flitwick" },
            new Staff { Id = 10, Gender = Gender.Male, FirstName = "Alastor", LastName = "Moody" },
            new Staff { Id = 11, Gender = Gender.Female, FirstName = "Wilhelmina", LastName = "Grubbly-Plank" },
            new Staff { Id = 12, Gender = Gender.Male, FirstName = "Rubeus", LastName = "Hagrid" },
            new Staff { Id = 13, Gender = Gender.Female, FirstName = "Rolanda", LastName = "Hooch" },
            new Staff { Id = 14, Gender = Gender.Male, FirstName = "Horace", LastName = "Slughorn" },
            new Staff { Id = 15, Gender = Gender.Male, FirstName = "Silvanus", LastName = "Kettleburn" },
            new Staff { Id = 16, Gender = Gender.Female, FirstName = "Pomona", LastName = "Sprout" },
            new Staff { Id = 17, Gender = Gender.Male, FirstName = "Argus", LastName = "Filch" },
            new Staff { Id = 18, Gender = Gender.Male, FirstName = "Gilderoy", LastName = "Lockhart" },
            new Staff { Id = 19, Gender = Gender.Male, FirstName = "Quirinus", LastName = "Quirrell" },
            new Staff { Id = 20, Gender = Gender.Female, FirstName = "Poppy", LastName = "Pomfrey" },
            new Staff { Id = 21, Gender = Gender.Male, FirstName = "Amycus", LastName = "Carrow" },
            new Staff { Id = 23, Gender = Gender.Male, FirstName = "Firenze", LastName = "" },
            new Staff { Id = 24, Gender = Gender.Female, FirstName = "Irma", LastName = "Pince" },
            new Staff { Id = 25, Gender = Gender.Female, FirstName = "Aurora", LastName = "Sinistra" },
            new Staff { Id = 26, Gender = Gender.Female, FirstName = "Dolores", LastName = "Umbridge" },
            new Staff { Id = 27, Gender = Gender.Female, FirstName = "Septima", LastName = "Vector" });

            //Seed Courses
            modelBuilder.Entity<Course>().HasData(
                new Course { Id = 1, Name = "History of Magic", Level = Level.Core },
                new Course { Id = 2, Name = "Muggle Studies", Level = Level.Elective },
                new Course { Id = 3, Name = "Transfiguration", Level = Level.Core },
                new Course { Id = 4, Name = "Care of Magical Creatures", Level = Level.Elective },
                new Course { Id = 5, Name = "Defence Against the Dark Arts", Level = Level.Core },
                new Course { Id = 6, Name = "Flying", Level = Level.ExtraCurricular },
                new Course { Id = 7, Name = "Charms", Level = Level.Core, },
                new Course { Id = 8, Name = "Astronomy", Level = Level.Core },
                new Course { Id = 9, Name = "Divination", Level = Level.Elective },
                new Course { Id = 10, Name = "Herbology", Level = Level.Core },
                new Course { Id = 11, Name = "Divination", Level = Level.Elective },
                new Course { Id = 12, Name = "Potions", Level = Level.Core },
                new Course { Id = 13, Name = "Alchemy", Level = Level.Elective },
                new Course { Id = 14, Name = "Arithmancy", Level = Level.Elective },
                new Course { Id = 15, Name = "Study of Ancient Runes", Level = Level.Elective },
                new Course { Id = 16, Name = "Apparition", Level = Level.ExtraCurricular },
                new Course { Id = 17, Name = "Advanced Arithmancy Studies", Level = Level.ExtraCurricular },
                new Course { Id = 18, Name = "Ancient Studies", Level = Level.ExtraCurricular },
                new Course { Id = 19, Name = "Art", Level = Level.ExtraCurricular },
                new Course { Id = 20, Name = "Ghoul Studies", Level = Level.ExtraCurricular },
                new Course { Id = 21, Name = "Magical Theory", Level = Level.ExtraCurricular },
                new Course { Id = 22, Name = "Muggle Art", Level = Level.ExtraCurricular },
                new Course { Id = 23, Name = "Muggle Music", Level = Level.ExtraCurricular },
                new Course { Id = 24, Name = "Music", Level = Level.ExtraCurricular },
                new Course { Id = 25, Name = "Xylomancy", Level = Level.ExtraCurricular }
                );

            modelBuilder.Entity<HeadOfHouse>().HasData(
                new HeadOfHouse { Id = 1, HouseId = 1, StaffId = 1 },
                new HeadOfHouse { Id = 2, HouseId = 1, StaffId = 2 },
                new HeadOfHouse { Id = 3, HouseId = 2, StaffId = 4 },
                new HeadOfHouse { Id = 4, HouseId = 2, StaffId = 14 },
                new HeadOfHouse { Id = 5, HouseId = 4, StaffId = 9 },
                new HeadOfHouse { Id = 6, HouseId = 3, StaffId = 16 }
                );


            modelBuilder.Entity<StaffCourse>().HasData(
                new StaffCourse { CourseId = 3, StaffId = 1 },
                new StaffCourse { CourseId = 8, StaffId = 25 },
                new StaffCourse { CourseId = 5, StaffId = 1 },
                new StaffCourse { CourseId = 3, StaffId = 2 },
                new StaffCourse { CourseId = 1, StaffId = 5 },
                new StaffCourse { CourseId = 2, StaffId = 6 },
                new StaffCourse { CourseId = 4, StaffId = 11 },
                new StaffCourse { CourseId = 5, StaffId = 8 },
                new StaffCourse { CourseId = 5, StaffId = 4 },
                new StaffCourse { CourseId = 2, StaffId = 7 },
                new StaffCourse { CourseId = 5, StaffId = 21 },
                new StaffCourse { CourseId = 11, StaffId = 23 },
                new StaffCourse { CourseId = 7, StaffId = 9 },
                new StaffCourse { CourseId = 4, StaffId = 12 },
                new StaffCourse { CourseId = 6, StaffId = 13 },
                new StaffCourse { CourseId = 4, StaffId = 15 },
                new StaffCourse { CourseId = 5, StaffId = 18 },
                new StaffCourse { CourseId = 2, StaffId = 19 },
                new StaffCourse { CourseId = 5, StaffId = 19 },
                new StaffCourse { CourseId = 12, StaffId = 14 },
                new StaffCourse { CourseId = 12, StaffId = 4 },
                new StaffCourse { CourseId = 9, StaffId = 3 },
                new StaffCourse { CourseId = 5, StaffId = 26 },
                new StaffCourse { CourseId = 14, StaffId = 27 },
                new StaffCourse { CourseId = 5, StaffId = 10 }
                );

            modelBuilder.Entity<Role>().HasData(
                new Role { Id = 1, Name = "Headmaster/Headmistress" },
                new Role { Id = 2, Name = "Deputy Headmaster/Headmistress" },
                new Role { Id = 3, Name = "Professor" },
                new Role { Id = 4, Name = "Patron/Matron" },
                new Role { Id = 5, Name = "Grounds Keeper" },
                new Role { Id = 6, Name = "House Head" },
                new Role { Id = 7, Name = "Librarian" },
                new Role { Id = 8, Name = "Caretaker" }
                );
            modelBuilder.Entity<StaffRole>().HasData(
                new StaffRole { StaffId = 1, RoleId = 1 },
                new StaffRole { StaffId = 1, RoleId = 3 },
                new StaffRole { StaffId = 2, RoleId = 1 },
                new StaffRole { StaffId = 4, RoleId = 1 },
                new StaffRole { StaffId = 7, RoleId = 2 },
                new StaffRole { StaffId = 2, RoleId = 2 },
                new StaffRole { StaffId = 9, RoleId = 3 },
                new StaffRole { StaffId = 7, RoleId = 3 },
                new StaffRole { StaffId = 21, RoleId = 3 },
                new StaffRole { StaffId = 21, RoleId = 2 },
                new StaffRole { StaffId = 6, RoleId = 3 },
                new StaffRole { StaffId = 5, RoleId = 3 },
                new StaffRole { StaffId = 4, RoleId = 3 },
                new StaffRole { StaffId = 3, RoleId = 3 },
                new StaffRole { StaffId = 2, RoleId = 3 },                
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
                new StaffRole { StaffId = 18, RoleId = 3 },
                new StaffRole { StaffId = 23, RoleId = 3 },
                new StaffRole { StaffId = 8, RoleId = 3 },
                new StaffRole { StaffId = 24, RoleId = 7 },
                new StaffRole { StaffId = 20, RoleId = 4 },
                new StaffRole { StaffId = 19, RoleId = 3 },
                new StaffRole { StaffId = 25, RoleId = 3 },
                 new StaffRole { StaffId = 26, RoleId = 1 },
                new StaffRole { StaffId = 26, RoleId = 3 },
                new StaffRole { StaffId = 17, RoleId = 8 },
                new StaffRole { StaffId = 27, RoleId = 3 }
               );

        }

    }
}

