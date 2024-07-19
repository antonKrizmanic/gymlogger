using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymLogger.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class AddedBelongsToUserInExerciseWorkoutMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BelongsToUserId",
                table: "ExerciseWorkouts",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BelongsToUserId",
                table: "ExerciseWorkouts");
        }
    }
}
