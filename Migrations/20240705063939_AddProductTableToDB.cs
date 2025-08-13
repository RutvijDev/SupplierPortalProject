using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CollegeProject.Migrations
{
    /// <inheritdoc />
    public partial class AddProductTableToDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "productData",
                columns: table => new
                {
                    ProductID = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    SupplierID = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    ProductName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateAdded = table.Column<DateOnly>(type: "date", nullable: false),
                    Last_Modified = table.Column<DateOnly>(type: "date", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_productData", x => x.ProductID);
                });

            migrationBuilder.CreateTable(
                name: "suppliers",
                columns: table => new
                {
                    SupplierID = table.Column<string>(type: "nvarchar(15)", maxLength:15, nullable: false),
                    SupplierName = table.Column<string>(type: "nvarchar(50)", maxLength:50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_suppliers", x => x.SupplierID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "productData");

            migrationBuilder.DropTable(
                name: "suppliers");
        }
    }
}
