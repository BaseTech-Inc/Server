using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Persistence.Migrations
{
    public partial class UpdateDatabase2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Alerta_Localizacao_LocalizacaoId",
                table: "Alerta");

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
                name: "FK_Pais_Localizacao_PontoId",
                table: "Pais");

            migrationBuilder.DropIndex(
                name: "IX_Pais_PontoId",
                table: "Pais");

            migrationBuilder.DropIndex(
                name: "IX_Estado_PontoId",
                table: "Estado");

            migrationBuilder.DropIndex(
                name: "IX_Distrito_PontoId",
                table: "Distrito");

            migrationBuilder.DropIndex(
                name: "IX_Cidade_PontoId",
                table: "Cidade");

            migrationBuilder.DropColumn(
                name: "PontoId",
                table: "Pais");

            migrationBuilder.DropColumn(
                name: "PontoId",
                table: "Estado");

            migrationBuilder.DropColumn(
                name: "PontoId",
                table: "Distrito");

            migrationBuilder.DropColumn(
                name: "PontoId",
                table: "Cidade");

            migrationBuilder.RenameColumn(
                name: "LocalizacaoId",
                table: "Alerta",
                newName: "PontoId");

            migrationBuilder.RenameIndex(
                name: "IX_Alerta_LocalizacaoId",
                table: "Alerta",
                newName: "IX_Alerta_PontoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Alerta_Localizacao_PontoId",
                table: "Alerta",
                column: "PontoId",
                principalTable: "Localizacao",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Alerta_Localizacao_PontoId",
                table: "Alerta");

            migrationBuilder.RenameColumn(
                name: "PontoId",
                table: "Alerta",
                newName: "LocalizacaoId");

            migrationBuilder.RenameIndex(
                name: "IX_Alerta_PontoId",
                table: "Alerta",
                newName: "IX_Alerta_LocalizacaoId");

            migrationBuilder.AddColumn<string>(
                name: "PontoId",
                table: "Pais",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PontoId",
                table: "Estado",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PontoId",
                table: "Distrito",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PontoId",
                table: "Cidade",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pais_PontoId",
                table: "Pais",
                column: "PontoId");

            migrationBuilder.CreateIndex(
                name: "IX_Estado_PontoId",
                table: "Estado",
                column: "PontoId");

            migrationBuilder.CreateIndex(
                name: "IX_Distrito_PontoId",
                table: "Distrito",
                column: "PontoId");

            migrationBuilder.CreateIndex(
                name: "IX_Cidade_PontoId",
                table: "Cidade",
                column: "PontoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Alerta_Localizacao_LocalizacaoId",
                table: "Alerta",
                column: "LocalizacaoId",
                principalTable: "Localizacao",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

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
                name: "FK_Pais_Localizacao_PontoId",
                table: "Pais",
                column: "PontoId",
                principalTable: "Localizacao",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
