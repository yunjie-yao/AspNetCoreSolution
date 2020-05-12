using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace YangXuAPI.Migrations
{
    public partial class InitialMiration1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "CompanyId", "DateOfBirth", "EmployeeNo", "FirstName", "Gender", "LastName" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2000, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "00001", "Nick", 2, "Smith" },
                    { 2, 1, new DateTime(2000, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "00002", "Linda", 1, "Trump" },
                    { 3, 2, new DateTime(2000, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "00003", "Crole", 1, "Fories" },
                    { 4, 2, new DateTime(2000, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "00004", "Kobe", 2, "Brant" },
                    { 5, 3, new DateTime(2000, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "00005", "Dora", 1, "Smith" },
                    { 6, 3, new DateTime(2000, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "00006", "Jack", 2, "Ma" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 6);
        }
    }
}
