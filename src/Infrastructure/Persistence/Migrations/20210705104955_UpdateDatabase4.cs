using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Persistence.Migrations
{
    public partial class UpdateDatabase4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Alerta_Localizacao_PontoId",
                table: "Alerta");

            migrationBuilder.DropForeignKey(
                name: "FK_HistoricoUsuario_Localizacao_PontoChegadaId",
                table: "HistoricoUsuario");

            migrationBuilder.DropForeignKey(
                name: "FK_HistoricoUsuario_Localizacao_PontoPartidaId",
                table: "HistoricoUsuario");

            migrationBuilder.DropForeignKey(
                name: "FK_Marcadores_Localizacao_PontoId",
                table: "Marcadores");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Localizacao",
                table: "Localizacao");

            migrationBuilder.RenameTable(
                name: "Localizacao",
                newName: "Ponto");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ponto",
                table: "Ponto",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Poligono",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Poligono", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PoligonoCidade",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PoligonoId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CidadeId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PoligonoCidade", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PoligonoCidade_Cidade_CidadeId",
                        column: x => x.CidadeId,
                        principalTable: "Cidade",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PoligonoCidade_Poligono_PoligonoId",
                        column: x => x.PoligonoId,
                        principalTable: "Poligono",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PoligonoDistrito",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PoligonoId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DistritoId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PoligonoDistrito", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PoligonoDistrito_Distrito_DistritoId",
                        column: x => x.DistritoId,
                        principalTable: "Distrito",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PoligonoDistrito_Poligono_PoligonoId",
                        column: x => x.PoligonoId,
                        principalTable: "Poligono",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PoligonoEstado",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PoligonoId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    EstadoId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PoligonoEstado", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PoligonoEstado_Estado_EstadoId",
                        column: x => x.EstadoId,
                        principalTable: "Estado",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PoligonoEstado_Poligono_PoligonoId",
                        column: x => x.PoligonoId,
                        principalTable: "Poligono",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PoligonoPais",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PoligonoId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    PaisId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PoligonoPais", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PoligonoPais_Pais_PaisId",
                        column: x => x.PaisId,
                        principalTable: "Pais",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PoligonoPais_Poligono_PoligonoId",
                        column: x => x.PoligonoId,
                        principalTable: "Poligono",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PoligonoPonto",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PoligonoId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    PontoId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PoligonoPonto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PoligonoPonto_Poligono_PoligonoId",
                        column: x => x.PoligonoId,
                        principalTable: "Poligono",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PoligonoPonto_Ponto_PontoId",
                        column: x => x.PontoId,
                        principalTable: "Ponto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PoligonoCidade_CidadeId",
                table: "PoligonoCidade",
                column: "CidadeId");

            migrationBuilder.CreateIndex(
                name: "IX_PoligonoCidade_PoligonoId",
                table: "PoligonoCidade",
                column: "PoligonoId");

            migrationBuilder.CreateIndex(
                name: "IX_PoligonoDistrito_DistritoId",
                table: "PoligonoDistrito",
                column: "DistritoId");

            migrationBuilder.CreateIndex(
                name: "IX_PoligonoDistrito_PoligonoId",
                table: "PoligonoDistrito",
                column: "PoligonoId");

            migrationBuilder.CreateIndex(
                name: "IX_PoligonoEstado_EstadoId",
                table: "PoligonoEstado",
                column: "EstadoId");

            migrationBuilder.CreateIndex(
                name: "IX_PoligonoEstado_PoligonoId",
                table: "PoligonoEstado",
                column: "PoligonoId");

            migrationBuilder.CreateIndex(
                name: "IX_PoligonoPais_PaisId",
                table: "PoligonoPais",
                column: "PaisId");

            migrationBuilder.CreateIndex(
                name: "IX_PoligonoPais_PoligonoId",
                table: "PoligonoPais",
                column: "PoligonoId");

            migrationBuilder.CreateIndex(
                name: "IX_PoligonoPonto_PoligonoId",
                table: "PoligonoPonto",
                column: "PoligonoId");

            migrationBuilder.CreateIndex(
                name: "IX_PoligonoPonto_PontoId",
                table: "PoligonoPonto",
                column: "PontoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Alerta_Ponto_PontoId",
                table: "Alerta",
                column: "PontoId",
                principalTable: "Ponto",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

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

            migrationBuilder.AddForeignKey(
                name: "FK_Marcadores_Ponto_PontoId",
                table: "Marcadores",
                column: "PontoId",
                principalTable: "Ponto",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Alerta_Ponto_PontoId",
                table: "Alerta");

            migrationBuilder.DropForeignKey(
                name: "FK_HistoricoUsuario_Ponto_PontoChegadaId",
                table: "HistoricoUsuario");

            migrationBuilder.DropForeignKey(
                name: "FK_HistoricoUsuario_Ponto_PontoPartidaId",
                table: "HistoricoUsuario");

            migrationBuilder.DropForeignKey(
                name: "FK_Marcadores_Ponto_PontoId",
                table: "Marcadores");

            migrationBuilder.DropTable(
                name: "PoligonoCidade");

            migrationBuilder.DropTable(
                name: "PoligonoDistrito");

            migrationBuilder.DropTable(
                name: "PoligonoEstado");

            migrationBuilder.DropTable(
                name: "PoligonoPais");

            migrationBuilder.DropTable(
                name: "PoligonoPonto");

            migrationBuilder.DropTable(
                name: "Poligono");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Ponto",
                table: "Ponto");

            migrationBuilder.RenameTable(
                name: "Ponto",
                newName: "Localizacao");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Localizacao",
                table: "Localizacao",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Alerta_Localizacao_PontoId",
                table: "Alerta",
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
        }
    }
}
