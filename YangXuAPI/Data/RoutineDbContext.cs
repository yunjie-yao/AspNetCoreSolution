using Microsoft.EntityFrameworkCore;
using System;
using YangXuAPI.Entities;

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
            modelBuilder.Entity<Company>()
                .Property(x => x.Country).HasMaxLength(50);
            modelBuilder.Entity<Company>()
                .Property(x => x.Industry).HasMaxLength(50);
            modelBuilder.Entity<Company>()
                .Property(x => x.Product).HasMaxLength(100);

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
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Company>().HasData(
                new Company
                {
                    Id = 1,
                    Name = "Microsoft",
                    Introduction = "Great Company",
                    Country = "USA",
                    Industry = "Software",
                    Product = "Software"
                },
                new Company
                {
                    Id = 2,
                    Name = "Google",
                    Introduction = "Don't be evil",
                    Country = "USA",
                    Industry = "Internet",
                    Product = "Software"
                },
                new Company
                {
                    Id = 3,
                    Name = "Alipapa",
                    Introduction = "Fubao Company",
                    Country = "China",
                    Industry = "Internet",
                    Product = "Software"
                },
                new Company
                {
                    Id = 4,
                    Name = "Tencent",
                    Introduction = "From Shenzhen",
                    Country = "China",
                    Industry = "ECommerce",
                    Product = "Software"
                },
                new Company
                {
                    Id = 5,
                    Name = "Baidu",
                    Introduction = "From Beijing",
                    Country = "China",
                    Industry = "Internet",
                    Product = "Software"
                },
                new Company
                {
                    Id = 6,
                    Name = "Adobe",
                    Introduction = "Photoshop?",
                    Country = "USA",
                    Industry = "Software",
                    Product = "Software"
                },
                new Company
                {
                    Id = 7,
                    Name = "SpaceX",
                    Introduction = "Wow",
                    Country = "USA",
                    Industry = "Technology",
                    Product = "Rocket"
                },
                new Company
                {
                    Id = 8,
                    Name = "AC Milan",
                    Introduction = "Football Club",
                    Country = "Italy",
                    Industry = "Football",
                    Product = "Football Match"
                },
                new Company
                {
                    Id = 9,
                    Name = "Suning",
                    Introduction = "From Jiangsu",
                    Country = "China",
                    Industry = "ECommerce",
                    Product = "Goods"
                },
                new Company
                {
                    Id = 10,
                    Name = "Twitter",
                    Introduction = "Blocked",
                    Country = "USA",
                    Industry = "Internet",
                    Product = "Tweets"
                },
                new Company
                {
                    Id = 11,
                    Name = "Youtube",
                    Introduction = "Blocked",
                    Country = "USA",
                    Industry = "Internet",
                    Product = "Videos"
                },
                new Company
                {
                    Id = 12,
                    Name = "360",
                    Introduction = "- -",
                    Country = "China",
                    Industry = "Security",
                    Product = "Security Product"
                },
                new Company
                {
                    Id = 13,
                    Name = "Jingdong",
                    Introduction = "Brothers",
                    Country = "China",
                    Industry = "ECommerce",
                    Product = "Goods"
                },
                new Company
                {
                    Id = 14,
                    Name = "NetEase",
                    Introduction = "Music?",
                    Country = "China",
                    Industry = "Internet",
                    Product = "Songs"
                },
                new Company
                {
                    Id = 15,
                    Name = "Amazon",
                    Introduction = "Store",
                    Country = "USA",
                    Industry = "ECommerce",
                    Product = "Books"
                },
                new Company
                {
                    Id = 16,
                    Name = "AOL",
                    Introduction = "Not Exists?",
                    Country = "USA",
                    Industry = "Internet",
                    Product = "Website"
                },
                new Company
                {
                    Id = 17,
                    Name = "Yahoo",
                    Introduction = "Who?",
                    Country = "USA",
                    Industry = "Internet",
                    Product = "Mail"
                },
                new Company
                {
                    Id = 18,
                    Name = "Firefox",
                    Introduction = "Is it a company?",
                    Country = "USA",
                    Industry = "Internet",
                    Product = "Browser"
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