using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HukukTakip.Api.Migrations
{
    /// <inheritdoc />
    public partial class Icra : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IcraDosyalari");

            migrationBuilder.DeleteData(
                table: "DavaTurleri",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "DavaTurleri",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "DavaTurleri",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "DavaTurleri",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "DavaTurleri",
                keyColumn: "Id",
                keyValue: 5);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "IcraDosyalari",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DosyaNo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DosyaTuru = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Durum = table.Column<int>(type: "int", nullable: false),
                    IcraDairesi = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    MusteriId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OlusturanUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    OlusturmaTarihiUtc = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    TakipTarihi = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IcraDosyalari", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "DavaTurleri",
                columns: new[] { "Id", "Ad" },
                values: new object[,]
                {
                    { 1, "İcra" },
                    { 2, "Aile" },
                    { 3, "Ceza" },
                    { 4, "İş" },
                    { 5, "Tüketici" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_IcraDosyalari_DosyaNo",
                table: "IcraDosyalari",
                column: "DosyaNo",
                unique: true);
        }
    }
}
