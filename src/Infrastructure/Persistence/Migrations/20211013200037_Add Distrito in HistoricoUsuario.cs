using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Persistence.Migrations
{
    public partial class AddDistritoinHistoricoUsuario : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DistritoId",
                table: "HistoricoUsuario",
                type: "varchar(767)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_HistoricoUsuario_DistritoId",
                table: "HistoricoUsuario",
                column: "DistritoId");

            migrationBuilder.AddForeignKey(
                name: "FK_HistoricoUsuario_Distrito_DistritoId",
                table: "HistoricoUsuario",
                column: "DistritoId",
                principalTable: "Distrito",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HistoricoUsuario_Distrito_DistritoId",
                table: "HistoricoUsuario");

            migrationBuilder.DropIndex(
                name: "IX_HistoricoUsuario_DistritoId",
                table: "HistoricoUsuario");

            migrationBuilder.DropColumn(
                name: "DistritoId",
                table: "HistoricoUsuario");
        }
    }
}
