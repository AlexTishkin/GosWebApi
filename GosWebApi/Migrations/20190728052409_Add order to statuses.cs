using Microsoft.EntityFrameworkCore.Migrations;

namespace GosWebApi.Migrations
{
    public partial class Addordertostatuses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "Statuses",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Order",
                table: "Statuses");
        }
    }
}
