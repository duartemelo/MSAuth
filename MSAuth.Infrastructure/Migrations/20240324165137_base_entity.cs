using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MSAuth.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class base_entity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DateOfRegister",
                table: "Users",
                newName: "DateOfCreation");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateOfModification",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfModification",
                table: "UserConfirmations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfCreation",
                table: "Apps",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfModification",
                table: "Apps",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateOfModification",
                table: "UserConfirmations");

            migrationBuilder.DropColumn(
                name: "DateOfCreation",
                table: "Apps");

            migrationBuilder.DropColumn(
                name: "DateOfModification",
                table: "Apps");

            migrationBuilder.RenameColumn(
                name: "DateOfCreation",
                table: "Users",
                newName: "DateOfRegister");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateOfModification",
                table: "Users",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }
    }
}
