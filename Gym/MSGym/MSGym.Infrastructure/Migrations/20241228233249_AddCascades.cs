using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MSGym.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddCascades : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Remove as constraints existentes para GymContacts
            migrationBuilder.DropForeignKey(
                name: "FK_GymContacts_Gyms_GymId",
                table: "GymContacts");

            // Remove as constraints existentes para GymSchedules
            migrationBuilder.DropForeignKey(
                name: "FK_GymSchedules_Gyms_GymId",
                table: "GymSchedules");

            // Remove as constraints existentes para UserRoles
            migrationBuilder.DropForeignKey(
                name: "FK_UserRoles_Users_UserId",
                table: "UserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRoles_Roles_RoleId",
                table: "UserRoles");

            // Adiciona as novas constraints com ON DELETE CASCADE para GymContacts
            migrationBuilder.AddForeignKey(
                name: "FK_GymContacts_Gyms_GymId",
                table: "GymContacts",
                column: "GymId",
                principalTable: "Gyms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            // Adiciona as novas constraints com ON DELETE CASCADE para GymSchedules
            migrationBuilder.AddForeignKey(
                name: "FK_GymSchedules_Gyms_GymId",
                table: "GymSchedules",
                column: "GymId",
                principalTable: "Gyms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            // Adiciona as novas constraints com ON DELETE CASCADE para UserRoles
            migrationBuilder.AddForeignKey(
                name: "FK_UserRoles_Users_UserId",
                table: "UserRoles",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRoles_Roles_RoleId",
                table: "UserRoles",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Remove as constraints com ON DELETE CASCADE para GymContacts
            migrationBuilder.DropForeignKey(
                name: "FK_GymContacts_Gyms_GymId",
                table: "GymContacts");

            // Remove as constraints com ON DELETE CASCADE para GymSchedules
            migrationBuilder.DropForeignKey(
                name: "FK_GymSchedules_Gyms_GymId",
                table: "GymSchedules");

            // Remove as constraints com ON DELETE CASCADE para UserRoles
            migrationBuilder.DropForeignKey(
                name: "FK_UserRoles_Users_UserId",
                table: "UserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRoles_Roles_RoleId",
                table: "UserRoles");

            // Restaura as constraints sem ON DELETE CASCADE para GymContacts
            migrationBuilder.AddForeignKey(
                name: "FK_GymContacts_Gyms_GymId",
                table: "GymContacts",
                column: "GymId",
                principalTable: "Gyms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            // Restaura as constraints sem ON DELETE CASCADE para GymSchedules
            migrationBuilder.AddForeignKey(
                name: "FK_GymSchedules_Gyms_GymId",
                table: "GymSchedules",
                column: "GymId",
                principalTable: "Gyms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            // Restaura as constraints sem ON DELETE CASCADE para UserRoles
            migrationBuilder.AddForeignKey(
                name: "FK_UserRoles_Users_UserId",
                table: "UserRoles",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRoles_Roles_RoleId",
                table: "UserRoles",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
