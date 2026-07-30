using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackEnd.Data.DB.Migrations
{
    /// <inheritdoc />
    public partial class AddSiteSettings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SiteSettings_GeneralSiteInformation_GeneralSiteInformationId",
                table: "SiteSettings");

            migrationBuilder.DropForeignKey(
                name: "FK_SiteSettings_ProjectSectionSetting_ProjectSectionSettingId",
                table: "SiteSettings");

            migrationBuilder.DropForeignKey(
                name: "FK_SiteSettings_SpecializedServicesSection_SpecializedServicesSectionId",
                table: "SiteSettings");

            migrationBuilder.AlterColumn<Guid>(
                name: "SpecializedServicesSectionId",
                table: "SiteSettings",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "ProjectSectionSettingId",
                table: "SiteSettings",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "GeneralSiteInformationId",
                table: "SiteSettings",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_SiteSettings_GeneralSiteInformation_GeneralSiteInformationId",
                table: "SiteSettings",
                column: "GeneralSiteInformationId",
                principalTable: "GeneralSiteInformation",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SiteSettings_ProjectSectionSetting_ProjectSectionSettingId",
                table: "SiteSettings",
                column: "ProjectSectionSettingId",
                principalTable: "ProjectSectionSetting",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SiteSettings_SpecializedServicesSection_SpecializedServicesSectionId",
                table: "SiteSettings",
                column: "SpecializedServicesSectionId",
                principalTable: "SpecializedServicesSection",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SiteSettings_GeneralSiteInformation_GeneralSiteInformationId",
                table: "SiteSettings");

            migrationBuilder.DropForeignKey(
                name: "FK_SiteSettings_ProjectSectionSetting_ProjectSectionSettingId",
                table: "SiteSettings");

            migrationBuilder.DropForeignKey(
                name: "FK_SiteSettings_SpecializedServicesSection_SpecializedServicesSectionId",
                table: "SiteSettings");

            migrationBuilder.AlterColumn<Guid>(
                name: "SpecializedServicesSectionId",
                table: "SiteSettings",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "ProjectSectionSettingId",
                table: "SiteSettings",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "GeneralSiteInformationId",
                table: "SiteSettings",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_SiteSettings_GeneralSiteInformation_GeneralSiteInformationId",
                table: "SiteSettings",
                column: "GeneralSiteInformationId",
                principalTable: "GeneralSiteInformation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SiteSettings_ProjectSectionSetting_ProjectSectionSettingId",
                table: "SiteSettings",
                column: "ProjectSectionSettingId",
                principalTable: "ProjectSectionSetting",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SiteSettings_SpecializedServicesSection_SpecializedServicesSectionId",
                table: "SiteSettings",
                column: "SpecializedServicesSectionId",
                principalTable: "SpecializedServicesSection",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
