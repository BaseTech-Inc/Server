using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Persistence.Migrations
{
    public partial class UpdatePontoRisco : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DistritoId",
                table: "PontoRisco",
                type: "varchar(767)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PontoRisco_DistritoId",
                table: "PontoRisco",
                column: "DistritoId");

            migrationBuilder.AddForeignKey(
                name: "FK_PontoRisco_Distrito_DistritoId",
                table: "PontoRisco",
                column: "DistritoId",
                principalTable: "Distrito",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PontoRisco_Distrito_DistritoId",
                table: "PontoRisco");

            migrationBuilder.DropIndex(
                name: "IX_PontoRisco_DistritoId",
                table: "PontoRisco");

            migrationBuilder.DropColumn(
                name: "DistritoId",
                table: "PontoRisco");
        }
    }
}
