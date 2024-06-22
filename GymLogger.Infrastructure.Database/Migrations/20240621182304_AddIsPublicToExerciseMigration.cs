using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymLogger.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class AddIsPublicToExerciseMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BelongsToUserId",
                table: "Exercises",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BelongsToUserId",
                table: "Exercises");
        }
    }
}
