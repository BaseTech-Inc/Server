using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Persistence.Migrations
{
    public partial class UpdatePK3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdTipoUsuaurio",
                table: "Usuario");

            migrationBuilder.DropColumn(
                name: "IdLocal",
                table: "Pais");

            migrationBuilder.DropColumn(
                name: "IdLocal",
                table: "Marcadores");

            migrationBuilder.DropColumn(
                name: "IdUsuario",
                table: "Marcadores");

            migrationBuilder.DropColumn(
                name: "IdLocalizacaoChegada",
                table: "HistoricoUsuario");

            migrationBuilder.DropColumn(
                name: "IdLocalizacaoPartida",
                table: "HistoricoUsuario");

            migrationBuilder.DropColumn(
                name: "IdUsuario",
                table: "HistoricoUsuario");

            migrationBuilder.DropColumn(
                name: "IdDistrito",
                table: "HistoricoPrevisao");

            migrationBuilder.DropColumn(
                name: "IdLocal",
                table: "Estado");

            migrationBuilder.DropColumn(
                name: "IdCidade",
                table: "Distrito");

            migrationBuilder.DropColumn(
                name: "IdLocal",
                table: "Distrito");

            migrationBuilder.DropColumn(
                name: "IdEstado",
                table: "Cidade");

            migrationBuilder.DropColumn(
                name: "IdLocal",
                table: "Cidade");

            migrationBuilder.DropColumn(
                name: "IdDistrito",
                table: "Alerta");

            migrationBuilder.DropColumn(
                name: "IdLocal",
                table: "Alerta");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdTipoUsuaurio",
                table: "Usuario",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IdLocal",
                table: "Pais",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IdLocal",
                table: "Marcadores",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IdUsuario",
                table: "Marcadores",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IdLocalizacaoChegada",
                table: "HistoricoUsuario",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IdLocalizacaoPartida",
                table: "HistoricoUsuario",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IdUsuario",
                table: "HistoricoUsuario",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IdDistrito",
                table: "HistoricoPrevisao",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IdLocal",
                table: "Estado",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IdCidade",
                table: "Distrito",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IdLocal",
                table: "Distrito",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IdEstado",
                table: "Cidade",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IdLocal",
                table: "Cidade",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IdDistrito",
                table: "Alerta",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IdLocal",
                table: "Alerta",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
