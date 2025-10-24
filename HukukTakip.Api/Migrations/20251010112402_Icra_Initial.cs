using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HukukTakip.Api.Migrations
{
    /// <inheritdoc />
    public partial class Icra_Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "YevmiyeNo",
                table: "Ihtarlar",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "IcraDosyalari",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DosyaNo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MusteriId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IcraDairesi = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DosyaTuru = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TakipTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Durum = table.Column<int>(type: "int", nullable: false),
                    OlusturanUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    OlusturmaTarihiUtc = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IcraDosyalari", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ihtarlar_NoterAdi_YevmiyeNo",
                table: "Ihtarlar",
                columns: new[] { "NoterAdi", "YevmiyeNo" },
                unique: true,
                filter: "[NoterAdi] IS NOT NULL AND [YevmiyeNo] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_IcraDosyalari_DosyaNo",
                table: "IcraDosyalari",
                column: "DosyaNo",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IcraDosyalari");

            migrationBuilder.DropIndex(
                name: "IX_Ihtarlar_NoterAdi_YevmiyeNo",
                table: "Ihtarlar");

            migrationBuilder.AlterColumn<string>(
                name: "YevmiyeNo",
                table: "Ihtarlar",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);
        }
    }
}
