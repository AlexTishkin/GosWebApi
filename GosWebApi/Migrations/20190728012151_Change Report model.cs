using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GosWebApi.Migrations
{
    public partial class ChangeReportmodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "SubThemeId",
                table: "Reports",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reports_SubThemeId",
                table: "Reports",
                column: "SubThemeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reports_SubThemes_SubThemeId",
                table: "Reports",
                column: "SubThemeId",
                principalTable: "SubThemes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reports_SubThemes_SubThemeId",
                table: "Reports");

            migrationBuilder.DropIndex(
                name: "IX_Reports_SubThemeId",
                table: "Reports");

            migrationBuilder.DropColumn(
                name: "SubThemeId",
                table: "Reports");
        }
    }
}
