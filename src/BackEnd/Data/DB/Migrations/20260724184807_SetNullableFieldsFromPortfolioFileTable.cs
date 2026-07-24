using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackEnd.Data.DB.Migrations
{
    /// <inheritdoc />
    public partial class SetNullableFieldsFromPortfolioFileTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "File_VideoAddress",
                schema: "portfolio",
                table: "PortfolioFiles",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "File_ImageAddress",
                schema: "portfolio",
                table: "PortfolioFiles",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "File_VideoAddress",
                schema: "portfolio",
                table: "PortfolioFiles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "File_ImageAddress",
                schema: "portfolio",
                table: "PortfolioFiles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
