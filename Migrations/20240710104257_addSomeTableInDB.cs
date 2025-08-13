using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CollegeProject.Migrations
{
    /// <inheritdoc />
    public partial class addSomeTableInDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    OrderID = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    CustomerID = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    CustomerName = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProductID = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    SupplierID = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    WareHouseManagerID = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    WareHouseID = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ShippingAddress = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    OrderStatus = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Order_Delivered_Date = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.OrderID);

                    table.ForeignKey(
                    name: "FK_Order_Customer_CustomerID",
                    column: x => x.CustomerID,
                    principalTable: "Customer",
                    principalColumn: "CustomerID",
                    onDelete: ReferentialAction.Cascade);

                    table.ForeignKey(
                    name: "FK_Order_Product_ProductID",
                    column: x => x.ProductID,
                    principalTable: "Product",
                    principalColumn: "ProductID",
                    onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SupplierOrder",
                columns: table => new
                {
                    SupplierOrderID = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    OrderID = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    SupplierID = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Supplier_Order_Status = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Supplier_Ship_Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Supplier_Delivered_Date = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupplierOrder", x => x.SupplierOrderID);

                    table.ForeignKey(
                    name: "FK_SupplierOrder_Supplier_SupplierID",
                    column: x => x.SupplierID,
                    principalTable: "Supplier",
                    principalColumn: "SupplierID",
                    onDelete: ReferentialAction.Cascade);

                    table.ForeignKey(
                    name: "FK_SupplierOrder_Order_OrderID",
                    column: x => x.OrderID,
                    principalTable: "Order",
                    principalColumn: "OrderID",
                    onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "WareHouseManagerOrder",
                columns: table => new
                {
                    WareHouseOrderID = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    WareHouseManagerID = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    OrderID = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    SupplierID = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    WareHouse_Received_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    WareHouse_Order_Status = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    WareHouse_Shipment_Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Order_Delivered_Status = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Order_Delivered_Date = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                 constraints: table =>
                 {
                     table.PrimaryKey("PK_WareHouseManagerOrder", x => x.WareHouseOrderID);

                     table.ForeignKey(
                     name: "FK_WareHouseManagerOrder_Order_OrderID",
                     column: x => x.OrderID,
                     principalTable: "Order",
                     principalColumn: "OrderID",
                     onDelete: ReferentialAction.NoAction);

                     table.ForeignKey(
                     name: "FK_WareHouseManagerOrder_WareHouseManager_WareHouseManagerID",
                     column: x => x.WareHouseManagerID,
                     principalTable: "WareHouseManager",
                     principalColumn: "WareHouseManagerID",
                     onDelete: ReferentialAction.Cascade);

                     table.ForeignKey(
                     name: "FK_WareHouseManagerOrder_Supplier_SupplierID",
                     column: x => x.SupplierID,
                     principalTable: "Supplier",
                     principalColumn: "SupplierID",
                     onDelete: ReferentialAction.NoAction);

                 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "SupplierOrder");

            migrationBuilder.DropTable(
                name: "WareHouseManagerOrder");
        }
    }
}
