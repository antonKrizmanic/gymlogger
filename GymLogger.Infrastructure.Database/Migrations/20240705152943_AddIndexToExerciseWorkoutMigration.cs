using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymLogger.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class AddIndexToExerciseWorkoutMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Index",
                table: "ExerciseWorkouts",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Index",
                table: "ExerciseWorkouts");
        }
    }
}
