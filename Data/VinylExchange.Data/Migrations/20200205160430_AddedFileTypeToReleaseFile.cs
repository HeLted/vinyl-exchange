using Microsoft.EntityFrameworkCore.Migrations;

namespace VinylExchange.Data.Migrations
{
    public partial class AddedFileTypeToReleaseFile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FileType",
                table: "ReleaseFiles",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileType",
                table: "ReleaseFiles");
        }
    }
}
