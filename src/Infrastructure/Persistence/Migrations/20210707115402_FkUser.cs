using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Persistence.Migrations
{
    public partial class FkUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usuario_AspNetUsers_ApplicationUserId",
                table: "Usuario");

            migrationBuilder.RenameColumn(
                name: "ApplicationUserId",
                table: "Usuario",
                newName: "ApplicationUserID");

            migrationBuilder.RenameIndex(
                name: "IX_Usuario_ApplicationUserId",
                table: "Usuario",
                newName: "IX_Usuario_ApplicationUserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Usuario_AspNetUsers_ApplicationUserID",
                table: "Usuario",
                column: "ApplicationUserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usuario_AspNetUsers_ApplicationUserID",
                table: "Usuario");

            migrationBuilder.RenameColumn(
                name: "ApplicationUserID",
                table: "Usuario",
                newName: "ApplicationUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Usuario_ApplicationUserID",
                table: "Usuario",
                newName: "IX_Usuario_ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Usuario_AspNetUsers_ApplicationUserId",
                table: "Usuario",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
