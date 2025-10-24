using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HukukTakip.Api.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAvukatlar : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Urunler_avukatlar_AvukatId",
                table: "Urunler");

            migrationBuilder.DropPrimaryKey(
                name: "PK_avukatlar",
                table: "avukatlar");

            migrationBuilder.DropColumn(
                name: "AdSoyad",
                table: "avukatlar");

            migrationBuilder.DropColumn(
                name: "Telefon",
                table: "avukatlar");

            migrationBuilder.RenameTable(
                name: "avukatlar",
                newName: "Avukatlar");

            migrationBuilder.RenameColumn(
                name: "BaroNo",
                table: "Avukatlar",
                newName: "VadesizHesapNo");

            migrationBuilder.AlterColumn<string>(
                name: "TCKN",
                table: "Musteriler",
                type: "nchar(11)",
                fixedLength: true,
                maxLength: 11,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(11)",
                oldMaxLength: 11,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Adi",
                table: "Avukatlar",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AvansHesapNo",
                table: "Avukatlar",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AvansHesapSubeId",
                table: "Avukatlar",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "AvansLimiti",
                table: "Avukatlar",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "AvukatTipi",
                table: "Avukatlar",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CepTelefonu",
                table: "Avukatlar",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "DialogYasal",
                table: "Avukatlar",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Dialogdan",
                table: "Avukatlar",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "DigerBankaIbanNo",
                table: "Avukatlar",
                type: "nvarchar(34)",
                maxLength: 34,
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "DogumTarihi",
                table: "Avukatlar",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HalkbankVadesizIbanNo",
                table: "Avukatlar",
                type: "nvarchar(34)",
                maxLength: 34,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "HesapAktifMi",
                table: "Avukatlar",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "IlceId",
                table: "Avukatlar",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IletisimVeribanMi",
                table: "Avukatlar",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "IsFaxNo",
                table: "Avukatlar",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IsTelefonu",
                table: "Avukatlar",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Normal",
                table: "Avukatlar",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "OlusturmaTarihiUtc",
                table: "Avukatlar",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "SehirId",
                table: "Avukatlar",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Soyadi",
                table: "Avukatlar",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TCKN",
                table: "Avukatlar",
                type: "nchar(11)",
                fixedLength: true,
                maxLength: 11,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TamAdres",
                table: "Avukatlar",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "VadesizHesapSubeId",
                table: "Avukatlar",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VergiDairesi",
                table: "Avukatlar",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VergiNo",
                table: "Avukatlar",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Avukatlar",
                table: "Avukatlar",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Avukatlar_AvansHesapSubeId",
                table: "Avukatlar",
                column: "AvansHesapSubeId");

            migrationBuilder.CreateIndex(
                name: "IX_Avukatlar_TCKN",
                table: "Avukatlar",
                column: "TCKN",
                unique: true,
                filter: "[TCKN] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Avukatlar_VadesizHesapSubeId",
                table: "Avukatlar",
                column: "VadesizHesapSubeId");

            migrationBuilder.CreateIndex(
                name: "IX_Avukatlar_VergiNo",
                table: "Avukatlar",
                column: "VergiNo",
                unique: true,
                filter: "[VergiNo] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Avukatlar_Subeler_AvansHesapSubeId",
                table: "Avukatlar",
                column: "AvansHesapSubeId",
                principalTable: "Subeler",
                principalColumn: "BrmKod",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Avukatlar_Subeler_VadesizHesapSubeId",
                table: "Avukatlar",
                column: "VadesizHesapSubeId",
                principalTable: "Subeler",
                principalColumn: "BrmKod",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Urunler_Avukatlar_AvukatId",
                table: "Urunler",
                column: "AvukatId",
                principalTable: "Avukatlar",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Avukatlar_Subeler_AvansHesapSubeId",
                table: "Avukatlar");

            migrationBuilder.DropForeignKey(
                name: "FK_Avukatlar_Subeler_VadesizHesapSubeId",
                table: "Avukatlar");

            migrationBuilder.DropForeignKey(
                name: "FK_Urunler_Avukatlar_AvukatId",
                table: "Urunler");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Avukatlar",
                table: "Avukatlar");

            migrationBuilder.DropIndex(
                name: "IX_Avukatlar_AvansHesapSubeId",
                table: "Avukatlar");

            migrationBuilder.DropIndex(
                name: "IX_Avukatlar_TCKN",
                table: "Avukatlar");

            migrationBuilder.DropIndex(
                name: "IX_Avukatlar_VadesizHesapSubeId",
                table: "Avukatlar");

            migrationBuilder.DropIndex(
                name: "IX_Avukatlar_VergiNo",
                table: "Avukatlar");

            migrationBuilder.DropColumn(
                name: "Adi",
                table: "Avukatlar");

            migrationBuilder.DropColumn(
                name: "AvansHesapNo",
                table: "Avukatlar");

            migrationBuilder.DropColumn(
                name: "AvansHesapSubeId",
                table: "Avukatlar");

            migrationBuilder.DropColumn(
                name: "AvansLimiti",
                table: "Avukatlar");

            migrationBuilder.DropColumn(
                name: "AvukatTipi",
                table: "Avukatlar");

            migrationBuilder.DropColumn(
                name: "CepTelefonu",
                table: "Avukatlar");

            migrationBuilder.DropColumn(
                name: "DialogYasal",
                table: "Avukatlar");

            migrationBuilder.DropColumn(
                name: "Dialogdan",
                table: "Avukatlar");

            migrationBuilder.DropColumn(
                name: "DigerBankaIbanNo",
                table: "Avukatlar");

            migrationBuilder.DropColumn(
                name: "DogumTarihi",
                table: "Avukatlar");

            migrationBuilder.DropColumn(
                name: "HalkbankVadesizIbanNo",
                table: "Avukatlar");

            migrationBuilder.DropColumn(
                name: "HesapAktifMi",
                table: "Avukatlar");

            migrationBuilder.DropColumn(
                name: "IlceId",
                table: "Avukatlar");

            migrationBuilder.DropColumn(
                name: "IletisimVeribanMi",
                table: "Avukatlar");

            migrationBuilder.DropColumn(
                name: "IsFaxNo",
                table: "Avukatlar");

            migrationBuilder.DropColumn(
                name: "IsTelefonu",
                table: "Avukatlar");

            migrationBuilder.DropColumn(
                name: "Normal",
                table: "Avukatlar");

            migrationBuilder.DropColumn(
                name: "OlusturmaTarihiUtc",
                table: "Avukatlar");

            migrationBuilder.DropColumn(
                name: "SehirId",
                table: "Avukatlar");

            migrationBuilder.DropColumn(
                name: "Soyadi",
                table: "Avukatlar");

            migrationBuilder.DropColumn(
                name: "TCKN",
                table: "Avukatlar");

            migrationBuilder.DropColumn(
                name: "TamAdres",
                table: "Avukatlar");

            migrationBuilder.DropColumn(
                name: "VadesizHesapSubeId",
                table: "Avukatlar");

            migrationBuilder.DropColumn(
                name: "VergiDairesi",
                table: "Avukatlar");

            migrationBuilder.DropColumn(
                name: "VergiNo",
                table: "Avukatlar");

            migrationBuilder.RenameTable(
                name: "Avukatlar",
                newName: "avukatlar");

            migrationBuilder.RenameColumn(
                name: "VadesizHesapNo",
                table: "avukatlar",
                newName: "BaroNo");

            migrationBuilder.AlterColumn<string>(
                name: "TCKN",
                table: "Musteriler",
                type: "nvarchar(11)",
                maxLength: 11,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nchar(11)",
                oldFixedLength: true,
                oldMaxLength: 11,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AdSoyad",
                table: "avukatlar",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Telefon",
                table: "avukatlar",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_avukatlar",
                table: "avukatlar",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Urunler_avukatlar_AvukatId",
                table: "Urunler",
                column: "AvukatId",
                principalTable: "avukatlar",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
