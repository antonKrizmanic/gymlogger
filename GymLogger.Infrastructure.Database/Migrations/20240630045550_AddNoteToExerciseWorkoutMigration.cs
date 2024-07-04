using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymLogger.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class AddNoteToExerciseWorkoutMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Note",
                table: "ExerciseWorkouts",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Note",
                table: "ExerciseSets",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Note",
                table: "ExerciseWorkouts");

            migrationBuilder.DropColumn(
                name: "Note",
                table: "ExerciseSets");
        }
    }
}
