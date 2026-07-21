using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackEnd.Data.DB.Migrations
{
    /// <inheritdoc />
    public partial class RoleChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Roles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Icon",
                table: "Roles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "IconColorCode",
                table: "Roles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "Icon",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "IconColorCode",
                table: "Roles");
        }
    }
}
