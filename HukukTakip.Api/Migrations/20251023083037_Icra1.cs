using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HukukTakip.Api.Migrations
{
    /// <inheritdoc />
    public partial class Icra1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "IcraDosyalari",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DosyaNo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MusteriId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AvukatId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    AvukatTevziNo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TakipTarihi = table.Column<DateOnly>(type: "date", nullable: true),
                    TakipTipi = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IhtarBorclulari = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    IhtarKonusuUrunler = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    IcraMudurlugu = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    MahiyetKodu = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Durum = table.Column<int>(type: "int", nullable: false),
                    OlusturmaTarihiUtc = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IcraDosyalari", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IcraDosyalari_Avukatlar_AvukatId",
                        column: x => x.AvukatId,
                        principalTable: "Avukatlar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_IcraDosyalari_Musteriler_MusteriId",
                        column: x => x.MusteriId,
                        principalTable: "Musteriler",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IcraDosyalari_AvukatId",
                table: "IcraDosyalari",
                column: "AvukatId");

            migrationBuilder.CreateIndex(
                name: "IX_IcraDosyalari_DosyaNo",
                table: "IcraDosyalari",
                column: "DosyaNo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_IcraDosyalari_MusteriId",
                table: "IcraDosyalari",
                column: "MusteriId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IcraDosyalari");
        }
    }
}
