using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class Add_Contracts_Logic : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DriverRoles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(nullable: true),
                    IsValid = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DriverRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Contract",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true),
                    EstaminateValue = table.Column<double>(nullable: false),
                    EstaminateYears = table.Column<int>(nullable: false),
                    EndOfContactDate = table.Column<DateTime>(nullable: false),
                    StartOfContactDate = table.Column<DateTime>(nullable: false),
                    ConstructorId = table.Column<int>(nullable: false),
                    DriverId = table.Column<int>(nullable: false),
                    DriverRolesId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contract", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contract_Constructors_ConstructorId",
                        column: x => x.ConstructorId,
                        principalTable: "Constructors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Contract_Drivers_DriverId",
                        column: x => x.DriverId,
                        principalTable: "Drivers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Contract_DriverRoles_DriverRolesId",
                        column: x => x.DriverRolesId,
                        principalTable: "DriverRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contract_ConstructorId",
                table: "Contract",
                column: "ConstructorId");

            migrationBuilder.CreateIndex(
                name: "IX_Contract_DriverId",
                table: "Contract",
                column: "DriverId");

            migrationBuilder.CreateIndex(
                name: "IX_Contract_DriverRolesId",
                table: "Contract",
                column: "DriverRolesId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contract");

            migrationBuilder.DropTable(
                name: "DriverRoles");
        }
    }
}
