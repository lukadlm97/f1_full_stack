using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class TINY_LABELING_CHANGES : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConstrucotrsStaffContracts");

            migrationBuilder.CreateTable(
                name: "ConstructorsStaffContracts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DateOfSign = table.Column<DateTime>(nullable: false),
                    DateOfEnd = table.Column<DateTime>(nullable: true),
                    ConstructorId = table.Column<int>(nullable: false),
                    TechnicalStaffId = table.Column<int>(nullable: false),
                    TechnicalStaffRoleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConstructorsStaffContracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConstructorsStaffContracts_Constructors_ConstructorId",
                        column: x => x.ConstructorId,
                        principalTable: "Constructors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_ConstructorsStaffContracts_TechnicalStaff_TechnicalStaffId",
                        column: x => x.TechnicalStaffId,
                        principalTable: "TechnicalStaff",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_ConstructorsStaffContracts_TechnicalStaffRoles_TechnicalStaffRoleId",
                        column: x => x.TechnicalStaffRoleId,
                        principalTable: "TechnicalStaffRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ConstructorsStaffContracts_ConstructorId",
                table: "ConstructorsStaffContracts",
                column: "ConstructorId");

            migrationBuilder.CreateIndex(
                name: "IX_ConstructorsStaffContracts_TechnicalStaffId",
                table: "ConstructorsStaffContracts",
                column: "TechnicalStaffId");

            migrationBuilder.CreateIndex(
                name: "IX_ConstructorsStaffContracts_TechnicalStaffRoleId",
                table: "ConstructorsStaffContracts",
                column: "TechnicalStaffRoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConstructorsStaffContracts");

            migrationBuilder.CreateTable(
                name: "ConstrucotrsStaffContracts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ConstructorId = table.Column<int>(type: "int", nullable: false),
                    DateOfEnd = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateOfSign = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    TechnicalStaffId = table.Column<int>(type: "int", nullable: false),
                    TechnicalStaffRoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConstrucotrsStaffContracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConstrucotrsStaffContracts_Constructors_ConstructorId",
                        column: x => x.ConstructorId,
                        principalTable: "Constructors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_ConstrucotrsStaffContracts_TechnicalStaff_TechnicalStaffId",
                        column: x => x.TechnicalStaffId,
                        principalTable: "TechnicalStaff",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_ConstrucotrsStaffContracts_TechnicalStaffRoles_TechnicalStaffRoleId",
                        column: x => x.TechnicalStaffRoleId,
                        principalTable: "TechnicalStaffRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ConstrucotrsStaffContracts_ConstructorId",
                table: "ConstrucotrsStaffContracts",
                column: "ConstructorId");

            migrationBuilder.CreateIndex(
                name: "IX_ConstrucotrsStaffContracts_TechnicalStaffId",
                table: "ConstrucotrsStaffContracts",
                column: "TechnicalStaffId");

            migrationBuilder.CreateIndex(
                name: "IX_ConstrucotrsStaffContracts_TechnicalStaffRoleId",
                table: "ConstrucotrsStaffContracts",
                column: "TechnicalStaffRoleId");
        }
    }
}
