using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Persistence.Migrations
{
    public partial class UpdateHistoricoUsuario : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HistoricoUsuario_Ponto_PontoChegadaId",
                table: "HistoricoUsuario");

            migrationBuilder.DropForeignKey(
                name: "FK_HistoricoUsuario_Ponto_PontoPartidaId",
                table: "HistoricoUsuario");

            migrationBuilder.DropIndex(
                name: "IX_HistoricoUsuario_PontoChegadaId",
                table: "HistoricoUsuario");

            migrationBuilder.DropIndex(
                name: "IX_HistoricoUsuario_PontoPartidaId",
                table: "HistoricoUsuario");

            migrationBuilder.DropColumn(
                name: "PontoChegadaId",
                table: "HistoricoUsuario");

            migrationBuilder.DropColumn(
                name: "PontoPartidaId",
                table: "HistoricoUsuario");

            migrationBuilder.RenameColumn(
                name: "Duracao",
                table: "HistoricoUsuario",
                newName: "TempoPartida");

            migrationBuilder.AddColumn<DateTime>(
                name: "TempoChegada",
                table: "HistoricoUsuario",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TempoChegada",
                table: "HistoricoUsuario");

            migrationBuilder.RenameColumn(
                name: "TempoPartida",
                table: "HistoricoUsuario",
                newName: "Duracao");

            migrationBuilder.AddColumn<string>(
                name: "PontoChegadaId",
                table: "HistoricoUsuario",
                type: "varchar(767)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PontoPartidaId",
                table: "HistoricoUsuario",
                type: "varchar(767)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_HistoricoUsuario_PontoChegadaId",
                table: "HistoricoUsuario",
                column: "PontoChegadaId");

            migrationBuilder.CreateIndex(
                name: "IX_HistoricoUsuario_PontoPartidaId",
                table: "HistoricoUsuario",
                column: "PontoPartidaId");

            migrationBuilder.AddForeignKey(
                name: "FK_HistoricoUsuario_Ponto_PontoChegadaId",
                table: "HistoricoUsuario",
                column: "PontoChegadaId",
                principalTable: "Ponto",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_HistoricoUsuario_Ponto_PontoPartidaId",
                table: "HistoricoUsuario",
                column: "PontoPartidaId",
                principalTable: "Ponto",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
