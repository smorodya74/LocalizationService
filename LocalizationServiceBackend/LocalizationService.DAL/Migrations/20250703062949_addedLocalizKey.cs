using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LocalizationService.DAL.Migrations
{
    /// <inheritdoc />
    public partial class addedLocalizKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Translations_Languages_languageCode",
                table: "Translations");

            migrationBuilder.RenameColumn(
                name: "languageCode",
                table: "Translations",
                newName: "LanguageCode");

            migrationBuilder.RenameIndex(
                name: "IX_Translations_languageCode",
                table: "Translations",
                newName: "IX_Translations_LanguageCode");

            migrationBuilder.RenameColumn(
                name: "languageCode",
                table: "Languages",
                newName: "LanguageCode");

            migrationBuilder.RenameIndex(
                name: "IX_Languages_languageCode",
                table: "Languages",
                newName: "IX_Languages_LanguageCode");

            migrationBuilder.CreateTable(
                name: "LocalizationKeys",
                columns: table => new
                {
                    LocalizationKey = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocalizationKeys", x => x.LocalizationKey);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LocalizationKeys_LocalizationKey",
                table: "LocalizationKeys",
                column: "LocalizationKey",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Translations_Languages_LanguageCode",
                table: "Translations",
                column: "LanguageCode",
                principalTable: "Languages",
                principalColumn: "LanguageCode",
                onDelete: ReferentialAction.Cascade,
                onUpdate: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Translations_LocalizationKeys_LocalizationKey",
                table: "Translations",
                column: "LocalizationKey",
                principalTable: "LocalizationKeys",
                principalColumn: "LocalizationKey",
                onDelete: ReferentialAction.Cascade,
                onUpdate: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Translations_Languages_LanguageCode",
                table: "Translations");

            migrationBuilder.DropForeignKey(
                name: "FK_Translations_LocalizationKeys_LocalizationKey",
                table: "Translations");

            migrationBuilder.DropTable(
                name: "LocalizationKeys");

            migrationBuilder.RenameColumn(
                name: "LanguageCode",
                table: "Translations",
                newName: "languageCode");

            migrationBuilder.RenameIndex(
                name: "IX_Translations_LanguageCode",
                table: "Translations",
                newName: "IX_Translations_languageCode");

            migrationBuilder.RenameColumn(
                name: "LanguageCode",
                table: "Languages",
                newName: "languageCode");

            migrationBuilder.RenameIndex(
                name: "IX_Languages_LanguageCode",
                table: "Languages",
                newName: "IX_Languages_languageCode");

            migrationBuilder.AddForeignKey(
                name: "FK_Translations_Languages_languageCode",
                table: "Translations",
                column: "languageCode",
                principalTable: "Languages",
                principalColumn: "languageCode",
                onDelete: ReferentialAction.Cascade,
                onUpdate: ReferentialAction.Cascade);
        }
    }
}
