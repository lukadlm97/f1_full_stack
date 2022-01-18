using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class SOME_LABELING_CHANGES : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConstrucotrsStuffContracts");

            migrationBuilder.DropTable(
                name: "TechnicalStuffs");

            migrationBuilder.DropTable(
                name: "TechnicalStuffRoles");

            migrationBuilder.CreateTable(
                name: "TechnicalStaff",
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
                    table.PrimaryKey("PK_TechnicalStaff", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TechnicalStaff_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "TechnicalStaffRoles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    RoleName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TechnicalStaffRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ConstrucotrsStaffContracts",
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

            migrationBuilder.CreateIndex(
                name: "IX_TechnicalStaff_CountryId",
                table: "TechnicalStaff",
                column: "CountryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConstrucotrsStaffContracts");

            migrationBuilder.DropTable(
                name: "TechnicalStaff");

            migrationBuilder.DropTable(
                name: "TechnicalStaffRoles");

            migrationBuilder.CreateTable(
                name: "TechnicalStuffRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TechnicalStuffRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TechnicalStuffs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CountryId = table.Column<int>(type: "int", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EducationDetails = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Forename = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ConstructorId = table.Column<int>(type: "int", nullable: false),
                    DateOfEnd = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateOfSign = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    TechnicalStuffId = table.Column<int>(type: "int", nullable: false),
                    TechnicalStuffRoleId = table.Column<int>(type: "int", nullable: false)
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
                    table.ForeignKey(
                        name: "FK_ConstrucotrsStuffContracts_TechnicalStuffRoles_TechnicalStuffRoleId",
                        column: x => x.TechnicalStuffRoleId,
                        principalTable: "TechnicalStuffRoles",
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
                name: "IX_ConstrucotrsStuffContracts_TechnicalStuffRoleId",
                table: "ConstrucotrsStuffContracts",
                column: "TechnicalStuffRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_TechnicalStuffs_CountryId",
                table: "TechnicalStuffs",
                column: "CountryId");
        }
    }
}
