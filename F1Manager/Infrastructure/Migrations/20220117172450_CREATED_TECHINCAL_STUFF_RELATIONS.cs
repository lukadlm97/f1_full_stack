using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class CREATED_TECHINCAL_STUFF_RELATIONS : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TechnicalStuffs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Forename = table.Column<string>(nullable: true),
                    Surname = table.Column<string>(nullable: true),
                    DateOfBirth = table.Column<DateTime>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    EducationDetails = table.Column<string>(nullable: true),
                    CountryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TechnicalStuffs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TechnicalStuffs_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "ConstrucotrsStuffContracts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DateOfSign = table.Column<DateTime>(nullable: false),
                    DateOfEnd = table.Column<DateTime>(nullable: false),
                    ConstructorId = table.Column<int>(nullable: false),
                    TechnicalStuffId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConstrucotrsStuffContracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConstrucotrsStuffContracts_Constructors_ConstructorId",
                        column: x => x.ConstructorId,
                        principalTable: "Constructors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_ConstrucotrsStuffContracts_TechnicalStuffs_TechnicalStuffId",
                        column: x => x.TechnicalStuffId,
                        principalTable: "TechnicalStuffs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ConstrucotrsStuffContracts_ConstructorId",
                table: "ConstrucotrsStuffContracts",
                column: "ConstructorId");

            migrationBuilder.CreateIndex(
                name: "IX_ConstrucotrsStuffContracts_TechnicalStuffId",
                table: "ConstrucotrsStuffContracts",
                column: "TechnicalStuffId");

            migrationBuilder.CreateIndex(
                name: "IX_TechnicalStuffs_CountryId",
                table: "TechnicalStuffs",
                column: "CountryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConstrucotrsStuffContracts");

            migrationBuilder.DropTable(
                name: "TechnicalStuffs");
        }
    }
}
