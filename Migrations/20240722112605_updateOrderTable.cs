using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CollegeProject.Migrations
{
    /// <inheritdoc />
    public partial class updateOrderTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DeliveryPersonID",
                table: "WareHouseManagerOrder",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeliveryPersonID",
                table: "Order",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeliveryPersonID",
                table: "WareHouseManagerOrder");

            migrationBuilder.DropColumn(
                name: "DeliveryPersonID",
                table: "Order");
        }
    }
}
