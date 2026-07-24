using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackEnd.Data.DB.Migrations
{
    /// <inheritdoc />
    public partial class AlterPortfolioTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Portfolios",
                table: "Portfolios");

            migrationBuilder.EnsureSchema(
                name: "portfolio");

            migrationBuilder.RenameTable(
                name: "Portfolios",
                newName: "PortfolioFiles",
                newSchema: "portfolio");

            migrationBuilder.RenameColumn(
                name: "Image",
                schema: "portfolio",
                table: "PortfolioFiles",
                newName: "File_VideoAddress");

            migrationBuilder.AddColumn<int>(
                name: "Category",
                schema: "portfolio",
                table: "PortfolioFiles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "File_CreationDate",
                schema: "portfolio",
                table: "PortfolioFiles",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "File_Id",
                schema: "portfolio",
                table: "PortfolioFiles",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "File_ImageAddress",
                schema: "portfolio",
                table: "PortfolioFiles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "File_IsImage",
                schema: "portfolio",
                table: "PortfolioFiles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "File_IsVideo",
                schema: "portfolio",
                table: "PortfolioFiles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Link",
                schema: "portfolio",
                table: "PortfolioFiles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PortfolioFiles",
                schema: "portfolio",
                table: "PortfolioFiles",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PortfolioFiles",
                schema: "portfolio",
                table: "PortfolioFiles");

            migrationBuilder.DropColumn(
                name: "Category",
                schema: "portfolio",
                table: "PortfolioFiles");

            migrationBuilder.DropColumn(
                name: "File_CreationDate",
                schema: "portfolio",
                table: "PortfolioFiles");

            migrationBuilder.DropColumn(
                name: "File_Id",
                schema: "portfolio",
                table: "PortfolioFiles");

            migrationBuilder.DropColumn(
                name: "File_ImageAddress",
                schema: "portfolio",
                table: "PortfolioFiles");

            migrationBuilder.DropColumn(
                name: "File_IsImage",
                schema: "portfolio",
                table: "PortfolioFiles");

            migrationBuilder.DropColumn(
                name: "File_IsVideo",
                schema: "portfolio",
                table: "PortfolioFiles");

            migrationBuilder.DropColumn(
                name: "Link",
                schema: "portfolio",
                table: "PortfolioFiles");

            migrationBuilder.RenameTable(
                name: "PortfolioFiles",
                schema: "portfolio",
                newName: "Portfolios");

            migrationBuilder.RenameColumn(
                name: "File_VideoAddress",
                table: "Portfolios",
                newName: "Image");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Portfolios",
                table: "Portfolios",
                column: "Id");
        }
    }
}
