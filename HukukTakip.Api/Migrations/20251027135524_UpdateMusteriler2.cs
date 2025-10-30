using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HukukTakip.Api.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMusteriler2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DogumYeri",
                table: "Musteriler");

            migrationBuilder.DropColumn(
                name: "OlusturanUserId",
                table: "Musteriler");

            migrationBuilder.AlterColumn<int>(
                name: "NufusaKayitliOlduguIl",
                table: "Musteriler",
                type: "int",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "NufusaKayitliOlduguIl",
                table: "Musteriler",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DogumYeri",
                table: "Musteriler",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OlusturanUserId",
                table: "Musteriler",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
