namespace VinylExchange.Data.Migrations
{
    #region

    using Microsoft.EntityFrameworkCore.Migrations;

    #endregion

    public partial class AddedIsPreviewToReleaseFiles : Migration
    {
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "IsPreview", table: "ReleaseFiles");
        }

        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPreview",
                table: "ReleaseFiles",
                nullable: false,
                defaultValue: false);
        }
    }
}