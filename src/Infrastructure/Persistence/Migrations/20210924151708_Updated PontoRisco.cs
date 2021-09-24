using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Persistence.Migrations
{
    public partial class UpdatedPontoRisco : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PontoRisco",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(767)", nullable: false),
                    Descricao = table.Column<string>(type: "text", nullable: true),
                    DistritoId = table.Column<string>(type: "varchar(767)", nullable: true),
                    PontoId = table.Column<string>(type: "varchar(767)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PontoRisco", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PontoRisco_Distrito_DistritoId",
                        column: x => x.DistritoId,
                        principalTable: "Distrito",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PontoRisco_Ponto_PontoId",
                        column: x => x.PontoId,
                        principalTable: "Ponto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PontoRisco_DistritoId",
                table: "PontoRisco",
                column: "DistritoId");

            migrationBuilder.CreateIndex(
                name: "IX_PontoRisco_PontoId",
                table: "PontoRisco",
                column: "PontoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PontoRisco");
        }
    }
}
