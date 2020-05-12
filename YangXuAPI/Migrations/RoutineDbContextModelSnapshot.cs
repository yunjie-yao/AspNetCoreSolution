﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using YangXuAPI.Data;

namespace YangXuAPI.Migrations
{
    [DbContext(typeof(RoutineDbContext))]
    partial class RoutineDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("YangXuAPI.Entities.Company", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Introduction")
                        .HasColumnType("varchar(500)")
                        .HasMaxLength(500);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(100)")
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.ToTable("Companies");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Introduction = "Software Company",
                            Name = "Microsoft"
                        },
                        new
                        {
                            Id = 2,
                            Introduction = "Great Company",
                            Name = "Google"
                        },
                        new
                        {
                            Id = 3,
                            Introduction = "Mayunpapa",
                            Name = "Alipapa"
                        });
                });

            modelBuilder.Entity("YangXuAPI.Entities.Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("CompanyId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime");

                    b.Property<string>("EmployeeNo")
                        .IsRequired()
                        .HasColumnType("varchar(10)")
                        .HasMaxLength(10);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("varchar(50)")
                        .HasMaxLength(50);

                    b.Property<int>("Gender")
                        .HasColumnType("int");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("varchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.ToTable("Employees");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CompanyId = 1,
                            DateOfBirth = new DateTime(2000, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            EmployeeNo = "00001",
                            FirstName = "Nick",
                            Gender = 2,
                            LastName = "Smith"
                        },
                        new
                        {
                            Id = 2,
                            CompanyId = 1,
                            DateOfBirth = new DateTime(2000, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            EmployeeNo = "00002",
                            FirstName = "Linda",
                            Gender = 1,
                            LastName = "Trump"
                        },
                        new
                        {
                            Id = 3,
                            CompanyId = 2,
                            DateOfBirth = new DateTime(2000, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            EmployeeNo = "00003",
                            FirstName = "Crole",
                            Gender = 1,
                            LastName = "Fories"
                        },
                        new
                        {
                            Id = 4,
                            CompanyId = 2,
                            DateOfBirth = new DateTime(2000, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            EmployeeNo = "00004",
                            FirstName = "Kobe",
                            Gender = 2,
                            LastName = "Brant"
                        },
                        new
                        {
                            Id = 5,
                            CompanyId = 3,
                            DateOfBirth = new DateTime(2000, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            EmployeeNo = "00005",
                            FirstName = "Dora",
                            Gender = 1,
                            LastName = "Smith"
                        },
                        new
                        {
                            Id = 6,
                            CompanyId = 0,
                            DateOfBirth = new DateTime(2000, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            EmployeeNo = "00006",
                            FirstName = "Jack",
                            Gender = 2,
                            LastName = "Ma"
                        });
                });

            modelBuilder.Entity("YangXuAPI.Entities.Employee", b =>
                {
                    b.HasOne("YangXuAPI.Entities.Company", "Company")
                        .WithMany("Employees")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
