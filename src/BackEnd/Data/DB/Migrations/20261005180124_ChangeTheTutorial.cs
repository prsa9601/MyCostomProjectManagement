using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackEnd.Data.DB.Migrations
{
    /// <inheritdoc />
    public partial class ChangeTheTutorial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Tutorials_TutorialApiId",
                schema: "tutorial",
                table: "Tutorials");

            migrationBuilder.CreateIndex(
                name: "IX_Tutorials_TutorialApiId",
                schema: "tutorial",
                table: "Tutorials",
                column: "TutorialApiId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Tutorials_TutorialApiId",
                schema: "tutorial",
                table: "Tutorials");

            migrationBuilder.CreateIndex(
                name: "IX_Tutorials_TutorialApiId",
                schema: "tutorial",
                table: "Tutorials",
                column: "TutorialApiId",
                unique: true);
        }
    }
}
