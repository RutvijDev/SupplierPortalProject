using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CollegeProject.Migrations
{
    /// <inheritdoc />
    public partial class updateOrderTableInDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "WarehouseManagerID",
                table: "Order",
                newName: "WareHouseManagerID");

            migrationBuilder.AddColumn<string>(
                name: "CustomerName",
                table: "Order",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "WareHouseID",
                table: "Order",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustomerName",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "WareHouseID",
                table: "Order");

            migrationBuilder.RenameColumn(
                name: "SupplierID",
                table: "WareHouseManagerOrder",
                newName: "SupplierId");

            migrationBuilder.RenameColumn(
                name: "WareHouse_Order_Status",
                table: "WareHouseManagerOrder",
                newName: "WareHouse_Ordered_Status");

            migrationBuilder.RenameColumn(
                name: "OrderID",
                table: "WareHouseManagerOrder",
                newName: "OrdererID");

            migrationBuilder.RenameColumn(
                name: "WareHouseManagerID",
                table: "Order",
                newName: "WarehouseManagerID");

            migrationBuilder.AlterColumn<string>(
                name: "WareHouseID",
                table: "Supplier",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(15)",
                oldMaxLength: 15,
                oldNullable: true);
        }
    }
}
