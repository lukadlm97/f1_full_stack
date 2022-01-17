using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class CREATED_TECHINCAL_STUFF_ROLE_RELATIONS : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChiefTechnicalOfficer",
                table: "Constructors");

            migrationBuilder.DropColumn(
                name: "TechnicalDirector",
                table: "Constructors");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateOfEnd",
                table: "ConstrucotrsStuffContracts",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<int>(
                name: "TechnicalStuffRoleId",
                table: "ConstrucotrsStuffContracts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "TechnicalStuffRoles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    RoleName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TechnicalStuffRoles", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ConstrucotrsStuffContracts_TechnicalStuffRoleId",
                table: "ConstrucotrsStuffContracts",
                column: "TechnicalStuffRoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_ConstrucotrsStuffContracts_TechnicalStuffRoles_TechnicalStuffRoleId",
                table: "ConstrucotrsStuffContracts",
                column: "TechnicalStuffRoleId",
                principalTable: "TechnicalStuffRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConstrucotrsStuffContracts_TechnicalStuffRoles_TechnicalStuffRoleId",
                table: "ConstrucotrsStuffContracts");

            migrationBuilder.DropTable(
                name: "TechnicalStuffRoles");

            migrationBuilder.DropIndex(
                name: "IX_ConstrucotrsStuffContracts_TechnicalStuffRoleId",
                table: "ConstrucotrsStuffContracts");

            migrationBuilder.DropColumn(
                name: "TechnicalStuffRoleId",
                table: "ConstrucotrsStuffContracts");

            migrationBuilder.AddColumn<string>(
                name: "ChiefTechnicalOfficer",
                table: "Constructors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TechnicalDirector",
                table: "Constructors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateOfEnd",
                table: "ConstrucotrsStuffContracts",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);
        }
    }
}
