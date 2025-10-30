using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HukukTakip.Api.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMusteriler : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AileSiraNo",
                table: "Musteriler");

            migrationBuilder.DropColumn(
                name: "CiltNo",
                table: "Musteriler");

            migrationBuilder.DropColumn(
                name: "KutukSiraNo",
                table: "Musteriler");

            migrationBuilder.DropColumn(
                name: "SurucuBelgesiNumarasi",
                table: "Musteriler");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AileSiraNo",
                table: "Musteriler",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CiltNo",
                table: "Musteriler",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "KutukSiraNo",
                table: "Musteriler",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SurucuBelgesiNumarasi",
                table: "Musteriler",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
