using Microsoft.EntityFrameworkCore.Migrations;

namespace GosWebApi.Migrations
{
    public partial class Databaseresult4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsCommercial",
                table: "Companies",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCommercial",
                table: "Companies");
        }
    }
}
