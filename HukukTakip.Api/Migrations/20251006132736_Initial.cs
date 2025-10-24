using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HukukTakip.Api.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "avukatlar",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AdSoyad = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    BaroNo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Telefon = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_avukatlar", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DavaTurleri",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ad = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DavaTurleri", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sehirler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ad = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PlakaKodu = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sehirler", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "urunler",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Ad = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Aciklama = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Fiyat = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_urunler", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KullaniciAdi = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    SifreHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rol = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    KayitTarihi = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IcraDaireleri",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ad = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    SehirId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IcraDaireleri", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IcraDaireleri_Sehirler_SehirId",
                        column: x => x.SehirId,
                        principalTable: "Sehirler",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Musteriler",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MusteriNo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    AdiUnvani = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    BorcluTipi = table.Column<int>(type: "int", nullable: false),
                    BorcluSoyadi = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    TCKN = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: true),
                    DogumTarihi = table.Column<DateOnly>(type: "date", nullable: true),
                    DogumYeri = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Cinsiyet = table.Column<int>(type: "int", nullable: true),
                    MedeniDurum = table.Column<int>(type: "int", nullable: true),
                    BabaAdi = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    AnneAdi = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    PasaportNumarasi = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    KimlikVerilisTarihi = table.Column<DateOnly>(type: "date", nullable: true),
                    NufusaKayitliOlduguIl = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CiltNo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    AileSiraNo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    KutukSiraNo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    SurucuBelgesiNumarasi = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    SehirId = table.Column<int>(type: "int", nullable: true),
                    IlceId = table.Column<int>(type: "int", nullable: true),
                    Semt = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    VergiDairesi = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: true),
                    VergiNo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    SSKNo = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    SSKIsyeriNumarasi = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    TicaretSicilNo = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    BorcuTipi = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    MusteriMusteriTipi = table.Column<int>(type: "int", nullable: false),
                    HayattaMi = table.Column<bool>(type: "bit", nullable: false),
                    OlusturanUserId = table.Column<int>(type: "int", nullable: false),
                    OlusturmaTarihiUtc = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Musteriler", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Musteriler_Sehirler_SehirId",
                        column: x => x.SehirId,
                        principalTable: "Sehirler",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Ihtarlar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MusteriId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MusteriUrunleri = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NoterAdi = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    YevmiyeNo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IhtarTarihi = table.Column<DateOnly>(type: "date", nullable: false),
                    IhtarnameSuresiGun = table.Column<int>(type: "int", nullable: true),
                    TebligTarihi = table.Column<DateOnly>(type: "date", nullable: true),
                    IhtarTebligGirisTarihi = table.Column<DateOnly>(type: "date", nullable: true),
                    KatTarihi = table.Column<DateOnly>(type: "date", nullable: true),
                    IhtarnameNakitTutar = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IhtarnameGayriNakitTutar = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IhtarNo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Aciklama = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OlusturanUserId = table.Column<int>(type: "int", nullable: false),
                    OlusturmaTarihiUtc = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ihtarlar", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ihtarlar_Musteriler_MusteriId",
                        column: x => x.MusteriId,
                        principalTable: "Musteriler",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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

            migrationBuilder.InsertData(
                table: "Sehirler",
                columns: new[] { "Id", "Ad", "PlakaKodu" },
                values: new object[,]
                {
                    { 6, "Ankara", "06" },
                    { 34, "İstanbul", "34" },
                    { 35, "İzmir", "35" },
                    { 38, "Kayseri", "38" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "KayitTarihi", "KullaniciAdi", "Rol", "SifreHash" },
                values: new object[] { 1, "admin@demo.com", new DateTime(2025, 9, 30, 0, 0, 0, 0, DateTimeKind.Utc), "admin", "admin", "$2a$11$DJUpOEmwBejBEoH.cGQQGueUPIhIWYHbpXkiTK7edw2fL.r9pC472" });

            migrationBuilder.InsertData(
                table: "IcraDaireleri",
                columns: new[] { "Id", "Ad", "SehirId" },
                values: new object[,]
                {
                    { 1001, "İstanbul 10. İcra Dairesi", 34 },
                    { 1002, "İstanbul 12. İcra Dairesi", 34 },
                    { 2001, "Ankara 5. İcra Dairesi", 6 },
                    { 3001, "İzmir 3. İcra Dairesi", 35 },
                    { 4001, "Kayseri 2. İcra Dairesi", 38 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_DavaTurleri_Ad",
                table: "DavaTurleri",
                column: "Ad",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_IcraDaireleri_SehirId_Ad",
                table: "IcraDaireleri",
                columns: new[] { "SehirId", "Ad" });

            migrationBuilder.CreateIndex(
                name: "IX_Ihtarlar_IhtarNo",
                table: "Ihtarlar",
                column: "IhtarNo",
                unique: true,
                filter: "[IhtarNo] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Ihtarlar_MusteriId",
                table: "Ihtarlar",
                column: "MusteriId");

            migrationBuilder.CreateIndex(
                name: "IX_Ihtarlar_YevmiyeNo",
                table: "Ihtarlar",
                column: "YevmiyeNo",
                unique: true,
                filter: "[YevmiyeNo] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Musteriler_MusteriNo",
                table: "Musteriler",
                column: "MusteriNo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Musteriler_SehirId",
                table: "Musteriler",
                column: "SehirId");

            migrationBuilder.CreateIndex(
                name: "IX_Musteriler_TCKN",
                table: "Musteriler",
                column: "TCKN",
                unique: true,
                filter: "[TCKN] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Musteriler_VergiNo",
                table: "Musteriler",
                column: "VergiNo",
                unique: true,
                filter: "[VergiNo] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Sehirler_Ad",
                table: "Sehirler",
                column: "Ad");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_KullaniciAdi",
                table: "Users",
                column: "KullaniciAdi",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "avukatlar");

            migrationBuilder.DropTable(
                name: "DavaTurleri");

            migrationBuilder.DropTable(
                name: "IcraDaireleri");

            migrationBuilder.DropTable(
                name: "Ihtarlar");

            migrationBuilder.DropTable(
                name: "urunler");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Musteriler");

            migrationBuilder.DropTable(
                name: "Sehirler");
        }
    }
}
