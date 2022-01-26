using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class ADD_COUNTRY_TO_POWER_UNIT_SUPPLIER : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CountryId",
                table: "PowerUnitSuppliers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PowerUnitSuppliers_CountryId",
                table: "PowerUnitSuppliers",
                column: "CountryId");

            migrationBuilder.AddForeignKey(
                name: "FK_PowerUnitSuppliers_Countries_CountryId",
                table: "PowerUnitSuppliers",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id",
        
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PowerUnitSuppliers_Countries_CountryId",
                table: "PowerUnitSuppliers");

            migrationBuilder.DropIndex(
                name: "IX_PowerUnitSuppliers_CountryId",
                table: "PowerUnitSuppliers");

            migrationBuilder.DropColumn(
                name: "CountryId",
                table: "PowerUnitSuppliers");
        }
    }
}
