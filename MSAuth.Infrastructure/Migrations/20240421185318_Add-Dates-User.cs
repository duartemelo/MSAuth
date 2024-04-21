using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MSAuth.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddDatesUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfModification",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfRegister",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateOfModification",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "DateOfRegister",
                table: "AspNetUsers");
        }
    }
}
