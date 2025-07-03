using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LocalizationService.DAL.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Languages",
                columns: table => new
                {
                    languageCode = table.Column<string>(type: "character varying(3)", maxLength: 3, nullable: false),
                    Name = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Languages", x => x.languageCode);
                });

            migrationBuilder.CreateTable(
                name: "Translations",
                columns: table => new
                {
                    LocalizationKey = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    languageCode = table.Column<string>(type: "character varying(3)", nullable: false),
                    TranslationText = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Translations", x => new { x.LocalizationKey, x.languageCode });
                    table.ForeignKey(
                        name: "FK_Translations_Languages_languageCode",
                        column: x => x.languageCode,
                        principalTable: "Languages",
                        principalColumn: "languageCode",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Languages_languageCode",
                table: "Languages",
                column: "languageCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Translations_languageCode",
                table: "Translations",
                column: "languageCode");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Translations");

            migrationBuilder.DropTable(
                name: "Languages");
        }
    }
}
