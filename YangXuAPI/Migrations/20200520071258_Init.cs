using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.Data.EntityFrameworkCore.Metadata;

namespace YangXuAPI.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    Introduction = table.Column<string>(maxLength: 500, nullable: true),
                    Country = table.Column<string>(maxLength: 50, nullable: true),
                    Industry = table.Column<string>(maxLength: 50, nullable: true),
                    Product = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    CompanyId = table.Column<int>(nullable: false),
                    EmployeeNo = table.Column<string>(maxLength: 10, nullable: false),
                    FirstName = table.Column<string>(maxLength: 50, nullable: false),
                    LastName = table.Column<string>(maxLength: 50, nullable: false),
                    Gender = table.Column<int>(nullable: false),
                    DateOfBirth = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employees_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "Country", "Industry", "Introduction", "Name", "Product" },
                values: new object[,]
                {
                    { 1, "USA", "Software", "Great Company", "Microsoft", "Software" },
                    { 16, "USA", "Internet", "Not Exists?", "AOL", "Website" },
                    { 15, "USA", "ECommerce", "Store", "Amazon", "Books" },
                    { 14, "China", "Internet", "Music?", "NetEase", "Songs" },
                    { 13, "China", "ECommerce", "Brothers", "Jingdong", "Goods" },
                    { 12, "China", "Security", "- -", "360", "Security Product" },
                    { 11, "USA", "Internet", "Blocked", "Youtube", "Videos" },
                    { 10, "USA", "Internet", "Blocked", "Twitter", "Tweets" },
                    { 9, "China", "ECommerce", "From Jiangsu", "Suning", "Goods" },
                    { 8, "Italy", "Football", "Football Club", "AC Milan", "Football Match" },
                    { 7, "USA", "Technology", "Wow", "SpaceX", "Rocket" },
                    { 6, "USA", "Software", "Photoshop?", "Adobe", "Software" },
                    { 5, "China", "Internet", "From Beijing", "Baidu", "Software" },
                    { 4, "China", "ECommerce", "From Shenzhen", "Tencent", "Software" },
                    { 3, "China", "Internet", "Fubao Company", "Alipapa", "Software" },
                    { 2, "USA", "Internet", "Don't be evil", "Google", "Software" },
                    { 17, "USA", "Internet", "Who?", "Yahoo", "Mail" },
                    { 18, "USA", "Internet", "Is it a company?", "Firefox", "Browser" }
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_Employees_CompanyId",
                table: "Employees",
                column: "CompanyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Companies");
        }
    }
}
