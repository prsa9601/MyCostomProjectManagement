using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackEnd.Data.DB.Migrations
{
    /// <inheritdoc />
    public partial class AlterProjectRequestv1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ISDone",
                table: "ProjectRequestV1",
                newName: "IsDone");

            migrationBuilder.RenameColumn(
                name: "FillName",
                table: "ProjectRequestV1",
                newName: "FullName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsDone",
                table: "ProjectRequestV1",
                newName: "ISDone");

            migrationBuilder.RenameColumn(
                name: "FullName",
                table: "ProjectRequestV1",
                newName: "FillName");
        }
    }
}
