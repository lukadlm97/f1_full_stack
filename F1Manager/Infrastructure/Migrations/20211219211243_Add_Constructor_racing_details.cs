using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class Add_Constructor_racing_details : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
              name: "ConstructorsRacingDetails",
              columns: table => new
              {
                  Id = table.Column<int>(type: "int", nullable: false)
                      .Annotation("SqlServer:Identity", "1, 1"),
                  ConstructorChampionships = table.Column<int>(type: "int", nullable: false),
                  DriverChampionships = table.Column<int>(type: "int", nullable: false),
                  RaceVictories = table.Column<int>(type: "int", nullable: false),
                  Podiums = table.Column<int>(type: "int", nullable: false),
                  PolPositions = table.Column<int>(type: "int", nullable: false),
                  FastesLaps = table.Column<int>(type: "int", nullable: false),
                  ConstructorId = table.Column<int>(type: "int", nullable: false)
              },
              constraints: table =>
              {
                  table.PrimaryKey("PK_ConstructorsRacingDetails", x => x.Id);
                  table.ForeignKey(
                      name: "FK_ConstructorsRacingDetails_Constructors_ConstructorId",
                      column: x => x.ConstructorId,
                      principalTable: "Constructors",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.NoAction);
              });
            migrationBuilder.CreateIndex(
              name: "IX_ConstructorsRacingDetails_ConstructorId",
              table: "ConstructorsRacingDetails",
              column: "ConstructorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                 name: "ConstructorsRacingDetails");

        }
    }
}
