using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MSGym.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDomain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExerciseCategories_Exercises_ExerciseId",
                table: "ExerciseCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_Roles_TrainingPlans_TrainingPlanId",
                table: "Roles");

            migrationBuilder.DropForeignKey(
                name: "FK_TrainingPlans_Users_AthleteId",
                table: "TrainingPlans");

            migrationBuilder.DropIndex(
                name: "IX_TrainingPlans_AthleteId",
                table: "TrainingPlans");

            migrationBuilder.DropIndex(
                name: "IX_Roles_TrainingPlanId",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "AthleteId",
                table: "TrainingPlans");

            migrationBuilder.DropColumn(
                name: "TrainingPlanId",
                table: "Roles");

            migrationBuilder.RenameColumn(
                name: "ExerciseId",
                table: "ExerciseCategories",
                newName: "GymId");

            migrationBuilder.RenameIndex(
                name: "IX_ExerciseCategories_ExerciseId",
                table: "ExerciseCategories",
                newName: "IX_ExerciseCategories_GymId");

            migrationBuilder.AddColumn<long>(
                name: "GymId",
                table: "TrainingPlans",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "GymId",
                table: "Exercises",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "Exercises",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ExerciseExerciseCategory",
                columns: table => new
                {
                    CategoriesId = table.Column<long>(type: "bigint", nullable: false),
                    ExercisesId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExerciseExerciseCategory", x => new { x.CategoriesId, x.ExercisesId });
                    table.ForeignKey(
                        name: "FK_ExerciseExerciseCategory_ExerciseCategories_CategoriesId",
                        column: x => x.CategoriesId,
                        principalTable: "ExerciseCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExerciseExerciseCategory_Exercises_ExercisesId",
                        column: x => x.ExercisesId,
                        principalTable: "Exercises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoleTrainingPlan",
                columns: table => new
                {
                    ResponsibleRolesId = table.Column<long>(type: "bigint", nullable: false),
                    TrainingPlansId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleTrainingPlan", x => new { x.ResponsibleRolesId, x.TrainingPlansId });
                    table.ForeignKey(
                        name: "FK_RoleTrainingPlan_Roles_ResponsibleRolesId",
                        column: x => x.ResponsibleRolesId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoleTrainingPlan_TrainingPlans_TrainingPlansId",
                        column: x => x.TrainingPlansId,
                        principalTable: "TrainingPlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TrainingPlanUser",
                columns: table => new
                {
                    AthletesId = table.Column<long>(type: "bigint", nullable: false),
                    TrainingPlansId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingPlanUser", x => new { x.AthletesId, x.TrainingPlansId });
                    table.ForeignKey(
                        name: "FK_TrainingPlanUser_TrainingPlans_TrainingPlansId",
                        column: x => x.TrainingPlansId,
                        principalTable: "TrainingPlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TrainingPlanUser_Users_AthletesId",
                        column: x => x.AthletesId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TrainingPlans_GymId",
                table: "TrainingPlans",
                column: "GymId");

            migrationBuilder.CreateIndex(
                name: "IX_Exercises_GymId",
                table: "Exercises",
                column: "GymId");

            migrationBuilder.CreateIndex(
                name: "IX_ExerciseExerciseCategory_ExercisesId",
                table: "ExerciseExerciseCategory",
                column: "ExercisesId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleTrainingPlan_TrainingPlansId",
                table: "RoleTrainingPlan",
                column: "TrainingPlansId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainingPlanUser_TrainingPlansId",
                table: "TrainingPlanUser",
                column: "TrainingPlansId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExerciseCategories_Gyms_GymId",
                table: "ExerciseCategories",
                column: "GymId",
                principalTable: "Gyms",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Exercises_Gyms_GymId",
                table: "Exercises",
                column: "GymId",
                principalTable: "Gyms",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TrainingPlans_Gyms_GymId",
                table: "TrainingPlans",
                column: "GymId",
                principalTable: "Gyms",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExerciseCategories_Gyms_GymId",
                table: "ExerciseCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_Exercises_Gyms_GymId",
                table: "Exercises");

            migrationBuilder.DropForeignKey(
                name: "FK_TrainingPlans_Gyms_GymId",
                table: "TrainingPlans");

            migrationBuilder.DropTable(
                name: "ExerciseExerciseCategory");

            migrationBuilder.DropTable(
                name: "RoleTrainingPlan");

            migrationBuilder.DropTable(
                name: "TrainingPlanUser");

            migrationBuilder.DropIndex(
                name: "IX_TrainingPlans_GymId",
                table: "TrainingPlans");

            migrationBuilder.DropIndex(
                name: "IX_Exercises_GymId",
                table: "Exercises");

            migrationBuilder.DropColumn(
                name: "GymId",
                table: "TrainingPlans");

            migrationBuilder.DropColumn(
                name: "GymId",
                table: "Exercises");

            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "Exercises");

            migrationBuilder.RenameColumn(
                name: "GymId",
                table: "ExerciseCategories",
                newName: "ExerciseId");

            migrationBuilder.RenameIndex(
                name: "IX_ExerciseCategories_GymId",
                table: "ExerciseCategories",
                newName: "IX_ExerciseCategories_ExerciseId");

            migrationBuilder.AddColumn<long>(
                name: "AthleteId",
                table: "TrainingPlans",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "TrainingPlanId",
                table: "Roles",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TrainingPlans_AthleteId",
                table: "TrainingPlans",
                column: "AthleteId");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_TrainingPlanId",
                table: "Roles",
                column: "TrainingPlanId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExerciseCategories_Exercises_ExerciseId",
                table: "ExerciseCategories",
                column: "ExerciseId",
                principalTable: "Exercises",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Roles_TrainingPlans_TrainingPlanId",
                table: "Roles",
                column: "TrainingPlanId",
                principalTable: "TrainingPlans",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TrainingPlans_Users_AthleteId",
                table: "TrainingPlans",
                column: "AthleteId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
