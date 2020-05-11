using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
                .Property(x => x.Name).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<Company>()
                .Property(x => x.Introduction).HasMaxLength(500);

            modelBuilder.Entity<Employee>()
                .Property(x => x.EmployeeNo).IsRequired().HasMaxLength(10);
            modelBuilder.Entity<Employee>()
                .Property(x => x.FirstName).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Employee>()
                .Property(x => x.LastName).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Employee>()
                .HasOne(x => x.Company)
                .WithMany(x => x.Employees)
                .HasForeignKey(x => x.CompanyId).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Company>().HasData(
                new Company
                {
                    Id = Guid.Parse("5CB9BEC5-3CDF-41B2-A8F1-B37F53623422"),
                    Name = "Microsoft",
                    Introduction = "Software Company"
                },
                new Company
                {
                    Id = Guid.Parse("3FFADA32-54CB-4F8F-99F6-8E1596ED93D5"),
                    Name = "Google",
                    Introduction = "Great Company"
                },
                new Company
                {
                    Id=Guid.Parse("72E05F54-B1D9-4C93-A14D-2707A48BF71C"),
                    Name="Alipapa",
                    Introduction="Mayunpapa"
                });
        }
    }
}
