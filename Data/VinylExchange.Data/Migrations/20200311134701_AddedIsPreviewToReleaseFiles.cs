using Microsoft.EntityFrameworkCore.Migrations;

namespace VinylExchange.Data.Migrations
{
    public partial class AddedIsPreviewToReleaseFiles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPreview",
                table: "ReleaseFiles",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPreview",
                table: "ReleaseFiles");
        }
    }
}
