using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class Tiny_Changes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropIndex(
                name: "IX_Constructors_ConstructorsRacingDetailId",
                table: "Constructors");

            migrationBuilder.DropColumn(
                name: "ConstructorsRacingDetailId",
                table: "Constructors");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ConstructorsRacingDetailId",
                table: "Constructors",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Constructors_ConstructorsRacingDetailId",
                table: "Constructors",
                column: "ConstructorsRacingDetailId");

            migrationBuilder.AddForeignKey(
                name: "FK_Constructors_ConstructorsRacingDetails_ConstructorsRacingDetailId",
                table: "Constructors",
                column: "ConstructorsRacingDetailId",
                principalTable: "ConstructorsRacingDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }
    }
}
