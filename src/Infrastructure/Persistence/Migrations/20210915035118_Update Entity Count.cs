using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Persistence.Migrations
{
    public partial class UpdateEntityCount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Count",
                table: "Ponto");

            migrationBuilder.AddColumn<int>(
                name: "Count",
                table: "PoligonoPonto",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Count",
                table: "PoligonoPonto");

            migrationBuilder.AddColumn<int>(
                name: "Count",
                table: "Ponto",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
