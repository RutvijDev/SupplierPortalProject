using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CollegeProject.Migrations
{
    /// <inheritdoc />
    public partial class addcolumntodb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "WareHouseID",
                table: "Customer",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WareHouseID",
                table: "Customer");
        }
    }
}
