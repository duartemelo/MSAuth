using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MSAuth.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UserAppAndExternalIdNotNull : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Apps_AppId",
                table: "AspNetUsers");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Apps_AppId",
                table: "AspNetUsers",
                column: "AppId",
                principalTable: "Apps",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Apps_AppId",
                table: "AspNetUsers");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Apps_AppId",
                table: "AspNetUsers",
                column: "AppId",
                principalTable: "Apps",
                principalColumn: "Id");
        }
    }
}
