using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class Add_RacingDetails_Relations_To_Constructors : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ConstructorsRacingDetailId",
                table: "Constructors",
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Constructors_ConstructorsRacingDetails_ConstructorsRacingDetailId",
                table: "Constructors");

            migrationBuilder.DropIndex(
                name: "IX_Constructors_ConstructorsRacingDetailId",
                table: "Constructors");

            migrationBuilder.DropColumn(
                name: "ConstructorsRacingDetailId",
                table: "Constructors");
        }
    }
}
