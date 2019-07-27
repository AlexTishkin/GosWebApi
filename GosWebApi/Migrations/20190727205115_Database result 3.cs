using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GosWebApi.Migrations
{
    public partial class Databaseresult3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Datetime",
                table: "ReportStatus",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Datetime",
                table: "ReportStatus");
        }
    }
}
