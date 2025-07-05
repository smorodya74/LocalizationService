using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LocalizationService.DAL.Migrations
{
    /// <inheritdoc />
    public partial class columnByLocalization : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LocalizationKey",
                table: "LocalizationKeys",
                newName: "KeyName");

            migrationBuilder.RenameIndex(
                name: "IX_LocalizationKeys_LocalizationKey",
                table: "LocalizationKeys",
                newName: "IX_LocalizationKeys_KeyName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "KeyName",
                table: "LocalizationKeys",
                newName: "LocalizationKey");

            migrationBuilder.RenameIndex(
                name: "IX_LocalizationKeys_KeyName",
                table: "LocalizationKeys",
                newName: "IX_LocalizationKeys_LocalizationKey");
        }
    }
}
