using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Persistence.Migrations
{
    public partial class UpdateUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Usuario");

            migrationBuilder.DropColumn(
                name: "Nome",
                table: "Usuario");

            migrationBuilder.DropColumn(
                name: "Senha",
                table: "Usuario");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Usuario",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_ApplicationUserId",
                table: "Usuario",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Usuario_AspNetUsers_ApplicationUserId",
                table: "Usuario",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usuario_AspNetUsers_ApplicationUserId",
                table: "Usuario");

            migrationBuilder.DropIndex(
                name: "IX_Usuario_ApplicationUserId",
                table: "Usuario");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Usuario");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Usuario",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Nome",
                table: "Usuario",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Senha",
                table: "Usuario",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
