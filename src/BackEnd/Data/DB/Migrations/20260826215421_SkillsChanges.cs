using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackEnd.Data.DB.Migrations
{
    /// <inheritdoc />
    public partial class SkillsChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_FAQs",
                table: "FAQs");

            migrationBuilder.EnsureSchema(
                name: "FAQ");

            migrationBuilder.RenameTable(
                name: "FAQs",
                newName: "faq",
                newSchema: "FAQ");

            migrationBuilder.AddPrimaryKey(
                name: "PK_faq",
                schema: "FAQ",
                table: "faq",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_faq",
                schema: "FAQ",
                table: "faq");

            migrationBuilder.RenameTable(
                name: "faq",
                schema: "FAQ",
                newName: "FAQs");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FAQs",
                table: "FAQs",
                column: "Id");
        }
    }
}
