using Hogwarts.Api.Extensions;
using Hogwarts.Api.Models;
using Hogwarts.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hogwarts.Api.DbContexts
{
    public class HogwartsDbContext : DbContext
    {


        public HogwartsDbContext(DbContextOptions<HogwartsDbContext> options) : base(options)
        {

        }

        public DbSet<House> Houses { get; set; }
        public DbSet<Staff> Staff { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }        
        public DbSet<HeadOfHouse> HeadOfHouses { get; set; }
        public DbSet<StaffCourse> StaffCourse { get; set; }       
        public DbSet<Role> Roles { get; set; }
        public DbSet<StaffRole> StaffRoles { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<StaffCourse>()
                .HasKey(tc => new { tc.CourseId, tc.StaffId });
            modelBuilder.Entity<StaffCourse>()
                .HasOne(sc => sc.Staff)
                .WithMany(s => s.StaffCourse)
                .HasForeignKey(sc => sc.StaffId);
            modelBuilder.Entity<StaffCourse>()
                .HasOne(tc => tc.Course)
                .WithMany(c => c.StaffCourse)
                .HasForeignKey(sc => sc.CourseId);
            modelBuilder.Entity<StaffRole>()
                .HasKey(sr => new { sr.StaffId, sr.RoleId });
            modelBuilder.Entity<StaffRole>()
                .HasOne(sr => sr.Staff)
                .WithMany(s => s.StaffRoles)
                .HasForeignKey(sr => sr.StaffId);
            modelBuilder.Entity<StaffRole>()
                .HasOne(sr => sr.Role)
                .WithMany(s => s.StaffRoles)
                .HasForeignKey(sr => sr.RoleId);


            modelBuilder.Seed();



        }
    }
}
