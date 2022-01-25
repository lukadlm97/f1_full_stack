using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class CREATED_POWER_UNIT_SUPPLIER_PART : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PowerUnitSuppliers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SupplierName = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PowerUnitSuppliers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ConstructorPowerUnits",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ConstructorId = table.Column<int>(nullable: false),
                    PowerUnitSupplierId = table.Column<int>(nullable: false),
                    YearEstaminate = table.Column<int>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: true),
                    IsFabricConnection = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConstructorPowerUnits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConstructorPowerUnits_Constructors_ConstructorId",
                        column: x => x.ConstructorId,
                        principalTable: "Constructors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_ConstructorPowerUnits_PowerUnitSuppliers_PowerUnitSupplierId",
                        column: x => x.PowerUnitSupplierId,
                        principalTable: "PowerUnitSuppliers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ConstructorPowerUnits_ConstructorId",
                table: "ConstructorPowerUnits",
                column: "ConstructorId");

            migrationBuilder.CreateIndex(
                name: "IX_ConstructorPowerUnits_PowerUnitSupplierId",
                table: "ConstructorPowerUnits",
                column: "PowerUnitSupplierId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConstructorPowerUnits");

            migrationBuilder.DropTable(
                name: "PowerUnitSuppliers");
        }
    }
}
