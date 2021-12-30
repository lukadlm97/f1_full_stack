using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class Add_Racing_Details : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RacingChampionships",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChampionshipNameShort = table.Column<string>(nullable: true),
                    ChampionshipNameFull = table.Column<string>(nullable: true),
                    OrganisedBy = table.Column<string>(nullable: true),
                    FirstEntry = table.Column<int>(nullable: false),
                    LastEntry = table.Column<int>(nullable: false),
                    TotalCompetitions = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RacingChampionships", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ConstructorsRacingDetails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ConstructorChampionships = table.Column<int>(nullable: false),
                    DriverChampionships = table.Column<int>(nullable: false),
                    RaceVictories = table.Column<int>(nullable: false),
                    Podiums = table.Column<int>(nullable: false),
                    PolPositions = table.Column<int>(nullable: false),
                    FastesLaps = table.Column<int>(nullable: false),
                    ConstructorId = table.Column<int>(nullable: false),
                    CompetitionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConstructorsRacingDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConstructorsRacingDetails_RacingChampionships_CompetitionId",
                        column: x => x.CompetitionId,
                        principalTable: "RacingChampionships",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_ConstructorsRacingDetails_Constructors_ConstructorId",
                        column: x => x.ConstructorId,
                        principalTable: "Constructors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "DriversRacingDetails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ConstructorChampionships = table.Column<int>(nullable: false),
                    DriverChampionships = table.Column<int>(nullable: false),
                    RaceVictories = table.Column<int>(nullable: false),
                    Podiums = table.Column<int>(nullable: false),
                    PolPositions = table.Column<int>(nullable: false),
                    FastesLaps = table.Column<int>(nullable: false),
                    CompetitionId = table.Column<int>(nullable: false),
                    DriverId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DriversRacingDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DriversRacingDetails_RacingChampionships_CompetitionId",
                        column: x => x.CompetitionId,
                        principalTable: "RacingChampionships",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_DriversRacingDetails_Drivers_DriverId",
                        column: x => x.DriverId,
                        principalTable: "Drivers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ConstructorsRacingDetails_CompetitionId",
                table: "ConstructorsRacingDetails",
                column: "CompetitionId");

            migrationBuilder.CreateIndex(
                name: "IX_ConstructorsRacingDetails_ConstructorId",
                table: "ConstructorsRacingDetails",
                column: "ConstructorId");

            migrationBuilder.CreateIndex(
                name: "IX_DriversRacingDetails_CompetitionId",
                table: "DriversRacingDetails",
                column: "CompetitionId");

            migrationBuilder.CreateIndex(
                name: "IX_DriversRacingDetails_DriverId",
                table: "DriversRacingDetails",
                column: "DriverId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConstructorsRacingDetails");

            migrationBuilder.DropTable(
                name: "DriversRacingDetails");

            migrationBuilder.DropTable(
                name: "RacingChampionships");
        }
    }
}
