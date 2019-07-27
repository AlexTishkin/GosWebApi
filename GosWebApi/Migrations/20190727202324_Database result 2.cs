using Microsoft.EntityFrameworkCore.Migrations;

namespace GosWebApi.Migrations
{
    public partial class Databaseresult2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Reports",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Reports",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MiddleName",
                table: "Reports",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Reports");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Reports");

            migrationBuilder.DropColumn(
                name: "MiddleName",
                table: "Reports");
        }
    }
}
