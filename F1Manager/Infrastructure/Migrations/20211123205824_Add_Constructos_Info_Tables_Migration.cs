using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class Add_Constructos_Info_Tables_Migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Constructors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShortName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Base = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChiefTechnicalOfficer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TechnicalDirector = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Website = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstEntry = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastEntry = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CountryId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Constructors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Constructors_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

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
                name: "IX_Constructors_CountryId",
                table: "Constructors",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_ConstructorsRacingDetails_ConstructorId",
                table: "ConstructorsRacingDetails",
                column: "ConstructorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConstructorsRacingDetails");

            migrationBuilder.DropTable(
                name: "Constructors");
        }
    }
}
