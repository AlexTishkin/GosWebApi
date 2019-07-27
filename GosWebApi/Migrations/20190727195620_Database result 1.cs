using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GosWebApi.Migrations
{
    public partial class Databaseresult1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubThemes_Themes_ThemeId",
                table: "SubThemes");

            migrationBuilder.AlterColumn<Guid>(
                name: "ThemeId",
                table: "SubThemes",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Regions",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Regions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Statuses",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Reports",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Message = table.Column<string>(nullable: true),
                    FailMessage = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Mark = table.Column<int>(nullable: false),
                    MarkDescription = table.Column<string>(nullable: true),
                    CompanyId = table.Column<Guid>(nullable: true),
                    RegionId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reports_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reports_Regions_RegionId",
                        column: x => x.RegionId,
                        principalTable: "Regions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ReportStatus",
                columns: table => new
                {
                    ReportId = table.Column<Guid>(nullable: false),
                    StatusId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportStatus", x => new { x.ReportId, x.StatusId });
                    table.ForeignKey(
                        name: "FK_ReportStatus_Reports_ReportId",
                        column: x => x.ReportId,
                        principalTable: "Reports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReportStatus_Statuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Statuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reports_CompanyId",
                table: "Reports",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Reports_RegionId",
                table: "Reports",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportStatus_StatusId",
                table: "ReportStatus",
                column: "StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_SubThemes_Themes_ThemeId",
                table: "SubThemes",
                column: "ThemeId",
                principalTable: "Themes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubThemes_Themes_ThemeId",
                table: "SubThemes");

            migrationBuilder.DropTable(
                name: "ReportStatus");

            migrationBuilder.DropTable(
                name: "Reports");

            migrationBuilder.DropTable(
                name: "Statuses");

            migrationBuilder.DropTable(
                name: "Regions");

            migrationBuilder.AlterColumn<Guid>(
                name: "ThemeId",
                table: "SubThemes",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddForeignKey(
                name: "FK_SubThemes_Themes_ThemeId",
                table: "SubThemes",
                column: "ThemeId",
                principalTable: "Themes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
