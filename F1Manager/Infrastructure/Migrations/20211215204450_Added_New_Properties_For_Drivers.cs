using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class Added_New_Properties_For_Drivers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsRetired",
                table: "Drivers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "PathToImage",
                table: "Drivers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsRetired",
                table: "Drivers");

            migrationBuilder.DropColumn(
                name: "PathToImage",
                table: "Drivers");
        }
    }
}
