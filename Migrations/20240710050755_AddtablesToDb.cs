using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CollegeProject.Migrations
{
    /// <inheritdoc />
    public partial class AddtablesToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "productData");

            migrationBuilder.DropTable(
                name: "suppliers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "User");

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "User",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "User",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "ConfirmPassword",
                table: "User",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                table: "User",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    CustomerID = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    CustomerName = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    CustomerAddress = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CustomerPhoneNumber = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    CustomerEmail = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.CustomerID);
                });

            migrationBuilder.CreateTable(
                name: "WareHouse",
                columns: table => new
                {
                    WareHouseID = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    WareHouseName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    WareHouseAddress = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WareHouse", x => x.WareHouseID);
                });

            migrationBuilder.CreateTable(
                name: "WareHouseManager",
                columns: table => new
                {
                    WareHouseManagerID = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    WareHouseManagerName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false),
                    WareHouseID = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    MobileNumber = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WareHouseManager", x => x.WareHouseManagerID);
                    table.ForeignKey(
                        name: "FK_WareHouseManager_User_Id", 
                        column: x => x.Id,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WareHouseManager_WareHouse_WareHouseID",
                        column: x => x.WareHouseID,
                        principalTable: "WareHouse",
                        principalColumn: "WareHouseId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Supplier",
                columns: table => new
                {
                    SupplierID = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    SupplierName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false),
                    WareHouseID = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    MobileNumber = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Supplier", x => x.SupplierID);
                    table.ForeignKey(
                        name: "FK_Supplier_User_Id",
                        column: x => x.Id,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Supplier_WareHouse_WareHouseId",
                        column: x => x.WareHouseID,
                        principalTable: "WareHouse",
                        principalColumn: "WareHouseId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    ProductID = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    SupplierID = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    ProductName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CompanyName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ProductCategory = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ProductDescription = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    ProductPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ProductQuantity = table.Column<int>(type: "int", nullable: false),
                    ProductImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductDateAdded = table.Column<DateOnly>(type: "date", nullable: false),
                    ProductLast_Modified = table.Column<DateOnly>(type: "date", nullable: false),
                    ProductStatus = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.ProductID);
                    table.ForeignKey(
                        name: "FK_Product_Supplier_SupplierID",
                        column: x => x.SupplierID,
                        principalTable: "Supplier",
                        principalColumn: "SupplierID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    OrderID = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    CustomerID = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProductID = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    SupplierID = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    WarehouseManagerID = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ShippingAddress = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    OrderStatus = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false)
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
                    table.ForeignKey(
                       name: "FK_Order_WareHouseManager_WareHouseManagerID",
                       column: x => x.WarehouseManagerID,
                       principalTable: "WareHouseManager",
                       principalColumn: "WarehouseManagerID",
                       onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "SupplierOrder",
                columns: table => new
                {
                    SupplierOrderID = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    OrderID = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    SupplierID = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Supplier_Order_Status = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Supplier_Ship_Date = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupplierOrder", x => x.SupplierOrderID);
                    table.ForeignKey(
                       name: "FK_SupplierOrder_Order_OrderID",
                       column: x => x.OrderID,
                       principalTable: "Order",
                       principalColumn: "OrderID",
                       onDelete: ReferentialAction.Cascade);
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
                       name: "FK_WareHouseManager_WareHouseManager_WareHouseManagerID",
                       column: x => x.WareHouseManagerID,
                       principalTable: "WareHouseManager",
                       principalColumn: "WareHouseManagerID",
                       onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "Supplier");

            migrationBuilder.DropTable(
                name: "SupplierOrder");

            migrationBuilder.DropTable(
                name: "WareHouse");

            migrationBuilder.DropTable(
                name: "WareHouseManager");

            migrationBuilder.DropTable(
                name: "WareHouseManagerOrder");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                table: "User");

            migrationBuilder.RenameTable(
                name: "User",
                newName: "Users");

            migrationBuilder.AlterColumn<string>(
                name: "Role",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(15)",
                oldMaxLength: 15);

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30);

            migrationBuilder.AlterColumn<string>(
                name: "ConfirmPassword",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "productData",
                columns: table => new
                {
                    ProductID = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Category = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CompanyName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DateAdded = table.Column<DateOnly>(type: "date", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Last_Modified = table.Column<DateOnly>(type: "date", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ProductName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    SupplierID = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_productData", x => x.ProductID);
                });

            migrationBuilder.CreateTable(
                name: "suppliers",
                columns: table => new
                {
                    SupplierID = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false),
                    SupplierName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_suppliers", x => x.SupplierID);
                });
        }
    }
}
