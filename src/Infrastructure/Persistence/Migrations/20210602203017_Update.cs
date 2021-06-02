using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Persistence.Migrations
{
    public partial class Update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Localizacao",
                table: "Localizacao");

            migrationBuilder.DropColumn(
                name: "IdLocal",
                table: "Localizacao");

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "Localizacao",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Localizacao",
                table: "Localizacao",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Pais",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IdLocal = table.Column<int>(type: "int", nullable: false),
                    LocalizacaoId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pais", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pais_Localizacao_LocalizacaoId",
                        column: x => x.LocalizacaoId,
                        principalTable: "Localizacao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TipoUsuario",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Nome = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoUsuario", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Estado",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IdLocal = table.Column<int>(type: "int", nullable: false),
                    LocalizacaoId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    IdPais = table.Column<int>(type: "int", nullable: false),
                    PaisId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estado", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Estado_Localizacao_LocalizacaoId",
                        column: x => x.LocalizacaoId,
                        principalTable: "Localizacao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Estado_Pais_PaisId",
                        column: x => x.PaisId,
                        principalTable: "Pais",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IdTipoUsuaurio = table.Column<int>(type: "int", nullable: false),
                    TipoUsuarioId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Senha = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContaBancaria = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Usuario_TipoUsuario_TipoUsuarioId",
                        column: x => x.TipoUsuarioId,
                        principalTable: "TipoUsuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Cidade",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IdLocal = table.Column<int>(type: "int", nullable: false),
                    LocalizacaoId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    IdEstado = table.Column<int>(type: "int", nullable: false),
                    EstadoId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cidade", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cidade_Estado_EstadoId",
                        column: x => x.EstadoId,
                        principalTable: "Estado",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Cidade_Localizacao_LocalizacaoId",
                        column: x => x.LocalizacaoId,
                        principalTable: "Localizacao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HistoricoUsuario",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IdUsuario = table.Column<int>(type: "int", nullable: false),
                    UsuarioId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    IdLocalizacaoChegada = table.Column<int>(type: "int", nullable: false),
                    LocalizacaoChegadaId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    IdLocalizacaoPartida = table.Column<int>(type: "int", nullable: false),
                    LocalizacaoPartidaId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DistPerc = table.Column<double>(type: "float", nullable: false),
                    Duracao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Rota = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoricoUsuario", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HistoricoUsuario_Localizacao_LocalizacaoChegadaId",
                        column: x => x.LocalizacaoChegadaId,
                        principalTable: "Localizacao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HistoricoUsuario_Localizacao_LocalizacaoPartidaId",
                        column: x => x.LocalizacaoPartidaId,
                        principalTable: "Localizacao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HistoricoUsuario_Usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Marcadores",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IdLocal = table.Column<int>(type: "int", nullable: false),
                    LocalizacaoId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    IdUsuario = table.Column<int>(type: "int", nullable: false),
                    UsuarioId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Marcadores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Marcadores_Localizacao_LocalizacaoId",
                        column: x => x.LocalizacaoId,
                        principalTable: "Localizacao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Marcadores_Usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Distrito",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IdLocal = table.Column<int>(type: "int", nullable: false),
                    LocalizacaoId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    IdCidade = table.Column<int>(type: "int", nullable: false),
                    CidadeId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Distrito", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Distrito_Cidade_CidadeId",
                        column: x => x.CidadeId,
                        principalTable: "Cidade",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Distrito_Localizacao_LocalizacaoId",
                        column: x => x.LocalizacaoId,
                        principalTable: "Localizacao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Alerta",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IdLocal = table.Column<int>(type: "int", nullable: false),
                    LocalizacaoId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    IdDistrito = table.Column<int>(type: "int", nullable: false),
                    DistritoId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Tempo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Transitividade = table.Column<bool>(type: "bit", nullable: false),
                    Atividade = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alerta", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Alerta_Distrito_DistritoId",
                        column: x => x.DistritoId,
                        principalTable: "Distrito",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Alerta_Localizacao_LocalizacaoId",
                        column: x => x.LocalizacaoId,
                        principalTable: "Localizacao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HistoricoPrevisao",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IdDistrito = table.Column<int>(type: "int", nullable: false),
                    DistritoId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Tempo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Umidade = table.Column<double>(type: "float", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TemperaturaMaxima = table.Column<double>(type: "float", nullable: false),
                    TemperaturaMinima = table.Column<double>(type: "float", nullable: false),
                    SensibilidadeTermica = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoricoPrevisao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HistoricoPrevisao_Distrito_DistritoId",
                        column: x => x.DistritoId,
                        principalTable: "Distrito",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Alerta_DistritoId",
                table: "Alerta",
                column: "DistritoId");

            migrationBuilder.CreateIndex(
                name: "IX_Alerta_LocalizacaoId",
                table: "Alerta",
                column: "LocalizacaoId");

            migrationBuilder.CreateIndex(
                name: "IX_Cidade_EstadoId",
                table: "Cidade",
                column: "EstadoId");

            migrationBuilder.CreateIndex(
                name: "IX_Cidade_LocalizacaoId",
                table: "Cidade",
                column: "LocalizacaoId");

            migrationBuilder.CreateIndex(
                name: "IX_Distrito_CidadeId",
                table: "Distrito",
                column: "CidadeId");

            migrationBuilder.CreateIndex(
                name: "IX_Distrito_LocalizacaoId",
                table: "Distrito",
                column: "LocalizacaoId");

            migrationBuilder.CreateIndex(
                name: "IX_Estado_LocalizacaoId",
                table: "Estado",
                column: "LocalizacaoId");

            migrationBuilder.CreateIndex(
                name: "IX_Estado_PaisId",
                table: "Estado",
                column: "PaisId");

            migrationBuilder.CreateIndex(
                name: "IX_HistoricoPrevisao_DistritoId",
                table: "HistoricoPrevisao",
                column: "DistritoId");

            migrationBuilder.CreateIndex(
                name: "IX_HistoricoUsuario_LocalizacaoChegadaId",
                table: "HistoricoUsuario",
                column: "LocalizacaoChegadaId");

            migrationBuilder.CreateIndex(
                name: "IX_HistoricoUsuario_LocalizacaoPartidaId",
                table: "HistoricoUsuario",
                column: "LocalizacaoPartidaId");

            migrationBuilder.CreateIndex(
                name: "IX_HistoricoUsuario_UsuarioId",
                table: "HistoricoUsuario",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Marcadores_LocalizacaoId",
                table: "Marcadores",
                column: "LocalizacaoId");

            migrationBuilder.CreateIndex(
                name: "IX_Marcadores_UsuarioId",
                table: "Marcadores",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Pais_LocalizacaoId",
                table: "Pais",
                column: "LocalizacaoId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_TipoUsuarioId",
                table: "Usuario",
                column: "TipoUsuarioId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Alerta");

            migrationBuilder.DropTable(
                name: "HistoricoPrevisao");

            migrationBuilder.DropTable(
                name: "HistoricoUsuario");

            migrationBuilder.DropTable(
                name: "Marcadores");

            migrationBuilder.DropTable(
                name: "Distrito");

            migrationBuilder.DropTable(
                name: "Usuario");

            migrationBuilder.DropTable(
                name: "Cidade");

            migrationBuilder.DropTable(
                name: "TipoUsuario");

            migrationBuilder.DropTable(
                name: "Estado");

            migrationBuilder.DropTable(
                name: "Pais");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Localizacao",
                table: "Localizacao");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Localizacao");

            migrationBuilder.AddColumn<int>(
                name: "IdLocal",
                table: "Localizacao",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Localizacao",
                table: "Localizacao",
                column: "IdLocal");
        }
    }
}
