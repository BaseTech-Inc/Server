using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Persistence.Migrations
{
    public partial class CreatedRefreshToken : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RefreshToken",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(767)", nullable: false),
                    Token = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<string>(type: "text", nullable: true),
                    ExpiryOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedByIp = table.Column<string>(type: "text", nullable: true),
                    RevokedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    RevokedByIp = table.Column<string>(type: "text", nullable: true),
                    ApplicationUserId = table.Column<string>(type: "varchar(85)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshToken", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefreshToken_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RefreshToken_ApplicationUserId",
                table: "RefreshToken",
                column: "ApplicationUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RefreshToken");
        }
    }
}
