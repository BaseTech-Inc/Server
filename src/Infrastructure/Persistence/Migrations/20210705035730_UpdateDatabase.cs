using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Persistence.Migrations
{
    public partial class UpdateDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cidade_Localizacao_LocalizacaoId",
                table: "Cidade");

            migrationBuilder.DropForeignKey(
                name: "FK_Distrito_Localizacao_LocalizacaoId",
                table: "Distrito");

            migrationBuilder.DropForeignKey(
                name: "FK_Estado_Localizacao_LocalizacaoId",
                table: "Estado");

            migrationBuilder.DropForeignKey(
                name: "FK_HistoricoUsuario_Localizacao_LocalizacaoChegadaId",
                table: "HistoricoUsuario");

            migrationBuilder.DropForeignKey(
                name: "FK_HistoricoUsuario_Localizacao_LocalizacaoPartidaId",
                table: "HistoricoUsuario");

            migrationBuilder.DropForeignKey(
                name: "FK_Marcadores_Localizacao_LocalizacaoId",
                table: "Marcadores");

            migrationBuilder.DropForeignKey(
                name: "FK_Pais_Localizacao_LocalizacaoId",
                table: "Pais");

            migrationBuilder.RenameColumn(
                name: "Nome",
                table: "TipoUsuario",
                newName: "Descricao");

            migrationBuilder.RenameColumn(
                name: "LocalizacaoId",
                table: "Pais",
                newName: "PontoId");

            migrationBuilder.RenameIndex(
                name: "IX_Pais_LocalizacaoId",
                table: "Pais",
                newName: "IX_Pais_PontoId");

            migrationBuilder.RenameColumn(
                name: "LocalizacaoId",
                table: "Marcadores",
                newName: "PontoId");

            migrationBuilder.RenameIndex(
                name: "IX_Marcadores_LocalizacaoId",
                table: "Marcadores",
                newName: "IX_Marcadores_PontoId");

            migrationBuilder.RenameColumn(
                name: "LocalizacaoPartidaId",
                table: "HistoricoUsuario",
                newName: "PontoPartidaId");

            migrationBuilder.RenameColumn(
                name: "LocalizacaoChegadaId",
                table: "HistoricoUsuario",
                newName: "PontoChegadaId");

            migrationBuilder.RenameColumn(
                name: "DistPerc",
                table: "HistoricoUsuario",
                newName: "DistanciaPercurso");

            migrationBuilder.RenameIndex(
                name: "IX_HistoricoUsuario_LocalizacaoPartidaId",
                table: "HistoricoUsuario",
                newName: "IX_HistoricoUsuario_PontoPartidaId");

            migrationBuilder.RenameIndex(
                name: "IX_HistoricoUsuario_LocalizacaoChegadaId",
                table: "HistoricoUsuario",
                newName: "IX_HistoricoUsuario_PontoChegadaId");

            migrationBuilder.RenameColumn(
                name: "Siglas",
                table: "Estado",
                newName: "Sigla");

            migrationBuilder.RenameColumn(
                name: "LocalizacaoId",
                table: "Estado",
                newName: "PontoId");

            migrationBuilder.RenameIndex(
                name: "IX_Estado_LocalizacaoId",
                table: "Estado",
                newName: "IX_Estado_PontoId");

            migrationBuilder.RenameColumn(
                name: "LocalizacaoId",
                table: "Distrito",
                newName: "PontoId");

            migrationBuilder.RenameIndex(
                name: "IX_Distrito_LocalizacaoId",
                table: "Distrito",
                newName: "IX_Distrito_PontoId");

            migrationBuilder.RenameColumn(
                name: "LocalizacaoId",
                table: "Cidade",
                newName: "PontoId");

            migrationBuilder.RenameIndex(
                name: "IX_Cidade_LocalizacaoId",
                table: "Cidade",
                newName: "IX_Cidade_PontoId");

            migrationBuilder.AddColumn<string>(
                name: "Sigla",
                table: "Pais",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Cidade_Localizacao_PontoId",
                table: "Cidade",
                column: "PontoId",
                principalTable: "Localizacao",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Distrito_Localizacao_PontoId",
                table: "Distrito",
                column: "PontoId",
                principalTable: "Localizacao",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Estado_Localizacao_PontoId",
                table: "Estado",
                column: "PontoId",
                principalTable: "Localizacao",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_HistoricoUsuario_Localizacao_PontoChegadaId",
                table: "HistoricoUsuario",
                column: "PontoChegadaId",
                principalTable: "Localizacao",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_HistoricoUsuario_Localizacao_PontoPartidaId",
                table: "HistoricoUsuario",
                column: "PontoPartidaId",
                principalTable: "Localizacao",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Marcadores_Localizacao_PontoId",
                table: "Marcadores",
                column: "PontoId",
                principalTable: "Localizacao",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Pais_Localizacao_PontoId",
                table: "Pais",
                column: "PontoId",
                principalTable: "Localizacao",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cidade_Localizacao_PontoId",
                table: "Cidade");

            migrationBuilder.DropForeignKey(
                name: "FK_Distrito_Localizacao_PontoId",
                table: "Distrito");

            migrationBuilder.DropForeignKey(
                name: "FK_Estado_Localizacao_PontoId",
                table: "Estado");

            migrationBuilder.DropForeignKey(
                name: "FK_HistoricoUsuario_Localizacao_PontoChegadaId",
                table: "HistoricoUsuario");

            migrationBuilder.DropForeignKey(
                name: "FK_HistoricoUsuario_Localizacao_PontoPartidaId",
                table: "HistoricoUsuario");

            migrationBuilder.DropForeignKey(
                name: "FK_Marcadores_Localizacao_PontoId",
                table: "Marcadores");

            migrationBuilder.DropForeignKey(
                name: "FK_Pais_Localizacao_PontoId",
                table: "Pais");

            migrationBuilder.DropColumn(
                name: "Sigla",
                table: "Pais");

            migrationBuilder.RenameColumn(
                name: "Descricao",
                table: "TipoUsuario",
                newName: "Nome");

            migrationBuilder.RenameColumn(
                name: "PontoId",
                table: "Pais",
                newName: "LocalizacaoId");

            migrationBuilder.RenameIndex(
                name: "IX_Pais_PontoId",
                table: "Pais",
                newName: "IX_Pais_LocalizacaoId");

            migrationBuilder.RenameColumn(
                name: "PontoId",
                table: "Marcadores",
                newName: "LocalizacaoId");

            migrationBuilder.RenameIndex(
                name: "IX_Marcadores_PontoId",
                table: "Marcadores",
                newName: "IX_Marcadores_LocalizacaoId");

            migrationBuilder.RenameColumn(
                name: "PontoPartidaId",
                table: "HistoricoUsuario",
                newName: "LocalizacaoPartidaId");

            migrationBuilder.RenameColumn(
                name: "PontoChegadaId",
                table: "HistoricoUsuario",
                newName: "LocalizacaoChegadaId");

            migrationBuilder.RenameColumn(
                name: "DistanciaPercurso",
                table: "HistoricoUsuario",
                newName: "DistPerc");

            migrationBuilder.RenameIndex(
                name: "IX_HistoricoUsuario_PontoPartidaId",
                table: "HistoricoUsuario",
                newName: "IX_HistoricoUsuario_LocalizacaoPartidaId");

            migrationBuilder.RenameIndex(
                name: "IX_HistoricoUsuario_PontoChegadaId",
                table: "HistoricoUsuario",
                newName: "IX_HistoricoUsuario_LocalizacaoChegadaId");

            migrationBuilder.RenameColumn(
                name: "Sigla",
                table: "Estado",
                newName: "Siglas");

            migrationBuilder.RenameColumn(
                name: "PontoId",
                table: "Estado",
                newName: "LocalizacaoId");

            migrationBuilder.RenameIndex(
                name: "IX_Estado_PontoId",
                table: "Estado",
                newName: "IX_Estado_LocalizacaoId");

            migrationBuilder.RenameColumn(
                name: "PontoId",
                table: "Distrito",
                newName: "LocalizacaoId");

            migrationBuilder.RenameIndex(
                name: "IX_Distrito_PontoId",
                table: "Distrito",
                newName: "IX_Distrito_LocalizacaoId");

            migrationBuilder.RenameColumn(
                name: "PontoId",
                table: "Cidade",
                newName: "LocalizacaoId");

            migrationBuilder.RenameIndex(
                name: "IX_Cidade_PontoId",
                table: "Cidade",
                newName: "IX_Cidade_LocalizacaoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cidade_Localizacao_LocalizacaoId",
                table: "Cidade",
                column: "LocalizacaoId",
                principalTable: "Localizacao",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Distrito_Localizacao_LocalizacaoId",
                table: "Distrito",
                column: "LocalizacaoId",
                principalTable: "Localizacao",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Estado_Localizacao_LocalizacaoId",
                table: "Estado",
                column: "LocalizacaoId",
                principalTable: "Localizacao",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_HistoricoUsuario_Localizacao_LocalizacaoChegadaId",
                table: "HistoricoUsuario",
                column: "LocalizacaoChegadaId",
                principalTable: "Localizacao",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_HistoricoUsuario_Localizacao_LocalizacaoPartidaId",
                table: "HistoricoUsuario",
                column: "LocalizacaoPartidaId",
                principalTable: "Localizacao",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Marcadores_Localizacao_LocalizacaoId",
                table: "Marcadores",
                column: "LocalizacaoId",
                principalTable: "Localizacao",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Pais_Localizacao_LocalizacaoId",
                table: "Pais",
                column: "LocalizacaoId",
                principalTable: "Localizacao",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
