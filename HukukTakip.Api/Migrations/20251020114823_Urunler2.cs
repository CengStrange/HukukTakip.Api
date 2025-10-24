using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HukukTakip.Api.Migrations
{
    /// <inheritdoc />
    public partial class Urunler2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_urunler",
                table: "urunler");

            migrationBuilder.DropColumn(
                name: "Ad",
                table: "urunler");

            migrationBuilder.RenameTable(
                name: "urunler",
                newName: "Urunler");

            migrationBuilder.RenameColumn(
                name: "Fiyat",
                table: "Urunler",
                newName: "MasrafBakiyesi");

            migrationBuilder.AddColumn<Guid>(
                name: "AvukatId",
                table: "Urunler",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "AylikFaizOrani",
                table: "Urunler",
                type: "decimal(18,4)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DovizTipi",
                table: "Urunler",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "FaizBakiyesi",
                table: "Urunler",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FaizMudiNo",
                table: "Urunler",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "KrediBirimKoduSubeId",
                table: "Urunler",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "KrediMudiNo",
                table: "Urunler",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MasrafMudiNo",
                table: "Urunler",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "MusteriId",
                table: "Urunler",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "OlusturmaTarihiUtc",
                table: "Urunler",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()");

            migrationBuilder.AddColumn<int>(
                name: "TakipBirimKoduSubeId",
                table: "Urunler",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "TakipMiktari",
                table: "Urunler",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "TakipMudiNo",
                table: "Urunler",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "TakipTarihi",
                table: "Urunler",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UrunTipi",
                table: "Urunler",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Urunler",
                table: "Urunler",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Urunler_AvukatId",
                table: "Urunler",
                column: "AvukatId");

            migrationBuilder.CreateIndex(
                name: "IX_Urunler_KrediBirimKoduSubeId",
                table: "Urunler",
                column: "KrediBirimKoduSubeId");

            migrationBuilder.CreateIndex(
                name: "IX_Urunler_MusteriId",
                table: "Urunler",
                column: "MusteriId");

            migrationBuilder.CreateIndex(
                name: "IX_Urunler_TakipBirimKoduSubeId",
                table: "Urunler",
                column: "TakipBirimKoduSubeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Urunler_Musteriler_MusteriId",
                table: "Urunler",
                column: "MusteriId",
                principalTable: "Musteriler",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Urunler_Subeler_KrediBirimKoduSubeId",
                table: "Urunler",
                column: "KrediBirimKoduSubeId",
                principalTable: "Subeler",
                principalColumn: "BrmKod",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Urunler_Subeler_TakipBirimKoduSubeId",
                table: "Urunler",
                column: "TakipBirimKoduSubeId",
                principalTable: "Subeler",
                principalColumn: "BrmKod",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Urunler_avukatlar_AvukatId",
                table: "Urunler",
                column: "AvukatId",
                principalTable: "avukatlar",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Urunler_Musteriler_MusteriId",
                table: "Urunler");

            migrationBuilder.DropForeignKey(
                name: "FK_Urunler_Subeler_KrediBirimKoduSubeId",
                table: "Urunler");

            migrationBuilder.DropForeignKey(
                name: "FK_Urunler_Subeler_TakipBirimKoduSubeId",
                table: "Urunler");

            migrationBuilder.DropForeignKey(
                name: "FK_Urunler_avukatlar_AvukatId",
                table: "Urunler");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Urunler",
                table: "Urunler");

            migrationBuilder.DropIndex(
                name: "IX_Urunler_AvukatId",
                table: "Urunler");

            migrationBuilder.DropIndex(
                name: "IX_Urunler_KrediBirimKoduSubeId",
                table: "Urunler");

            migrationBuilder.DropIndex(
                name: "IX_Urunler_MusteriId",
                table: "Urunler");

            migrationBuilder.DropIndex(
                name: "IX_Urunler_TakipBirimKoduSubeId",
                table: "Urunler");

            migrationBuilder.DropColumn(
                name: "AvukatId",
                table: "Urunler");

            migrationBuilder.DropColumn(
                name: "AylikFaizOrani",
                table: "Urunler");

            migrationBuilder.DropColumn(
                name: "DovizTipi",
                table: "Urunler");

            migrationBuilder.DropColumn(
                name: "FaizBakiyesi",
                table: "Urunler");

            migrationBuilder.DropColumn(
                name: "FaizMudiNo",
                table: "Urunler");

            migrationBuilder.DropColumn(
                name: "KrediBirimKoduSubeId",
                table: "Urunler");

            migrationBuilder.DropColumn(
                name: "KrediMudiNo",
                table: "Urunler");

            migrationBuilder.DropColumn(
                name: "MasrafMudiNo",
                table: "Urunler");

            migrationBuilder.DropColumn(
                name: "MusteriId",
                table: "Urunler");

            migrationBuilder.DropColumn(
                name: "OlusturmaTarihiUtc",
                table: "Urunler");

            migrationBuilder.DropColumn(
                name: "TakipBirimKoduSubeId",
                table: "Urunler");

            migrationBuilder.DropColumn(
                name: "TakipMiktari",
                table: "Urunler");

            migrationBuilder.DropColumn(
                name: "TakipMudiNo",
                table: "Urunler");

            migrationBuilder.DropColumn(
                name: "TakipTarihi",
                table: "Urunler");

            migrationBuilder.DropColumn(
                name: "UrunTipi",
                table: "Urunler");

            migrationBuilder.RenameTable(
                name: "Urunler",
                newName: "urunler");

            migrationBuilder.RenameColumn(
                name: "MasrafBakiyesi",
                table: "urunler",
                newName: "Fiyat");

            migrationBuilder.AddColumn<string>(
                name: "Ad",
                table: "urunler",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_urunler",
                table: "urunler",
                column: "Id");
        }
    }
}
