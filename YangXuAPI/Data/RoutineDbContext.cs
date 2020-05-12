using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YangXuAPI.Entities;
using YangXuAPI.Models;

namespace YangXuAPI.Data
{
    public class RoutineDbContext:DbContext
    {
        public RoutineDbContext(DbContextOptions<RoutineDbContext> options):base(options)
        {

        }

        public DbSet<Company> Companies { get; set; }
        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Company>()
                .Property(x => x.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Company>()
                .Property(x => x.Name).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<Company>()
                .Property(x => x.Introduction).HasMaxLength(500);

            modelBuilder.Entity<Employee>()
                .Property(x => x.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Employee>()
                .Property(x => x.EmployeeNo).IsRequired().HasMaxLength(10);
            modelBuilder.Entity<Employee>()
                .Property(x => x.FirstName).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Employee>()
                .Property(x => x.LastName).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Employee>()
                .HasOne(x => x.Company)
                .WithMany(x => x.Employees)
                .HasForeignKey(x => x.CompanyId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Company>().HasData(
                new Company
                {
                    Id = 1,
                    Name = "Microsoft",
                    Introduction = "Software Company",

                },
                new Company
                {
                    Id = 2,
                    Name = "Google",
                    Introduction = "Great Company",
                },
                new Company
                {
                    Id=3,
                    Name="Alipapa",
                    Introduction="Mayunpapa"
                });

            modelBuilder.Entity<Employee>().HasData(
                new Employee
                {
                    Id = 1,
                    EmployeeNo = "00001",
                    FirstName = "Nick",
                    LastName = "Smith",
                    DateOfBirth = new DateTime(2000, 1, 2),
                    Gender = Gender.男,
                    CompanyId = 1
                },
                new Employee
                {
                    Id = 2,
                    EmployeeNo = "00002",
                    FirstName = "Linda",
                    LastName = "Trump",
                    DateOfBirth = new DateTime(2000, 1, 2),
                    Gender = Gender.女,
                    CompanyId = 1
                },
                new Employee
                {
                    Id = 3,
                    EmployeeNo = "00003",
                    FirstName = "Crole",
                    LastName = "Fories",
                    DateOfBirth = new DateTime(2000, 1, 2),
                    Gender = Gender.女,
                    CompanyId = 2
                },
                new Employee
                {
                    Id = 4,
                    EmployeeNo = "00004",
                    FirstName = "Kobe",
                    LastName = "Brant",
                    DateOfBirth = new DateTime(2000, 1, 2),
                    Gender = Gender.男,
                    CompanyId = 2
                },
                new Employee
                {
                    Id = 5,
                    EmployeeNo = "00005",
                    FirstName = "Dora",
                    LastName = "Smith",
                    DateOfBirth = new DateTime(2000, 1, 2),
                    Gender = Gender.女,
                    CompanyId = 3
                },
                new Employee
                {
                    Id = 6,
                    EmployeeNo = "00006",
                    FirstName = "Jack",
                    LastName = "Ma",
                    DateOfBirth = new DateTime(2000, 1, 2),
                    Gender = Gender.男,
                    CompanyId = 3
                });

        }
    }
}