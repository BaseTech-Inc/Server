using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Persistence.Migrations
{
    public partial class AddEntityCount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Created",
                table: "Ponto");

            migrationBuilder.DropColumn(
                name: "LastModified",
                table: "Ponto");

            migrationBuilder.AddColumn<int>(
                name: "Count",
                table: "Ponto",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Count",
                table: "Ponto");

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "Ponto",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModified",
                table: "Ponto",
                type: "datetime",
                nullable: true);
        }
    }
}
