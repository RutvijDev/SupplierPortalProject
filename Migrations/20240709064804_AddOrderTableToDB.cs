using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CollegeProject.Migrations
{
    /// <inheritdoc />
    public partial class AddOrderTableToDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Image",
                table: "productData",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "Orderstatus",
                columns: table => new
                {
                    OrderID = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    CustomerID = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    ProductID = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    SupplierID = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Order_Date = table.Column<DateOnly>(type: "date", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Total_Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Supplier_Order_Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Supplier_Delivery_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Warehouse_Recieved_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Shipping_Address = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Warehouse_Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Warehouse_Shipment_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Customer_Delivery_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Payment_Status = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orderstatus", x => x.OrderID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Orderstatus");

            migrationBuilder.AlterColumn<string>(
                name: "Image",
                table: "productData",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
