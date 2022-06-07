using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class AddSeasonTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           

         
            migrationBuilder.CreateTable(
                name: "Seasons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShortName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RacingChampionshipId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seasons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Seasons_RacingChampionships_RacingChampionshipId",
                        column: x => x.RacingChampionshipId,
                        principalTable: "RacingChampionships",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });
            



            migrationBuilder.CreateIndex(
                name: "IX_Seasons_RacingChampionshipId",
                table: "Seasons",
                column: "RacingChampionshipId");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
         

            migrationBuilder.DropTable(
                name: "Seasons");
            
        }
    }
}
